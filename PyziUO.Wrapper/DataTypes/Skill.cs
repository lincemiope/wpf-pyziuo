namespace PyziWrap.DataTypes
{
    public class Skill
    {
        public int Normal { get; set; }
        public int Real { get; set; }
        public int Cap { get; set; }
        public int Lock { get; set; }
        public Skill()
        {
             Cap = 1000;
        }
    }
}
