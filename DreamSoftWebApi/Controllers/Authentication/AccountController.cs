using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftLogic.Services.Email;
using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Models.Email;
using Microsoft.AspNetCore.Mvc;

namespace DreamSoftWebApi.Controllers.Authentication;

[ApiController]
[Route("dreamsoftapi/[controller]")]
public class AccountController(IAccountServices services, IEmailService emailService,
    IVerificationCodeManager verificationCodeManager) : ControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<Account>> CreateAccount([FromBody] AccountCreate account)
    {
        var result = await services.CreateNewAccount(account);
        return result;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<SendVerificationCodeResponse>> SendVerificationCode(
        [FromBody] SendVerificationCodeRequest request)
    {
        var emailexist = await services.CheckEmailExistence(request.Email);
        if(emailexist)
        {
            return new SendVerificationCodeResponse
            {
                Success = false,
                Message = "Email already in use"
            };
        }
        var code = verificationCodeManager.GenerateCode(request.Email);
        var emailSent = await emailService.SendVerificationCodeAsync(request.Email, code);
        return new SendVerificationCodeResponse
        {
            Success = emailSent,
            Message = emailSent ? "Verification code sent successfully" : "Failed to send verification code"
        };
    }

    [HttpPost("[action]")]
    public ActionResult<VerifyEmailCodeResponse> VerifyEmailCode(
        [FromBody] VerifyEmailCodeRequest request)
    {
        var isValid = verificationCodeManager.ValidateCode(request.Email, request.Code);

        if (isValid)
        {
            verificationCodeManager.RemoveCode(request.Email);

            return Ok(new VerifyEmailCodeResponse
            {
                Success = true,
                Verified = true,
                Message = "Email verified successfully"
            });
        }
        else
        {
            return Ok(new VerifyEmailCodeResponse
            {
                Success = true,
                Verified = false,
                Message = "Invalid or expired verification code"
            });
        }
    }
}
