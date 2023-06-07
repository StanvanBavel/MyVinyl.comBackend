
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using MyVinyl.com_authentication_service.Database.Contexts;
using MyVinyl.com_authentication_service.Database.Converters;
using MyVinyl.com_authentication_service.Database.Datamodels;
using MyVinyl.com_authentication_service.Database.Datamodels.Dtos;
using MyVinyl.com_authentication_service.Helpers;
using MyVinyl.com_authentication_service.Services;


namespace MyVinyl.com_authentication_service.com.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly UserContext _context;
        private readonly IDtoConverter<User, UserRequest, UserResponse> _converter;

        public AuthenticationService(UserContext context, IDtoConverter<User, UserRequest, UserResponse> converter)
        {
            _context = context;
            _converter = converter;
        }


        public async Task<UserResponse> AddAsync(UserRequest request)
        {
            User user = _converter.DtoToModel(request);

            if (await IsDuplicateAsync(user))
            {
                throw new DuplicateException("This User already exists.");
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(user);
        }


        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);

            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            return await CreateResponseAsync(user);
        }


        private async Task<bool> IsDuplicateAsync(User User, Guid? id=null)
        {
            if (id == null)
            {
                return await _context.Users.AnyAsync(
                    e => e.Name == User.Name
                    && e.Email == User.Email
                    && e.Phonenumber == User.Phonenumber
                    && e.IsActive);
            }
            else
            {
                return await _context.Users.AnyAsync(
                    e => e.Name == User.Name
                    && e.Email == User.Email
                    && e.Phonenumber == User.Phonenumber
                    && e.IsActive && e.Id != id);
            }
        }

        private async Task<UserResponse> CreateResponseAsync(User user)
        {
            UserResponse response = _converter.ModelToDto(user);
        

            return response;
        }
    }
}
