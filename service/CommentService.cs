using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        

        public Task<IEnumerable<BlogFeedQuery.CommentFeedQuery>> GetCommentsForFeedAsync()
        {
            return _commentRepository.GetCommentsForFeedAsync();
        }
        
        public async Task<BlogFeedQuery.CommentFeedQuery> GetCommentByIdAsync(int Id)
        {
            return await _commentRepository.GetCommentByIdAsync(Id);
        }
        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return false;
            }

            await _commentRepository.DeleteCommentAsync(id);
            return true;
        }

        public object? CreatecommentAsync(string Name, string Email, string Text, DateTime PublicationDate, int BlogPostId)
        {
            
        
            return _commentRepository.CreatecommentAsync(Name, Email, Text, PublicationDate, BlogPostId);
            
        }

        public object? UpdateCommentAsync(string Name, string Email, string Text, DateTime PublicationDate, int BlogPostId)
        {
            return _commentRepository.UpdateCommentAsync( Name, Email, Text, PublicationDate, BlogPostId);

        }

       

        
    }
}
