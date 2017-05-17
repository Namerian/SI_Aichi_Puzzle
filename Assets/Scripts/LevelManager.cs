using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

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

    public Player[] players = new Player[2];
    public List<EnemyFollowing> enemiesFollowing = new List<EnemyFollowing>();
    public Slider kyoiSlider;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        print(((-players[0].KyoiPoints / 2 + players[1].KyoiPoints / 2) / 100));
        kyoiSlider.value = .5f + ((-players[0].KyoiPoints / 2 + players[1].KyoiPoints / 2) / 100);
        SplitEnnemies();
    }

    void SplitEnnemies()
    {
        float num;
        if(players[0].KyoiPoints > players[1].KyoiPoints)
        {
            num = 50 + (players[0].KyoiPoints - players[1].KyoiPoints);
            SetEnnemiesTarget(players[0], players[1], num);
        }
        else
        {
            if (players[1].KyoiPoints > players[0].KyoiPoints)
            {
                num = 50 + (players[1].KyoiPoints - players[0].KyoiPoints);
                SetEnnemiesTarget(players[1], players[0], num);
            }
        }
    }

    void SetEnnemiesTarget(Player mostTartgeted, Player lessTargeted, float percent)
    {
        print((enemiesFollowing.Count / 100f) * percent);
        for (int i = 0; i < enemiesFollowing.Count; i++)
        {
            if(i < (enemiesFollowing.Count / 100f) * percent)
            {
                enemiesFollowing[i].target = mostTartgeted.gameObject;
            }
            else enemiesFollowing[i].target = lessTargeted.gameObject;
        }
    }
}
