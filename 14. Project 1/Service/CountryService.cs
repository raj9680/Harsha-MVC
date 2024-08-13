using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
    public class CountryService : ICountryService
    {
        // private field
        private readonly List<Country> _countries;
        private readonly PersonsDbContext _db;

        // constructor to initialize Country object

        public CountryService(bool initialize = true)
        {
            // initialize value can we passed from other classes which create or calls the constructor of CountrySrvice class, i.e chk CountryServiceTest.cs(hence we planned - during performing testing mock data will not execute)
            _countries = new List<Country>();

            if (initialize)
            {
                _countries.AddRange(new List<Country>()
                {
                    new Country
                    {
                        CountryID = Guid.Parse("23B7D838-B045-47A3-ACCB-1192A0F88FE7"),
                        CountryName = "India"
                    },
                    new Country
                    {
                        CountryID = Guid.Parse("630BBDF6-B703-439E-9CBD-6020045BBA8C"),
                        CountryName = "Russia"
                    },
                    new Country
                    {
                        CountryID = Guid.Parse("F62A9B10-9DF9-44CC-9E11-CBCF5DBA6813"),
                        CountryName = "South Korea"
                    },
                    new Country
                    {
                        CountryID = Guid.Parse("8C3C3838-7BE1-454B-8155-775526351802"),
                        CountryName = "Japan"
                    },
                    new Country
                    {
                        CountryID = Guid.Parse("2300968F-EEA0-4E45-877D-F1C2845A5BDF"),
                        CountryName = "U.S.A"
                    }
                });
            }
        }

        public CountryService( PersonsDbContext db, bool initialize = true)
        {
            _db = db;
        }
        

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validation: countryAddRequest parameter can't be null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            // Validation: CountryName parameter can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest.CountryName));
            }

            // Validation: CountryName can't be duplicate
            if (await _db.Countries.CountAsync(temp => temp.CountryName == countryAddRequest.CountryName) > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry(); 

            // generate Guid
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            
            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if(countryID == null)
            {
                throw new ArgumentNullException(nameof(countryID));
            }
            Country? country_ID_found = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == countryID);

            if(country_ID_found == null) 
            { 
                return null; 
            }

            // typeCast Country object to CountryResponse
            return country_ID_found.ToCountryResponse();
        }
    }
}
