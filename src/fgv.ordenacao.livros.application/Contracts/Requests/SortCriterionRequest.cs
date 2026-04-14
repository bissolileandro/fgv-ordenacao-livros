namespace fgv.ordenacao.livros.application.Contracts.Requests;

public sealed class SortCriterionRequest
{
    public string Field { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
}
