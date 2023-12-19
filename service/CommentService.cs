using System.Collections.Generic;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;
using api.TransferModels; 

namespace service
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<IEnumerable<CommentFeedQuery>> GetCommentsForFeedAsync()
        {
            return _commentRepository.GetCommentsForFeedAsync();
        }

        public async Task<CommentFeedQuery> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetCommentByIdAsync(id);
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            return await _commentRepository.DeleteCommentAsync(id);
        }

        public async Task<Comment> CreateCommentAsync(CreateCommentRequestDto dto)
        {
            return await _commentRepository.CreateCommentAsync(dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogId);
        }

        public async Task<Comment> UpdateCommentAsync(int id, UpdateCommentRequestDto dto)
        {
            return await _commentRepository.UpdateCommentAsync(id, dto.Name, dto.Email, dto.Text, dto.PublicationDate, dto.BlogId);
        }
    }
}