using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public ProductService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsDTOAsync()
        {
            // var productsEntity = await productRepository.GetAllAsync();
            // return mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
            var productQuery = new GetProductsQuery();

            if(productQuery == null )
                throw new Exception("Entity could not be found");

            var result = await mediator.Send(productQuery);
            return mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        // public async Task<ProductDTO> GetByIdDTOAsync(int? id)
        // {
        //     var productEntity = await productRepository.GetByIdAsync(id);
        //     return mapper.Map<ProductDTO>(productEntity);
        // }

        // public async Task CreateDTOAsync(ProductDTO productDTO)
        // {
        //     var product = mapper.Map<Product>(productDTO);
        //     await productRepository.CreateAsync(product);
        // }

        // public async Task UpdateDTOAsync(ProductDTO productDTO)
        // {
        //     var product = mapper.Map<Product>(productDTO);
        //     await productRepository.UpdateAsync(product);
        // }

        // public async Task DeleteDTOAsync(int? id)
        // {
        //     var product = productRepository.GetByIdAsync(id).Result;
        //     await productRepository.DeleteAsync(product);
        // }
    }
}