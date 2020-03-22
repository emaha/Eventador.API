using Eventador.Domain;
using System;

namespace Eventador.Services.Contract.Api
{
    public class UserResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public static UserResponseModel Create(User user)
        {
            return new UserResponseModel
            {
                Id = user.Id,
                Name = user.Firstname
            };
        }
    }
}
