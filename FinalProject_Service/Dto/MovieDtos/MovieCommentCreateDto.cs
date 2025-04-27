using FinalProject_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Dto.MovieDtos
{
    public class MovieCommentCreateDto
    {
        public int MovieId { get; set; }

        public string Text { get; set; }
        public CommentStatus Status { get; set; } = CommentStatus.Pending;
    }

}
