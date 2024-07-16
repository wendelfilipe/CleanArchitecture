using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await categoryService.GetCategoriesDTOAsync();

            if(categories == null)
                return NotFound("Categories not found");
            
            return Ok(categories);
            
        }
        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await categoryService.GetByIdDTOAsync(id);
            if(category == null)
                return NotFound("Category not found");

            return Ok(category);
        } 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
                return BadRequest("Invalid Data");
                
            await categoryService.CreateDTOAsync(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new {id = categoryDTO.Id}, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO )
        {
            if(id != categoryDTO.Id)
                return BadRequest();

            if(categoryDTO == null)
                return BadRequest();

            await categoryService.UpdateDTOAsync(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await categoryService.GetByIdDTOAsync(id);

            if(category == null)
                return NotFound("Category not found");

            await categoryService.DeleteDTOAsync(id);
            
            return Ok(category);
        }
    }
}