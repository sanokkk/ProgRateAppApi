using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IRequestRepo
    {
        Task AddRequestAsync(Request entity);
        Task<List<Request>> GetRequestsAsync(string userId);
        Task<List<GetUserDto>> GetIssuersAsync(List<Request> requests);
        Task DeleteRequestAsync(int id);
        Task<Request> GetByIdAsync(int id);
        public Task<bool> IsRequestedAsync(string firstUserId, string secondUserId);
    }
}
