using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesDTOAsync()
        {
            //retorna objetos da entidade, não é um DTO
            var categoriesEntity = await categoryRepository.GetAllAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetByIdDTOAsync(int? id)
        {
            var categoriesEntity = await categoryRepository.GetByIdAsync(id);
            return mapper.Map<CategoryDTO>(categoriesEntity);
        }

        public async Task CreateDTOAsync(CategoryDTO categoryDTO)
        {
            
            var category = mapper.Map<Category>(categoryDTO);
            await categoryRepository.CreateAsync(category);
        }

        public async Task UpdateDTOAsync(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            await categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteDTOAsync(int? id)
        {
            var categoryEntity = categoryRepository.GetByIdAsync(id).Result;
            await categoryRepository.DeleteAsync(categoryEntity);
        }
    }
}