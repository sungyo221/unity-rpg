using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUISlot : SkillQuickSlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{    
    public SkillManager.SKILL_TYPE skillType;
    public bool slotPointer = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillInfo skillInfo = GameManager.GetInstance().skillManager.GetSkillInfo(skillType);
        if (eventData.button == PointerEventData.InputButton.Right && slotPointer)
        {
            if(GameManager.GetInstance().player.level >= 5)
            {
                if(skillType == SkillManager.SKILL_TYPE.IceAttack || skillType == SkillManager.SKILL_TYPE.FireBall)
                {
                    AddSkillQuickSlotLevel5(skillInfo);
                }
            }
            if(GameManager.GetInstance().player.level >= 10)
            {
                if(skillType == SkillManager.SKILL_TYPE.IceRain || skillType == SkillManager.SKILL_TYPE.FireRain)
                {
                    AddSkillQuickSlotLevel10(skillInfo);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotPointer = false;
    }
}
