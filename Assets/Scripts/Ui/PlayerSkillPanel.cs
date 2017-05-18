using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _skillGroups;

    [SerializeField]
    private List<Image> _skillImages;

    public void FillSkills(Player player)
    {
        List<Card> skills = player.Cards;
        int numSkills = Mathf.Min(skills.Count, 5);

        for(int i = 0;i < numSkills;i++)
        {

        }
    }
}
