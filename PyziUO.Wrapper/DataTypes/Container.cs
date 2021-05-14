using System;
using System.Collections.Generic;
using System.Linq;

namespace PyziWrap.DataTypes
{
    public class Container
    {
        public int Kind { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int SX { get; set; }
        public int SY { get; set; }
        public int ID { get; set; }
        public int Type { get; set; }
        public int HP { get; set; }
        public string Name { get; set; }
        public Container(List<Object> data)
        {
            if (data.Count() >= 8)
            {
                Name = data[0].ToString() ?? string.Empty;
                X = int.TryParse(data[1].ToString(), out int x) ? x : 0;
                Y = int.TryParse(data[2].ToString(), out int y) ? y : 0;
                SX = int.TryParse(data[3].ToString(), out int sx) ? sx : 0;
                SY = int.TryParse(data[4].ToString(), out int sy) ? sy : 0;
                Type = int.TryParse(data[5].ToString(), out int type) ? type : 0;
                ID = int.TryParse(data[6].ToString(), out int id) ? id : 0;
                Kind = int.TryParse(data[7].ToString(), out int kind) ? kind : 0; //Marshal.PtrToStringAnsi((IntPtr)data[7]);
                HP = (data.Count == 9) ? (int.TryParse(data[8].ToString(), out int hp) ? hp : 0) : 0;
            }
        }
        public Container() { }
    }
}
