using System;
using System.Collections.Generic;
using CakesManagement.Entities;

namespace CakesManagement.Services
{
    public interface ICakeService
    {
        List<Cake> GetProductByCategoryId(int categoryId);
        bool Create(Cake model);
        bool Edit(Cake model);
        Cake Get(int cakeId);
        bool Remove(int cakeId);

    }
}
