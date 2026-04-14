namespace fgv.ordenacao.livros.infrastructure.Configuration;

public sealed class OrderSettings
{
    public const string SectionName = "BookOrdering";
    public List<OrderCriterionSettings> Criteria { get; set; } = [];
}
