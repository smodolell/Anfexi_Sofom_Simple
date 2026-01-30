namespace Anfx.Profuturo.Sofom.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{

    Task<int> SaveAsync(CancellationToken cancellationToken = default);


    Task BeginTransactionAsync(CancellationToken cancellationToken = default);


    Task CommitTransactionAsync(CancellationToken cancellationToken = default);


    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);



}

