using NorthWindCRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthWindCRUD.Dtos
{
    public class EmployeeDto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeDto()
        {
            //Orders = new HashSet<Order>();
        }

        [Required]
        public int EmployeeID { get; set; }


        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? BirthDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Name")]
        public string CommonName
        {
            get => TitleOfCourtesy + (string.IsNullOrWhiteSpace(TitleOfCourtesy) ? "" : " ") +
                  FirstName + ((string.IsNullOrWhiteSpace(FirstName + LastName)) ? "" : " ") + LastName;
        }

        [Display(Name = "Location")]
        public string CommonLocation
        {
            get => City + (string.IsNullOrWhiteSpace(Region) ? "" : ",") +
                Region + (string.IsNullOrWhiteSpace(Country) ? "" : ",") + Country;
        }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string HomePhone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }

        [Display(Name = "Photo")]
        public string PhotoBase64 { get; set; }

        public string Notes { get; set; }

        public int? ReportsTo { get; set; }
        public virtual Employee ReportsEmployee { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }
    }
}