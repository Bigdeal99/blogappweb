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

    public async Task<IEnumerable<BlogFeedQuery>> GetBlogForFeedAsync()
    {
        // Business Logic: You might want to include logic for caching, sorting, or filtering here.
        var blogs = await _blogRepository.GetBlogForFeedAsync();

        // Example: Perform some post-processing or sorting
        return blogs.OrderBy(blog => blog.PublicationDate);
    }



    public async Task<object?> GetBlogByIdAsync(int id)
    {
        // Business Logic: Check if the ID is valid or perform logging
        if (id <= 0)
        {
            // Handle invalid ID scenario, log it if necessary
            return null;
        }

        return await _blogRepository.GetBlogByIdAsync(id);
    }

   


    public async Task<bool> UpdateBlogAsync(Blog blog)
    {
        // Business Logic: Validate blog data or check if the blog exists
        if (blog.Id <= 0 || string.IsNullOrEmpty(blog.Title))
        {
            // Log error or throw an exception
            throw new ArgumentException("Invalid blog data.");
        }

        // Additional logic before updating the blog
        // For example, logging the update action
        return await _blogRepository.UpdateBlogAsync(blog);
    }


    public async Task<bool> DeleteBlogAsync(int id)
    {
        // Business Logic: Validate the ID
        if (id <= 0)
        {
            // Log error or handle invalid ID scenario
            throw new ArgumentException("Invalid blog ID.");
        }

        // Additional logic before deletion, such as logging or checking dependencies
        return await _blogRepository.DeleteBlogAsync(id);
    }

    
}
