using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await productService.GetProductsDTOAsync();

            if(products == null)
                return NotFound("Products not found");
            
            return Ok(products);
            
        }
        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await productService.GetByIdDTOAsync(id);
            if(product == null)
                return NotFound("Products not found");

            return Ok(product);
        } 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if(productDTO == null)
                return BadRequest("Invalid Data");
                
            await productService.CreateDTOAsync(productDTO);

            return new CreatedAtRouteResult("GetProduct", new {id = productDTO.Id}, productDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO )
        {
            if(id != productDTO.Id)
                return BadRequest("Invalid Data");

            if(productDTO == null)
                return BadRequest("Invalid Data");

            await productService.UpdateDTOAsync(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await productService.GetByIdDTOAsync(id);

            if(product == null)
                return NotFound("Product not found");

            await productService.DeleteDTOAsync(id);
            
            return Ok(product);
        }
    }
}