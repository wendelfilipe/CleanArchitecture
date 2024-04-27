using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesDTOAsync();
        Task<CategoryDTO> GetByIdDTOAsync(int? id);
        Task CreateDTOAsync(CategoryDTO categoryDTO);
        Task UpdateDTOAsync(CategoryDTO categoryDTO);
        Task DeleteDTOAsync(int? id);

    }
}