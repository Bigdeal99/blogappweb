
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.DataModels;

using infrastructure.Repositories;

namespace service;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

   
    public async Task<object?> GetBlogsByCategoryIdAsync(int Id)
    { 
        return await _categoryRepository.GetBlogsByCategoryIdAsync(Id);
    }


    public Category UpdateCategoryAsync(int id, string Name, string Description)
    {
        return _categoryRepository.UpdateCategoryAsync( id, Name, Description);
    }

   


    public object? CreateCategoryAsync(string Name, string Description)
    {
        var doesCategoryExist = _categoryRepository.DoesCategorytWithNameExist( Name);
        if (doesCategoryExist)
        {
            throw new ValidationException("Category already exists with name " + Name);
        }
        
        return _categoryRepository.CreateCategoryAsync(Name, Description);
    }


    public async Task<bool> DeleteCategoryAsync(int Id)
    {
        // Check if the blog exists
        var categoty = await _categoryRepository.GetBlogsByCategoryIdAsync(Id);
        if (categoty == null)
        {
            return false;
        }

        // Call repository method to delete the blog entity
        await _categoryRepository.DeleteCategoryAsync(Id);
        return true;    }
}