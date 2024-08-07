using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entites
{
    //sealed -> não pode ser herdada
    public sealed class Category : Entity
    {
        // private set -> não pode ser alterado
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
				 "Invalid name, Name is required");
			DomainExceptionValidation.When(name.Length < 3,
				 "Invalid name, too short, minimum 3 caracters");
                 
            Name = name;
        }

        public ICollection<Product> Products { get; private set; }
    }
}