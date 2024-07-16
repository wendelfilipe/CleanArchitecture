using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entites;
using MediatR;

namespace CleanArchMvc.Application.Products.Commands
{
    public class ProductRemoveCommand : ProductCommand
    {
        public int Id { get; set; }
        public ProductRemoveCommand(int id)
        {
            Id = id;
        }
    }
}