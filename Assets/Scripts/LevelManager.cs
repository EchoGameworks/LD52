using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        CreateLevel(CurrrentLevel);
        StartTurn();
    }

    public void CreateLevel(Level level)
    {
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
        switch (CurrentEffect.EffectArea)
        {
            case Effect.EffectAreas.FullRowCol: case Effect.EffectAreas.ThreeRowColl:
                ActionManager.instance.UpdateArrows(cm);
                break;
        }
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

    public void WorldTurn()
    {
        print("worldTurn");
    }

    public void EndTurn()
    {
        handManager.SendActiveCardToHand();
        EndTurnButton.instance.UpdateCounter(-1);
        WorldTurn();
    }

    public void StartTurn()
    {
        ActionsRemaining = ActionsMax;
        EndTurnButton.instance.UpdateCounter(ActionsMax);
        if (RoundNumber == 0)
        {
            handManager.DrawCards(5);
        }
        else
        {
            handManager.DrawCards(3);
        }
        RoundNumber++;
        RoundManager.instance.UpdateRound(RoundNumber, CurrrentLevel.MaxRounds);
        if(RoundNumber > CurrrentLevel.MaxRounds)
        {
            GameManager.instance.GameOver();
        }
    }
}
