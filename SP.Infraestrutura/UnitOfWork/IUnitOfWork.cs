using Microsoft.EntityFrameworkCore.Storage;

namespace SP.Infraestrutura.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Salva todas as mudanças pendentes no contexto.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Inicia uma nova transação.
        /// </summary>
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirma a transação atual.
        /// </summary>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Desfaz a transação atual.
        /// </summary>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Executa uma operação dentro de uma transação.
        /// Se a operação for bem-sucedida, a transação é confirmada.
        /// Se houver erro, a transação é desfeita.
        /// </summary>
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executa uma operação dentro de uma transação.
        /// Se a operação for bem-sucedida, a transação é confirmada.
        /// Se houver erro, a transação é desfeita.
        /// </summary>
        Task ExecuteInTransactionAsync(Func<Task> operation, CancellationToken cancellationToken = default);
    }
}
