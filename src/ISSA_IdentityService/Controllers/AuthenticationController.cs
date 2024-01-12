using ISSA_IdentityService.Contract.Service.Interface;
using ISSA_IdentityService.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ISSA_IdentityService.Controllers
{
    [ApiController]
    public class AuthenticationController(IAuthenticateService authenticateService) : ApiControllerBase
    {

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = new AuthenticateModel();
                model.IdToken = request.IdToken;
                model.RefreshToken = request.RefreshToken;
                model.IsAnonymous = request.IsAnonymous;
                //model.SessionId = HttpContext.Session.Id;  
                model.IPAdress = HttpContext.Connection.RemoteIpAddress?.ToString();
                //HttpContext.Session.SetString("SessionId", "Registerd device");
                HttpContext.Request.Headers.TryGetValue("Sec-CH-UA", out StringValues browser);
                HttpContext.Request.Headers.TryGetValue("Sec-CH-UA-Platform", out StringValues os);
                //model.Browser = browser.ToString().Replace("\"" , "");
                model.Browser = browser.ToString();
                model.OS = os.ToString().Replace("\"", "");
                var response = await authenticateService.AuthenticateAsync(model, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] AuthenticationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var model = new AuthenticateModel();
                model.IdToken = request.IdToken;
                model.RefreshToken = request.RefreshToken;
                model.IsAnonymous = request.IsAnonymous;
                //model.SessionId = HttpContext.Session.Id;  
                model.IPAdress = HttpContext.Connection.RemoteIpAddress?.ToString();
                //HttpContext.Session.SetString("SessionId", "Registerd device");
                HttpContext.Request.Headers.TryGetValue("Sec-CH-UA", out StringValues browser);
                HttpContext.Request.Headers.TryGetValue("Sec-CH-UA-Platform", out StringValues os);
                //model.Browser = browser.ToString().Replace("\"" , "");
                model.Browser = browser.ToString();
                model.OS = os.ToString().Replace("\"", "");
                var response = await authenticateService.RefreshTokenAsync(model, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOutAsync([FromBody] string refreshToken, CancellationToken cancellationToken = default)
        {
            try
            {
                await authenticateService.SignOutAsync(refreshToken, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-out-all")]
        public async Task<IActionResult> SignOutAllAsync([FromBody] string refreshToken, CancellationToken cancellationToken = default)
        {
            try
            {
                await authenticateService.SignOutAllAsync(refreshToken, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
