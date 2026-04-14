using fgv.ordenacao.livros.domain.Enums;
using fgv.ordenacao.livros.domain.Exceptions;
using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.application.Common;

public static class SortCriterionMapper
{
    private static readonly IReadOnlyDictionary<string, SortField> FieldAliases =
        new Dictionary<string, SortField>(StringComparer.OrdinalIgnoreCase)
        {
            ["titulo"] = SortField.Title,
            ["title"] = SortField.Title,
            ["autor"] = SortField.Author,
            ["author"] = SortField.Author,
            ["edicao"] = SortField.EditionYear,
            ["edição"] = SortField.EditionYear,
            ["edition"] = SortField.EditionYear,
            ["editionyear"] = SortField.EditionYear
        };

    private static readonly IReadOnlyDictionary<string, SortDirection> DirectionAliases =
        new Dictionary<string, SortDirection>(StringComparer.OrdinalIgnoreCase)
        {
            ["asc"] = SortDirection.Ascending,
            ["ascending"] = SortDirection.Ascending,
            ["ascendente"] = SortDirection.Ascending,
            ["desc"] = SortDirection.Descending,
            ["descending"] = SortDirection.Descending,
            ["descendente"] = SortDirection.Descending
        };

    private static readonly IReadOnlyDictionary<SortField, string> FieldDisplayNames =
        new Dictionary<SortField, string>
        {
            [SortField.Title] = "Title",
            [SortField.Author] = "Author",
            [SortField.EditionYear] = "EditionYear"
        };

    private static readonly IReadOnlyDictionary<SortDirection, string> DirectionDisplayNames =
        new Dictionary<SortDirection, string>
        {
            [SortDirection.Ascending] = "Ascending",
            [SortDirection.Descending] = "Descending"
        };

    public static SortField ParseField(string value, string errorMessagePrefix)
    {
        if (Enum.TryParse<SortField>(value, true, out var parsedField))
        {
            return parsedField;
        }

        if (FieldAliases.TryGetValue(value.Trim(), out var field))
        {
            return field;
        }

        throw new OrdenacaoException($"{errorMessagePrefix}: {value}.");
    }

    public static SortDirection ParseDirection(string value, string errorMessagePrefix)
    {
        if (Enum.TryParse<SortDirection>(value, true, out var parsedDirection))
        {
            return parsedDirection;
        }

        if (DirectionAliases.TryGetValue(value.Trim(), out var direction))
        {
            return direction;
        }

        throw new OrdenacaoException($"{errorMessagePrefix}: {value}.");
    }

    public static string Format(SortCriterion criterion)
    {
        var field = FieldDisplayNames.TryGetValue(criterion.Field, out var fieldName)
            ? fieldName
            : criterion.Field.ToString();

        var direction = DirectionDisplayNames.TryGetValue(criterion.Direction, out var directionName)
            ? directionName
            : criterion.Direction.ToString();

        return $"{field}:{direction}";
    }
}
