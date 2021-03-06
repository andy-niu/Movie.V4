using M.Models;
using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace M.ServiceAPI.Controllers
{
    [ApiVersion("1.0"), ApiController, Consumes("application/json"), Route("api/v{version:apiVersion}/[controller]")]
    public class MovieAttributesController : ControllerBase
    {
        private IMovieAttributesService _service;
        public MovieAttributesController(IMovieAttributesService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieAttributes>> Get(int id)
        {
            try
            {
                var result = await _service.GetEntity((model) => true);
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpPost("list/{page}/{pageSize}")]
        public async Task<IActionResult> GetList(MovieAttributes model, int page = 1, int pageSize = 10)
        {
            try
            {
                var result = await _service.GetEntitiesForPaging(page, pageSize, (model => true));
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody]MovieAttributes model)
        {
            try
            {
                var result = await _service.Add(model);
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]MovieAttributes model)
        {
            try
            {
                var result = await _service.Update(model);
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpDelete("destory")]
        public async Task<IActionResult> Destory([FromBody]MovieAttributes model)
        {
            try
            {
                var result = await _service.Delete(model);
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
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
                var result = await _service.Delete(model => model.AttributesId == id);
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [HttpDelete("del")]
        public IActionResult Destorys()
        {
            Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary stateDictionary = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            stateDictionary.AddModelError("value", "null");
            return new BadRequestObjectResult(stateDictionary);
        }
    }
} 
