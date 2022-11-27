using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
    public interface IUserRepository
    {
        ResponseBase Insert(EntityUser user);
        ResponseBase Login(EntityLogin login);
    }
}
