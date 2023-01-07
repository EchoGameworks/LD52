using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;
    public Controls Controller;

    public GameObject CurrentTileGO;
    public TileManager CurrentTileManager;


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
        Controller = GameManager.instance.Controller;
        Controller.Level.Click.performed += Click_performed;
        Controller.Level.Click.canceled += Click_canceled;
    }



    private void Click_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        print("click performed");
    }

    private void Click_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        print("click cancelled");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Controller.Level.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Tile")))
        {
            if (hit.transform.gameObject != CurrentTileGO)
            {
                CurrentTileGO = hit.transform.gameObject;
                CurrentTileManager = CurrentTileGO.GetComponent<TileManager>();
                if(CurrentTileManager.CurrentGrowableManager != null)
                {
                    InfoBubble.instance.Show(CurrentTileManager.CurrentGrowableManager);
                    print(CurrentTileManager.CurrentGrowableManager.name);
                }
                else
                {
                    InfoBubble.instance.Hide();
                    print("no growable on tile");
                }
            }            
        }
    }
}
