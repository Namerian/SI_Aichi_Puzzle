using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    //=================================================================
    // Variables - editor
    //=================================================================

    //public List<EnemyFollowing> enemiesFollowing = new List<EnemyFollowing>();

    //=================================================================
    // Variables - private
    //=================================================================

    private Player[] _players = new Player[2];
    private float _kyoiSliderValue = 0.5f;

    //=================================================================
    // Properties
    //=================================================================

    public bool IsGamePaused { get; private set; }

    //=================================================================
    // Monobehaviour Methods
    //=================================================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (CardManager.Instance == null)
            {
                Instantiate(Resources.Load("Prefabs/PersistentGameObjects/CardManager"));
            }

            if (SoundManager.Instance == null)
            {
                Instantiate(Resources.Load("Prefabs/PersistentGameObjects/SoundManager"));
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        IsGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsGamePaused && _players[0].IsDead && _players[1].IsDead)
        {
            IsGamePaused = true;
            GameUiManager.Instance.ActivateGameOverPanel();
        }

        //print(((-players[0].KyoiPoints / 2 + players[1].KyoiPoints / 2) / 100));
        float newKyoiValue = .5f + ((-_players[0].KyoiPoints / 2 + _players[1].KyoiPoints / 2) / 100);

        //if (kyoiSlider.value != newKyoiValue)
        //{
        _kyoiSliderValue = newKyoiValue;
        GameUiManager.Instance.IngameUi.SetKyoiSliderValue(_kyoiSliderValue);
        //SplitEnnemies();
        //}
    }

    //=================================================================
    // Public Methods
    //=================================================================

    public void RegisterPlayer(string playerName, Player player)
    {
        switch (playerName)
        {
            case "PlayerA":
                _players[0] = player;
                break;
            case "PlayerB":
                _players[1] = player;
                break;
        }
    }

    //=================================================================
    // Private Methods
    //=================================================================

    //void SplitEnnemies()
    //{
    //    float num;

    //    if (_kyoiSliderValue >= 0.5f)
    //    {
    //        num = _kyoiSliderValue * 100;
    //        SetEnnemiesTarget(_players[0], _players[1], num);
    //    }
    //    else
    //    {
    //        num = 100 - _kyoiSliderValue * 100;
    //        SetEnnemiesTarget(_players[1], _players[0], num);
    //    }
    //}

    //void SetEnnemiesTarget(Player mostTartgeted, Player lessTargeted, float percent)
    //{
    //    //print((enemiesFollowing.Count / 100f) * percent);
    //    for (int i = 0; i < enemiesFollowing.Count; i++)
    //    {
    //        if (i < (enemiesFollowing.Count / 100f) * percent)
    //        {
    //            enemiesFollowing[i].target = mostTartgeted.gameObject;
    //        }
    //        else enemiesFollowing[i].target = lessTargeted.gameObject;
    //    }
    //}
}
