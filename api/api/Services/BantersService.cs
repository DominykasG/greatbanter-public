using api.DTO;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class BantersService : IBantersService
    {
        private readonly BanterContext _dbContext;
        public BantersService(BanterContext context) {
            _dbContext = context;
        }

        public async Task AddBanter(AddBanterDTO addBanter, string userId)
        {
            _dbContext.Banters.Add(new Banter {
                Content = addBanter.Content,
                Score = 0,
                UserId = int.Parse(userId),
                DisplayUsername = bool.Parse(addBanter.DisplayUsername)
            });
            await _dbContext.SaveChangesAsync();
        }
        public async Task SetScore(int score, int id) 
        {
            _dbContext.Banters.FirstOrDefault(b => b.Id == id).Score += score;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ICollection<BanterDTO>> GetAllBanters()
        {
            var bantersInDb = await _dbContext.Banters.Include(e => e.User).ToArrayAsync();
            var banters = new List<BanterDTO>();
            for (int i = 0; i < bantersInDb.Count(); i++)
            {
                var banter = bantersInDb[i];
                if (banter.DisplayUsername == false)
                {

                    banters.Add(new BanterDTO(banter.Id, banter.Content, banter.Score, banter.UserId, "Anonymous"));
                }
                else {
                    banters.Add(new BanterDTO(banter.Id, banter.Content, banter.Score, banter.UserId, banter.User.UserName));
                }
            }
            return banters;
        }
        public async Task<BanterDTO> GetRandomBanter() 
        {
            var bantersInDb = await _dbContext.Banters.Include(e => e.User).ToArrayAsync();
            var rand = new Random();
            int num = rand.Next(bantersInDb.Length);
            var randomBanter = new BanterDTO();

            var banter = bantersInDb[num];
            if (banter.DisplayUsername == false)
            {

                randomBanter = new BanterDTO(banter.Id, banter.Content, banter.Score, banter.UserId, "Anonymous");
            }
            else
            {
                randomBanter = new BanterDTO(banter.Id, banter.Content, banter.Score, banter.UserId, banter.User.UserName);
            }

            return randomBanter;
        }
        public async Task<BanterDTO> GetBestBanter() 
        {
            var bantersInDb = await _dbContext.Banters.Include(e => e.User).ToListAsync();
            var rand = new Random();
            var randomBanter = new BanterDTO();
            bantersInDb.Sort((x, y) => x.Score.CompareTo(y.Score));
            bantersInDb = bantersInDb.OrderByDescending(b => b.Score).ToList();
            List<Banter> best = new List<Banter>(); 
            if (bantersInDb.Count > 10)
            {
                best = bantersInDb.GetRange(0, 10);
            }
            else
            {
                best = bantersInDb;
            }

            Banter banter = best[rand.Next(best.Count())];

            if (banter.DisplayUsername == false)
            {

                randomBanter = new BanterDTO(banter.Id, banter.Content, banter.Score, banter.UserId, "Anonymous");
            }
            else
            {
                randomBanter = new BanterDTO(banter.Id, banter.Content, banter.Score, banter.UserId, banter.User.UserName);
            }

            return randomBanter;
        }
        public async Task<BanterDTO> GetBanter(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
