using FluentValidation;
using SAQAYA.UserAPI.Models;
using SAQAYA.UserAPI.Services;
using ViewModels;

namespace SAQAYA.UserAPI.Web
{
    public  class MinimalApiEndpoints
    {
        #region RegisterUser
        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="app" type="WebApplication"></param>
        /// <returns name="result" type="ResultViewModel<ResponseModel>"></returns>
        public static ResultViewModel<ResponseModel> RegisterUser(WebApplication app)
        {
            ResultViewModel<ResponseModel> result = new();
            app.MapPost("/MinimalUserApi/RegisterUser", (IValidator<UserModel> validator, UserModel model, UserService userService) =>
            {
                try
                {
                    var validationResult = validator.Validate(model);
                    if (!validationResult.IsValid)
                    {
                        result.Message = "Invalid Model State";
                    }
                    else
                    {
                        ResponseModel response = userService.Post(model);
                        result.Successed = true;
                        result.Message = "User has been added successfully";
                        result.Data = response;
                    }
                }
                catch (Exception ex)
                {
                    result.Successed = false;
                    result.Message = "Something wrong has happened";
                }

                return result;

            }).WithOpenApi();

            return result;

        }
        #endregion

        #region GetUser
        /// <summary>
        /// Get user with Id
        /// </summary>
        /// <param name="app" type="WebApplication"></param>
        /// <returns name="result" type="ResultViewModel<object>"></returns>
        public static ResultViewModel<object> GetUser(WebApplication app)
        {
            ResultViewModel<object> result = new();
            app.MapGet("/MinimalUserApi/GetUser/{id}", (string id, UserService userService) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        result.Successed = false;
                        result.Message = "Id is not valid";
                        return result;
                    }

                    var user = userService.Get(id);

                    if (user is null)
                    {
                        result.Successed = false;
                        return result;
                    }

                    result.Successed = true;
                    result.Data = user;
                }
                catch (Exception ex)
                {
                    result.Successed = false;
                    result.Message = "Something wrong has happened";
                }

                return result;

            }).WithOpenApi();

            return result;

        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// Update exist user
        /// </summary>
        /// <param name="app" type="WebApplication"></param>
        /// <returns name="result" type="ResultViewModel<ResponseModel>"></returns>
        public static ResultViewModel<ResponseModel> UpdateUser(WebApplication app)
        {
            ResultViewModel<ResponseModel> result = new();
            app.MapPost("/MinimalUserApi/UpdateUser", (IValidator<UserModel> validator, UserModel model, UserService userService) =>
            {
                try
                {
                    var validationResult = validator.Validate(model);
                    if (!validationResult.IsValid)
                    {
                        result.Message = "Invalid Model State";
                    }
                    else
                    {
                        ResponseModel response = userService.Put(model);
                        result.Successed = true;
                        result.Message = "User has been updated successfully";
                        result.Data = response;
                    }
                }
                catch (Exception ex)
                {
                    result.Successed = false;
                    result.Message = "Something wrong has happened";
                }

                return result;

            }).WithOpenApi();

            return result;

        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Delete exist user
        /// </summary>
        /// <param name="app" type="WebApplication"></param>
        /// <returns name="result" type="ResultViewModel<ResponseModel>"></returns>
        public static ResultViewModel<ResponseModel> DeleteUser(WebApplication app)
        {
            ResultViewModel<ResponseModel> result = new();
            app.MapPost("/MinimalUserApi/DeleteUser/{id}", (string id, UserService userService) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        result.Successed = false;
                        result.Message = "Id is not valid";
                        return result;
                    }
                    userService.Delete(id);
                    result.Successed = true;
                    result.Message = "User has been deleted successfully";
                }
                catch (Exception ex)
                {
                    result.Successed = false;
                    result.Message = "Something wrong has happened";
                }

                return result;

            }).WithOpenApi();

            return result;
            
        }
        #endregion
    }
}
