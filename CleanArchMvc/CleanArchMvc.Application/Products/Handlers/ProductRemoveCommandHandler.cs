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
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository productRepository;
        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Product> Handle(
            ProductRemoveCommand request, 
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
                var result = await productRepository.DeleteAsync(product);
                return result;
            }
        }
    }
}