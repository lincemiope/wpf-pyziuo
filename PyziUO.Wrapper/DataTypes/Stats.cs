namespace PyziWrap.DataTypes
{
    public class Stats
    { 
        public int HP { get; set; }
        public int ContID { get; set; }
        public int Index { get; set; }
        public string Color { get; set; }
        public Stats(int hp, string color, int contid, int index)
        {
            HP = hp;
            Color = color;
            ContID = contid;
            Index = index;
        }
    }
}
