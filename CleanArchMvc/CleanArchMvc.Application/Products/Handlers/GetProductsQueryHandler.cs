using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository productRepository;
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(
            GetProductsQuery request, 
            CancellationToken cancellationToken
        )
        {
            return await productRepository.GetAllAsync();
        }
    }
}