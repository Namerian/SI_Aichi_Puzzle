using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
    public override CardResult ResolveCard(Player player, Enemy enemy)
    {
        Debug.Log("Resolving Attack");

        return CardResult.PlayerVictory;
    }
}
