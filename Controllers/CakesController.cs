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
        [Route("Product/Edit/{productId}")]
        public IActionResult Edit(int productId)
        {
           var product = productService.Get(productId);


           var editProduct = new EditProduct()
           {
               CategoryId = product.CategoryId,
               ProductId = product.ProductId,
               PicturePath = product.Pictures,
               Price = product.Price,
               ProductName = product.ProductName,
               Quantity = product.Quantity
           };
           ViewBag.Category = category;
           return View(editProduct);
        }
        [HttpPost]
        public IActionResult Edit(EditProduct model)
        {
           if (ModelState.IsValid)
           {
               var product = productService.Get(model.ProductId);

               if (model.Pictures != null)
               {
                   var existFilename = product.Pictures.Split("/").Last();
                   if (string.Compare(existFilename, "no-picture.jpg") != 0)
                   {
                       System.IO.File.Delete(Path.Combine(webHostEnvironment.WebRootPath, "images", existFilename));
                   }
                   var folderPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
                   var filename = $"{Guid.NewGuid()}_{model.Pictures.FileName}";
                   var filePath = Path.Combine(folderPath, filename);
                   using (var fs = new FileStream(filePath, FileMode.Create))
                   {
                       model.Pictures.CopyTo(fs);
                   }
                   model.PicturePath = $"~/images/{filename}";
               }

               if (productService.Edit(model))
               {
                   return RedirectToAction("Index", "Product", new { catId = model.CategoryId });
               }
           }
           ViewBag.Category = category;
           return View(model);
        }

        [HttpGet]
        [Route("Cakes/Detail/{cakeId}")]
        public IActionResult Detail(int productId)
        {
            var cake = cakeService.Get(productId);
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
    
}
