using System;
using System.Collections.Generic;
using System.Text;

namespace SportsClubModel
{
    public class PaddleCourt : Field
    {
        public PaddleCourt()
        {
            Players = 4;
        }

        public PaddleCourt(string name, Surfaces surface, decimal price)
        {
            Name = name;
            Surface = surface;
            Price = price;
            Players = 4;
        }
    }
}
