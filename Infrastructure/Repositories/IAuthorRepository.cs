using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindersManager.Models;

namespace AuthorsManager.Infrastructure.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();

        Task<Author> GetAsync(Guid id);

        Task<Author> AddAsync(Author item);

        Task<Author> FindAsync(string subject);

        Task<Author> RemoveAsync(Guid id);

        Task<Author> UpdateAsync(Author item);
    }
}
