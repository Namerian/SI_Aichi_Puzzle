using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanningPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private Transform _playerASkillPanel;

    [SerializeField]
    private Transform _playerBSkillPanel;

    [SerializeField]
    private Text _playerAReadyText;

    [SerializeField]
    private Text _playerBReadyText;

    private bool _playerAReady;
    private bool _playerBReady;

    private void Awake()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(Player playerA, Player playerB)
    {

    }
}
