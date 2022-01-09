using api.DTO;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services.Interfaces
{
    public interface IBantersService
    {
        Task<ICollection<BanterDTO>> GetAllBanters();
        Task<BanterDTO> GetBanter(int Id);
        Task AddBanter(AddBanterDTO banter, string userId);
        Task<BanterDTO> GetRandomBanter();
        Task<BanterDTO> GetBestBanter();
        Task SetScore(int score, int id);
    }
}
