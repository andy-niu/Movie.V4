using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M.Model;
using M.Repository.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M.ServiceAPI.Controllers
{
    [ApiVersion("1.0"), ApiController, Consumes("application/json"), Route("api/v{version:apiVersion}/[controller]")]
    public class MovieBaseController : ControllerBase
    {
        private Service.Interfaces.IBaseService<MovieBase> _baseService;
        public MovieBaseController(Service.Interfaces.IBaseService<MovieBase> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _baseService.GetEntity((model) => model.MovieId == id);
                var returnReult = new ApiResult<MovieBase>(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return new JsonResult(returnReult, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpPost("list/{page}/{pageSize}")]
        public async Task<IActionResult> GetList(MovieBase model, int page,int pageSize=10)
        {
            try
            {
                var result = await _baseService.GetEntitiesForPaging(page, pageSize, (model => true));
                var returnReult = new ApiResult<IEnumerable<MovieBase>>(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return new JsonResult(returnReult, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody]MovieBase model)
        {
            try
            {
                var result = await _baseService.Add(model);
                var returnReult = new ApiResult<bool>(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return new JsonResult(returnReult, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]MovieBase model)
        {
            try
            {
                var result = await _baseService.Update(model);
                var returnReult = new ApiResult<bool>(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return new JsonResult(returnReult, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpDelete("destory")]
        public async Task<IActionResult> Destory([FromBody]MovieBase model)
        {
            try
            {
                var result = await _baseService.Delete(model);
                var returnReult = new ApiResult<bool>(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return new JsonResult(returnReult, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpDelete("destory/{id}")]
        public async Task<IActionResult> Destory(int id)
        {
            try
            {
                var result = await _baseService.Delete(model => model.MovieId == id);
                var returnReult = new ApiResult<bool>(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return new JsonResult(returnReult, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

    }
}