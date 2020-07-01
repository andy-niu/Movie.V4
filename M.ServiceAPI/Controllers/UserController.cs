using M.Models;
using M.Models.ViewModels;
using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace M.ServiceAPI.Controllers
{
    [ApiVersion("1.0"), ApiController, Consumes("application/json"), Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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
        public async Task<IActionResult> GetList(User model, int page = 1, int pageSize = 10)
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
        public async Task<IActionResult> Save([FromBody]User model)
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
        public async Task<IActionResult> Edit([FromBody]User model)
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
        public async Task<IActionResult> Destory([FromBody]User model)
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
        public async Task<IActionResult> Destory(Guid id)
        {
            try
            {
                var result = await _service.Delete(model => model.UserId == id);
                var returnReult = new ApiResult(ApiResultCode.Success, "Sccuessfully.", "操作成功", result);
                return Ok(returnReult);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResult { code = ApiResultCode.SystemError, msg = "fail," + ex.Message, msgcn = "系统异常" });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateRequest model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var response = await _service.Authenticate(model, ipAddress());

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                setTokenCookie(response.RefreshToken);

                return Ok(response);
            }
            else
            {
                return Ok("");
            }
            
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _service.RefreshToken(refreshToken, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response =await _service.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        [HttpGet("{id}/refresh-tokens")]
        public async Task<IActionResult> GetRefreshTokens(Guid id)
        {
            var user =await _service.GetEntity(x => x.UserId == id);
            return Ok(user.RefreshTokens);
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
} 
