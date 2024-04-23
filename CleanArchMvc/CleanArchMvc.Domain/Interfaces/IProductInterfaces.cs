using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entites;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductInterfaces
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Category category);
        Task<Product> UpdateAsync(Category category);
        Task<Product> DeleteAsync(int id);
    }
}