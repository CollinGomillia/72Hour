using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Models
{
    public class CommentCreate
    {
        [Required]
        [MinLength(3, ErrorMessage = "Need more text.")]
        [MaxLength(8000, ErrorMessage = "Need less text.")]
        public string Text { get; set; }

        public Guid AuthorId { get; set; }
    }
}
