using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBigCard : Card
{
    public override CardResult ResolveCard(Player player, Enemy enemy)
    {
        Debug.Log("Resolving AttackBig");

        return CardResult.PlayerVictory;
    }
}
