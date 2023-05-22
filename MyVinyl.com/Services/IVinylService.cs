using MyVinyl.com.Database.Datamodels;
using MyVinyl.com.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVinyl.com.Services
{
    public interface IVinylService
    {
        Task<VinylResponse> AddAsync(VinylRequest request);
        Task<List<VinylResponse>> GetAllAsync();
        Task<VinylResponse> GetByIdAsync(Guid id);
        Task<VinylResponse> GetByNameAsync(string name);
        Task<VinylResponse> UpdateAsync(Guid id, VinylRequest request);
        Task<VinylResponse> DeleteAsync(Guid id);
    }
}
