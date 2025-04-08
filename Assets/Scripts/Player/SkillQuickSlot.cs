using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillQuickSlot : MonoBehaviour
{    
    public SkillQuick[] skillQuick;

    public void AddSkillQuickSlotLevel5(SkillInfo skill)
    {
        if (skillQuick[0].quickSlotImage.sprite == null)
        {
            skillQuick[0].skillType = (SkillManager.SKILL_TYPE)skill.skillEffect;
            skillQuick[0].quickSlotImage.sprite = skill.skillImg;
            skillQuick[0].quickSlotImage.color = Color.white;
        }
        else
        {
            skillQuick[0].skillType = (SkillManager.SKILL_TYPE)skill.skillEffect;
            skillQuick[0].quickSlotImage.sprite = skill.skillImg;
        }
    }

    public void AddSkillQuickSlotLevel10(SkillInfo skill)
    {
        if (skillQuick[1].quickSlotImage.sprite == null)
        {
            skillQuick[1].skillType = (SkillManager.SKILL_TYPE)skill.skillEffect;
            skillQuick[1].quickSlotImage.sprite = skill.skillImg;
            skillQuick[1].quickSlotImage.color = Color.white;
        }
        else
        {
            skillQuick[1].skillType = (SkillManager.SKILL_TYPE)skill.skillEffect;
            skillQuick[1].quickSlotImage.sprite = skill.skillImg;
        }
    }
}
