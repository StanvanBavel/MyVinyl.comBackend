using MyVinyl.com_authentication_service.Database.Datamodels;
using MyVinyl.com_authentication_service.Database.Datamodels.Dtos;
using System.Collections.Generic;

namespace MyVinyl.com_authentication_service.Database.Converters
{
    public class UserDtoConverter : IDtoConverter<User, UserRequest, UserResponse>
    {
        public User DtoToModel(UserRequest request)
        {
            return new User
            {
                Name = request.Name,
                Email= request.Email,
                Phonenumber= request.Phonenumber
            };
        }

        public UserResponse ModelToDto(User model)
        {
            return new UserResponse
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Phonenumber = model.Phonenumber
            };
        }

        public List<UserResponse> ModelToDto(List<User> models)
        {
            List<UserResponse> responseDtos = new List<UserResponse>();

            foreach (User user in models)
            {
                responseDtos.Add(ModelToDto(user));
            }

            return responseDtos;
        }
    }
}
