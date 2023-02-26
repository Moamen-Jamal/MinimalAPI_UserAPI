using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Repositories;
using SAQAYA.UserAPI.Entities.Entities;
using SAQAYA.UserAPI.Models;
using ViewModels;

namespace SAQAYA.UserAPI.Services
{
    public class UserService
    {
        private Generic<User> userRepo;
        private UnitOfWork unitOfWork;
        private IMapper mapper;
        private IConfiguration config;

        public UserService(UnitOfWork _unitOfWork, IMapper _mapper, IConfiguration _config)
        {
            mapper = _mapper;
            config = _config;
            unitOfWork = _unitOfWork;
            userRepo = unitOfWork.UserRepo;
        }

        #region AddUser
        /// <summary>
        /// 1- Generate new Id using SHA1
        /// 2- mapping to User object
        /// 3- Add new User
        /// 4- Generate new AccessToken
        /// </summary>
        /// <param name="editModel" type="UserModel"></param>
        /// <returns name="response" type="ResponseModel"></returns>
        public ResponseModel Post(UserModel editModel)
        {
            editModel.Id = SecurityHelpers.GenerateId(editModel.Email, config.GetSection("Security")["SecretId"]);
            EntityEntry<User> user = userRepo.Post(mapper.Map<User>(editModel));
            unitOfWork.Commit();
            ResponseModel response = new();
            response.Id = user.Entity.Id;
            response.AccessToken = SecurityHelpers.BuildToken(config.GetSection("Security")["TokenKey"], editModel);
            return response;
        }
        #endregion

        #region GetUser
        /// <summary>
        /// 1- Get user by Id
        /// 2- make mapping for user object
        /// </summary>
        /// <param name="id" type="string"></param>
        /// <returns name="userResponse" type="object?"></returns>
        public object? Get(string id)
        {
            User user = userRepo.Get(id);

            if(user is null)
            {
                return null;
            }

            object userResponse;

            if (!user.MarketingConsent)
                userResponse = mapper.Map<UserResponse>(user);
            else
                userResponse =  mapper.Map<UserDTO>(user);
            
            return userResponse;
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// 1- mapping to User object
        /// 2- Update the user
        /// 3- Generate new AccessToken
        /// </summary>
        /// <param name="editModel" type="UserModel"></param>
        /// <returns name="response" type="ResponseModel"></returns>
        public ResponseModel Put(UserModel editModel)
        {
            User user = userRepo.Put(mapper.Map<User>(editModel));
            unitOfWork.Commit();
            ResponseModel response = new();
            response.Id = user.Id;
            response.AccessToken = SecurityHelpers.BuildToken(config.GetSection("Security")["TokenKey"], editModel);
            return response;
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Delete exist user by Id
        /// </summary>
        /// <param name="id" type="string"></param>
        public void Delete(string id)
        {
            userRepo.Delete(new User { Id = id });
            unitOfWork.Commit();
        }
        #endregion
    }
}
