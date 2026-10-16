using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Dtos;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Repositories;
using Real_Estate___Rental_System_CRUD.Repositories.Base;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _db;
        // public PropertiesController(AppDbContext db)
        // {
        //     _db = db;
        // }

        // === Old Repository Pattern Injection (Commented Out) ===
        // private readonly IPropertyRepository _PropertyRepository;
        // public PropertiesController(IPropertyRepository propertyRepository)
        // {
        //     _PropertyRepository = propertyRepository;
        // }

        // === New UnitOfWork Injection ===
        private readonly IUnitOfWork _unitOfWork;

        public PropertiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //////
        // Index
        //////
        public async Task<IActionResult> Index()
        {
            // === Old Repository Code (Commented Out) ===
            // var propertiesFromDb = await _PropertyRepository.GetAllPropertiesAsync();

            // === New Repository Code via UnitOfWork ===
            var propertiesFromDb = await _unitOfWork.PropertyRepository.GetAllPropertiesAsync();

            IEnumerable<PropertyDto> properties = propertiesFromDb.Select(e => new PropertyDto
            {
                // mapping the properties of properties to PropertyDto
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
        // {
        //     IEnumerable<Property> properties = _db.Properties
        //         .Include(p => p.PropertyType)
        //         .ToList();
        //     return View(properties);
        // }

        // Create

        // AI Test the PropertyType
        // private void LoadPropertyTypes(int? selectedId = null)
        // {
        //     ViewBag.PropertyTypeId = new SelectList(_db.PropertyType, "Id", "Name", selectedId);
        // }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // LoadPropertyTypes(); // AI is on

            // === Old Repository Code (Commented Out) ===
            // var propertyTypes = await _PropertyRepository.GetPropertyTypesAsync();

            // === New Repository Code via UnitOfWork ===
            var propertyTypes = await _unitOfWork.PropertyRepository.GetPropertyTypesAsync();
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

                // === Old Repository Code (Commented Out) ===
                // await _PropertyRepository.AddPropertyAsync(property);

                // === New Repository Code via UnitOfWork ===
                await _unitOfWork.PropertyRepository.AddPropertyAsync(property);
                return RedirectToAction("Index");
            }

            // === New Repository Code via UnitOfWork ===
            var propertyTypes = await _unitOfWork.PropertyRepository.GetPropertyTypesAsync();
            ViewBag.d = new SelectList(propertyTypes, "Id", "Name");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(propertydto);
        }

        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string Uuid)
        {
            // === Old Repository Code (Commented Out) ===
            // var property = await _PropertyRepository.GetPropertyByUuidAsync(Uuid);

            // === New Repository Code via UnitOfWork ===
            var property = await _unitOfWork.PropertyRepository.GetPropertyByUuidAsync(Uuid);
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
                // === New Repository Code via UnitOfWork ===
                var oldProperty = await _unitOfWork.PropertyRepository.GetPropertyByUuidAsync(propertydto.Uuid);

                if (oldProperty == null)
                {
                    return NotFound();
                }

                // Mapping updated properties
                oldProperty.PropertyTypeId = propertydto.PropertyTypeId;
                oldProperty.City = propertydto.City;
                oldProperty.Address = propertydto.Address;
                oldProperty.AnnualRent = propertydto.AnnualRent;

                await _unitOfWork.PropertyRepository.UpdatePropertyAsync(oldProperty);
                return RedirectToAction("Index");
            }

            // إرجاع الـ View مع إعادة تحميل أنواع العقارات في حال وجود خطأ في المدخلات
            var propertyTypes = await _unitOfWork.PropertyRepository.GetPropertyTypesAsync();
            ViewBag.d = new SelectList(propertyTypes, "Id", "Name");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(propertydto);
        }

        //
       // [HttpPost]
    //    public async Task<IActionResult> Edit(PropertyUpdateDto propertydto)
      //  {
       //     if (ModelState.IsValid)
       //     {
                // === Old Repository Code (Commented Out) ===
                // var oldProperty = await _PropertyRepository.GetPropertyByUuidAsync(propertydto.Uuid);

                // === New Repository Code via UnitOfWork ===

          //      var property = await _unitOfWork.PropertyRepository.GetPropertyByUuidAsync(Uuid);
          //  }
       // }
    }
}
