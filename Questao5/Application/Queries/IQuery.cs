namespace Questao5.Application.Queries
{
    public interface IQuery<TRequest, TResponse>
    {
        Task<TResponse> Get(TRequest request);
    }
}
