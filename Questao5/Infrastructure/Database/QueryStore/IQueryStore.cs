namespace Questao5.Infrastructure.Database.QueryStore
{
    public interface IQueryStore<T, TypeKey>
    {
        Task<T> Get(TypeKey id);
    }
}
