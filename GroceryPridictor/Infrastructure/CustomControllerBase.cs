using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryPridictor.Infrastructure
{
    public class CustomControllerBase : ControllerBase
    {
        protected OkObjectResult DataUpdated()
        {
            var value = Array.Empty<object>();
            CustomResponseModel model = new CustomResponseModel
            {
                Data = value,
                Message = "Data has been Updated successfully",
                Status = true
            };
            return base.Ok(model);
        }

        protected OkObjectResult Error(string errorMessage)
        {
            var value = Array.Empty<object>();
            CustomResponseModel model = new CustomResponseModel
            {
                Data = value,
                Message = errorMessage,
                Status = false
            };
            return base.Ok(model);
        }

        protected OkObjectResult ExecutionFailed()
        {
            CustomResponseModel model = new CustomResponseModel
            {
                Data = Array.Empty<object>(),
                Message = "Execution un-Successfull",
                Status = false
            };
            return base.Ok(model);
        }

        protected OkObjectResult NoDataFound()
        {
            var value = Array.Empty<object>();
            CustomResponseModel model = new CustomResponseModel
            {
                Data = value,
                Message = "No Data Found",
                Status = true
            };
            return base.Ok(model);
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            CustomResponseModel model = new CustomResponseModel
            {
                Data = value,
                Message = "Data Found",
                Status = true
            };
            return base.Ok(model);
        }

        protected OkObjectResult Ok<T>(Func<T> value)
        {
            try
            {
                var model = value.Invoke();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        protected async Task<OkObjectResult> Ok<T>(Func<Task<T>> value)
        {
            try
            {
                var model = await value.Invoke();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        protected new OkObjectResult Ok()
        {
            CustomResponseModel model = new CustomResponseModel
            {
                Data = Array.Empty<object>(),
                Message = "Execution Successfull",
                Status = true
            };
            return base.Ok(model);
        }
        protected OkObjectResult Ok<T>([ActionResultObjectValue] IEnumerable<T> value)
        {
            var model = new CustomResponseModel<IEnumerable<T>>
            {
                Data = value,
                Message = "Data Found",
                Status = true
            };
            return base.Ok(model);
        }

        protected async Task<OkObjectResult> Ok<T>(Func<Task<IEnumerable<T>>> value)
        {
            try
            {
                var model = await value.Invoke();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}