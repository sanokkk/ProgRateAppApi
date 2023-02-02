using Antoher.DAL.Interfaces;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class RequestRepo: IRequestRepo
    {
        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
        public RequestRepo(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task AddRequestAsync(Request entity)
        {
            await _db.requests.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRequestAsync(int id)
        {
            var request = await GetByIdAsync(id);
            _db.requests.Remove(request);
            await _db.SaveChangesAsync();
        }

        public async Task<Request> GetByIdAsync(int id)
        {
            var request = await _db.requests.FirstOrDefaultAsync(x => x.request_id == id);
            return request;
        }

        public async Task<List<GetUserDto>> GetIssuersAsync(List<Request> requests)
        {
            var issuers = new List<GetUserDto>();
            foreach(var request in requests)
            {
                var issuerId = request.issuer_id;
                var issuer = await _userManager.FindByIdAsync(issuerId);
                var user = new GetUserDto()
                {
                    userId = issuer.Id,
                    UserName = issuer.UserName,
                    FullName = issuer.FullName,
                    Email = issuer.Email
                };
                issuers.Add(user);
            }
            return issuers;
        }

        public async Task<List<Request>> GetRequestsAsync(string userId)
        {
            var requests = await _db.requests.Where(x => x.target_id == userId).ToListAsync();
            return requests;
        }

        public Task<bool> IsIssuerAsync(string issuerId, Request request)
        {
            bool checker = (request.issuer_id == issuerId) ? true : false;
            return Task.FromResult(checker);
        }

        public async Task<bool> IsRequestedAsync(string firstUserId, string secondUserId)
        {
            var requests = await _db.requests.Where(x => x.issuer_id == firstUserId && x.target_id == secondUserId ||
            x.target_id == firstUserId && x.issuer_id == secondUserId).ToListAsync();
            if (requests != null)
                return true;
            else
                return false;
        }
    }
}
