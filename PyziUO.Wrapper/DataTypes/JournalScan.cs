namespace PyziWrap.DataTypes
{
    public class JournalScan
    {
        public int NewRef { get; set; }
        public int Cnt { get; set; }
        public JournalScan(int _newref, int _cnt)
        {
            NewRef = _newref;
            Cnt = _cnt;
        }
    }
}
