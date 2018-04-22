namespace ContactDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Mvc;

    public partial class Contact
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EMail { get; set; }

        [DisplayName("Phone Number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Column(Order = 3)]
        public bool Status { get; set; }

        public string StatusDescription
        {
            get
            {
                return (Status == true ? "Active" : "Inactive");
            }
        }

        [DisplayName("Created Date")]
        public DateTime? DateCreated { get; }

    }
}
