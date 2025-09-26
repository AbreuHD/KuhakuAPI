using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ShareListRepository(KhakuContext dbContext) : GenericRepository<ShareList>(dbContext), IShareListRepository
    {
        private readonly KhakuContext _dbContext = dbContext;

        public async Task<List<ShareList>> GetAllByUserId(string userId, bool isUserLoged)
        {
            if (isUserLoged) { return await _dbContext.Set<ShareList>().Where(x => x.UserID == userId).ToListAsync(); }
            return await _dbContext.Set<ShareList>().Where(x => x.UserID == userId && x.IsPublic).ToListAsync();
        }

        public override async Task UpdateAsync(ShareList shareList, int ID)
        {
            ShareList request = await _dbContext.Set<ShareList>().FindAsync(ID) ?? throw new KeyNotFoundException("ShareList not found");
            if (request.UserID != shareList.UserID)
            {
                throw new UnauthorizedAccessException("You are not allowed to edit this ShareList");
            }
            _dbContext.Entry(request).CurrentValues.SetValues(shareList);
            await _dbContext.SaveChangesAsync();
        }

        public override async Task DeleteAsync(ShareList shareList)
        {
            ShareList request = await _dbContext.Set<ShareList>().FindAsync(shareList.ID) ?? throw new KeyNotFoundException("ShareList not found");
            if (request.UserID != shareList.UserID)
            {
                throw new UnauthorizedAccessException("You are not allowed to delete this ShareList");
            }
            _dbContext.Set<ShareList>().Remove(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ShareList>> SearchShareList(string? name, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            IQueryable<ShareList> query = _dbContext.Set<ShareList>()
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.Created);

            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchKeywords = name
                    .ToLowerInvariant()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var keyword in searchKeywords)
                {
                    var k = keyword;
                    query = query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{k}%"));
                }
            }

            return await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<ShareList> GetByIdWithMoviesAsync(int id)
        {
            return await _dbContext.Set<ShareList>()
                .Include(sl => sl.MovieListMovie)
                    .ThenInclude(mlm => mlm.Movie)
                .AsNoTracking()
                .FirstOrDefaultAsync(sl => sl.ID == id);
        }
    }
}
