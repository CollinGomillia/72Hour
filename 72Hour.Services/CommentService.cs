using _72HourProject.Data;
using _72HourProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    Text = model.Text,
                    AuthorId = _userId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentList> GetComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Comments.Where(e => e.AuthorId == _userId).Select(e => new CommentList { CommentId = e.CommentId, Text = e.Text });
                return query.ToArray();
            }
        }
    }
}
