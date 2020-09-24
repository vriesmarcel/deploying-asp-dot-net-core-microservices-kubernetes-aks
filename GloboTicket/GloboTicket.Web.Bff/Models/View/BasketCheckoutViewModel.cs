using System;
using System.ComponentModel.DataAnnotations;

namespace GloboTicket.Web.Models.View
{
    public class BasketCheckoutViewModel
    {
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }


        //payment information
        [Required]
        [StringLength(16)]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string CardName { get; set; }
        [Required]
        [StringLength(4)]
        public string CardExpiration { get; set; }
        [Required]
        [StringLength(3)]
        public string CvvCode { get; set; }
    }
}
