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
    

    

        public async Task<bool> DeleteBlogAsync(int Id)
        {
            // Check if the blog exists
            var blog = await _blogRepository.GetBlogByIdAsync(Id);
            if (blog == null)
            {
                return false;
            }

            // Call repository method to delete the blog entity
            await _blogRepository.DeleteBlogAsync(Id);
            return true;
        }

        

        public object? CreateBlogAsync(string Title, string Summary, string Content, DateTime PublicationDate, int CategoryId, string FeaturedImage)
        {
            var doesBlogExist = _blogRepository.DoesBlogtWithNameExist( Title);
            if (doesBlogExist)
            {
                throw new ValidationException("Title already exists with name " + Title);
            }
        
            return _blogRepository.CreateBlogAsync(Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage);        }

        public object? UpdateBlogAsync(int Id, string Title, string Summary, string Content, DateTime PublicationDate, int CategoryId, string FeaturedImage)
        {
            return _blogRepository.UpdateBlogAsync(Id, Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage);
        }
}