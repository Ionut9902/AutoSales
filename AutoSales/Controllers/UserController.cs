using AutoSales.Data;
using AutoSales.Models;
using AutoSales.Models.DBObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoSales.Models.Repository;
using System.Drawing.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using AutoSales.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace AutoSales.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private UserRepository _userRepository;
        public UserController(ApplicationDbContext dbContext)
        {
            _userRepository = new UserRepository(dbContext);
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details()
        {
            var model = new UserModel();
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                model = _userRepository.GetUserByEmail(email);
            }
            return View("Details", model);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {

            return View("Create");       
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

            try
            {
                var model = new UserModel();

                if (User.Identity.IsAuthenticated)
                {
                    
                    var email = User.Identity.Name;
                    model.EmailAddress = email;
                }
                model.FirstRegistered = DateTime.Now;
                model.NumberOfPosts = 0;
                var task = TryUpdateModelAsync(model);
                task.Wait();
             
                if (task.Result)
                {
                    _userRepository.InsertUser(model);
                }
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _userRepository.GetUserByID(id);
            return View("Edit", model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = _userRepository.GetUserByEmail(User.Identity.Name);
                    var model = new UserModel();
                    var task = TryUpdateModelAsync(model);
                    model.IdUser = id;
                    model.NumberOfPosts = user.NumberOfPosts;
                    model.FirstRegistered = user.FirstRegistered;
                    model.EmailAddress = user.EmailAddress;
                    task.Wait();
                    if (task.Result)
                    {
                        _userRepository.UpdateUser(model);
                    }
                }
                return RedirectToAction("Details");
            }
            catch
            {
                return View("Edit", id);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
