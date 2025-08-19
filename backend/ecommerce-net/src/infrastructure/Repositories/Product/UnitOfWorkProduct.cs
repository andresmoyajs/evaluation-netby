using System.Collections;
using application.Persistence;
using infrastructure.Persistence;

namespace infrastructure.Repositories.Product;

public class UnitOfWorkProduct: IUnitOfWork
{
    private Hashtable? repositores;
    private readonly ProductDbContext context;

    public UnitOfWorkProduct( ProductDbContext context)
    {
        this.context = context;
    }
    public async Task<int> Complete()
    {
        try
        {
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw new Exception("Error en transacción", e);
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (repositores is null)
        {
            repositores = new Hashtable();
        }

        var type = typeof(TEntity).Name;

        if (!repositores.Contains(type))
        {
            var repositoryType = typeof(RepositoryBaseProduct<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);
            repositores.Add(type, repositoryInstance);
        }

        return (IAsyncRepository<TEntity>)repositores[type]!;
    }
}