using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductsController(
            IProductService productService,
            ICategoryService categoryService
        )
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductsDTOAsync();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await categoryService.GetCategoriesDTOAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await productService.CreateDTOAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return NotFound();
            
            var productDTO = await productService.GetByIdDTOAsync(id);

            if(productDTO == null)
                return NotFound();

            var categories = await categoryService.GetCategoriesDTOAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDTO.CategoryId);

            return View(productDTO);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await productService.UpdateDTOAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }
    }
}