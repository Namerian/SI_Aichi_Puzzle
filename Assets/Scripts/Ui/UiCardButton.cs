using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCardButton : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    private Card _card = null;

    public Card Card { get { return _card; } }

    // Use this for initialization
    void Start()
    {
        if (_card != null)
        {
            this.transform.FindChild("Image").GetComponent<Image>().sprite = _card.Sprite;
        }
    }

    public void Initialize(Card card)
    {
        _card = card;
    }

    public void SetSelectionColor(Color color)
    {
        _image.color = color;
    }
}
