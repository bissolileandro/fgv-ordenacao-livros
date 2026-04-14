namespace fgv.ordenacao.livros.domain.Exceptions;

public sealed class OrdenacaoException : Exception
{
    public OrdenacaoException(string message) : base(message)
    {
    }
}
