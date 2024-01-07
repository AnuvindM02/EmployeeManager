using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeeTask
    {
        [Key]
        public Guid? TaskID { get; set; }

        [StringLength(150)]
        [Required]
        public string? TaskDetails { get; set; }

        public Guid? EmployeeID { get; set; }

        public bool IsCompleted { get; set; }
    }
}
