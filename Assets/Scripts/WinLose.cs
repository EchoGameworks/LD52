using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public static WinLose instance;

    public Image backgroundImage;
    public TextMeshProUGUI textCongrats;
    public TextMeshProUGUI textCongratsDescription;

    public TextMeshProUGUI textLose;
    public TextMeshProUGUI textLoseDescription;


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

    void Start()
    {
        backgroundImage.color = backgroundImage.color.ReplaceA(0f);
        textCongrats.color = textCongrats.color.ReplaceA(0f);
        textCongratsDescription.color = textCongratsDescription.color.ReplaceA(0f);

        textLose.color = textLose.color.ReplaceA(0f);
        textLoseDescription.color = textLoseDescription.color.ReplaceA(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Win")]
    public void ShowWin()
    {
        LeanTween.value(backgroundImage.color.a, 1f, 0.5f)
            .setEaseInOutCirc()
            .setOnUpdate((float a) =>
            {
                backgroundImage.color = backgroundImage.color.ReplaceA(a);
                textCongrats.color = textCongrats.color.ReplaceA(a);
                textCongratsDescription.color = textCongratsDescription.color.ReplaceA(a);
            });
    }

    [ContextMenu("Lose")]
    public void ShowLose()
    {
        LeanTween.value(backgroundImage.color.a, 1f, 0.5f)
            .setEaseInOutCirc()
            .setOnUpdate((float a) =>
            {
                backgroundImage.color = backgroundImage.color.ReplaceA(a);

                textLose.color = textLose.color.ReplaceA(a);
                textLoseDescription.color = textLoseDescription.color.ReplaceA(a);
            });
    }

    [ContextMenu("Hide All")]
    public void HideAll()
    {
        LeanTween.value(backgroundImage.color.a, 0f, 0.5f)
            .setEaseInOutCirc()
            .setOnUpdate((float a) =>
            {
                backgroundImage.color = backgroundImage.color.ReplaceA(a);

                textCongrats.color = textCongrats.color.ReplaceA(a);
                textCongratsDescription.color = textCongratsDescription.color.ReplaceA(a);

                textLose.color = textLose.color.ReplaceA(a);
                textLoseDescription.color = textLoseDescription.color.ReplaceA(a);
            });
    }
}
