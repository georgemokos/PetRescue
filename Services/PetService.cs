using System.Buffers.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using PetRescue.Models;
using System.Globalization;

namespace PetRescue.Services
{
    public class PetService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private string _accessToken = String.Empty;

        public PetService(HttpClient httpClient,  IConfiguration configuration) {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration["PetApi: BaseUrl"];
        }

        /* Returns valid access token if we dont already have one */
        public async Task<string> GetAccessTokenAsync()
        {
            if(!(String.IsNullOrEmpty(_accessToken)))
            {
                //possible expired token
                return _accessToken;
            }

            var credentials = new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"client_id", _configuration["PetApi:ApiKey"]},
                {"client_secret", _configuration["PetApi:ApiSecret"]}
            };

            foreach (var item in credentials)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }

            var response = await _httpClient.PostAsync("https://api.petfinder.com/v2/oauth2/token", new FormUrlEncodedContent(credentials));

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // Log errorContent (for example, to the console or a logging framework)
                throw new Exception($"Failed retrieving API token. ACCESS DENIED! Response: {errorContent}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseBodyJSON = JsonSerializer.Deserialize<JsonElement>(responseBody);
            _accessToken = responseBodyJSON.GetProperty("access_token").GetString();
            return _accessToken;
        }

public async Task<(List<Pet> Pets, int TotalPages, int CurrentPage)> GetPetsAsync(string Location, int pageNumber = 1)
{
    var token = await GetAccessTokenAsync();
    _httpClient.DefaultRequestHeaders.Authorization = 
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

    var response = await _httpClient.GetAsync($"https://api.petfinder.com/v2/animals?location={Location}&limit=12&page={pageNumber}");

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception("Failed to fetch data");
    }

    var responseBody = await response.Content.ReadAsStringAsync();
    var responseBodyJSON = JsonSerializer.Deserialize<JsonElement>(responseBody);

    var petList = new List<Pet>();

    foreach (var pet in responseBodyJSON.GetProperty("animals").EnumerateArray())
    {
        petList.Add(
            new Pet
            {
                PetId = pet.GetProperty("id").GetInt32().ToString(),
                Name = pet.GetProperty("name").GetString() ?? "Not Available",
                Type = pet.GetProperty("type").GetString() ?? "Not Available",
                Age = pet.GetProperty("age").GetString() ?? "Not Available",
                Gender = pet.GetProperty("gender").GetString() ?? "Not Available",
                Breed = pet.GetProperty("breeds").GetProperty("primary").GetString() ?? "Not Available",
                ImageUrl = GetFirstPhotoUrl(pet.GetProperty("photos")) ?? "Not Available",
                Status = pet.GetProperty("status").GetString() ?? "Unknown"
            });
    }

    var pagination = responseBodyJSON.GetProperty("pagination");
    int totalPages = pagination.GetProperty("total_pages").GetInt32();
    int currentPage = pagination.GetProperty("current_page").GetInt32();

    return (petList, totalPages, currentPage);
}


        private string GetFirstPhotoUrl(JsonElement photosElement)
        {
            // Initialize an empty string that will eventually hold the image URL.
            string imageUrl = "";

            // Check if the JsonElement represents an array and that it contains at least one element.
            // - photosElement.ValueKind == JsonValueKind.Array ensures that the "photos" property is an array.
            // - photosElement.GetArrayLength() > 0 ensures that the array is not empty.
            if (photosElement.ValueKind == JsonValueKind.Array && photosElement.GetArrayLength() > 0)
            {
                // Enumerate the array and obtain the first element.
                // EnumerateArray() returns an IEnumerable<JsonElement> so we can use LINQ's First() method.
                var firstPhoto = photosElement.EnumerateArray().First();

                // Try to retrieve the "medium" property from the first photo object.
                // TryGetProperty returns true if the property exists; otherwise, it returns false.
                // If the property is found, the out parameter 'mediumPhoto' is populated with its value.
                if (firstPhoto.TryGetProperty("medium", out JsonElement mediumPhoto))
                {
                    // Convert the JSON element representing the medium image URL into a string.
                    imageUrl = mediumPhoto.GetString();
                }
            }

            // Return the extracted image URL (or an empty string if not found).
            return imageUrl;

            
        }

        //Getting pet by id

        public async Task<Pet> GetPetByIdAsync(string petId)
        {
              // Ensure we have a valid access token
             var token = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
             new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

             var response = await _httpClient.GetAsync($"https://api.petfinder.com/v2/animals/{petId}");

             if (!response.IsSuccessStatusCode)
             {
                  var errorContent = await response.Content.ReadAsStringAsync();
                  throw new Exception($"Failed to fetch pet data ");
             }
             var responseBody = await response.Content.ReadAsStringAsync();
             var responseBodyJSON = JsonSerializer.Deserialize<JsonElement>(responseBody);
             var animalElement = responseBodyJSON.GetProperty("animal");

             var pet = new Pet
             {
                PetId = animalElement.GetProperty("id").GetInt32().ToString(),
                Name = animalElement.GetProperty("name").GetString() ?? "Not Available",
                Type = animalElement.GetProperty("type").GetString() ?? "Not Available",
                Description = animalElement.GetProperty("description").GetString() ?? "Not Available",
                Age = animalElement.GetProperty("age").GetString() ?? "Not Available",
                Gender = animalElement.GetProperty("gender").GetString() ?? "Not Available",
                Status = animalElement.GetProperty("status").GetString() ?? "Unknown",
                Breed = animalElement.GetProperty("breeds").GetProperty("primary").GetString() ?? "Not Known",
                ContactEmail = animalElement.GetProperty("contact").GetProperty("email").GetString() ?? "Not Available",
                ContactPhone = animalElement.GetProperty("contact").GetProperty("phone").GetString() ?? "Not Available",
                ContactAddress1 = animalElement.GetProperty("contact").GetProperty("address").GetProperty("address1").GetString() ?? "Not Available",
                ContactAddress2 = animalElement.GetProperty("contact").GetProperty("address").GetProperty("address2").GetString() ?? "Not Available",
                ContactCity  = animalElement.GetProperty("contact").GetProperty("address").GetProperty("city").GetString() ?? "Not Available",
                ContactState = animalElement.GetProperty("contact").GetProperty("address").GetProperty("state").GetString() ?? "Not Available",
                ContactCountry = animalElement.GetProperty("contact").GetProperty("address").GetProperty("country").GetString() ?? "Not Available",
                ImageUrl = GetFirstPhotoUrl(animalElement.GetProperty("photos")),
                Link = animalElement.GetProperty("url").GetString() ?? "Not Available",
                

             };
             return pet;


        }

    }
}
