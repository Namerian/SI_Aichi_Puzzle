﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private CardType _type;

    [SerializeField]
    private int _kyoiPoints;

    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }
    public CardType Type { get { return _type; } }
    public int KyoiPoints { get { return _kyoiPoints; } }

    abstract public CardResult ResolveCard(Player player, Enemy enemy);
}