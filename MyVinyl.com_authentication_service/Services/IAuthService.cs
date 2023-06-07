
using MyVinyl.com_authentication_service.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyVinyl.com_authentication_service.Services
{
    public interface IAuthService
    {
        Task<UserResponse> AddAsync(UserRequest request);
        //Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        //Task<UserResponse> GetByNameAsync(string name);
        //Task<UserResponse> UpdateAsync(Guid id, UserRequest request);
        //Task<UserResponse> DeleteAsync(Guid id);
    }
}
