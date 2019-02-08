using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skybot.UI.Models;
using Skybot.UI.Services;
using IAuthorizationService = Skybot.UI.Services.IAuthorizationService;

namespace Skybot.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAuthorizationService _authorizationService;
        
        public AccountController(IAccountService accountService, IAuthorizationService authorizationService)
        {
            _accountService = accountService;
            _authorizationService = authorizationService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Check(UserAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountService.HasAccountAsync(model.PhoneNumber))
                {
                    await _accountService.SendAccessCodeAsync(model.PhoneNumber);
                    return RedirectToAction("VerificationCode", new VerificationCodeModel{PhoneNumber = model.PhoneNumber});
                }

                return RedirectToAction("Create");
            }

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult VerificationCode(VerificationCodeModel model)
        {
            return View("VerificationCode", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> VerifyCode(VerificationCodeModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountService.ValidateAccessCodeAsync(model))
                {
                    // add cookie.
                    var userAccount = await _accountService.GetByPhoneNumberAsync(model.PhoneNumber);
                    await _authorizationService.UserSignInAsync(HttpContext, userAccount);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("code", "Invalid verification code");
            }
            return RedirectToAction("VerificationCode", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(UserAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await _accountService.CreateAsync(model);
                if (isCreated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("Create", model);
        }
    }
}