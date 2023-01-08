using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;
    public Controls Controller;

    public GameObject CurrentTileGO;
    public TileManager CurrentTileManager;
    public ActionEvent CurrentActionEvent;

    public Sprite plusBright;
    public Sprite minusBright;
    public Sprite plusHot;
    public Sprite minusHot;
    public Sprite plusWet;
    public Sprite minusWet;

    Camera cam;

    private HandManager handManager;

    public List<TileManager> listTileManagers;

    void Awake()
    {
        //CurrentLevelContainer = null;
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
    void Start()
    {
        listTileManagers = new List<TileManager>();
        handManager = GameObject.FindGameObjectWithTag("HandManager").GetComponent<HandManager>();
        cam = Camera.main;
        Controller = GameManager.instance.Controller;
        Controller.Level.Click.performed += Click_performed;
        Controller.Level.Click.canceled += Click_canceled;
    }



    private void Click_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //print("click performed");
    }

    private void Click_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Ray ray = cam.ScreenPointToRay(Controller.Level.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Action")))
        {
            if (CurrentActionEvent != null) CurrentActionEvent.Activate();
        }
        else if(Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Card")))
        {

        }
        else if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Tile")))
        {
            print("hit tile");
            if (LevelManager.instance.CurrentEffect != null)
            {
                if (LevelManager.instance.CurrentEffect.EffectArea == Effect.EffectAreas.Single)
                {
                    TileManager tm = hit.transform.GetComponent<TileManager>();
                    LevelManager.instance.CurrentCard.TakeAction(tm);
                    tm.HideSingleTarget();
                }

                if (LevelManager.instance.CurrentEffect.EffectArea == Effect.EffectAreas.ThreeX)
                {
                    LevelManager.instance.CurrentCard.TakeSilentAction(listTileManagers);
                    foreach (TileManager t in listTileManagers)
                    {
                        t.HideSingleTarget();
                    }

                    TileManager tm = hit.transform.GetComponent<TileManager>();
                    LevelManager.instance.CurrentCard.TakeAction(tm);
                    tm.HideSingleTarget();

                }
                
            }
        }
        else
        {
            LevelManager.instance.CancelAction();            
        }
        
    }

    public Sprite GetEffectSprite(Effect currentEffect)
    {
        Sprite previewSprite;

        switch (currentEffect.ImpactPropery)
        {
            case Growable.ImpactProperty.Temperature:
                if (currentEffect.EffectValue > 0)
                {
                    previewSprite = plusHot;
                }
                else
                {
                    previewSprite = minusHot;
                }
                break;
            case Growable.ImpactProperty.Sun:
                if (currentEffect.EffectValue > 0)
                {
                    previewSprite = plusBright;
                }
                else
                {
                    previewSprite = minusBright;
                }
                break;
            default:
                if (currentEffect.EffectValue > 0)
                {
                    previewSprite = plusWet;
                }
                else
                {
                    previewSprite = minusWet;
                }
                break;
        }
        return previewSprite;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Controller.Level.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Tile")))
        {            
            if (hit.transform.gameObject != CurrentTileGO)
            {
                if(CurrentTileManager != null)
                {
                    CurrentTileManager.HideSingleTarget();
                    CurrentTileManager.UnHighlight();
                }

                if(listTileManagers.Count > 0)
                {
                    foreach(TileManager tm in listTileManagers)
                    {
                        tm.HideSingleTarget();
                    }
                }
                
                CurrentTileGO = hit.transform.gameObject;
                CurrentTileManager = CurrentTileGO.GetComponent<TileManager>();
                if (CurrentTileManager.CurrentGrowableManager != null)
                {
                    InfoBubble.instance.Show(CurrentTileManager.CurrentGrowableManager);
                    CurrentTileManager.Highlight();
                    print(CurrentTileManager.CurrentGrowableManager.name);
                }
                else
                {
                    print("no growable on tile");
                    InfoBubble.instance.Hide();
                } 
                
                if(LevelManager.instance.CurrentEffect != null)
                {
                    if (LevelManager.instance.CurrentEffect.EffectArea == Effect.EffectAreas.Single)
                    {
                        CurrentTileManager.ShowSingleTarget(GetEffectSprite(LevelManager.instance.CurrentEffect));
                    }
                    if (LevelManager.instance.CurrentEffect.EffectArea == Effect.EffectAreas.ThreeX)
                    {
                        CurrentTileManager.ShowSingleTarget(GetEffectSprite(LevelManager.instance.CurrentEffect));
                        listTileManagers = LevelManager.instance.Get3x3Tiles(CurrentTileManager);
                        foreach (TileManager tm in listTileManagers)
                        {
                            tm.ShowSingleTarget(GetEffectSprite(LevelManager.instance.CurrentEffect));
                        }
                    }
                }
            }            
        }
        else
        {
            if(CurrentTileGO != null)
            {
                CurrentTileGO = null;
                CurrentTileManager = null;
                InfoBubble.instance.Hide();
            }

        }

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Action")))
        {
            ActionEvent ae = hit.transform.GetComponent<ActionEvent>();
            if(ae != null)
            {
                if (CurrentActionEvent != null)
                {
                    CurrentActionEvent.UnHighlight();
                    CurrentActionEvent = null;
                }
                CurrentActionEvent = ae;
                Vector3 worldPosition = cam.ScreenToWorldPoint(Controller.Level.MousePosition.ReadValue<Vector2>());
                ae.Highlight(worldPosition);
            }

        }
        else
        {
            if(CurrentActionEvent != null)
            {
                CurrentActionEvent.UnHighlight();
                CurrentActionEvent = null;
            }
        }
    }
}
