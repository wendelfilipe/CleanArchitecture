using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository productRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
        }
        public async Task<Product> Handle(
            ProductUpdateCommand request,
            CancellationToken cancellationToken
        )
        {
            var product = await productRepository.GetByIdAsync(request.Id);

            if(product == null)
            {
                throw new ApplicationException("Entity could not be found");
            }
            else
            {
                product.Update(request.Id,request.Name, request.Description, request.Price, request.Stock, request.Image);

                return await productRepository.UpdateAsync(product);
            }
        }
    }
}