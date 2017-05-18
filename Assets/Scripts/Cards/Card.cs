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
        Vector3 invertedPlayerRot = player.transform.localEulerAngles;
        invertedPlayerRot.y += 180;
        if(invertedPlayerRot.y >= 360)
        {
            invertedPlayerRot.y -= 360;
        }

        switch (_type)
        {
            case CardType.AttackType1:
                if(enemy.Type == EnemyType.EnemyType1)
                {
                    if(enemy.IsBoss && invertedPlayerRot != enemy.transform.localEulerAngles)
                    {
                        return CardResult.PlayerVictory;
                    }
                    else if(!enemy.IsBoss)
                    {
                        return CardResult.PlayerVictory;
                    }
                }
                break;
            case CardType.AttackType2:
                if (enemy.Type == EnemyType.EnemyType2)
                {
                    if (enemy.IsBoss && invertedPlayerRot != enemy.transform.localEulerAngles)
                    {
                        return CardResult.PlayerVictory;
                    }
                    else if (!enemy.IsBoss)
                    {
                        return CardResult.PlayerVictory;
                    }
                }
                break;
            case CardType.AttackType3:
                if (enemy.Type == EnemyType.EnemyType3)
                {
                    if (enemy.IsBoss && invertedPlayerRot != enemy.transform.localEulerAngles)
                    {
                        return CardResult.PlayerVictory;
                    }
                    else if (!enemy.IsBoss)
                    {
                        return CardResult.PlayerVictory;
                    }
                }
                break;
        }

        return CardResult.EnemyVictory;
    }
}
