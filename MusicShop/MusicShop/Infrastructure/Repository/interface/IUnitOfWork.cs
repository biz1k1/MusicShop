namespace MusicShop.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get;  }
        IProductRepository Product { get;  }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
