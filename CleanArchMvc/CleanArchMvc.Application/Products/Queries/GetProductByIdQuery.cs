using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entites;
using MediatR;

namespace CleanArchMvc.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int  Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}