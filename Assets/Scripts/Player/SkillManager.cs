using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillInfo
{
    public enum SKILL_EFFECT {Null, IceAttack, IceRain, FireBall, FireRain, Max}
    public SKILL_EFFECT skillEffect;
    public string skillName;
    public string skillContent;
    public Sprite skillImg;
    public GameObject skillPref;
    public int skillDamage;
    public int skillCoolTime;    
    public int skillDuration;

    public SkillInfo(SKILL_EFFECT _skillEffect, string _skillName, string _skillContent, string _skillImg, 
        string _skillPref, int _skillDamage, int _skillCoolTime, int _skillDuration)
    {
        skillEffect = _skillEffect;
        skillName = _skillName;
        skillContent = _skillContent;
        skillImg = Resources.Load<Sprite>("SkillImage/" + _skillImg);
        skillPref = Resources.Load<GameObject>("Prefabs/Skill/" + _skillPref);
        skillDamage = _skillDamage;
        skillCoolTime = _skillCoolTime;
        skillDuration = _skillDuration;
    }
        
}

public class SkillManager : MonoBehaviour
{
    public enum SKILL_TYPE {Null, IceAttack, IceRain, FireBall, FireRain}
    public SKILL_TYPE skillType;
    public List<SkillInfo> listSkillInfo;

    public void Initialize()
    {
        listSkillInfo = new List<SkillInfo>((int)SkillInfo.SKILL_EFFECT.Max);
        listSkillInfo.Add(new SkillInfo(SkillInfo.SKILL_EFFECT.Null, "", "", "", "", 0, 0, 0));
        listSkillInfo.Add(new SkillInfo(SkillInfo.SKILL_EFFECT.IceAttack, "��������", "�ٴڿ��� �������ð� �K�ƿö� �����Ѵ�.", "IceAttack", "IceAttack", 100, 60, 0));
        listSkillInfo.Add(new SkillInfo(SkillInfo.SKILL_EFFECT.IceRain, "��帧 ��", "�ϴÿ��� ��帧�� �������� �����Ѵ�.", "IceRain", "IceRain", 30, 120, 10));
        listSkillInfo.Add(new SkillInfo(SkillInfo.SKILL_EFFECT.FireBall, "�� ��", "�� ���� �����ؼ� ������ ���� �����Ѵ�.", "FireBall", "FireBall", 150, 90, 0));
        listSkillInfo.Add(new SkillInfo(SkillInfo.SKILL_EFFECT.FireRain, "�� ��", "�ϴÿ��� ���� �������� �����Ѵ�.", "FireRain", "FireRain", 30, 120, 10));
    }

    private void Awake()
    {
        Initialize();
    }

    public SkillInfo GetSkillInfo (int idx)
    {
        Debug.Log($"SkillManager+{gameObject.name}");
        return listSkillInfo[idx];
    }

    public SkillInfo GetSkillInfo (SKILL_TYPE type)
    {
        return listSkillInfo[(int)type];
    }
}
