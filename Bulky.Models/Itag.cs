using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Itag
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Last Name")]
        public string LName { get; set; }    
    }
}
