using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class PetSpecification : BaseSpecification<Pet>
{
    public PetSpecification(string? type, char? gender, string? sort) : base(x =>
    (string.IsNullOrEmpty(type) || x.Type == type) &&
    (!gender.HasValue || x.Gender == gender)
    )
    {
        switch (sort)
        {
            case "ByAge":
                AddOrderBy(x => x.Age);
                break;
            case "ByAgeDesc":
                AddOrderByDescending(x => x.Age);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
