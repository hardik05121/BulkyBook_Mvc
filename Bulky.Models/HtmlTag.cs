using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class HtmlTag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("About Me")]
        public string AboutMe { get; set; }
        public int Age { get; set; }
        [DisplayName("Is Public")]
        public string IsPublic { get; set; }
        [DisplayName("Are You 18+")]
		public string Adult { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Date")]
        public DateTime CreateDate { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
