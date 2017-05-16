using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Sprite _sprite;

    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }
}
