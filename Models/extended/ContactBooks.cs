using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactBookProject.Models
{
    [MetadataType(typeof(contactbookmetadata))]
    public partial class ContactBook
    {
    }

    public class contactbookmetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "please provide first name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "please provide Last name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "please provide EmailID")]
        public string EmailID { get; set; }
    }
}