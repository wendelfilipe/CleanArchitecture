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
        public void CreateProduct_WithValidParamters_ResultObjectvalidState()
        {
            Action action = () => new Product(1, "Product Name", "Product description", 9.99m, 99, "Product image" );
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "Product Name", "Product description", 9.99m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }
        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Pr", "Product description", 9.99m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }
        [Fact]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Pruduct Name", "Prod", 99.9m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 5 characters");
        }
        [Fact]
        public void CreateProduct_MissingNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "", "Product description", 9.99m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
        }
        [Fact]
        public void CreateProduct_WithNullNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(1, null , "Product description", 9.99m, 99, "Product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
        }
        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name" , "Product description", 9.99m, 99, "Product image toooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, max 150 characters");
        }
        [Fact]
        public void CreateProduct_WithNullImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name" , "Product description", 9.99m, 99, null );
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateProduct_WithImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name" , "Product description", 9.99m, 99, null );
            action.Should()
                .NotThrow<NullReferenceException>();
        }
        [Fact]
        public void CreateProduct_WithEmptyImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name" , "Product description", 9.99m, 99, "");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        [Fact]
        public void CreateProduct_NegativePrice_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name" , "Product description", -99.9m, 99, "product image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }
        [Theory]
        [InlineData(-5)]
        public void CreateProduct_NegativeStock_DomainExceptionValidation(int value)
        {
            Action action = () => new Product(1, "Product Name" , "Product description", 99.9m, value, "product image" );
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }
    }
}