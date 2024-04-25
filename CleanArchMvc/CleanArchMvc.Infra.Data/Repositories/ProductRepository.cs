using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();

            return product;
        }
    }
}