using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsDTOAsync();
        // Task<ProductDTO> GetByIdDTOAsync(int? id);
        // Task CreateDTOAsync(ProductDTO productDTO);
        // Task UpdateDTOAsync(ProductDTO productDTO);
        // Task DeleteDTOAsync(int? id);
    }
}