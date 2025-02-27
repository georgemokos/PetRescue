using Microsoft.AspNetCore.Identity;

namespace PetRescue.Models
{
    public class FavoritePet
    {
        public int Id { get; set; }
        
        // ID from the external API
        public string PetId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string? ImageUrl  { get; set; }
        public DateTime FavoritedOn { get; set; }
        //public IdentityUser Owner { get; set; }

         public string UserIdentifier { get; set; }
    }
}
