using Entities;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Xunit.Abstractions;

namespace X_Unit
{
    // Very Important Concepts inside Get All Person Block
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();

            // false to not initialize the country mock data , chk CountryService.cs

            _countryService = new CountryService(false);
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson

        // When we supply null values as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public async Task AddPerson_NullPerson()
        {
            // Arrange
            PersonAddRequest? request = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            {
                await _personService.AddPerson(request);
            });
        }


        // When we supply null values as PersonName, it should throw ArgumentException
        [Fact]
        public async Task AddPerson_PersonNameIsNull()
        {
            // Arrange
            PersonAddRequest? request = new PersonAddRequest() { PersonName = null }; 

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _personService.AddPerson(request);
            });
        }


        // When we supply valid Person Details, it should insert the person into the person
        // list; & it should return an object of PersonResponse, which includes with the
        // newly generated person id
        [Fact]
        public async Task AddPerson_ValidPersonDetails()
        {
            // Arrange - sample data
            PersonAddRequest? request = new PersonAddRequest() 
            { 
                PersonName = "Raj",
                Email = "email@email.com",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1997-01-01"),
                ReceivesNewsLetter = true
            };

            // Act
            PersonResponse person_response_from_add = await _personService.AddPerson(request);
            List<PersonResponse> checkPersonIsInserted = await _personService.GetAllPersons();

            // & Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, checkPersonIsInserted);
        }

        #endregion


        #region GetPersonByPersonID
        [Fact]
        // If we supplu null personID, it should return null as PersonResponse
        public async Task GetPersonByPersonID_NullPersonID()
        {
            // Arrange
            Guid personID = Guid.Empty;

            // Act
            PersonResponse? person_response_from_get =  await _personService.GetPersonByPersonID(personID);
           
            // Assert
            Assert.Null(person_response_from_get);
        }
        
        
        [Fact]
        // If we supplu a valid personID, it should return valid person details as personResponse object
        public async Task GetPersonByPersonID_ValidPersonID()
        {
            // Arrange - Add Country & then Add Person with countryID
            CountryAddRequest countryRequest = new CountryAddRequest()
            {
                CountryName = "Canada"
            };

            CountryResponse country_from_response = await _countryService.AddCountry(countryRequest);

            PersonAddRequest request = new PersonAddRequest()
            {
                PersonName = "Raj",
                CountryID = country_from_response.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("1997-01-01"),
                ReceivesNewsLetter = true,
                Email = "email@email.com"
            };

            PersonResponse person_response_from_add = await _personService.AddPerson(request);

            PersonResponse? personResponse = await _personService.GetPersonByPersonID(person_response_from_add.PersonID);

            // Assert
            Assert.Equal(person_response_from_add, personResponse);
        }
        #endregion


        #region GetAllPerson

        [Fact]
        public async Task GetAllPerson_EmptyList()
        {
            // Act
            List<PersonResponse> person_from_get = await _personService.GetAllPersons();

            // Assert
            Assert.Empty(person_from_get);
        }


        // First we will add few persons, add then when we call GetAllPerson(), it should return the same persons that were added
        [Fact]
        public async Task GetAllPersons_AddFewPersons()
        {
            // Arrange
            // Add Country
            CountryAddRequest country_AddRequest1 = new CountryAddRequest() 
            { CountryName = "USA" };

            CountryAddRequest country_AddRequest2 = new CountryAddRequest()
            { CountryName = "Italy" };

            CountryResponse country_response1 = await _countryService.AddCountry(country_AddRequest1);
            CountryResponse country_response2 =  await _countryService.AddCountry(country_AddRequest2);

            // Add Person One
            PersonAddRequest person_request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "email@email.com",
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                DateOfBirth = DateTime.Parse("1998-01-01"),
                ReceivesNewsLetter = true
            };

            // Add Person Two
            PersonAddRequest person_request2 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            };

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {
                person_request1, person_request2
            };

            // Storing person response - added person, in separate list
            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

            // Adding Person
            foreach (PersonAddRequest person in personAddRequests)
            {
                PersonResponse personResponse = await _personService.AddPerson(person);
                personResponseListFromAdd.Add(personResponse);
            }

            // Expected print personResponseListFromAdd
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            // Act 
            List<PersonResponse> person_response_from_get = await _personService.GetAllPersons();

            // Actual print person_response_from_get
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in person_response_from_get)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            #region VeryVeryImportant

            // Expected result you will get 
            // Expected:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse
            // Actual:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse

            // We are getting class output only bcoz bydefault PersonResponse class return
            // ToString() which returns object.... to retunr actual data we need to
            // override ToString() method of class PersonResponse class as per our
            // requirement.

            #endregion End

            // Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            {
                Assert.Contains(personResponseFromAdd, person_response_from_get);
            }
        }

        #endregion


        #region GetFilteredPerson
        // If search text is empty nd search by "PersonName", it should return all persons
        [Fact]
        public async Task GetFilteredPersons_EmptySearchText()
        {
            // Arrange
            // Add Country
            CountryAddRequest country_AddRequest1 = new CountryAddRequest()
            { CountryName = "USA" };

            CountryAddRequest country_AddRequest2 = new CountryAddRequest()
            { CountryName = "Italy" };

            CountryResponse country_response1 = await _countryService.AddCountry(country_AddRequest1);
            CountryResponse country_response2 = await _countryService.AddCountry(country_AddRequest2);

            // Add Person One
            PersonAddRequest person_request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "email@email.com",
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                DateOfBirth = DateTime.Parse("1998-01-01"),
                ReceivesNewsLetter = true
            };

            // Add Person Two
            PersonAddRequest person_request2 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            };

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {
                person_request1, person_request2
            };

            // Storing person response - added person, in separate list
            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

            // Adding Person
            foreach (PersonAddRequest person in personAddRequests)
            {
                PersonResponse personResponse = await _personService.AddPerson(person);
                personResponseListFromAdd.Add(personResponse);
            }

            // Expected print personResponseListFromAdd
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            // Act 
            List<PersonResponse> person_response_from_search = await _personService.GetFilteredPersons(nameof(Person.PersonName), "");

            // Actual print person_response_from_get
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in person_response_from_search)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            #region VeryVeryImportant

            // Expected result you will get 
            // Expected:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse
            // Actual:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse

            // We are getting class output only bcoz bydefault PersonResponse class return
            // ToString() which returns object.... to retunr actual data we need to
            // override ToString() method of class PersonResponse class as per our
            // requirement.

            #endregion End

            // Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            {
                Assert.Contains(personResponseFromAdd, person_response_from_search);
            }
        }


        //  First we will add some persons and search based on person name with ome search string, so it should return matching person 
        [Fact]
        public async Task GetFilteredPersons_SearchByPersonName()
        {
            // Arrange
            // Add Country
            CountryAddRequest country_AddRequest1 = new CountryAddRequest()
            { CountryName = "USA" };

            CountryAddRequest country_AddRequest2 = new CountryAddRequest()
            { CountryName = "Italy" };

            CountryResponse country_response1 = await _countryService.AddCountry(country_AddRequest1);
            CountryResponse country_response2 = await _countryService.AddCountry(country_AddRequest2);

            // Add Person One
            PersonAddRequest person_request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "email@email.com",
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                DateOfBirth = DateTime.Parse("1998-01-01"),
                ReceivesNewsLetter = true
            };

            // Add Person Two
            PersonAddRequest person_request2 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            };

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {
                person_request1, person_request2
            };

            // Storing person response - added person, in separate list
            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

            // Adding Person
            foreach (PersonAddRequest person in personAddRequests)
            {
                PersonResponse personResponse = await _personService.AddPerson(person);
                personResponseListFromAdd.Add(personResponse);
            }

            // Expected print personResponseListFromAdd
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            // Act 
            List<PersonResponse> person_response_from_search = await _personService.GetFilteredPersons(nameof(Person.PersonName), "th");

            // Actual print person_response_from_get
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in person_response_from_search)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            #region VeryVeryImportant

            // Expected result you will get 
            // Expected:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse
            // Actual:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse

            // We are getting class output only bcoz bydefault PersonResponse class return
            // ToString() which returns object.... to retunr actual data we need to
            // override ToString() method of class PersonResponse class as per our
            // requirement.

            #endregion End

            // Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            {
                if(personResponseFromAdd.PersonName != null)
                {
                    if (personResponseFromAdd.PersonName.Contains("th", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponseFromAdd, person_response_from_search);
                    }
                }
            }
        }

        #endregion


        #region GetSortedPersons
        //  When we sort based on person name in descending order, it sould return the person in descending order itself
        [Fact]
        public async Task GetSortedPersons()
        {
            // Arrange
            // Add Country
            CountryAddRequest country_AddRequest1 = new CountryAddRequest()
            { CountryName = "USA" };

            CountryAddRequest country_AddRequest2 = new CountryAddRequest()
            { CountryName = "Italy" };

            CountryResponse country_response1 = await _countryService.AddCountry(country_AddRequest1);
            CountryResponse country_response2 = await _countryService.AddCountry(country_AddRequest2);

            // Add Person One
            PersonAddRequest person_request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "email@email.com",
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                DateOfBirth = DateTime.Parse("1998-01-01"),
                ReceivesNewsLetter = true
            };

            // Add Person Two
            PersonAddRequest person_request2 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            };

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {
                person_request1, person_request2
            };

            // Storing person response - added person, in separate list
            List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

            // Adding Person
            foreach (PersonAddRequest person in personAddRequests)
            {
                PersonResponse personResponse = await _personService.AddPerson(person);
                personResponseListFromAdd.Add(personResponse);
            }

            // Expected print personResponseListFromAdd
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person in personResponseListFromAdd)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            List<PersonResponse> allPersons = await _personService.GetAllPersons();

            // Act 
            List<PersonResponse> person_list_from_sort = await _personService.GetSortedPerson(allPersons, nameof(Person.PersonName), SortOrderOptions.ASC);

            // Actual print person_response_from_get
            _testOutputHelper.WriteLine("Actual: ");
            foreach (PersonResponse person in person_list_from_sort)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //personResponseListFromAdd = personResponseListFromAdd.OrderByDescending(x => x.PersonName).ToList();
            personResponseListFromAdd = personResponseListFromAdd.OrderBy(x => x.PersonName).ToList();

            #region VeryVeryImportant

            // Expected result you will get 
            // Expected:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse
            // Actual:
            // ServiceContracts.DTO.PersonResponse
            // ServiceContracts.DTO.PersonResponse

            // We are getting class output only bcoz bydefault PersonResponse class return
            // ToString() which returns object.... to retunr actual data we need to
            // override ToString() method of class PersonResponse class as per our
            // requirement.

            #endregion End

            // Assert
            for (int i = 0; i < personResponseListFromAdd.Count; i++)
            {
                // object from personResponseListFromAdd should be equal to object from person_list_from_sort,
                // personResponseListFromAdd represent the expected & person_list_from_sort one represent the actual
                Assert.Equal(personResponseListFromAdd[i], person_list_from_sort[i]);
            }
        }

        #endregion


        #region UpdatePerson

        // If we supply null as PersonUpdateRequest, it should throw ArgumetNullException
        [Fact]
        public async Task UpdatePerson_NullPerson()
        {
            // Arrange
            PersonUpdateRequest? personUpdateRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act 
                await _personService.UpdatePerson(personUpdateRequest);
            });
        }


        // If we supplu Invalid Person ID, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_InvalidPersonID()
        {
            // Arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid()
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act 
                await _personService.UpdatePerson(personUpdateRequest);
            });
        }


        // When Person Name is null, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_PersonNameIsNull()
        {
            CountryAddRequest country_AddRequest = new CountryAddRequest()
            { CountryName = "USA" };
            CountryResponse countryResponseFromAdd = await _countryService.AddCountry(country_AddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = countryResponseFromAdd.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            }; 

            PersonResponse person_response_from_add = await _personService.AddPerson(personAddRequest);

            // Arrange
            PersonUpdateRequest? personUpdateRequest = person_response_from_add.ToPersonUpdateRequest();
            // here we passing null person name
            personUpdateRequest.PersonName = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act 
                await _personService.UpdatePerson(personUpdateRequest);
            });
        }


        // First we will add new person & try to update the same
        [Fact]
        public async Task UpdatePerson_PersonFullDetailsUpdation()
        {
            CountryAddRequest country_AddRequest = new CountryAddRequest()
            { CountryName = "USA" };
            CountryResponse countryResponseFromAdd = await _countryService.AddCountry(country_AddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = countryResponseFromAdd.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            };

            PersonResponse person_response_from_add = await _personService.AddPerson(personAddRequest);

            // Arrange
            PersonUpdateRequest? personUpdateRequest = person_response_from_add.ToPersonUpdateRequest();
            // here we passing new person name
            personUpdateRequest.PersonName = "William";
            personUpdateRequest.Email = "updated@email.com";

            // Act 
            PersonResponse person_response_from_update = await _personService.UpdatePerson(personUpdateRequest);

            PersonResponse? personResponseFromGet = await _personService.GetPersonByPersonID(person_response_from_update.PersonID);

            // Assert
            Assert.Equal(personResponseFromGet, person_response_from_update);
        }

        #endregion


        #region PersonDelete

        [Fact]
        // if you supply valid PersonID,it returns true
        public async Task DeletePerson_ValidPersonID()
        {
            CountryAddRequest country_AddRequest = new CountryAddRequest()
            { CountryName = "USA" };
            CountryResponse countryResponseFromAdd = await _countryService.AddCountry(country_AddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Gender = GenderOptions.Female,
                CountryID = countryResponseFromAdd.CountryID,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                ReceivesNewsLetter = false
            };

            PersonResponse person_response_from_add = await _personService.AddPerson(personAddRequest);

            // Act
            bool isDeleted = await _personService.DeletePerson(person_response_from_add.PersonID);

            // Assert
            Assert.True(isDeleted);
        }


        [Fact]
        // if you supply valid PersonID,it returns true
        public async Task DeletePerson_InvalidPersonID()
        {
            // Act
            bool isDeleted = await _personService.DeletePerson(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }

        #endregion
    }
}
