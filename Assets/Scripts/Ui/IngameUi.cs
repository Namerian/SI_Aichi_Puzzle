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
                if (currentCardSprite)
                {
                    _currentCardPlayerA.sprite = currentCardSprite;
                }
                else
                {
                    _currentCardPlayerA.color = Color.clear;
                }

                if (nextCardSprite)
                {
                    _nextCardPlayerA.sprite = nextCardSprite;
                }
                else
                {
                    _nextCardPlayerA.color = Color.clear;
                }

                break;
            case "PlayerB":
                if (currentCardSprite)
                {
                    _currentCardPlayerB.sprite = currentCardSprite;
                }
                else
                {
                    _currentCardPlayerB.color = Color.clear;
                }

                if (nextCardSprite)
                {
                    _nextCardPlayerB.sprite = nextCardSprite;
                }
                else
                {
                    _nextCardPlayerB.color = Color.clear;
                }

                break;
        }
    }
}
