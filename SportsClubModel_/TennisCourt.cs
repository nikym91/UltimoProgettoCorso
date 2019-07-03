using System;
using System.Collections.Generic;
using System.Text;

namespace SportsClubModel
{
    public class TennisCourt : Field
    {
        public TennisCourt()
        {
            Players = 2;
        }

        public TennisCourt(string name, Surfaces surface, decimal price)
        {
            Name = name;
            Surface = surface;
            Price = price;
            Players = 2;
        }
    }
}
