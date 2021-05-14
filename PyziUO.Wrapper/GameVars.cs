using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyziWrap
{
    public partial class Wrapper
    {
        #region Character Info
        public int CharPosX => GetInt("CharPosX");
        public ushort CharPosY { get { return (ushort)GetInt("CharPosY"); } }
        public byte CharPosZ => (byte)GetInt("CharPosZ");
        public int CharDir => GetInt("CharDir");
        public bool CharGhost => (CharType == 402);
        public int CharID => GetInt("CharID");
        public int CharType => GetInt("CharType");
        public string CharStatus => GetString("CharStatus");
        public int BackpackID => GetInt("BackpackID");
        #endregion
        #region Status Info
        public string CharName => GetString("CharName");
        public int Sex => GetInt("Sex");
        public int Str => GetInt("Str");
        public int Dex => GetInt("Dex");
        public int Int => GetInt("Int");
        public int Hits => GetInt("Hits");
        public int MaxHits => GetInt("MaxHits");
        public int Stamina => GetInt("Stamina");
        public int MaxStam => GetInt("MaxStam");
        public int Mana => GetInt("Mana");
        public int MaxMana => GetInt("MaxMana");
        public int MaxStats => GetInt("MaxStats");
        public int Luck => GetInt("Luck");
        public int Weight => GetInt("Weight");
        public int MaxWeight => GetInt("MaxWeight");
        public int DiffHits => GetInt("MaxHits") - GetInt("Hits");
        public int DiffWeight => GetInt("MaxWeight") - GetInt("Weight");
        public int MinDmg => GetInt("MinDmg");
        public int MaxDmg => GetInt("MaxDmg");
        public int Gold => GetInt("Gold");
        public int Followers => GetInt("Followers");
        public int MaxFol => GetInt("MaxFol");
        public int AR => GetInt("AR");
        public int FR => GetInt("FR");
        public int CR => GetInt("CR");
        public int PR => GetInt("PR");
        public int ER => GetInt("ER");
        public int TP => GetInt("TP");
        #endregion
        #region Client Info
        public int CliNr { get { return GetInt("CliNr"); } set { SetInt("CliNr", value); } }
        public int CliCnt => GetInt("CliCnt");
        public string CliLang => GetString("CliLang");
        public string CliVer => GetString("CliVer");
        public bool CliLogged => GetBoolean("CliLogged");
        public int CliLeft => GetInt("CliLeft");
        public int CliTop => GetInt("CliTop");
        public int CliXRes => GetInt("CliXRes");
        public int CliYRes => GetInt("CliYRes");
        public string CliTitle { get { return GetString("CliTitle"); } set { SetString("CliTitle", value); } }
        #endregion
        #region Container Info
        public int NextCPosX { get { return GetInt("NextCPosX"); } set { SetInt("NextCPosX", value); } }
        public int NextCPosY { get { return GetInt("NextCPosY"); } set { SetInt("NextCPosY", value); } }
        public int ContPosX { get { return GetInt("ContPosX"); } set { SetInt("ContPosX", value); } }
        public int ContPosY { get { return GetInt("ContPosY"); } set { SetInt("ContPosY", value); } }
        public int ContSizeX => GetInt("ContSizeX");
        public int ContSizeY => GetInt("ContSizeY");
        public int ContKind => GetInt("ContKind");
        public string ContName => GetString("ContName");
        public int ContType => GetInt("ContType");
        public int ContID => GetInt("ContID");
        #endregion
        #region Combat
        public int EnemyHits => GetInt("EnemyHits");
        public int EnemyID => GetInt("EnemyID");
        public int RHandID { get { return GetInt("RHandID"); } set { SetInt("RHandID", value); } }
        public int LHandID { get { return GetInt("LHandID"); } set { SetInt("LHandID", value); } }
        public int CursorX => GetInt("CursorX");
        public int CursorY => GetInt("CursorY");
        public int CursKind => GetInt("CursKind");
        public bool TargCurs { get { return GetBoolean("TargCurs"); } set { SetBoolean("TargCurs", value); } }
        public string Shard => GetString("Shard");
        public string LShard => GetString("LShard");
        public string SysMsg => GetString("SysMsg");
        #endregion
        #region Last Action
        public int LObjectID { get { return GetInt("LObjectID"); } set { SetInt("LObjectID", value); } }
        public int LObjectType => GetInt("LObjectType");
        public int LTargetID { get { return GetInt("LTargetID"); } set { SetInt("LTargetID", value); } }
        public int LTargetKind { get { return GetInt("LTargetKind"); } set { SetInt("LTargetKind", value); } }
        public int LTargetTile { get { return GetInt("LTargetTile"); } set { SetInt("LTargetTile", value); } }
        public int LTargetX { get { return GetInt("LTargetX"); } set { SetInt("LTargetX", value); } }
        public int LTargetY { get { return GetInt("LTargetY"); } set { SetInt("LTargetY", value); } }
        public int LTargetZ { get { return GetInt("LTargetZ"); } set { SetInt("LTargetZ", value); } }
        public int LLiftedID => GetInt("LLiftedID");

        public int LLiftedKind => GetInt("LLiftedKind");

        public int LLiftedType => GetInt("LLiftedType");

        public int LSpell { get { return GetInt("LSpell"); } set { SetInt("LSpell", value); } }
        #endregion
        #region Misc
        public int RNG => new Random().Next(1, 1000);
        #endregion
        #region Time
        public string Time => DateTime.Now.ToString("HHmmss");
        public string Date => DateTime.Now.ToString("yyMMdd");
        public long Millisec => (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        public int Seconds => (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        #endregion
        #region Functions Results
        public int FoundID => Global._FoundID;
        public int FoundType => Global._FoundType;
        public int FoundX => Global._FoundX;
        public int FoundY => Global._FoundY;
        public int FoundZ => Global._FoundZ;
        public int FoundKind => Global._FoundKind;
        public int FoundStack => Global._FoundStack;
        public int FoundCont => Global._FoundCont;
        public int FoundRep => Global._FoundRep;
        public int FoundCol => Global._FoundCol;
        public int FoundDist => Global._FoundDist;

        public int TileType => Global._TileType;
        public int TileZ => Global._TileZ;
        public string TileName => Global._TileName;
        public int TileFlags => Global._TileFlags;
        public int Jindex => journalRef;
        #endregion
    }
}