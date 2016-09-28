using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace service_station.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Order Amount")]
        [Range(typeof(decimal), "0,00", "10000,00")]
        public decimal OrderAmount { get; set; }

        [Required]
        [Display(Name = "Order Status")]
        public Status OrderStatus { get; set; }

        [Required]
        [Display(Name = "Car customer")]
        public int CarCustomerId { get; set; }
        [ForeignKey("CarCustomerId")]
        public Car Car { get; set; }
    }

    public enum Status
    {
        Completed = 1,
        InProgress = 2,
        Cancelled = 3
    }
}