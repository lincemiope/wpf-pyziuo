using System.Collections.Generic;

namespace PyziWrap.DataTypes
{
    public class Tile
    {
        public int Type { get; set; }
        public int Z { get; set; }
        public string Name { get; set; }
        public int Flags { get; set; }
        public Tile(List<object> data)
        {
            if (data.Count < 4)
            {
                Name = string.Empty;
            }
            else
            {
                Type = int.TryParse(data[0].ToString(), out int type) ? type : 0;
                Z = int.TryParse(data[1].ToString(), out int z ) ? z : 0;
                Name = data[2].ToString() ?? string.Empty;
                Flags = int.TryParse(data[3].ToString(), out int flags) ? flags : 0;
            }          
        }
    }
}
