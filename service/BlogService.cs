using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service;
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


    public async Task<object?> GetBlogByIdAsync(int id)
    {
        return await _blogRepository.GetBlogByIdAsync(id);

    }
}
