using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI worldScoreText;
    public TextMeshProUGUI levelScoreText;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRound(int round, int maxRounds)
    {
        roundText.text = round + "/" + maxRounds;
    }

    public void UpdateYourScore(int score)
    {
        yourScoreText.text = score.ToString();
    }

    public void UpdateWorldScore(int score)
    {
        worldScoreText.text = score.ToString();
    }


    public void UpdateLevelScore(int lvl)
    {
        levelScoreText.text = lvl.ToString();
    }
}
