using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service;

public class CommentService
{
    private readonly CommentRepository _commentRepository;

    public CommentService(CommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }


    public async Task<object?> GetCommentByIdAsync(int Id)
    {
        return await _commentRepository.GetCommentByIdAsync(Id);
    }

   
    public Task<IEnumerable<BlogFeedQuery.CommentFeedQuery>> GetCommenForFeedAsync()
    {
        return _commentRepository.GetCommenForFeedAsync();
    }

    public object? CreateCommentAsync(string Name, string Email, string Text, DateTime PublicationDate, int BlogPostId)
    {
        var doescommentExist = _commentRepository.DoesCommenttWithNameExist( Name);
        if (doescommentExist)
        {
            throw new ValidationException("Name already exists with name " + Name);
        }
        
        return _commentRepository.CreateCommentAsync(Name, Email, Text, PublicationDate, BlogPostId);
        
    }
    

    public async Task<bool> DeleteCommentAsync(int Id)
    {
        // Check if the blog exists
        var comment = await _commentRepository.GetCommentByIdAsync(Id);
        if (comment == null)
        {
            return false;
        }

        // Call repository method to delete the blog entity
        await _commentRepository.DeleteCommentAsync(Id);
        return true;    }

    public object? UpdateCommentAsync(int Id, string Name, string Email, string Text, DateTime PublicationDate,  int BlogPostId)
    {
        return _commentRepository.UpdateCommentAsync(Id, Name, Email, Text, PublicationDate, BlogPostId);
    }
}