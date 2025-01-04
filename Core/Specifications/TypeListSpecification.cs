using System;
using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecification : BaseSpecification<Pet, string>
{
    public TypeListSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
