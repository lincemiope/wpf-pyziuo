namespace PyziWrap.DataTypes
{
    public class JournalEntry
    {
        public string Line { get; set; }
        public int Col { get; set; }
        public JournalEntry(string line, int col)
        {
            Line = line;
            Col = col;
        }
    }
}
