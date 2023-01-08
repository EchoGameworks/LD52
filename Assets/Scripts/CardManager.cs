using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool IsFaceDown;

    public Card Card;

    public TextMeshProUGUI Title;
    List<TextMeshProUGUI> listActionDescriptions;
    public TextMeshProUGUI Description;
    private Sprite CardFront;
    public Sprite CardBack;
    public Image CardFaceShowing;

    private RectTransform rt;

    public float ScaleInHand = 2f;
    public float ScaleInZoom = 4f;

    public bool IsZoomed;
    private int childPositionPreZoom;
    private Vector3 positionPreZoom;
    //public void Start()
    //{
    //    Init(Card);
    //}

    public bool IsBeingDragged;
    private Camera cam;
    //public RectTransform parentCanvasRT;

    private RectTransform activeCardSlotRT;
    private bool IsActive;
    private int moveStep;
    public Effect CurrentEffect;
    private HandManager handManager;
    int colorId;

    public void Init(HandManager handManager, Card card)
    {
        this.handManager = handManager;
        cam = Camera.main;
        activeCardSlotRT = GameObject.Find("Active Card Slot").GetComponent<RectTransform>();  
        //parentCanvasRT = GameObject.FindGameObjectWithTag("OverlayCanvas").GetComponent<RectTransform>();
        rt = this.GetComponent<RectTransform>();
        this.Card = card;
        IsFaceDown = true;
        Title.text = card.Name;
        CardFront = card.Sprite;
        Description.text = "";
        for (int i = 0; i < card.Effects.Count; i++)
        {
            Description.text += card.Effects[i].Description + "<br>";
        }
        UpdateFaceDirection();
    }

    public void SetPosition(Vector3 position)
    {
        LeanTween.value(gameObject, rt.anchoredPosition3D, position, 0.3f)
            .setEaseInOutCirc()
            .setOnUpdate((Vector3 val) => rt.anchoredPosition3D = val);        
    }

    public void SetScale(Vector3 scale)
    {
        LeanTween.value(gameObject, rt.localScale, scale, 0.3f)
            .setEaseInOutCirc()
            .setOnUpdate((Vector3 val) => rt.localScale = val);
    }

    public void Zoom()
    {
        if (IsZoomed || IsFaceDown) return;
        IsZoomed = true;
        positionPreZoom = rt.anchoredPosition3D;
        childPositionPreZoom = this.transform.GetSiblingIndex();
        this.transform.SetAsLastSibling();
        SetPosition(positionPreZoom.ReplaceY(90f));
        SetScale(Vector3.one * ScaleInZoom);
    }

    public void UnZoom()
    {
        if (!IsZoomed || IsActive) return;
        IsZoomed = false;
        this.transform.SetSiblingIndex(childPositionPreZoom);
        SetPosition(positionPreZoom.ReplaceY(0f));
        SetScale(Vector3.one * ScaleInHand);
    }

    public void MoveToDraw(Transform drawParent)
    {
        IsFaceDown = true;
        this.transform.parent = drawParent;
        //SetAnchorsToMiddle();
        SetPosition(Vector3.zero);
        SetScale(Vector3.one);
        UpdateFaceDirection();
        CancelFlashing();

    }

    public void MoveToHand(Transform handParent)
    {
        IsFaceDown = false;
        this.transform.parent = handParent;
        SetScale(Vector3.one * ScaleInHand);
        UpdateFaceDirection();
        CancelFlashing();
        moveStep = 0;
        CurrentEffect = null;
    }

    public void MoveToDiscard(Transform discardParent)
    {
        IsFaceDown = true;
        this.transform.parent = discardParent;
        SetScale(Vector3.one);
        //SetAnchorsToMiddle();
        SetPosition(Vector3.zero);        
        UpdateFaceDirection();
        CancelFlashing();
    }

    private void SetAnchorsToMiddle()
    {
        rt.anchorMin = new Vector2(1, 0);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);
    }

    public void UpdateFaceDirection()
    {
        if (IsFaceDown)
        {
            CardFaceShowing.sprite = CardBack;
            Title.gameObject.SetActive(false);
            Description.gameObject.SetActive(false);            
        }
        else
        {
            CardFaceShowing.sprite = CardFront;
            Title.gameObject.SetActive(true);
            Description.gameObject.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Zoom();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnZoom();
    }

    private void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MoveToActiveSlot();
    }

    public void MoveToActiveSlot()
    {
        IsActive = true;
        moveStep = 0;
        transform.parent = activeCardSlotRT;
        SetPosition(activeCardSlotRT.anchoredPosition3D);
        SetScale(Vector3.one * ScaleInZoom);
        handManager.SetActiveCard(this);
        PlanAction();
        
    }

    private void CancelFlashing()
    {
        if (colorId != 0) LeanTween.cancel(colorId);
        Description.color = new Color(223f / 255f, 246f / 255f, 243f / 255f);
    }


    public void PlanAction()
    {
        //ActionManager.instance.ShowArrows();
        colorId = LeanTween.value(gameObject, new Color(223f / 255f, 246f / 255f, 243f / 255f), new Color(48f / 255f, 44f / 255f, 46f / 255f), 2f)
            .setOnUpdate((Color c) => Description.color = c)
            .setEaseInOutCirc()
            .setLoopPingPong()
            .id;
        CurrentEffect = Card.Effects[moveStep];
        LevelManager.instance.SetCurrentEffect(this);
        //switch (Card.Effects[moveStep].EffectArea)
        //{
        //    case Effect.EffectAreas.ThreeRowColl:
        //        //ActionManager.instance.ShowArrows();
        //        print("3rc");
        //        break;
        //    case Effect.EffectAreas.ThreeX:
        //        print("3x");
        //        break;
        //    case Effect.EffectAreas.Single:
        //        print("1x");
        //        break;
        //}
    }

    public void TakeAction(TileManager tile)
    {

        if(tile.CurrentGrowableManager != null)
        {
            tile.CurrentGrowableManager.ModifyRating(CurrentEffect.ImpactPropery, CurrentEffect.EffectValue);
        }        
        ActionTaken();
    }

    public void TakeSilentAction(List<TileManager> tiles)
    {
        foreach (TileManager tile in tiles)
        {
            if (tile.CurrentGrowableManager != null)
            {
                tile.CurrentGrowableManager.ModifyRating(CurrentEffect.ImpactPropery, CurrentEffect.EffectValue, true);
            }
        }
    }
    public void ActionTaken()
    {
        if(moveStep == Card.Effects.Count - 1)
        {
            print("last action done");
            moveStep = 0;
            MoveToDiscard(handManager.DiscardDeck);
            LevelManager.instance.ActionTaken();
        }
        else
        {
            print("moving to next ability");
            moveStep++;
            PlanAction();
        }
    }
}
