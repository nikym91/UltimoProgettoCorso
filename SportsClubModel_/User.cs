using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsClubModel
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime DateOfRegistration { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int Reservations { get; set; }
        public decimal SpentMoney { get; set; }
        public int Challenges { get; set; }
        public int Wins { get; set; }
        public ICollection<ChallengesUsers> ChallengesUsers { get; set; }
        public ICollection<Reservation> Reservation { get; set; }

        public User() { }

        public User(string firstName, string lastName, DateTime birthdate, string address, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthdate.Date;
            DateOfRegistration = DateTime.Now.Date;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
        }
     }

}
