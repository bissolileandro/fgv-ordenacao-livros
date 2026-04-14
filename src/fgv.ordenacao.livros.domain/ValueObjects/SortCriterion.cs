using fgv.ordenacao.livros.domain.Enums;

namespace fgv.ordenacao.livros.domain.ValueObjects;

public sealed record SortCriterion(SortField Field, SortDirection Direction);
