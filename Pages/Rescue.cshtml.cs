using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescue.Models;
using PetRescue.Services;

namespace PetRescue.Pages
{
    public class RescueModel : PageModel
    {
        [BindProperty]
        public string Location { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();
        private readonly PetService _petService;
        public string ErrorMessage { get; set; } // To store error messages

        // Pagination properties
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public RescueModel(PetService petService)
        {
            _petService = petService;
        }

        public async Task<IActionResult> OnPostAsync(string location, int pageNumber = 1)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                ErrorMessage = "Location is required.";
                return Page();
            }

            try
            {
                var result = await _petService.GetPetsAsync(location, pageNumber);

                Pets = result.Pets;
                TotalPages = result.TotalPages;
                CurrentPage = result.CurrentPage;

                if (Pets == null || !Pets.Any())
                {
                    ErrorMessage = "No pets found for the given location. Please check the location and try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while fetching pets: {ex.Message}";
            }

            return Page();
        }
    }
}
