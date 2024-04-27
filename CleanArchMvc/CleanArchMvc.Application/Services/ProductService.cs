using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository? productRepository, IMapper mapper)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsDTOAsync()
        {
            var productsEntity = await productRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetByIdDTOAsync(int? id)
        {
            var productEntity = await productRepository.GetByIdAsync(id);
            return mapper.Map<ProductDTO>(productEntity);
        }

        public async Task CreateDTOAsync(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            await productRepository.CreateAsync(product);
        }

        public async Task UpdateDTOAsync(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            await productRepository.UpdateAsync(product);
        }

        public async Task DeleteDTOAsync(int? id)
        {
            var product = productRepository.GetByIdAsync(id).Result;
            await productRepository.DeleteAsync(product);
        }
    }
}