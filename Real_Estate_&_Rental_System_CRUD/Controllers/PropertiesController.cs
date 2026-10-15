using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Dtos;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Repositories;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        // private readonly AppDbContext _db;

        //public PropertiesController(AppDbContext db)
        //{
        //    _db = db;
       // }
        private readonly IPropertyRepository _PropertyRepository;

        public PropertiesController(IPropertyRepository propertyRepository)
        {
            _PropertyRepository = propertyRepository;
         }




        //////

        //Index

        //////
        ///

        public async Task<IActionResult> Index()
        {
            var propertiesFromDb = await _PropertyRepository.GetAllPropertiesAsync();

            IEnumerable<PropertyDto> properties = propertiesFromDb.Select(e => new PropertyDto
            {
                // mapping the properties of properties  to PropertyDto
                Id = e.Id,
               PropertyTypeId = e.PropertyTypeId,
               PropertyTypeName = e.PropertyType!.Name,
               City = e.City,
               Address = e.Address,
               AnnualRent = e.AnnualRent,
               IsAvailable = e.IsAvailable,
               // PropertyTypeName = e.PropertyType != null ? e.PropertyType.Name : null,
            }).ToList();
            return View(properties);

        }


       // public ActionResult Index()
        //{
          //  IEnumerable<Property> properties = _db.Properties
            //    .Include(p => p.PropertyType)
              //  .ToList();
          //  return View(properties);
            
       // }


        //Create

        //AI Test the PropertyType
        //private void LoadPropertyTypes(int? selectedId = null)
        //{
          //  ViewBag.PropertyTypeId = new SelectList(_db.PropertyType, "Id", "Name", selectedId);
        //}

            [HttpGet]
        public async Task<IActionResult> Create()
        {
            //LoadPropertyTypes();  //AI
            // is on 

            var propertyTypes = await _PropertyRepository.GetPropertyTypesAsync();
            SelectList selectList = new SelectList(propertyTypes, "Id", "Name");
            ViewBag.d = selectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyCreateDto propertydto)
        {
            if (ModelState.IsValid)
            {
                // Mapping the properties of the PropertyCreateDto to property
                var property = new Property
                {
                    PropertyTypeId = propertydto.PropertyTypeId,
                    City = propertydto.City,
                    Address = propertydto.Address,
                    AnnualRent = propertydto.AnnualRent
                };

                await _PropertyRepository.AddPropertyAsync(property);
                return RedirectToAction("Index");
            }

            var propertyTypes = await _PropertyRepository.GetPropertyTypesAsync();
            ViewBag.d = new SelectList(propertyTypes, "Id", "Name");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(propertydto);
        }

        //Edit


        [HttpGet]
        public async Task<IActionResult> Edit(string Uuid)
        {
            var property = await _PropertyRepository.GetPropertyByUuidAsync(Uuid);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PropertyUpdateDto propertydto)
        {
            if (ModelState.IsValid)
            {
                var oldProperty = await _PropertyRepository.GetPropertyByUuidAsync(propertydto.Uuid);
                if (oldProperty == null)
                {
                    return NotFound();
                }
                // Mapping the properties of the PropertyCreateDto to property

                oldProperty.PropertyTypeId = propertydto.PropertyTypeId;
                oldProperty.City = propertydto.City;
                oldProperty.Address = propertydto.Address;
                oldProperty.AnnualRent = propertydto.AnnualRent;


                await _PropertyRepository.UpdatePropertyAsync(oldProperty);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(propertydto);
        }


        //Delete


        [HttpGet]
        public async Task<IActionResult> Delete(string Uuid)
        {
            var property = await _PropertyRepository.GetPropertyByUuidAsync(Uuid);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(PropertyUpdateDto propertydto)
        {
            var oldprop = await _PropertyRepository.GetPropertyByUuidAsync(propertydto.Uuid);
            if (oldprop == null)
            {
                return NotFound();
            }

            _PropertyRepository.DeletePropertyAsync(propertydto.Uuid);
            return RedirectToAction("Index");
        }
    }
}

