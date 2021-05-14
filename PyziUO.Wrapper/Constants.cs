using System.Collections.Generic;

namespace PyziWrap
{
    public partial class Wrapper
    {
        private const int T_BOOLEAN = 1;
        private const int T_POINTER = 2;
        private const int T_NUMBER = 3;
        private const int T_STRING = 4;
        private static readonly int[] SOP_VALUES = { 105, 110, 115, 120 };
        private static readonly string[] SOP_SKILLS = {"Anatomy", "Animal Lore", "Animal Taming", "Archery",
                "Blacksmithy", "Bushido", "Chivalry", "Discordance", "Eval Intelligence",
                "Fencing", "Fishing", "Focus", "Healing", "Imbuing", "Mace Fighting",
                "Magery", "Meditation", "Musicianship", "Mysticism", "Necromancy",
                "Ninjitsu", "Parrying", "Peacemaking", "Provocation", "Resisting Spells",
                "Spellweaving", "Spirit Speak", "Stealing", "Stealth", "Swordsmanship",
                "Tactics", "Tailoring", "Throwing", "Veterinary", "Wrestling"};
        private static readonly string[] SKILL_STATI = { "up", "down", "locked" };
        private static readonly Dictionary<string, int> HOTKEY_VALUES = new Dictionary<string, int>
        {
            {"0", 0x30}, {"1", 0x31}, {"2", 0x32}, {"3", 0x33}, {"4", 0x34},
            {"5", 0x35}, {"6", 0x36}, {"7", 0x37}, {"8", 0x38}, {"9", 0x39},
            {"a", 0x41}, {"b", 0x42}, {"c", 0x43}, {"d", 0x44}, {"e", 0x45},
            {"f", 0x46}, {"g", 0x47}, {"h", 0x48}, {"i", 0x49}, {"j", 0x4a},
            {"k", 0x4b}, {"l", 0x4c}, {"m", 0x4d}, {"n", 0x4e}, {"o", 0x4f},
            {"p", 0x50}, {"q", 0x51}, {"r", 0x52}, {"s", 0x53}, {"t", 0x54},
            {"u", 0x55}, {"v", 0x56}, {"w", 0x57}, {"x", 0x58}, {"y", 0x59},
            {"z", 0x5a}, { "f1", 0x70 }, { "f2", 0x71 }, { "f3", 0x72 }, { "f4", 0x73 },
            { "f5", 0x74 }, { "f6", 0x75 }, { "f7", 0x76 }, { "f8", 0x77 },
            { "f9", 0x78 }, { "f10", 0x79},  { "f11", 0x7A },  { "f12", 0x7B },
            { "esc", 0x1B}, { "back", 0x08},  { "tab", 0x09 },  { "enter", 0x0D },
            { "pause", 0x13}, { "capslock", 0x14 }, { "space", 0x20 },
            { "pgup", 0x21}, { "pgdn", 0x22}, { "end", 0x23 },  { "home", 0x24 },
            { "left", 0x25}, { "right", 0x27}, { "up", 0x26 },  { "down", 0x28 },
            { "prnscr", 0x2A}, { "insert", 0x2D}, { "delete", 0x2E },
            { "numlock", 0x90}, { "scrolllock", 0x91 }
        };
    }
}
