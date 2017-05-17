using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBigCard : Card
{
    public override CardResult ResolveCard(Player player, Enemy enemy)
    {
        Debug.Log("Resolving DefenceBig");

        return CardResult.PlayerVictory;
    }
}
