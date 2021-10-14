using System;
using System.Collections.Generic;
using System.Linq;
using CakesManagement.Contexts;
using CakesManagement.Entities;
using CakesManagement.Models.Cake;
using Microsoft.EntityFrameworkCore;

namespace CakesManagement.Services
{
    public class CakeService : ICakeService
    {
        private readonly CakesManagementDBContext context;

        public CakeService(CakesManagementDBContext context)
        {
            this.context = context;
        }

        public bool Create(Cake model)
        {
            try
            {
                context.Add(model);
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Cake model)
        {
            try
            {
                var cake = Get(model.CakeId);
                cake.CategoryId = model.CategoryId;
                cake.Price = model.Price;
                cake.Ingredients = model.Ingredients;
                cake.ExpiredDate = model.ExpiredDate;
                cake.ManufactureDate = model.ManufactureDate;
                cake.Status = model.Status;
                cake.CakeName = model.CakeName;
                cake.CakeId = model.CakeId;
                context.Attach(cake);
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Cake Get(int cakeId)
        {
            return context.Cakes.Include(p => p.Category).FirstOrDefault(p => p.CakeId == cakeId);
        }

        public List<Cake> GetProductByCategoryId(int categoryId)
        {
            return context.Cakes.Include(p => p.Category).Where(p => p.CategoryId == categoryId).ToList();
        }

        public bool Remove(int cakeId)
        {
            try
            {
                var cake = Get(cakeId);
                cake.Status = !cake.Status;
                context.Attach(cake);
                context.Entry(cake).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool Create(Cake model)
        //{
        //    try
        //    {
        //        context.Add(model);
        //        return context.SaveChanges() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public bool Edit(EditCake model)
        //{
        //    try
        //    {
        //        var cake = Get(model.CakeId);
        //        cake.CategoryId = model.CategoryId;
        //        cake.Price = model.Price;
        //        cake.Ingredients = model.Ingredients;
        //        cake.ExpiredDate = model.ExpiredDate;
        //        cake.ManufactureDate = model.ManufactureDate;
        //        cake.Status = model.Status;
        //        cake.CakeName = model.CakeName;
        //        cake.CakeId = model.CakeId;
        //        context.Attach(cake);
        //        return context.SaveChanges() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public Cake Get(int cakeId)
        //{
        //    return context.Cakes.Include(p => p.Category).FirstOrDefault(p => p.CakeId == cakeId);
        //}

        //public List<Cake> GetProductByCategoryId(int categoryId)
        //{
        //    return context.Cakes.Include(p => p.Category).Where(p => p.CategoryId == categoryId).ToList();
        //}

        //public bool Remove(int cakeId)
        //{
        //    try
        //    {
        //        var cake = Get(cakeId);
        //        context.Remove(cake);
        //        return context.SaveChanges() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
