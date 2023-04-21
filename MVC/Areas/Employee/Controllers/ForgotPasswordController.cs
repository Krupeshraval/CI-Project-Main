using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using System.Net;
using System.Net.Mail;


namespace CI_Project.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ForgotPasswordController : Controller
    {
        private readonly CIPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public ForgotPasswordController(CIPlatformContext db, IUserInterface Iuser)
        {
            _db = db; //underscore db ma baddho database store thai jase
            _Iuser = Iuser;
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _Iuser.UserByEmail(model.Email);
                if (user == null)
                {
                    ViewBag.emailerror = "Email not Exist";
                    return View();
                }

                // Generate a password reset token for the user
                var token = Guid.NewGuid().ToString();

                // Store the token in the password resets table with the user's email
                var passwordReset = new PasswordReset
                {
                    Email = model.Email,
                    Token = token
                };
                _db.PasswordResets.Add(passwordReset);
                _db.SaveChanges();

                // Send an email with the password reset link to the user's email address
                var resetLink = Url.Action("ResetPassword", "ForgotPassword", new { email = model.Email, token }, Request.Scheme);
                // Send email to user with reset password link
                // ...
                var fromAddress = new MailAddress("testermaster43@gmail.com", "CI Platform");
                var toAddress = new MailAddress(model.Email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("testermaster43@gmail.com", "rnonuvkukadwpnpx"),
                    EnableSsl = true
                };
                smtpClient.Send(message);
                TempData["Done"] = "Reset link has been sent to your registered Email-id";
                return RedirectToAction("ForgotPassword", "ForgotPassword");
            }



            return View();

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string email, string token)
        {
            var passwordReset = _Iuser.ResetPass(email, token);
            if (passwordReset == null)
            {
                return RedirectToAction("ResetPassword", "ForgotPassword");
            }
            // Pass the email and token to the view for resetting the password
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel rsp)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = _Iuser.UserByEmail(rsp.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPassword", "ForgotPassword");
                }

                // Find the password reset record by email and token
                var passwordReset = _Iuser.ResetPass(rsp.Email, rsp.Token);
                if (passwordReset == null)
                {
                    return RedirectToAction("ResetPassword", "ForgotPassword");
                }

                // Update the user's password
                user.Password = rsp.Password;
                _db.SaveChanges();

                // Remove the password reset record from the database
                _db.PasswordResets.Remove(passwordReset);
                _db.SaveChanges();

                TempData["Done"] = "Password Changed Succesfully";
                return RedirectToAction("Index", "User");
            }

            return View();
        }












        // GET: ForgetController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ForgetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForgetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForgetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
