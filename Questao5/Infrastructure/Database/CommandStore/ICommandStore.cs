using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public interface ICommandStore<T>
    {
        Task Add(T item);
    }
}
