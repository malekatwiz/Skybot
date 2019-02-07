using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skybot.UI.Models;
using Skybot.UI.Services;

namespace Skybot.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
                if (await _accountService.HasAccount(model.PhoneNumber))
                {
                    await _accountService.SendAccessCode(model.PhoneNumber);
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
                if (await _accountService.ValidateAccessCode(model))
                {
                    // add cookie.
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
                var isCreated = await _accountService.Create(model);
                if (isCreated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("Create", model);
        }
    }
}