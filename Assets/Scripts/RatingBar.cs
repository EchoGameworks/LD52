using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Growable;

public class RatingBar : MonoBehaviour
{
    public Transform Highlight;
    public List<Image> Ratings;

    public int CurrentHighlight = 0;

    public Sprite positiveSprite;
    public Sprite negativeSprite;
    public Sprite neutralSprite;


    public void UpdateIcons(List<Impact> impacts)
    {
        for(int i = 0; i < impacts.Count; i++)
        {
            switch (impacts[i])
            {
                case Impact.Positive:
                    Ratings[i].sprite = positiveSprite;
                    break;
                case Impact.Negative:
                    Ratings[i].sprite = negativeSprite;
                    break;
                default:
                    Ratings[i].sprite = neutralSprite;
                    break;
            }
            
        }
    }

    public void MoveHighlightToRating(int index)
    {
        Highlight.transform.position = Ratings[index].transform.position;
    }
}
