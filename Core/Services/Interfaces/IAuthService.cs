using ECommerce.Entities.DtoModels.Create;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(CreateUser model);

        Task<AuthModel> GetTokenAsync(LoginUser model);
    }
}
