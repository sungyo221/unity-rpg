using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSlot : SkillManager
{
    public List<SkillInfo> skillInfo;    
    public Image[] skillSlot;

    private void Start()
    {
        AddSkill(GetSkillInfo(1));
        AddSkill(GetSkillInfo(2));
        AddSkill(GetSkillInfo(3));
        AddSkill(GetSkillInfo(4));
    }

    public void AddSkill(SkillInfo skill)
    {
        Debug.Log($"PlayerSkillSlot+{gameObject.name}");
        skillInfo.Add(skill);
    }
}
