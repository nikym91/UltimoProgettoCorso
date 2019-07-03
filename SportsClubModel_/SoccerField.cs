using System;
using System.Collections.Generic;
using System.Text;

namespace SportsClubModel
{
    public class SoccerField : Field
    {
        public bool IsSeven { get; set; }

        public SoccerField()
        {
            Players = IsSeven ? 14 : 10;
        }

        public SoccerField(string name, Surfaces surface, decimal price, bool isSeven)
        {
            Name = name;
            Surface = surface;
            Price = price;
            IsSeven = isSeven;
            Players = IsSeven ? 14 : 10;
            //if (IsSeven)
            //{
            //    Players = 14;
            //}
            //else Players = 10;
        }
    }
}
