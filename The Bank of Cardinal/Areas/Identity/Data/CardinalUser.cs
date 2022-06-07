using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace The_Bank_of_Cardinal.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CardinalUser class
    public class CardinalUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(500)")]
        public string Street { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string State { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string ZipCode { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LoginName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string SSN { get; set; }

        [PersonalData]
        [Column(TypeName = "double")]
        public double AccountBalance { get; set; }








    }
}
