using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Controls Controller;

    public List<Level> Levels;
    public int LevelNumber;
    public HandManager handManager;

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
        Controller = new Controls();
        Controller.Level.Enable();
    }

    void Start()
    {
        handManager = GameObject.FindGameObjectWithTag("HandManager").GetComponent<HandManager>();
        Controller.Level.Restart.performed += Restart_performed;
        RestartGame();
    }

    private void Restart_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        RestartGame();
    }
    public void RestartGame()
    {
        LevelManager.instance.RestartGame();
        ActionManager.instance.HideArrow();
        handManager.StartGame();
        WinLose.instance.HideAll();
        LevelNumber = 0;
        NextLevel();
    }

    public void LoadLevel(int i)
    {
        LevelManager.instance.CreateLevel(i, Levels[i-1]);
    }

    public void NextLevel()
    {
        LevelNumber++;
        if (LevelNumber <= Levels.Count)
        {
            LoadLevel(LevelNumber);
        }
        else
        {
            Win();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        WinLose.instance.ShowLose();
    }

    public void Win()
    {
        WinLose.instance.ShowWin();
    }
}
