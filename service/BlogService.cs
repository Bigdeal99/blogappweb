using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service

{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;

        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Task<IEnumerable<BlogFeedQuery>> GetBlogForFeedAsync()
        {
            return _blogRepository.GetBlogForFeedAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(int id)
        {
            return await _blogRepository.GetBlogByIdAsync(id);
        }

        public async Task<int> CreateBlogAsync(CreateBlogRequestDto dto)
        {
            var blog = new Blog
            {
                Title = dto.Title,
                Summary = dto.Summary,
                Content = dto.Content,
                PublicationDate = dto.PublicationDate,
                CategoryId = dto.CategoryId,
                FeaturedImage = dto.FeaturedImage
            };
            return await _blogRepository.CreateBlogAsync(blog);
        }
    }
}
