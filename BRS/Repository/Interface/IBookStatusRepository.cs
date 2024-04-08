using BRS.Entities;
using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IBookStatusRepository
    {
        Task AddBookStatus(Books book);
        Task<bool> UpdateBookStatus(Guid BookId);

    }
}
