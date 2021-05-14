using PyziWrap.Lists;
using System;

namespace PyziWrap
{
    public partial class Wrapper
    {
        #region Event Macro
        protected void EventMacro(params object[] param)
        {
            if (param.Length == 0 || param[0].GetType() != typeof(int)) { return; }
            if (param.Length == 1)
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Call");
                DLL.NativeMethods.PushStrVal(Handle, "Macro");
                DLL.NativeMethods.PushInteger(Handle, (int)param[0]);
                DLL.NativeMethods.PushInteger(Handle, 0);
                DLL.NativeMethods.Execute(Handle);
            }
            else
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Call");
                DLL.NativeMethods.PushStrVal(Handle, "Macro");
                DLL.NativeMethods.PushInteger(Handle, (int)param[0]);
                bool flag1 = false;
                if (param[1].GetType() == typeof(int))
                {
                    DLL.NativeMethods.PushInteger(Handle, (int)param[1]);
                    flag1 = true;
                }
                else if (param[1].GetType() == typeof(string))
                {
                    var par1 = (string)param[1];
                    if (!string.IsNullOrEmpty(par1))
                    {
                        DLL.NativeMethods.PushStrVal(Handle, par1);
                        flag1 = true;
                    }
                }
                if (flag1 && param.Length > 2)
                {
                    if (param[2].GetType() == typeof(int))
                    {
                        DLL.NativeMethods.PushInteger(Handle, (int)param[2]);
                    }
                    else if (param[2].GetType() == typeof(string))
                    {
                        var par2 = (string)param[2];
                        if (!string.IsNullOrEmpty(par2))
                        {
                            DLL.NativeMethods.PushStrVal(Handle, par2);
                        }
                    }
                }
                DLL.NativeMethods.Execute(Handle);
            }
        }
        #endregion
        #region SPEECH
        public void Say(string txt)
        {
            EventMacro(1, txt);
        }
        public void Emote(string txt)
        {
            EventMacro(2, txt);
        }
        public void Whisper()
        {
            EventMacro(3);
        }
        public void Yell()
        {
            EventMacro(4, 4);
        }
        #endregion
        #region MOVEMENT
        public void Walk(int Dir)
        {
            string[] _dir = new string[] { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };
            EventMacro(5, _dir[Dir]);
        }
        #endregion
        #region GUMP CONTROL
        public void WarPeace()
        {
            EventMacro(6);
        }
        public void OpenConfiguration()
        {
            EventMacro(8);
        }

        public void OpenPaperdoll()
        {
            EventMacro(8, 1);
        }

        public void OpenStatus()
        {
            EventMacro(8, 2);
        }

        public void OpenJournal()
        {
            EventMacro(8, 3);
        }

        public void OpenSkills()
        {
            EventMacro(8, 4);
        }

        public void OpenSpellbook()
        {
            EventMacro(8, 5);
        }

        public void OpenChat()
        {
            EventMacro(8, 6);
        }

        public void OpenBackpack()
        {
            EventMacro(8, 7);
        }

        public void OpenOverView()
        {
            EventMacro(8, 8);
        }

        public void OpenMail()
        {
            EventMacro(8, 9);
        }

        public void OpenPartyManifest()
        {
            EventMacro(8, 10);
        }

        public void OpenPartyChat()
        {
            EventMacro(8, 11);
        }

        public void OpenNecroSpellbook()
        {
            EventMacro(8, 12);
        }

        public void OpenPaladinSpellbook()
        {
            EventMacro(8, 13);
        }

        public void OpenCombatBook()
        {
            EventMacro(8, 14);
        }

        public void OpenBushidoSpellbook()
        {
            EventMacro(8, 15);
        }

        public void OpenNinjitsuSpellbook()
        {
            EventMacro(8, 16);
        }
        public void OpenGuild()
        {
            EventMacro(8, 17);
        }
        public void OpenSpellweavingSpellbook()
        {
            EventMacro(8, 18);
        }
        public void OpenQuestLog()
        {
            EventMacro(8, 19);
        }
        public void CloseConfiguration()
        {
            EventMacro(9);
        }
        public void ClosePaperdoll()
        {
            EventMacro(9, 1);
        }
        public void CloseStatus()
        {
            EventMacro(9, 2);
        }
        public void CloseJournal()
        {
            EventMacro(9, 3);
        }
        public void CloseSkills()
        {
            EventMacro(9, 4);
        }
        public void CloseSpellbook()
        {
            EventMacro(9, 5);
        }
        public void CloseChat()
        {
            EventMacro(9, 6);
        }
        public void CloseBackpack()
        {
            EventMacro(9, 7);
        }
        public void CloseOverview()
        {
            EventMacro(9, 8);
        }
        public void CloseMail()
        {
            EventMacro(9, 9);
        }
        public void ClosePartyManifest()
        {
            EventMacro(9, 10);
        }
        public void ClosePartyChat()
        {
            EventMacro(9, 11);
        }
        public void CloseNecroSpellbook()
        {
            EventMacro(9, 12);
        }
        public void ClosePaladinSpellbook()
        {
            EventMacro(9, 13);
        }
        public void CloseCombatBook()
        {
            EventMacro(9, 14);
        }
        public void CloseBushidoSpellbook()
        {
            EventMacro(9, 15);
        }
        public void CloseNinjitsuSpellbook()
        {
            EventMacro(9, 16);
        }
        public void CloseGuild()
        {
            EventMacro(9, 17);
        }
        public void MinimizePaperdoll()
        {
            EventMacro(10, 1);
        }
        public void MinimizeStatus()
        {
            EventMacro(10, 2);
        }
        public void MinimizeJournal()
        {
            EventMacro(10, 3);
        }
        public void MinimizeSkills()
        {
            EventMacro(10, 4);
        }
        public void MinimizeSpellbook()
        {
            EventMacro(10, 5);
        }
        public void MinimizeChat()
        {
            EventMacro(10, 6);
        }
        public void MinimizeBackpack()
        {
            EventMacro(10, 7);
        }
        public void MinimizeOverview()
        {
            EventMacro(10, 8);
        }
        public void MinimizeMail()
        {
            EventMacro(10, 9);
        }
        public void MinimizePartyManifest()
        {
            EventMacro(10, 10);
        }
        public void MinimizePartyChat()
        {
            EventMacro(10, 11);
        }
        public void MinimizeNecroSpellbook()
        {
            EventMacro(10, 12);
        }
        public void MinimizePaladinSpellbook()
        {
            EventMacro(10, 13);
        }
        public void MinimizeCombatBook()
        {
            EventMacro(10, 14);
        }
        public void MinimizeBushidoSpellbook()
        {
            EventMacro(10, 15);
        }
        public void MinimizeNinjitsuSpellbook()
        {
            EventMacro(10, 16);
        }
        public void MinimizeGuild()
        {
            EventMacro(10, 17);
        }
        public void MaximizePaperdoll()
        {
            EventMacro(11, 1);
        }
        public void MaximizeStatus()
        {
            EventMacro(11, 2);
        }
        public void MaximizeJournal()
        {
            EventMacro(11, 3);
        }
        public void MaximizeSkills()
        {
            EventMacro(11, 4);
        }
        public void MaximizeSpellbook()
        {
            EventMacro(11, 5);
        }
        public void MaximizeChat()
        {
            EventMacro(11, 6);
        }
        public void MaximizeBackpack()
        {
            EventMacro(11, 7);
        }
        public void MaximizeOverview()
        {
            EventMacro(11, 8);
        }
        public void MaximizeMail()
        {
            EventMacro(11, 9);
        }
        public void MaximizePartyManifest()
        {
            EventMacro(11, 10);
        }
        public void MaximizePartyChat()
        {
            EventMacro(11, 11);
        }
        public void MaximizeNecroSpellbook()
        {
            EventMacro(11, 12);
        }
        public void MaximizePaladinSpellbook()
        {
            EventMacro(11, 13);
        }
        public void MaximizeCombatBook()
        {
            EventMacro(11, 14);
        }
        public void MaximizeBushidoSpellbook()
        {
            EventMacro(11, 15);
        }
        public void MaximizeNinjitsuSpellbook()
        {
            EventMacro(11, 16);
        }
        public void MaximizeGuild()
        {
            EventMacro(11, 17);
        }
        #endregion
        #region OPEN DOOR
        public void OpenDoor()
        {
            EventMacro(12);
        }
        #endregion
        #region SPELLS	
        public void Cast(string spell)
        {
            if (Enum.TryParse(spell.Replace("'", "_").Replace(" ", "_"), true, out ESpell result) && result != ESpell.None)
            {
                int _spell = (int)result;
                EventMacro(15, _spell);
            }
        }
        public void SmartCast(string spell, string target, int x = 0, int y = 0, int z = 0)
        {
            Cast(spell);
            bool t = Target(15000);
            switch (target.ToLower())
            {
                case "self":
                    LTargetKind = 1;
                    TargetSelf();
                    break;
                case "last":
                    LTargetKind = 1;
                    LastTarget();
                    break;
                case "ground":
                    LTargetX = x;
                    LTargetY = y;
                    LTargetZ = z;
                    LTargetKind = 2;
                    LastTarget();
                    break;
                    /*case "resource":
                        LTargetX = x;
                        LTargetY = y;
                        LTargetZ = z;
                        LTargetKind = 3;
                        LastTarget();
                        break;*/
            }
        }
        #endregion
        #region MISCELLANEOUS
        public void LastSpell()
        {
            EventMacro(16);
        }
        public void LastObject()
        {
            EventMacro(17);

        }
        public void Bow()
        {
            EventMacro(18);
        }
        public void Salute()
        {
            EventMacro(19);
        }
        public void QuitGame()
        {
            EventMacro(20);
        }
        public void AllNames()
        {
            EventMacro(21);
        }
        public void LastTarget()
        {
            EventMacro(22);
        }
        public void TargetSelf()
        {
            EventMacro(23);
        }
        public void ToggleLHand()
        {
            EventMacro(24, 1);
        }
        public void ToggleRHand()
        {
            EventMacro(24, 2);
        }
        public void WaitForTarget()
        {
            EventMacro(25);
        }
        public void TargetNext()
        {
            EventMacro(26);
        }
        public void AttackLast()
        {
            EventMacro(27);
        }
        public void Delay(int TimeOutMS)
        {
            EventMacro(28, 0, TimeOutMS);
        }
        public void CircleTrans()
        {
            EventMacro(29);
        }
        public void CloseGumps()
        {
            EventMacro(31);
        }
        public void AlwaysRun()
        {
            EventMacro(32);
        }
        public void SaveDesktop()
        {
            EventMacro(33);
        }
        public void KillGumpOpen()
        {
            EventMacro(34);
        }
        public void PrimaryAbility()
        {
            EventMacro(35);
        }
        public void SecondaryAbility()
        {
            EventMacro(36);
        }
        public void EquipLastWeapon()
        {
            EventMacro(37);
        }
        #endregion
        #region CLIENT'S RANGE CONTROL
        public void SetUpdateRange(int Range)
        {
            EventMacro(38, 0, Range);
        }
        public void ModifyUpdateRange(int Range)
        {
            EventMacro(39, 0, Range);
        }
        public void IncreaseUpdateRange()
        {
            EventMacro(40);
        }
        public void DecreaseUpdateRange()
        {
            EventMacro(41);
        }
        public void MaximumUpdateRange()
        {
            EventMacro(42);
        }
        public void MinimumUpdateRange()
        {
            EventMacro(43);
        }
        public void DefaultUpdateRange()
        {
            EventMacro(44);
        }
        public void UpdateUpdateRange()
        {
            EventMacro(45);
        }
        public void EnableUpdateRangeColor()
        {
            EventMacro(46);
        }
        public void DisableUpdateRangeColor()
        {
            EventMacro(47);
        }
        public void ToggleUpdateRangeColor()
        {
            EventMacro(48);
        }
        #endregion
        #region INVOKE VIRTUES
        public void Invoke(string virtue)
        {
            string[] v = new string[] {"Honor", "Sacrifice", "Valor",
                "Compassion", "N/A", "N/A",
                "Justice", "N/A"};
            EventMacro(49, Array.IndexOf(v, virtue) + 1);
        }
        #endregion
        #region TARGETING SYSTEM
        // num(1,5) { 1-hostile; 2-party members; 3-followers; 4-objects; 5-mobiles
        public void SelectNext(int num)
        {
            if (num > 5)
                num = 5;
            if (num < 1)
                num = 1;
            EventMacro(50, num);
        }

        public void SelectPrevious(int num)
        {
            if (num > 5)
                num = 5;
            if (num < 1)
                num = 1;
            EventMacro(51, num);
        }

        public void SelectNearest(int num)
        {
            if (num > 5)
                num = 5;
            if (num < 1)
                num = 1;
            EventMacro(52, num);
        }
        #endregion
        #region Actions
        public void AttackSelected()
        {
            EventMacro(53);
        }

        public void UseSelected()
        {
            EventMacro(54);
        }
        public void CurrentTarget()
        {
            EventMacro(55);
        }
        public void ToggleTargetingSystem()
        {
            EventMacro(56);
        }
        public void ToggleBuffWindow()
        {
            EventMacro(57);
        }
        public void BandageSelf()
        {
            EventMacro(58);
        }
        public void Bandage()
        {
            EventMacro(59);
        }
        #endregion
        #region GARGOYLE
        public void ToggleFly()
        {
            EventMacro(60);
        }
        #endregion
    }
}
