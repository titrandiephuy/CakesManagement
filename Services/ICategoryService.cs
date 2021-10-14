using System;
using System.Collections.Generic;
using CakesManagement.Entities;
using CakesManagement.Models.Category;

namespace CakesManagement.Services
{
    public interface ICategoryService
    {
        List<Category> Gets();
        Category Get(int categoryId);
        bool Create(Create create);
        bool Edit(Edit edit);
    }
}

