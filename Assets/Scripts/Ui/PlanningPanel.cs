using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanningPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private PlayerSkillPanel _playerASkillPanel;

    [SerializeField]
    private PlayerSkillPanel _playerBSkillPanel;

    [SerializeField]
    private GameObject _playerAPressAObj;

    [SerializeField]
    private GameObject _playerBPressAObj;

    [SerializeField]
    private GameObject _playerAReadyObj;

    [SerializeField]
    private GameObject _playerBReadyObj;

    [SerializeField]
    private bool _invertPlayerASkills = false;

    [SerializeField]
    private bool _invertPlayerBSkills = false;

    private bool _playerAReady;
    private bool _playerBReady;
    private OnPlayersReadyDelegate _callback;

    // Update is called once per frame
    void Update()
    {
        if (!_playerAReady && Input.GetButtonDown("PlayerA_Action"))
        {
            _playerAReady = true;
            _playerAPressAObj.SetActive(false);
            _playerAReadyObj.SetActive(true);
        }

        if (!_playerBReady && Input.GetButtonDown("PlayerB_Action"))
        {
            _playerBReady = true;
            _playerBPressAObj.SetActive(false);
            _playerBReadyObj.SetActive(true);
        }

        if ((_playerAReady && _playerBReady) || Input.GetKeyDown(KeyCode.G))
        {
            _callback();
        }
    }

    public void Activate(Player playerA, Player playerB, OnPlayersReadyDelegate callback)
    {
        _playerAPressAObj.SetActive(true);
        _playerAReadyObj.SetActive(false);
        _playerBPressAObj.SetActive(true);
        _playerBReadyObj.SetActive(false);

        _canvasGroup.alpha = 1;

        _playerASkillPanel.FillSkills(playerA, _invertPlayerASkills);
        _playerBSkillPanel.FillSkills(playerB, _invertPlayerBSkills);

        _callback = callback;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
