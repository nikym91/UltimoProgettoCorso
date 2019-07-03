using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsClubModel
{
    public abstract class Field
    {
        [Key]
        public int FieldId { get; set; }
        public string Name { get; set; }
        public Surfaces Surface { get; set; }
        public decimal Price { get; set; }
        public int Players { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }

    public enum Sports { Tennis, Paddle, Soccer }
    public enum Surfaces { Clay, Grass, Concrete }


}
