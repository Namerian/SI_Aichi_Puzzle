using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool _isBoss;

    [SerializeField]
    private EnemyType _type;

    public bool IsBoss { get { return _isBoss; } }
    public EnemyType Type { get { return _type; } }

    private void Start()
    {
        LevelManager.Instance.RegisterEnemy(this);
    }
}
