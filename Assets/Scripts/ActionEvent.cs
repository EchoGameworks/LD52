using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEvent : MonoBehaviour
{
    public Sprite plusBright;
    public Sprite minusBright;
    public Sprite plusHot;
    public Sprite minusHot;
    public Sprite plusWet;
    public Sprite minusWet;

    public SpriteRenderer IndicatorSprite;
    public CardManager CurrentCard;
    public Effect CurrentEffect;

    public List<SpriteRenderer> PreviewSprites;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            PreviewSprites[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight(Vector3 worldPosition)
    {
        IndicatorSprite.color = IndicatorSprite.color.ReplaceA(255 /255f);
        for (int i = 0; i < 5; i++)
        {
            PreviewSprites[i].gameObject.SetActive(true);
        }
    }

    public void UnHighlight()
    {
        IndicatorSprite.color = IndicatorSprite.color.ReplaceA(100 / 255f);
        for (int i = 0; i < 5; i++)
        {
            PreviewSprites[i].gameObject.SetActive(false);
        }
    }

    public void SetCurrentEffect(CardManager currentCard)
    {
        CurrentCard = currentCard;
        CurrentEffect = CurrentCard.CurrentEffect;
        //Icons
        Sprite previewSprite = null;

        switch (CurrentEffect.ImpactPropery)
        {
            case Growable.ImpactProperty.Temperature:
                if (CurrentEffect.EffectValue > 0) {
                    previewSprite = plusHot;
                }
                else
                {
                    previewSprite = minusHot;
                }
                break;
            case Growable.ImpactProperty.Sun:
                if (CurrentEffect.EffectValue > 0)
                {
                    previewSprite = plusBright;
                }
                else
                {
                    previewSprite = minusBright;
                }
                break;

           default:
                if (CurrentEffect.EffectValue > 0)
                {
                    previewSprite = plusWet;
                }
                else
                {
                    previewSprite = minusWet;
                }
                break;
        }

        //Number & Direction
        int maxDist = 0;
        switch (CurrentEffect.EffectArea)
        {
            case Effect.EffectAreas.ThreeRowColl:
                maxDist = 3;
                break;
            case Effect.EffectAreas.FullRowCol:
                maxDist = 5;
                break;
        }

        if(CurrentEffect.EffectArea == Effect.EffectAreas.FullRowCol || CurrentEffect.EffectArea == Effect.EffectAreas.ThreeRowColl)
        {
            for (int i = 0; i < 5; i++)
            {
                PreviewSprites[i].color = PreviewSprites[i].color.ReplaceA(0f);
                PreviewSprites[i].sprite = previewSprite;
            }
            for (int i = 0; i < maxDist; i++)
            {
                PreviewSprites[i].color = PreviewSprites[i].color.ReplaceA(0.5f);
            }
        }

    }

    public void Activate()
    {
        if (CurrentEffect.EffectArea == Effect.EffectAreas.FullRowCol || CurrentEffect.EffectArea == Effect.EffectAreas.ThreeRowColl)
        {
            int dist = 5;
            if (CurrentEffect.EffectArea == Effect.EffectAreas.ThreeRowColl) dist = 3;
            for (int i = 0; i < 5; i++)
            {
                if (PreviewSprites[i].gameObject.activeSelf)
                {
                    Ray ray = new Ray(PreviewSprites[i].transform.position, Vector3.down);
                    Debug.DrawRay(PreviewSprites[i].transform.position, Vector3.down, Color.red, 1f);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, 2f, LayerMask.GetMask("Tile")))
                    {
                        //print("hit something");
                        TileManager tm = hitInfo.transform.GetComponent<TileManager>();
                        if (tm != null)
                        {
                            //print("hit tm");
                            if (tm.CurrentGrowableManager != null)
                            {
                                //print("hit tm with Growable");
                                tm.CurrentGrowableManager.ModifyRating(CurrentEffect.ImpactPropery, CurrentEffect.EffectValue);
                            }
                        }
                        else
                        {
                            //print("no growable on activated area");
                        }
                    }
                }
            }

        }

        CurrentCard.ActionTaken();
        ActionManager.instance.HideArrow();
    }
}
