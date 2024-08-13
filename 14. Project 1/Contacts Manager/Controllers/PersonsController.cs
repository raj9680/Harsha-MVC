using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Contacts_Manager.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;
        public PersonsController(IPersonService personService, ICountryService countryService)
        {
            _personService = personService;
            _countryService = countryService;
        }


        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            // Search
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { // "PersonName", "Person name" OR
                nameof(PersonResponse.PersonName), "Person Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.CountryID), "Country" }
            };

            List<PersonResponse> persons = await _personService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            // Sort
            List<PersonResponse> sortedPerson = await _personService.GetSortedPerson(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPerson); // Views/Persons/index.cshtml
        }


        // Executes when user click create person
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<CountryResponse> countries = await _countryService.GetAllCountries();
            // Options 1
            // ViewBag.Countries = countries;

            // Option 2 - recommended - SelectListItem() type 
            // new SelectListItem() { Text="Raj", Value="1" };
            // <option value="1">Raj</option>  // So for ViewBag.Countries we can use this as

            ViewBag.Countries = countries.Select(x =>
                new SelectListItem()
                {
                    Text = x.CountryName,
                    Value = x.CountryID.ToString()
                }
            );

            return View();
        }


        // Executes when submit form to create user
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countryService.GetAllCountries();
                // Options 1
                // ViewBag.Countries = countries;

                // Option 2 - recommended - SelectListItem() type 
                // new SelectListItem() { Text="Raj", Value="1" };
                // <option value="1">Raj</option>  // So for ViewBag.Countries we can use this as

                ViewBag.Countries = countries.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.CountryName,
                        Value = x.CountryID.ToString()
                    }
                );

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            PersonResponse personResponse = await _personService.AddPerson(personAddRequest);

            // navigate to index action method
            return RedirectToAction("Index", "Persons");
        }


        [HttpGet]
        [Route("[action]/{personID}")] // Eg: /persons/edit/1
        public async Task<IActionResult> Edit(Guid personID)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(personID);

            if (personResponse == null)
            {
                return RedirectToAction("index");
            }

            // Converting personResponse to persoUpdateRequest
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            // Populating Countries List
            List<CountryResponse> countries = await _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
            new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

            return View(personUpdateRequest);
        }


        [HttpPost]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(personUpdateRequest.PersonID);

            if (personResponse == null)
            {
                return RedirectToAction("index");
            }

            if (ModelState.IsValid)
            {
                PersonResponse updatedPerson = await _personService.UpdatePerson(personUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                // Populating Countries List
                List<CountryResponse> countries = await _countryService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View(personResponse.ToPersonUpdateRequest());
            }
        }

        [HttpGet]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Delete(Guid personID)
        {
            if (!String.IsNullOrEmpty(personID.ToString()))
            {
                PersonResponse? personResponse = await _personService.GetPersonByPersonID(personID);
                return View(personResponse);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Delete(PersonUpdateRequest person)
        {
            PersonResponse? personResponse = await _personService.GetPersonByPersonID(person.PersonID);

            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }

            await _personService.DeletePerson(person.PersonID);
            return View();
        }


        [Route("[action]")]
        public async Task<IActionResult> PersonsPDF()
        {
            // Get list of persons
            List<PersonResponse> persons = await _personService.GetAllPersons();
            // ViewData, if any ViewData object is there in controller
            return new ViewAsPdf("PersonsPDF", persons, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                // bydefault portrait/landscape page orientation
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
            };
        }


    }
}
