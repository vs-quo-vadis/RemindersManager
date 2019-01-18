using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorsManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using RemindersManager.DAL;
using RemindersManager.Models;


namespace RemindersManager.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ReminderContext _context;

        public AuthorRepository(ReminderContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAsync(Guid id)
        {
            return await _context.Authors.FirstAsync(x => x.Id == id);
        }

        public async Task<Author> AddAsync(Author item)
        {
            var newAuthor = await _context.Authors.AddAsync(item);
            await _context.SaveChangesAsync();

            return newAuthor.Entity;
        }

        public async Task<Author> UpdateAsync(Author item)
        {
            var updatedAuthor = _context.Authors.Update(item);
            await _context.SaveChangesAsync();

            return updatedAuthor.Entity;
        }

        public async Task<Author> FindAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
        }


        public async Task<Author> RemoveAsync(Guid id)
        {
            var authorToDelete = GetAsync(id);

            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete.Result);
                await _context.SaveChangesAsync();
            }

            return null;
        }       
    }
}
