using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCard : Card
{
    public override CardResult ResolveCard(Player player, Enemy enemy)
    {
        Debug.Log("Resolving Defence");

        return CardResult.PlayerVictory;
    }
}
