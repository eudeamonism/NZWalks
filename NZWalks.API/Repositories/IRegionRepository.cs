using Microsoft.EntityFrameworkCore.Update.Internal;
using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //This task has List because it returns a list of Regions
        Task<List<Region>> GetAllAsync();

        //This Task is only Region but has a questionmark because what if the Id cannot be found
        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region region);

       Task<Region?> DeleteAsync(Guid id);
    }
}
