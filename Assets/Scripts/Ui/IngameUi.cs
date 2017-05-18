﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUi : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

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

    [SerializeField]
    private GameObject _AButtonPlayerA;

    [SerializeField]
    private GameObject _AButtonPlayerB;

    public void SetKyoiSliderValue(float value)
    {
        _kyoiSlider.value = value;
    }

    public void Activate()
    {
        _canvasGroup.alpha = 1;

        _AButtonPlayerA.SetActive(false);
        _AButtonPlayerB.SetActive(false);
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
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

    public void ShowAButton(string playerName)
    {
        switch(playerName)
        {
            case "PlayerA":
                _AButtonPlayerA.SetActive(true);
                break;
            case "PlayerB":
                _AButtonPlayerB.SetActive(true);
                break;
        }
    }

    public void HideAButton(string playerName)
    {
        switch (playerName)
        {
            case "PlayerA":
                _AButtonPlayerA.SetActive(false);
                break;
            case "PlayerB":
                _AButtonPlayerB.SetActive(false);
                break;
        }
    }
}
