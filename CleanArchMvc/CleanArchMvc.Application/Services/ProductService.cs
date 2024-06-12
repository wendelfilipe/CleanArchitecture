using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
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
            var productQuery = new GetProductsQuery();

            if(productQuery == null )
                throw new Exception("Entity could not be found");

            var result = await mediator.Send(productQuery);
            return mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetByIdDTOAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);
            if(productByIdQuery == null)
                throw new Exception("Entity could not be found");

            var result = await mediator.Send(productByIdQuery);

            return mapper.Map<ProductDTO>(result);
        }

        public async Task CreateDTOAsync(ProductDTO productDTO)
        {
            var productCreateCommand = mapper.Map<ProductCreateCommand>(productDTO);
            await mediator.Send(productCreateCommand);
        }

        public async Task UpdateDTOAsync(ProductDTO productDTO)
        {
            var productUpdateCommand = mapper.Map<ProductUpdateCommand>(productDTO);
            await mediator.Send(productUpdateCommand);
        }

        public async Task DeleteDTOAsync(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if(productRemoveCommand == null)
                throw new Exception("Entity could not bet loaded.");
            
            await mediator.Send(productRemoveCommand);
        }
    }
}