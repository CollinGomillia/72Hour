using _72Hour.Data;
using _72Hour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _72Hour.Data.ApplicationUser;

namespace _72Hour.Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    AuthorId = _userId,
                    Title = model.Title,
                    Text = model.Text,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PostListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.AuthorId == _userId)
                        .Select(
                            e =>
                                new PostListItem
                                {
                                    Id = e.Id,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                            );

                return query.ToArray();
            }
        }

        public PostDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.Id == id && e.AuthorId == _userId);
                return
                    new PostDetail
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        Text = entity.Text,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                       .Posts
                       .Single(e => e.Id == model.Id && e.AuthorId == _userId);

                entity.Title = model.Title;
                entity.Text = model.Text;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                         .Posts
                         .Single(e => e.Id == postId && e.AuthorId == _userId);

                ctx.Posts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

