using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entites
{
    //sealed -> não pode ser herdada
    public sealed class Product : Entity
    {
        // private set -> não pode ser alterado
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name) || name.Length < 3 , "Invalid name. Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid name. Name is required");
            DomainExceptionValidation.When(description.Length < 5, "Invalid name, too short, minimum 5 characters");
            
            DomainExceptionValidation.When(price < 0, "Invalid price value");
            
            DomainExceptionValidation.When(stock < 0, "Invalid stock value");
            
            DomainExceptionValidation.When(image.Length > 250,
                "Invalid image name, too long, max 250 characters");
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}