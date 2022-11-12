using AutoSales.Data;
using AutoSales.Models;
using AutoSales.Models.DBObjects;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectulFinal.Models.Repository;
using System.Data;
using System.Security.Claims;

namespace AutoSales.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext _DbContext;
        private PostRepository _postRepository;
        private UserRepository _userRepository;
        public PostController(ApplicationDbContext dbContext)
        {
            _postRepository = new PostRepository(dbContext);
            _userRepository = new UserRepository(dbContext);
            _DbContext = dbContext;
        }
        // GET: PostController
        public ActionResult Index()
        {
            var list = _postRepository.GetAllPosts();
            return View(list);
        }

        // GET: PostController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _postRepository.GetPostByID(id);
            return View("Details", model);
        }

        // GET: PostController/Create
        [Authorize(Roles = "User")]

        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new PostModel();
                if (User.Identity.IsAuthenticated)
                {
                    var emailUri = User.FindFirst(ClaimTypes.Email);
                    var email = emailUri.ToString().Substring(68);
                    var user = _userRepository.GetUserByEmail(email);
                    model.IdUser = user.IdUser;
                }
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _postRepository.InsertPost(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }


        // GET: PostController/Edit/5
        [Authorize(Roles = "User, Admin")]


        public ActionResult Edit(Guid id)
        {
            var model = _postRepository.GetPostByID(id);
            return View("Edit", model);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var post = _postRepository.GetPostByID(id);
                    var emailUri = User.FindFirst(ClaimTypes.Email);
                    var email = emailUri.ToString().Substring(68);
                    var user = _userRepository.GetUserByEmail(email);
                    if(user.IdUser == post.IdUser)
                    {
                        var model = new PostModel();
                        var task = TryUpdateModelAsync(model);
                        model.IdPost = id;
                        task.Wait();
                        if (task.Result)
                        {
                            _postRepository.UpdatePost(model);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit", id);
            }
        }

        // GET: PostController/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(Guid Id)
        {
            var model = _postRepository.GetPostByID(Id);
            return View("Delete", model);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid Id, IFormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var post = _postRepository.GetPostByID(Id);
                    var emailUri = User.FindFirst(ClaimTypes.Email);
                    var email = emailUri.ToString().Substring(68);
                    var user = _userRepository.GetUserByEmail(email);
                    if (user.IdUser == post.IdUser)
                    {
                        _postRepository.DeletePost(Id);

                    }

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            { 
                return View("Delete", Id); 
            }
        }
        [Authorize(Roles = "User")]
        public ActionResult AddToFavourites(Guid Id)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var favourite = new Favourite();
                var emailUri = User.FindFirst(ClaimTypes.Email);
                var email = emailUri.ToString().Substring(68);
                var user = _userRepository.GetUserByEmail(email);
                favourite.IdUser = user.IdUser;
                favourite.IdPost = Id;
                favourite.IdFavourite = Guid.NewGuid();
                _DbContext.Favourites.Add(favourite);
                _DbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Post");
        }
        [Authorize(Roles = "User")]
        public ActionResult FavouriteIndex()
        {
            try
            {
                var listOfPosts = new List<PostModel>();

                if (User.Identity.IsAuthenticated)
                {
                    var email = User.Identity.Name.ToString();
                    var user = _userRepository.GetUserByEmail(email);
                    var list = _DbContext.Favourites.ToList();
                    foreach(var favourite in list)
                    {
                        if (favourite.IdUser == user.IdUser)
                        {
                            var list2 = _DbContext.Favourites.ToList();
                            foreach(var item in list2)
                            {
                               listOfPosts.Add(_postRepository.GetPostByID(item.IdPost));
                            }
                        }
                    }
                }
                return View(listOfPosts);
            } 
            catch
            {
                return View("Index");
            }
        }
    }
}
