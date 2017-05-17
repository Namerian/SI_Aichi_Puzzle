using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private CardType _type;

    [SerializeField]
    private int _kyoiPoints;

    public string Name { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }
    public CardType Type { get { return _type; } }
    public int KyoiPoints { get { return _kyoiPoints; } }

    public CardResult ResolveCard(Player player, Enemy enemy)
    {
        switch (_type)
        {
            case CardType.AttackType1:
                if (enemy.Type == EnemyType.EnemyType1 &&
                    ((enemy.IsBoss && player.transform.forward == -enemy.transform.forward) || !enemy.IsBoss))
                {
                    return CardResult.PlayerVictory;
                }
                break;
            case CardType.AttackType2:
                if (enemy.Type == EnemyType.EnemyType2 &&
                    ((enemy.IsBoss && player.transform.forward == -enemy.transform.forward) || !enemy.IsBoss))
                {
                    return CardResult.PlayerVictory;
                }
                break;
            case CardType.AttackType3:
                if (enemy.Type == EnemyType.EnemyType3 &&
                    ((enemy.IsBoss && player.transform.forward == -enemy.transform.forward) || !enemy.IsBoss))
                {
                    return CardResult.PlayerVictory;
                }
                break;
        }

        return CardResult.EnemyVictory;
    }
}
