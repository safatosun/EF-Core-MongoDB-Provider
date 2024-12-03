using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbProvider.Models;
using MongoDbProvider.Services;

namespace MongoDbProvider.Controllers
{
    public class PetController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPetService _petService;

        public PetController(IUserService userService, IPetService petService)
        {
            _userService = userService;
            _petService = petService;
        }

        public IActionResult Index()
        {
            var pets = _petService.GetAllPets();
            return View(pets);
        }

        public IActionResult Add(ObjectId userId) 
        {
            TempData["UserId"] = userId;

            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm]Pet pet)
        {
            _petService.AddPet(pet);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string Id) 
        {
            if(Id == null || string.IsNullOrEmpty(Id)) 
            { 
                return NotFound(); 
            
            }
            var selectedPet = _petService.GetPetById(new ObjectId(Id));
            return View(selectedPet);

        }

        [HttpPost]
        public IActionResult Edit(Pet pet) 
        {
            try
            {
                var existingPet = _petService.GetPetById(pet.Id);
                if (existingPet == null)
                {
                    ModelState.AddModelError("",$"The pet with ID {pet.Id} does not exist!");
                }

                _petService.EditPet(pet);
                return RedirectToAction("Index");

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", $"Updating the pet failed, please try again! Error: {ex.Message}");
            }
            
            return View(pet);  

        }

        public IActionResult Delete(string Id)
        {
            if (Id == null || string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var selectedPet = _petService.GetPetById(new ObjectId(Id));
            return View(selectedPet);
        }

        [HttpPost]
        public IActionResult Delete(Pet pet)
        {
            if (pet.Id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the pet failed, invalid ID!";
                return View();
            }

            try
            {
                _petService.DeletePet(pet);
                TempData["PetDeleted"] = "Pet deleted ";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the pet failed, please try again! Error: {ex.Message}";
            }

            var selectedPet = _petService.GetPetById(pet.Id);
            return View(selectedPet);
        }

    }
}
