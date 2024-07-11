using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment environment;
        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IWebHostEnvironment environment
        )
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.environment = environment;
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
            else
            {
                ViewBag.CategoryId = new SelectList( await categoryService.GetCategoriesDTOAsync(), "Id", "Name");
            }

            return View(productDTO);
        }
        [HttpGet()]
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

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
                return NotFound();

            var productDTO = await productService.GetByIdDTOAsync(id);

            if(productDTO == null)
                return NotFound();

            return View(productDTO);
        }
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await productService.DeleteDTOAsync(id);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var productDTO = await productService.GetByIdDTOAsync(id);

            var wwwroot = environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDTO.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDTO);
        }
    }
}