using PyziWrap.DataTypes;
using System;
using PyziWrap.Lists;

namespace PyziWrap
{
    public partial class Wrapper
    {
        public int LSkill { get { return GetInt("LSkill"); } set { SetInt("LSkill", value); } }
        public Skill GetSkill(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            Skill result = null;
            try
            {
                string translation = string.Empty;
                switch (name.ToUpper())
                {
                    case "STEALTH":
                        translation = "Stlt";
                        break;
                    case "ANIMAL LORE":
                        translation = "Anil";
                        break;
                    default:
                        translation = name.Substring(0, 4);
                        break;
                }
                var skillObject = _executeCommand(true, "GetSkill", new object[] { translation });
                if (skillObject != null)
                {
                    result = new Skill
                    {
                        Normal = int.TryParse(skillObject[0].ToString(), out int normal) ? normal : 0,
                        Real = int.TryParse(skillObject[1].ToString(), out int real) ? real : 0,
                        Cap = int.TryParse(skillObject[2].ToString(), out int cap) ? cap : 0,
                        Lock = int.TryParse(skillObject[3].ToString(), out int lck) ? lck : 0
                    };
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
        public int GetSkillReal(string name)
        {
            Skill skill = GetSkill(name);
            if (skill != null)
            {
                return skill.Real;
            }
            else
            {
                return 0;
            }
        }
        public int GetSkillNormal(string name)
        {
            Skill Skill = GetSkill(name);
            if (Skill != null)
            {
                return Skill.Normal;
            }
            else
            {
                return 0;
            }
        }
        public int GetSkillCap(string name)
        {
            Skill Skill = GetSkill(name);
            if (Skill != null)
            {
                return Skill.Cap;
            }
            else
            {
                return 1000;
            }
        }
        public int GetSkillLock(string name)
        {
            Skill Skill = GetSkill(name);
            if (Skill != null)
            {
                return Skill.Lock;
            }
            else
            {
                return 0;
            }
        }
        public void SkillLock(string name, string status)
        {
            try
            {
                string translation = string.Empty;
                switch (name.ToUpper())
                {
                    case "STEALTH":
                        translation = "Stlt";
                        break;
                    case "ANIMAL LORE":
                        translation = "Anil";
                        break;
                    default:
                        translation = name.Substring(0, 4);
                        break;
                }
                int i = Array.IndexOf(SKILL_STATI, status);
                _executeCommand("SkillLock", translation, i);
            } catch (Exception)
            {

            }  
        }
        #region ACTION SKILLS
        public void UseSkill(string name)
        {
            if (Enum.TryParse(name.Replace(" ", ""), true, out ESkill skill) && skill != ESkill.None)
            {
                EventMacro(13, (int)skill);
            }
        }
        #endregion
    }
}
