using System;
using System.Collections;
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
    private PlanningPanel _planningPanel;

    [SerializeField]
    private IngameUi _ingameUi;

    [SerializeField]
    private GameOverPanel _gameOverPanel;

    public EventSystem EventSystem { get { return _eventSystem; } }
    public IngameUi IngameUi { get { return _ingameUi; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _planningPanel.gameObject.SetActive(true);
            _ingameUi.gameObject.SetActive(true);
            _gameOverPanel.gameObject.SetActive(true);

            _planningPanel.Hide();
            _ingameUi.Hide();
            _gameOverPanel.Hide();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void ActivatePlanningPanel(Player playerA, Player playerB, OnPlayersReadyDelegate OnPlayersReady)
    {
        _ingameUi.Hide();
        _gameOverPanel.Hide();

        _planningPanel.Activate(playerA, playerB, OnPlayersReady);
    }

    public void ActivateIngameUi()
    {
        _planningPanel.Hide();
        _gameOverPanel.Hide();

        _ingameUi.Activate();
        SoundManager.Instance.PlaySound(SoundManager.Instance._musicAudioSource, SoundManager.Instance._GameMusic, true);
        SoundManager.Instance.PlaySound(SoundManager.Instance._motorAudioSource, SoundManager.Instance._MotorSound, true);
    }

    public void ActivateGameOverPanel()
    {
        _planningPanel.Hide();
        _ingameUi.Hide();

        _gameOverPanel.Activate();
        SoundManager.Instance.PlaySound(SoundManager.Instance._fxAudioSource, SoundManager.Instance._Lose, false);
    }
}
