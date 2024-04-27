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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> DeleteAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
            return await context.Categories.FindAsync(id); 
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return category;
        }
    }
}