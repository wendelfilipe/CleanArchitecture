using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entites;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParamters_ResultObjectvalidState()
        {
            Action action = () => new Product(1, "Product Name", "Product description", 99.9m, 99, "Product image" );
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "Product Name", "Product description", 99.9m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }
        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "Pr", "Product description", 99.9m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 caracters");
        }
        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "", "Product description", 99.9m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, Name is required");
        }
        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, null , "Product description", 99.9m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateCategory_LongImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "Product Name" , "Product description", 99.9m, 99, "Product image toooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong" );
            action.Should()
                .Throw<DomainExceptionValidation>().
                WithMessage("Invalid image name, too long, max 250 characters");
        }
        [Fact]
        public void CreateCategory_WithNullImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "ProductName" , "Product description", 99.9m, 99, null );
            action.Should()
                .Throw<DomainExceptionValidation>();
        }[Fact]
        public void CreateCategory_WithEmptyImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "ProductName" , "Product description", 99.9m, 99, "" );
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}