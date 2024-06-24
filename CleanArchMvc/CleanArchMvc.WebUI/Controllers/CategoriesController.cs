using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetCategoriesDTOAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if(ModelState.IsValid)
            {
                await categoryService.CreateDTOAsync(categoryDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }
        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return NotFound();

            var categoryDTO = await categoryService.GetByIdDTOAsync(id);

            if(categoryDTO == null)
                return NotFound();

            return View(categoryDTO);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await categoryService.UpdateDTOAsync(categoryDTO);
                }
                catch(Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(categoryDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Detele(int? id)
        {
            if(id == null)
                return NotFound();

            var categoryDTO = await categoryService.GetByIdDTOAsync(id);

            if(categoryDTO == null) 
                return NotFound();

            return View(categoryDTO);
        }
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await categoryService.DeleteDTOAsync(id);
            return RedirectToAction("Index");
        }
    
    }
}