using System;
using System.Collections.Generic;
using CakesManagement.Entities;
using CakesManagement.Models.Cake;

namespace CakesManagement.Services
{
    public interface ICakeService
    {
        List<Cake> GetProductByCategoryId(int categoryId);
        bool Create(Cake model);
        bool Edit(EditCake model);
        Cake Get(int cakeId);
        bool Remove(int cakeId);

    }
}
