using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsClubModel
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Sport { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public bool IsDouble { get; set; }
        public decimal Price { get; set; }
        public bool IsChallenge { get; set; }
        public Challenge Challenge { get; set; }

        public Reservation() { }

        //Time Start e time end di che tipo devono essere?
        public Reservation(int userId, int fieldId, DateTime date, string timeStart,
            string timeEnd, bool isDouble, bool isChallenge)
        {
            UserId = userId;
            FieldId = fieldId;
            Date = date.Date;
            Sport = null;
            TimeStart = DateTime.Parse(timeStart);
            TimeEnd = DateTime.Parse(timeEnd);
            IsDouble = isDouble;
            IsChallenge = isChallenge;
        }
        public bool IsDoubleReservation()
        {
            return IsDouble && Sport == Sports.Tennis.ToString();
        }

        

    }
}