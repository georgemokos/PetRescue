using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using PetRescue.Models;
using PetRescue.Services;
using PetRescue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace PetRescue.Pages
{
 
    [Authorize]
     public class DetailsModel : PageModel
     {
        private readonly PetService _petService;
        private readonly PetRescueDbContext _context;
         private readonly UserManager<IdentityUser> _userManager;


        public DetailsModel(PetService petService,PetRescueDbContext context,UserManager<IdentityUser> userManager)
        {
            _petService = petService;
            _context = context;
            _userManager = userManager;

        }

        public Pet Pet { get; set; }

        public async Task<IActionResult> OnGetAsync(string petId)
        {
            if (string.IsNullOrEmpty(petId))
            {
                return NotFound();
            }

            // Retrieve the pet by its ID using the service method.
            Pet = await _petService.GetPetByIdAsync(petId);

            if (Pet == null)
            {
                return NotFound();
            }

            return Page();
        }


   
    public async Task<IActionResult> OnPostSaveFavoriteAsync(string petId, string name, string imageUrl, string breed,string type)
{
    var userId = _userManager.GetUserId(User);

           if (userId == null)
            {
                return Challenge(); // Ensure user is logged in
            }
    

    if (string.IsNullOrEmpty(petId))
    {
        return BadRequest("Invalid pet ID.");
    }


    var existingFavorite = await _context.FavoritePets.FirstOrDefaultAsync(p => p.PetId == petId && p.UserIdentifier == userId);

     if (existingFavorite != null)
    {
        // showing message if user tries to insert the same pet twice
        TempData["Message"] = "This pet is already in your favorites!";
        return RedirectToPage("/Details", new { petId = petId });
    }


   

    // Creating a new favorite pet entry
    var favoritePet = new FavoritePet
    {
        PetId = petId,
        Name = name,
        ImageUrl = imageUrl,
        Breed = breed,
        Type = type,
        UserIdentifier = userId,  
        FavoritedOn = DateTime.Now
    };

    _context.FavoritePets.Add(favoritePet);
    await _context.SaveChangesAsync();
    

 return RedirectToPage("/Favorites");
     }



 }

        
     
}



