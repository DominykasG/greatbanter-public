using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories.Interfaces
{
    public interface ITestsRepository
    {
        Task<ICollection<Banter>> GetTests();
        Task<Banter> GetTest(int Id);
    }
}
