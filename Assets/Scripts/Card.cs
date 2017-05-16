using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardsName {attack, attackBig, defence, trap }

public class Card : MonoBehaviour
{
    public CardsName cardName;

    [SerializeField]
    private string _name;

    [SerializeField]
    private Sprite _sprite;

    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }
    public int kyoiPoints;
}
