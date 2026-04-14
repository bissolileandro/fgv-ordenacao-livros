using AutoMapper;
using fgv.ordenacao.livros.application.Common;
using fgv.ordenacao.livros.domain.ValueObjects;
using fgv.ordenacao.livros.infrastructure.Configuration;

namespace fgv.ordenacao.livros.infrastructure.Mappings;

public sealed class BookOrderingInfrastructureProfile : Profile
{
    public BookOrderingInfrastructureProfile()
    {
        CreateMap<OrderCriterionSettings, SortCriterion>()
            .ConstructUsing(source => new SortCriterion(
                SortCriterionMapper.ParseField(source.Field, "Campo configurado inválido"),
                SortCriterionMapper.ParseDirection(source.Direction, "Direção configurada inválida")));
    }
}
