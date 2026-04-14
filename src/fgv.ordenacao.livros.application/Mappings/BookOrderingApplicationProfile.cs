using AutoMapper;
using fgv.ordenacao.livros.application.Contracts.Requests;
using fgv.ordenacao.livros.application.Contracts.Responses;
using fgv.ordenacao.livros.domain.Entities;
using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.application.Mappings;

public sealed class BookOrderingApplicationProfile : Profile
{
    public BookOrderingApplicationProfile()
    {
        CreateMap<BookRequest, Book>();
        CreateMap<Book, BookResponse>();
        CreateMap<SortCriterionRequest, SortCriterion>()
            .ConstructUsing(source => new SortCriterion(
                Common.SortCriterionMapper.ParseField(source.Field, "Campo de ordenação inválido"),
                Common.SortCriterionMapper.ParseDirection(source.Direction, "Direção de ordenação inválida")));
        CreateMap<SortCriterion, string>().ConvertUsing(criterion => Common.SortCriterionMapper.Format(criterion));
    }
}
