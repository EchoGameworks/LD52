using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndTurnButton : MonoBehaviour, IPointerUpHandler
{

    public static EndTurnButton instance;

    public TextMeshProUGUI CounterText;

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

    public void OnPointerUp(PointerEventData eventData)
    {
        print("end turn");
        LevelManager.instance.EndTurn();
    }

    void Start()
    {
        
    }

    public void UpdateCounter(int count)
    {
        if(count == -1)
        {
            CounterText.text = "World's<br>Turn";
        }
        else
        {
            CounterText.text = count.ToString() + "/3";
        }
    }
}
