using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUi : MonoBehaviour
{
    [SerializeField]
    private Slider _kyoiSlider;

    [SerializeField]
    private Image _currentCardPlayerA;

    [SerializeField]
    private Image _nextCardPlayerA;

    [SerializeField]
    private Image _currentCardPlayerB;

    [SerializeField]
    private Image _nextCardPlayerB;

    public void SetKyoiSliderValue(float value)
    {
        _kyoiSlider.value = value;
    }

    public void SetPlayerCards(string playerName, Sprite currentCardSprite, Sprite nextCardSprite)
    {
        switch (playerName)
        {
            case "PlayerA":
                _currentCardPlayerA.sprite = currentCardSprite;
                _nextCardPlayerA.sprite = nextCardSprite;
                break;
            case "PlayerB":
                _currentCardPlayerB.sprite = currentCardSprite;
                _nextCardPlayerB.sprite = nextCardSprite;
                break;
        }
    }
}
