using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndTurnButton : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    public static EndTurnButton instance;

    public TextMeshProUGUI CounterText;

    public bool IsInEndTurnIcon;

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
        

    }

    public void PressEndTurn()
    {
        print("next turn start");
        if (LevelManager.instance.IsWorldTurn || !LevelManager.instance.IsTutorialComplete || !IsInEndTurnIcon) return;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelManager.instance.IsWorldTurn || !LevelManager.instance.IsTutorialComplete) return;
        IsInEndTurnIcon = true;
        LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.3f).setEaseInOutCirc();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsInEndTurnIcon = false;
        LeanTween.scale(gameObject, Vector3.one * 1.0f, 0.3f).setEaseInOutCirc();
    }
}
