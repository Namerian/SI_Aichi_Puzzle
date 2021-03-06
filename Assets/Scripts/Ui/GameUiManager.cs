﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager Instance { get; private set; }

    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private IngameUi _ingameUi;

    [SerializeField]
    private GameOverPanel _gameOverPanel;

    public EventSystem EventSystem { get { return _eventSystem; } }
    public IngameUi IngameUi { get { return _ingameUi; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(!_ingameUi.gameObject.activeSelf)
        {
            _ingameUi.gameObject.SetActive(true);
        }

        if(!_gameOverPanel.gameObject.activeSelf)
        {
            _gameOverPanel.gameObject.SetActive(true);
        }
    }

    public void ActivateGameOverPanel()
    {
        _gameOverPanel.Activate();
    }
}
