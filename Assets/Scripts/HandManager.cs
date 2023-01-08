using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject prefabCard;
    public List<Card> StartingCards;

    //public List<CardManager> DrawDeckCards;
    //public List<CardManager> HandCards;
    //public List<CardManager> DiscardDeckCards;

    public Transform DrawDeck;
    public Transform Hand;
    public Transform DiscardDeck;
    public Transform ActiveCardHolder;

    public CardManager activeCardManager;

    void Start()
    {
        //StartGame();
    }

    public void StartGame()
    {
        //DrawDeckCards = new List<CardManager>();
        //HandCards = new List<CardManager>();
        //DiscardDeckCards = new List<CardManager>();
        for (int iChild = Hand.childCount - 1; iChild >= 0; iChild--)
        {
            Destroy(Hand.GetChild(iChild).gameObject);
        }

        for (int iChild = DiscardDeck.childCount - 1; iChild >= 0; iChild--)
        {
            Destroy(DiscardDeck.GetChild(iChild).gameObject);
        }

        for (int iChild = DrawDeck.childCount - 1; iChild >= 0; iChild--)
        {
            Destroy(DrawDeck.GetChild(iChild).gameObject);
        }

        for (int iChild = ActiveCardHolder.childCount - 1; iChild >= 0; iChild--)
        {
            Destroy(ActiveCardHolder.GetChild(iChild).gameObject);
        }

        for (int i = 0; i < StartingCards.Count; i++)
        {
            GameObject cardGO = Instantiate(prefabCard, DrawDeck);
            CardManager cm = cardGO.GetComponent<CardManager>();
            //DrawDeckCards.Add(cm);
            cm.Init(this, StartingCards[i]);
            cardGO.name = "Card - " + cm.Card.Name;
            cm.MoveToDraw(DrawDeck);
        }
        
        //DrawCards(5);

        //LeanTween.delayedCall(3f, () => DiscardCardsFromHand(4));
        //LeanTween.delayedCall(5f,() => DrawCards(5));
    }

    public void DrawCards(int numCardsToDraw)
    {
        while(numCardsToDraw > 0)
        {
            if (Hand.childCount >= 6) break;
            if (DrawDeck.childCount == 0)
            {
                ShuffleDiscardToDeck();
            }
            Transform cardToMove = DrawDeck.GetChild(Random.Range(0, DrawDeck.childCount));
            cardToMove.GetComponent<CardManager>().MoveToHand(Hand);
            numCardsToDraw--;
            
        }

        //LeanTween.delayedCall(0.1f, ArrangeHand);
        ArrangeHand();
    }

    private void ArrangeHand()
    {
        float spacing = 80f;
        float offset = (spacing - 10f) * Hand.childCount / 2f;
        for (int i = 0; i < Hand.childCount; i++)
        {
            Hand.GetChild(i).GetComponent<CardManager>().SetPosition(Vector3.one.ReplaceX(i* spacing - offset));
        }
    }

    public void DiscardCard(Transform cardToMove)
    {
        cardToMove.GetComponent<CardManager>().MoveToDiscard(DiscardDeck);
    }

    public void DiscardCardsFromHand(int numCardsToDiscard)
    {
        for (int i = numCardsToDiscard - 1; i >= 0; i--)
        {
            Transform cardToMove = Hand.GetChild(i);
            cardToMove.GetComponent<CardManager>().MoveToDiscard(DiscardDeck);
        }
    }

    public void ShuffleDiscardToDeck()
    {
        for(int i = DiscardDeck.childCount - 1; i >= 0; i--)
        {
            DiscardDeck.GetChild(Random.Range(0, DiscardDeck.childCount)).GetComponent<CardManager>().MoveToDraw(DrawDeck);
        }
    }

    public void SetActiveCard(CardManager cm)
    {
        if(activeCardManager != null)
        {
            activeCardManager.MoveToHand(Hand);
            LeanTween.delayedCall(0.31f, ArrangeHand);
        }

        activeCardManager = cm;
    }



    public void SendActiveCardToHand()
    {
        if (activeCardManager != null)
        {            
            activeCardManager.MoveToHand(Hand);
            activeCardManager = null;
            ArrangeHand();
        }
    }
}
