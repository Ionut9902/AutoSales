using AutoSales.Data;
using AutoSales.Models;
using AutoSales.Models.Repository;
using AutoSales.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSales.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext _DBContext;
        private MessageRepository _messageRepository;
        private PostRepository _postRepository;
        private UserRepository _userRepository;

        public MessageController(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
            _messageRepository = new MessageRepository(dBContext);
            _postRepository = new PostRepository(dBContext);
            _userRepository = new UserRepository(dBContext);
        }

        // GET: MessageController
        [Authorize(Roles = "User")]

        public ActionResult Index()
        {
            var listOfMessages = new List<MessageModel>();
            if (User.Identity.IsAuthenticated)
            {
                var list = _messageRepository.GetAllMessages();
                var user = _userRepository.GetUserByEmail(User.Identity.Name);
                foreach(var message in list)
                {
                    if(user.IdUser == message.IdUser)
                    {
                        listOfMessages.Add(message);
                    }
                }
            }
            return View(listOfMessages);
        }

        // GET: MessageController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _messageRepository.GetMessageByID(id);
            return View("Details", model);
        }

        // GET: MessageController/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: MessageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MessageModel();
                var post = _postRepository.GetPostByID(id);
                model.IdPost = post.IdPost;
                model.IdUser = post.IdUser;
                var task = TryUpdateModelAsync(model);
                if (task.Result)
                {
                    _messageRepository.InsertMessage(model);
                }
                return RedirectToAction("Index","Post");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: MessageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MessageController/Edit/5
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

        // GET: MessageController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _messageRepository.GetMessageByID(id);
            return View("Delete",model);
        }

        // POST: MessageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var message = _messageRepository.GetMessageByID(id);
                var user = _userRepository.GetUserByEmail(User.Identity.Name);
                if(message.IdUser == user.IdUser)
                {
                    _messageRepository.DeleteMessage(id);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete",id);
            }
        }
    }
}
