using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    
    public class ProductsController : Controller
    {
        private List<Product> myProducts;

        public ProductsController(Data data)
        {
            myProducts = data.Products;
        }
        
        public IActionResult Index(int filterId, string filtername)
        {
            return View(myProducts);
        }
        
        public IActionResult View(int id)
        {
            Product prod = myProducts.Find(prod => prod.Id == id);
            if (prod != null)
            {
                return View(prod);
            }

            return View("NotExists");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product prod = myProducts.Find(prod => prod.Id == id);
            if (prod != null)
            {
                return View(prod);
            }

            return View("NotExists");
        } 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                myProducts[myProducts.FindIndex(prod => prod.Id == product.Id)] = product;
                return RedirectToAction(nameof(View), new {id = product.Id});
            }
            return View(product);
        }

        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                myProducts.Add(product);
                return RedirectToAction(nameof(View), new {id = product.Id});
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View(new Product(){Id = myProducts.Last().Id + 1});
        }

        public IActionResult Delete(int id)
        {
            myProducts.Remove( myProducts.Find(product => product.Id == id));
            return View("Index", myProducts);
        }

        [HttpGet]
        public IActionResult BulkEdit(Product.Category category)
        {
            var filteredProducts = myProducts.Where<Product>(x => x.Type == category).ToList();
            return View(filteredProducts);
        }

        [HttpPost]
        public IActionResult BulkEdit(List<Product> products)
        {
            if (ModelState.IsValid)
            {
                for(int i = 0; i < myProducts.Count; i++)
                {
                    for(int j = 0; j < products.Count; j++)
                    {
                        if (myProducts[i].Id == products[j].Id)
                        {
                            myProducts[i] = products[j];
                            break;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
