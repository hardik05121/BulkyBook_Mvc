using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public  DateTime CreatedDate { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(450)]
        public string CreatedBy { get; set; }

    }
}
