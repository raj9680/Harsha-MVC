using Entities;
using Microsoft.EntityFrameworkCore;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;

namespace X_Unit
{
    public class CountryServiceTest
    {
        private readonly ICountryService _countryService;
        public CountryServiceTest()
        {
            // supply false if dont want to initialize mock data, see CountryService Constructor


            _countryService = new CountryService(false);

            // OR

            //_countryService = new CountryService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));


        }

        #region AddCountry
        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest request = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _countryService.AddCountry(request);
            });
        }


        // When CountryName is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_NullCountryName()
        {
            // Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName = null };

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _countryService.AddCountry(request);
            });
        }


        // When CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA"};
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA"};

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _countryService.AddCountry(request1);
                await _countryService.AddCountry(request2);
            });
        }


        // When CountryName is proper, it should insert the country to the existing list of countries.
        [Fact]
        public async Task AddCountry_ProperCountryName()
        {
            // Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName = "USA"};

            // Act
            CountryResponse response = await _countryService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = await _countryService.GetAllCountries();

            // Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
            // Contains actually calls equal method to compare
        }
        #endregion


        #region GetAllCountries

        [Fact]
        // List of countries should be empty by default (before adding any countries)
        public async Task GetAllCountries_EmptyList()
        {
            // Acts
            List<CountryResponse> actual_country_response_list =  await _countryService.GetAllCountries();

            //Assert
            Assert.Empty(actual_country_response_list);
        }


        [Fact]
        // List of countries few
        public async Task GetAllCountries_AddFewCountries()
        {
            // Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "India" },
                new CountryAddRequest() { CountryName = "Japan" }
            };

            //Assert
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach(CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(await _countryService.AddCountry(country_request));
            }

           List<CountryResponse> actualCountryResponse = await _countryService.GetAllCountries();

            // Read each element from actualCountryResponse
            foreach (CountryResponse expectedCountry in actualCountryResponse)
            {
                Assert.Contains(expectedCountry, actualCountryResponse);
            }
        }

        #endregion


        #region GetCountryByCountryID

        // If we supply null as CountryID, it should return null as CountryResponse
        [Fact]
        public async Task GetCountryByCountryID_NullCountryID()
        {
            // Arrange
            Guid? countryID = Guid.Empty;

            // Act 
            CountryResponse? countryResponse = await _countryService.GetCountryByCountryID(countryID);

            Assert.Null(countryResponse);
        }


        [Fact]
        // If we supply valid CountryID, it should return matching country details as Country object as response
        public async Task GetCountryByCountryID_ValidCountryID()
        {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "Russia" };
            CountryResponse country_add_response = await _countryService.AddCountry(country_add_request);

            // Act
            CountryResponse? country_get_response = await _countryService.GetCountryByCountryID(country_add_response.CountryID);

            // Assert
            Assert.Equal(country_add_response, country_get_response);
        }

        #endregion
    }
}