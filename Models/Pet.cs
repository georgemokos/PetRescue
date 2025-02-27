namespace PetRescue.Models
{
    public class Pet
    {
        /* ID from the external API*/
        public string PetId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string? ImageUrl { get; set; }
        public string Status { get; set; }

        public string Description { get; set; }

        //Additional information

        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAddress1 { get; set; }
        public string ContactAddress2 { get; set; }
        public string ContactCity { get; set; }
        public string ContactState { get; set; }
        public string ContactCountry { get; set; }

        public string Link { get; set; } = "Not Available";
        
    }
}
