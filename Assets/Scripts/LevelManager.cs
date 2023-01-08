using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Level CurrrentLevel;

    public Transform TileHolder;
    public List<TileManager> Tiles;

    public CardManager CurrentCard;
    public Effect CurrentEffect;

    public int ActionsRemaining;
    private int ActionsMax = 3;

    public int RoundNumber = 0;
    private HandManager handManager;

    public int PlayerScore = 0;
    public int WorldScore = 0;

    public TextMeshPro worldSpeechText;
    public WorldAction NextWorldAction;

    public List<string> TutorialText;
    public int TutorialStep = 0;
    public bool IsTutorialComplete;
    public bool IsWorldTurn;
    void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        handManager = GameObject.FindGameObjectWithTag("HandManager").GetComponent<HandManager>();
        GameManager.instance.Controller.Level.Click.canceled += Click_canceled;
        GameManager.instance.Controller.Level.Skip.performed += Skip_performed;
        
        
    }

    public void RestartGame()
    {
        TutorialStep = 0;
        IsTutorialComplete = false;
    }

    private void Skip_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //print("skip performed");
        TutorialStep = TutorialText.Count - 1;
        NextTutorialStep();
    }

    private void Click_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        NextTutorialStep();
    }

    public void NextTutorialStep()
    {
        if (IsTutorialComplete) return;

        if (TutorialStep == TutorialText.Count - 1)        {
            IsTutorialComplete = true;
            StartTurn();            
            return;
        }
        TutorialStep++;
        worldSpeechText.text = TutorialText[TutorialStep];
    }

    public void CreateLevel(int levelNumber, Level level)
    {
        if(levelNumber == 1) worldSpeechText.text = TutorialText[TutorialStep];

        PlayerScore = 0;
        WorldScore = 0;
        RoundManager.instance.UpdateYourScore(PlayerScore);
        RoundManager.instance.UpdateWorldScore(WorldScore);
        RoundManager.instance.UpdateLevelScore(levelNumber);

        for (int iChild = this.transform.childCount - 1; iChild >= 0; iChild--)
        {
            TileManager tm = this.transform.GetChild(iChild).GetComponent<TileManager>();
            if (tm != null)
            {
                if(tm.CurrentGrowableManager != null)
                {
                    Destroy(tm.CurrentGrowableManager);
                }
            }
            Destroy(this.transform.GetChild(iChild).gameObject);
        }

        Tiles = new List<TileManager>();
        CurrrentLevel = level;
        for(int x = 0; x < 5; x++)
        {
            for(int y = 0; y < 5; y++)
            {
                int r = Random.Range(0, level.Tiles.Count);
                GameObject tileGO = Instantiate(CurrrentLevel.Tiles[r], TileHolder);
                tileGO.transform.position = new Vector3(x, 0f, y);
                TileManager tm = tileGO.GetComponent<TileManager>();
                Tiles.Add(tm);
                tm.Init(new Vector2Int(x, y));
            }
        }


        int i = 0;
        int index = 0;
        while (index < level.Growables.Count)
        {
            int x = Random.Range(0, 5);
            int y = Random.Range(0, 5);
            //print("trying Tile " + x + ", " + y + " | index: " + index);
            TileManager t = Tiles.FirstOrDefault(o => o.GridPosition == new Vector2Int(x, y) && o.CurrentGrowableManager == null);
            if (t != null)
            {
                //print("placing");
                GameObject growable = Instantiate(level.Growables[index], TileHolder);
                t.SetGrowable(growable.GetComponent<GrowableManager>());
                index++;
            }
            i++;
            if (i > 100)
            {
                //print("got to 100");
                break;
            }
        }
        //print("out of while");
    }

    public List<TileManager> Get3x3Tiles(TileManager tm)
    {
        List<TileManager> tms = new List<TileManager>();
        TileManager e = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x + 1 && o.GridPosition.y == tm.GridPosition.y);
        TileManager w = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x - 1 && o.GridPosition.y == tm.GridPosition.y);
        TileManager n = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x && o.GridPosition.y == tm.GridPosition.y + 1);
        TileManager s = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x && o.GridPosition.y == tm.GridPosition.y - 1);
        TileManager nw = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x -1 && o.GridPosition.y == tm.GridPosition.y + 1);
        TileManager ne = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x + 1 && o.GridPosition.y == tm.GridPosition.y + 1);
        TileManager sw = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x - 1 && o.GridPosition.y == tm.GridPosition.y - 1);
        TileManager se = Tiles.FirstOrDefault(o => o.GridPosition.x == tm.GridPosition.x + 1 && o.GridPosition.y == tm.GridPosition.y - 1);

        if (e != null) tms.Add(e);
        if (w != null) tms.Add(w);
        if (n != null) tms.Add(n);
        if (s != null) tms.Add(s);
        if (nw != null) tms.Add(nw);
        if (ne != null) tms.Add(ne);
        if (sw != null) tms.Add(sw);
        if (se != null) tms.Add(se);

        return tms;
    }

    public void SetCurrentEffect(CardManager cm)
    {
        CurrentCard = cm;
        CurrentEffect = CurrentCard.CurrentEffect;
        if(CurrentEffect.ImpactPropery == Growable.ImpactProperty.Harvest)
        {
            LeanTween.delayedCall(0.5f, Harvest);
            return;
        }

        switch (CurrentEffect.EffectArea)
        {
            case Effect.EffectAreas.FullRowCol: case Effect.EffectAreas.ThreeRowColl:
                ActionManager.instance.UpdateArrows(cm);
                break;
        }
    }

    public void Harvest()
    {
        foreach (TileManager t in Tiles)
        {
            if (t.CurrentGrowableManager != null)
            {
                PlayerScore += t.CurrentGrowableManager.GetHarvest();
            }
        }
        RoundManager.instance.UpdateYourScore(PlayerScore);
        handManager.activeCardManager.ActionTaken();
    }

    public void Reap()
    {
        foreach (TileManager t in Tiles)
        {
            if (t.CurrentGrowableManager != null)
            {
                WorldScore += t.CurrentGrowableManager.TryReap();
            }
        }
        RoundManager.instance.UpdateWorldScore(WorldScore);
    }

    public void CancelAction()
    {
        handManager.SendActiveCardToHand();
        CurrentCard = null;
        CurrentEffect = null;
        ActionManager.instance.HideArrow();
        
    }

    public void ActionTaken()
    {
        ActionsRemaining -= 1;
        CurrentCard = null;
        CurrentEffect = null;
        handManager.activeCardManager = null;
        if(ActionsRemaining <= 0)
        {
            EndTurn();
        }
        EndTurnButton.instance.UpdateCounter(ActionsRemaining);
    }

    public void GetNextWorldAction()
    {
        print("getting next world Action");
        NextWorldAction = CurrrentLevel.WorldActions[Random.Range(0, CurrrentLevel.WorldActions.Count)];
        worldSpeechText.text = NextWorldAction.Description;
    }

    public void WorldTurn()
    {
        IsWorldTurn = true;
        worldSpeechText.text = "My Turn!";
        if(NextWorldAction.ActionType == WorldAction.ActionTypes.Spawn)
        {
            WorldSpawnObject();
            EndRound();
        }
        else if(NextWorldAction.ActionType == WorldAction.ActionTypes.Rest)
        {
            print("starting rest");
            var seq = LeanTween.sequence();
            seq.append(() => worldSpeechText.text = "zzz");
            seq.append(0.4f);
            seq.append(() => worldSpeechText.text = "zzzz");
            seq.append(0.4f);
            seq.append(() => worldSpeechText.text = "zzzz");
            seq.append(0.4f);
            seq.append(() => worldSpeechText.text = "zzzz");
            seq.append(EndRound);
            print("ending rest");
        }
        else if(NextWorldAction.ActionType == WorldAction.ActionTypes.Reap)
        {
            worldSpeechText.text = "I'n reaping the rewards of your inaction!";
            
            Reap();
            print("ending reap");
            LeanTween.delayedCall(1f, EndRound);
        }
        
    }

    public void WorldSpawnObject()
    {
        int i = 0;
        while (true)
        {
            int x = Random.Range(0, 5);
            int y = Random.Range(0, 5);
            //print("trying Tile " + x + ", " + y + " | index: " + index);
            TileManager t = Tiles.FirstOrDefault(o => o.GridPosition == new Vector2Int(x, y) && o.CurrentGrowableManager == null);
            if (t != null)
            {
                //print("placing");
                GameObject growable = Instantiate(NextWorldAction.spawnObject, TileHolder);
                t.SetGrowable(growable.GetComponent<GrowableManager>());
                return;
            }

            i++;

            if (i > 100)
            {
                print("got to 100 on spawn");
                break;
            }
        }
    }

    public void EndTurn()
    {       
        handManager.SendActiveCardToHand();
        EndTurnButton.instance.UpdateCounter(-1);
        WorldTurn();
    }

    public void EndRound()
    {
        IsWorldTurn = false;
        StartTurn();
    }

    public void StartTurn()
    {
        ActionsRemaining = ActionsMax;
        EndTurnButton.instance.UpdateCounter(ActionsRemaining);
        if (RoundNumber == 0)
        {
            handManager.DrawCards(5);
        }
        else
        {
            handManager.DrawCards(3);
        }
        GetNextWorldAction();
        RoundNumber++;
       
        int remainingPlants = Tiles.Where(o => o.CurrentGrowableManager != null).Count();
        if(RoundNumber > CurrrentLevel.MaxRounds || remainingPlants == 0)
        {
            if(PlayerScore <= WorldScore)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                GameManager.instance.NextLevel();
                RoundNumber = 1;
            }
            
        }

        RoundManager.instance.UpdateRound(RoundNumber, CurrrentLevel.MaxRounds);
    }
}
