using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entites;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The description is required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The price is required")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("price")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The Stock is Required")]
        [Range(1, 9999)]
        [DisplayName("Stock")]
        public int Stock { get; set; }
        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string? Image { get; set; }
        
        [JsonIgnore]
        public Category? Category { get; set; }

        [DisplayName("Categories")]
        public int CategoryId { get; set; }
    }
}