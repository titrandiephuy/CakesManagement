using System;
using System.Collections.Generic;
using System.Linq;
using CakesManagement.Contexts;
using CakesManagement.Entities;
using CakesManagement.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace CakesManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CakesManagementDBContext context;

        public CategoryService(CakesManagementDBContext context)
        {
            this.context = context;
        }

        public bool Create(Create create)
        {
            try
            {
                var category = new Category()
                {
                    CategoryName = create.CategoryName,
                    Description = create.Description
                };
                context.Add(category);
                return context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Edit(Edit edit)
        {
            throw new NotImplementedException();
        }

        public Category Get(int categoryId)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public List<Category> Gets()
        {
            return context.Categories.Include(p => p.Cakes).ToList();
        }
    }
}
