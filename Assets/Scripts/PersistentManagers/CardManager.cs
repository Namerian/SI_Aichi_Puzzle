using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField]
    private int _maxCards = 10;

    [SerializeField]
    private List<CardListElement> _cards;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddCard(Card card)
    {
        if(ComputeCardAmount() >= _maxCards)
        {
            Debug.LogError("CardManager: could not add card! max amount reached!");
            return;
        }

        foreach (CardListElement element in _cards)
        {
            if (element.card.Name == card.Name)
            {
                element.amount++;
                return;
            }
        }

        _cards.Add(new CardListElement(card, 1));
    }

    public List<Card> GetAllCards()
    {
        List<Card> result = new List<Card>();

        foreach(CardListElement element in _cards)
        {
            for(int i =0;i < element.amount;i++)
            {
                result.Add(element.card);
            }
        }

        return result;
    }

    private int ComputeCardAmount()
    {
        int result = 0;

        foreach (CardListElement element in _cards)
        {
            result += element.amount;
        }

        return result;
    }

    [Serializable]
    private class CardListElement
    {
        public Card card;
        public int amount;

        public CardListElement(Card card, int amount)
        {
            this.card = card;
            this.amount = amount;
        }
    }
}
