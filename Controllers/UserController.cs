using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbProvider.Models;
using MongoDbProvider.Services;

namespace MongoDbProvider.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        public IActionResult Add() 
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Add([FromForm]User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userService.AddUser(user);
            return RedirectToAction("Index");   
        }

        public IActionResult Edit(ObjectId id)
        {
            if (id == null || id == ObjectId.Empty)
            { 
                return NotFound();
            }

            var selectedUser = _userService.GetUserById(id);
            return View(selectedUser);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _userService.EditUser(user);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",$"Updating the user failed, please try again!");
            }
            return View(user);
        }

        public IActionResult Delete(ObjectId id) 
        {
            if (id == null || id == ObjectId.Empty) 
            {
                return NotFound();
            }

            var selectedUser= _userService.GetUserById(id);
            return View(selectedUser);

        }

        [HttpPost]
        public IActionResult Delete(User user) 
        {
            if (user.Id == ObjectId.Empty)
            {
                ViewData["ErrorMessage"] = "Deleting the user failed, inalid ID!";
                return View();  
            }

            try
            {
                _userService.DeleteUser(user);
                TempData["UserDeleted"] = "User deleted !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the user failed, please try again!";
            }

            var selectedUser = _userService.GetUserById(user.Id);
            return View(selectedUser);

        }

    }
}
