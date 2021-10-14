using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakesManagement.Entities;
using CakesManagement.Models.Cake;
using CakesManagement.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CakesManagement.Controllers
{
    public class CakesController : Controller
    {
            private readonly ICakeService cakeService;
            private readonly ICategoryService categoryService;
            private readonly IWebHostEnvironment webHostEnvironment;
            private static Category category = new Category();

            public CakesController(ICakeService cakeService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
            {
                this.cakeService = cakeService;
                this.categoryService = categoryService;
                this.webHostEnvironment = webHostEnvironment;
            }

            [Route("/Cakes/Index/{catId}")]
            public IActionResult Index(int catId)
            {
                category = categoryService.Get(catId);
                ViewBag.Category = category;
                return View(cakeService.GetProductByCategoryId(catId));
            }
            [HttpGet]
            public IActionResult Create()
            {
                ViewBag.Category = category;
                return View();
            }

            [HttpPost]
            public IActionResult Create(CreateCake model)
            {
                if (ModelState.IsValid)
                {
                    model.CategoryId = category.CategoryId;
                    var cake = new Cake()
                    {
                        CategoryId = model.CategoryId,
                        CakeName   = model.CakeName,
                        Ingredients = model.Ingredients,
                        ExpiredDate = model.ExpiredDate,
                        ManufactureDate = model.ManufactureDate,
                        Status = model.Status,
                        Price = model.Price
                        
                    };
                    if (cakeService.Create(cake))
                    {
                        return RedirectToAction("Index", new { catId = category.CategoryId });
                    }
                }
                ViewBag.Category = category;
                return View(model);
            }

        [HttpGet]
        [Route("Cakes/Edit/{cakeId}")]
        public IActionResult Edit(int cakeId)
        {
           var cake = cakeService.Get(cakeId);


           var editProduct = new EditCake()
           {
               CategoryId = cake.CategoryId,
               CakeName = cake.CakeName,
               Ingredients = cake.Ingredients,
               ExpiredDate = cake.ExpiredDate,
               ManufactureDate = cake.ManufactureDate,
               Status = cake.Status,
               Price = cake.Price
           };
           ViewBag.Category = category;
           return View(editProduct);
        }
        [HttpPost]
        public IActionResult Edit(EditCake model)
        {
           if (ModelState.IsValid)
           {
               var product = cakeService.Get(model.CakeId);

               if (cakeService.Edit(model))
               {
                   return RedirectToAction("Index", "Cakes", new { catId = model.CategoryId });
               }
           }
           ViewBag.Category = category;
           return View(model);
        }

        [HttpGet]
        [Route("Cakes/Detail/{cakeId}")]
        public IActionResult Detail(int cakeId)
        {
            var cake = cakeService.Get(cakeId);
            var detailCake = new DetailCake()
            {
                CakeId = cake.CakeId,
                CakeName = cake.CakeName,
                Price = cake.Price,
                Ingredients = cake.Ingredients,
                ExpiredDate = cake.ExpiredDate,
                ManufactureDate = cake.ManufactureDate,
                Status = cake.Status
            };
            ViewBag.Category = category;
            return View(detailCake);
        }

        [HttpGet]
        [Route("Cakes/Remove/{cakeId}")]
        public IActionResult Remove(int cakeId)
        {
            if (cakeService.Remove(cakeId))
            {
                return RedirectToAction("Index", "Cakes", new { catId = category.CategoryId });
            }
            return RedirectToAction("Index", "Cakes", new { cakeId = cakeId });
        }
    }
    
}
