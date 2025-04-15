using FinalProject_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Core.Models
{
    public class MovieComment : Audit
    {
         public string Text { get; set; }
         public int Rate { get; set; }
         public string AppUserId { get; set; }
         public AppUser AppUser { get; set; }
         public int MovieId { get; set; }
         public Movie Movie { get; set; }
         public CommentStatus Status { get; set; } = CommentStatus.Pending;
    }
}
