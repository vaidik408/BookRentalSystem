using BRS.Model;

namespace BRS.Repository.Interface
{
    public interface IBookStatusRepository
    {
        Task AddBookStatus(BookStatusDto bookStatusDto);
        Task<bool> UpdateBookStatus(Guid BookId);
    }
}
