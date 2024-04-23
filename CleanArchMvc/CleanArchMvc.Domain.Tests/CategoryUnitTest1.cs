using Xunit;
using CleanArchMvc.Domain.Entites;
using FluentAssertions;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category With Valid State")]
    public void CreateCategory_WithValidParamters_ResultObjectvalidState()
    {
        Action action = () => new Category(1, "Category Name" );
        action.Should().NotThrow<DomainExceptionValidation>();
    }
    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionValidation()
    {
        Action action = () => new Category(-1, "Category Name" );
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }
    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionValidation()
    {
        Action action = () => new Category(1, "Ca" );
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 caracters");
    }
    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionValidation()
    {
        Action action = () => new Category(1, "" );
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, Name is required");
    }
    [Fact]
    public void CreateCategory_WithNullNameValue_DomainExceptionValidation()
    {
        Action action = () => new Category(1, null );
        action.Should()
            .Throw<DomainExceptionValidation>();
    }
}