using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace service_station.Models
{
    public class FindCustomerViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateofBirth { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }

    public class CarOrders
    {
        public int CarId { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class CarCustomerOrders
    {
        public int CustomerId { get; set; }
        public Car Car { get; set; }
        public Order Order { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}