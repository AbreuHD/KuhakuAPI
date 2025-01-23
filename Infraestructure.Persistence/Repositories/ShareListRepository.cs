using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.UserThings;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class ShareListRepository(KhakuContext dbContext) : GenericRepository<ShareList>(dbContext), IShareListRepository
    {
        private readonly KhakuContext _dbContext = dbContext;

        public async Task<List<ShareList>> GetAllByUserId(string userId, bool isUserLoged)
        {
            if (isUserLoged) { return await _dbContext.Set<ShareList>().Where(x => x.UserID == userId).ToListAsync(); }
            return await _dbContext.Set<ShareList>().Where(x => x.UserID == userId && x.IsPublic).ToListAsync();
        }

        public override async Task UpdateAsync(ShareList shareList, int id)
        {
            ShareList request = await _dbContext.Set<ShareList>().FindAsync(id) ?? throw new KeyNotFoundException("ShareList not found");
            if(request.UserID != shareList.UserID)
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

        public async Task<List<ShareList>> SearchShareList(string? Name)
        {
            var responseList = new List<ShareList>();
            if (!string.IsNullOrEmpty(Name))
            {
                var searchKeywords = Name.ToLower().Split(' ');
                foreach (var keyword in searchKeywords)
                {
                    var listMatchingKeyword = await _dbContext.Set<ShareList>()
                        .Where(x => x.Name.ToLower().Contains(keyword) && x.IsPublic)
                        .ToListAsync();
                    responseList.AddRange(listMatchingKeyword);
                }
                responseList = responseList.Distinct().ToList();
            }
            else
            {
                responseList = await _dbContext.Set<ShareList>().Where(x => x.IsPublic).ToListAsync();
            }

            return responseList;
        }
    }
}
