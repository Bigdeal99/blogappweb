using System;
using System.Collections.Generic;
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

        public IEnumerable<CommentFeedQuery> GetCommentsForFeedAsync()
        {
            return _commentRepository.GetCommentsForFeedAsync();
        }
        
        public async Task<Comment> GetCommentByIdAsync(int Id)
        {
            return await _commentRepository.GetCommentByIdAsync(Id);
        }

        public async Task<bool> DeleteCommentAsync(int Id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(Id);
            if (comment == null)
            {
                return false;
            }

            await _commentRepository.DeleteCommentAsync(Id);
            return true;
        }
        
    


       
        public Comment UpdateCommentAsync(int Id, string Name, string Email, string Text, DateTime PublicationDate, int BlogId)
        {
            return _commentRepository.UpdateCommentAsync( Id, Name, Email, Text, PublicationDate, BlogId);
        }
        

        
        public Comment CreateCommentAsync(string Name, string Email, string Text, DateTime PublicationDate, int BlogId)
        {
           
        
            return _commentRepository.CreateCommentAsync(Name, Email, Text, PublicationDate, BlogId);
        }
        

        
    }
}
