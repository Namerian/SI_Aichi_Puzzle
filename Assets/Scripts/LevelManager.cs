using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    //=================================================================
    // Variables - editor
    //=================================================================

    public Player[] players = new Player[2];
    public List<EnemyFollowing> enemiesFollowing = new List<EnemyFollowing>();
    public Slider kyoiSlider;

    //=================================================================
    // Monobehaviour Methods
    //=================================================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(((-players[0].KyoiPoints / 2 + players[1].KyoiPoints / 2) / 100));
        float newKyoiValue = .5f + ((-players[0].KyoiPoints / 2 + players[1].KyoiPoints / 2) / 100);

        //if (kyoiSlider.value != newKyoiValue)
        //{
            kyoiSlider.value = newKyoiValue;
            SplitEnnemies();
        //}
    }

    //=================================================================
    // Public Methods
    //=================================================================

    //=================================================================
    // Private Methods
    //=================================================================

    void SplitEnnemies()
    {
        float num;

        if (kyoiSlider.value >= 0.5f)
        {
            num = kyoiSlider.value * 100;
            SetEnnemiesTarget(players[0], players[1], num);
        }
        else
        {
            num = 100 - kyoiSlider.value * 100;
            SetEnnemiesTarget(players[1], players[0], num);
        }
    }

    void SetEnnemiesTarget(Player mostTartgeted, Player lessTargeted, float percent)
    {
        //print((enemiesFollowing.Count / 100f) * percent);
        for (int i = 0; i < enemiesFollowing.Count; i++)
        {
            if (i < (enemiesFollowing.Count / 100f) * percent)
            {
                enemiesFollowing[i].target = mostTartgeted.gameObject;
            }
            else enemiesFollowing[i].target = lessTargeted.gameObject;
        }
    }
}
