﻿using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;

namespace MyLedgerApp.Services
{
    public interface IUserService
    {
        UserDTO AddUser(UserRequest request);
        void DeleteUser(Guid id);
        UserDTO GetUserById(Guid id);
        IEnumerable<UserDTO> GetUsers(UserType? type);
        UserDTO UpdateUser(Guid id, UserDTO user);
    }
}
