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

    public void FillSkills(Player player, bool invertSkills)
    {
        List<Card> skills = player.Cards;
        int numSkills = Mathf.Min(skills.Count, 5);

        if(invertSkills)
        {
            Vector3 tmp = _skillGroups[0].transform.localPosition;
            _skillGroups[0].transform.localPosition = _skillGroups[4].transform.localPosition;
            _skillGroups[4].transform.localPosition = tmp;

            tmp = _skillGroups[1].transform.localPosition;
            _skillGroups[1].transform.localPosition = _skillGroups[3].transform.localPosition;
            _skillGroups[3].transform.localPosition = tmp;
        }

        for(int i = 0;i < 5;i++)
        {
            if(i < numSkills)
            {
                _skillGroups[i].SetActive(true);
                _skillImages[i].sprite = skills[i].Sprite;
            }
            else
            {
                _skillGroups[i].SetActive(false);
            }
        }
    }
}
