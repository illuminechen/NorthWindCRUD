namespace NorthWindCRUD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        [Required]
        public int EmployeeID { get; set; }

        [DisplayName("Name")]
        public string CommonName
        {
            get => TitleOfCourtesy + (string.IsNullOrWhiteSpace(TitleOfCourtesy) ? "" : " ") +
                  FirstName + ((string.IsNullOrWhiteSpace(FirstName + LastName)) ? "" : " ") + LastName;
        }

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

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [DisplayName("Location")]
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

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [Display(Name = "Photo")]
        [NotMapped]
        public string PhotoBase64
        {
            get => Photo == null ? ""
                : string.Format("data:image;base64,{0}", Convert.ToBase64String(OleImageUnwrap.GetImageBytesFromOLEField(Photo)));
            set => Photo = string.IsNullOrWhiteSpace(value) ? null : Base64Util.TryParseToByteArray(value.Split(',')[1]);
        }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [ForeignKey("ReportsEmployee")]
        public int? ReportsTo { get; set; }

        [NotMapped]
        public int IntReportsTo { get => ReportsTo.HasValue ? ReportsTo.Value : 0; set { if (value == 0) ReportsTo = null; else ReportsTo = value; } }

        public virtual Employee ReportsEmployee { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
