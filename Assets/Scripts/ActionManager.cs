using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager instance;

    public Transform ArrowHolder;

    private List<ActionEvent> arrowActions;

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

        arrowActions = new List<ActionEvent>();
        foreach (Transform t in ArrowHolder)
        {
            arrowActions.Add(t.GetComponent<ActionEvent>());
        }
        HideArrow();
    }

    void Start()
    {

    }

    public void UpdateArrows(CardManager cm)
    {
        foreach(ActionEvent a in arrowActions)
        {
            a.SetCurrentEffect(cm);
        }
        ShowArrows();
    }

    public void ShowArrows()
    {
        foreach(ActionEvent a in arrowActions)
        {
            a.gameObject.SetActive(true);
        }
    }

    public void HideArrow()
    {
        foreach (ActionEvent a in arrowActions)
        {
            a.gameObject.SetActive(false);
        }
    }
}
