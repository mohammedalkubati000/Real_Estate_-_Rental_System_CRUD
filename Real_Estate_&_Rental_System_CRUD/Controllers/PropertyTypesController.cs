using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Repositories;
using Real_Estate___Rental_System_CRUD.Dtos;
using Real_Estate___Rental_System_CRUD.Repositories.Base;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    public class PropertyTypesController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _context;
        // public PropertyTypesController(AppDbContext context)
        // {
        //     _context = context;
        // }

        // === Old Repository Pattern Injection (Commented Out) ===
        // private readonly IPropertyTypeRepository _propertyTypeRepository;
        // public PropertyTypesController(IPropertyTypeRepository propertyTypeRepository)
        // {
        //     _propertyTypeRepository = propertyTypeRepository;
        // }

        // === New UnitOfWork Injection ===
        private readonly IUnitOfWork _unitOfWork;

        public PropertyTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: PROPERTYTYPES
        public async Task<IActionResult> Index()
        {
            // === Old DbContext Code (Commented Out) ===
            // IEnumerable<PropertyType> properties = await _context.PropertyType.Include(p=>p.Properties).ToListAsync();
            // return View(properties);
            // return View(await _context.PropertyType.ToListAsync());

            // === Old Repository Code (Commented Out) ===
            // var properties = await _propertyTypeRepository.GetAllPropertyTypesAsync();

            // === New Repository Code via UnitOfWork ===
            var properties = await _unitOfWork.PropertyTypeRepository.GetAllPropertyTypesAsync();
            return View(properties);
        }

        // GET: PROPERTYTYPES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // === Old DbContext Code (Commented Out) ===
            // var propertytype = await _context.PropertyType
            //     .FirstOrDefaultAsync(m => m.Id == id);

            // === Old Repository Code (Commented Out) ===
            // var propertytype = await _propertyTypeRepository.GetPropertyTypeByIdAsync(id.Value);

            // === New Repository Code via UnitOfWork ===
            var propertytype = await _unitOfWork.PropertyTypeRepository.GetPropertyTypeByIdAsync(id.Value);

            if (propertytype == null)
            {
                return NotFound();
            }

            return View(propertytype);
        }

        // GET: PROPERTYTYPES/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PROPERTYTYPES/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Properties")] PropertyType propertytype)
        {
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // _context.Add(propertytype);
                // await _context.SaveChangesAsync();

                // === Old Repository Code (Commented Out) ===
                // await _propertyTypeRepository.AddPropertyTypeAsync(propertytype);

                // === New Repository Code via UnitOfWork ===
                await _unitOfWork.PropertyTypeRepository.AddPropertyTypeAsync(propertytype);

                return RedirectToAction(nameof(Index));
            }
            return View(propertytype);
        }

        // GET: PROPERTYTYPES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // === Old DbContext Code (Commented Out) ===
            // var propertytype = await _context.PropertyType.FindAsync(id);

            // === Old Repository Code (Commented Out) ===
            // var propertytype = await _propertyTypeRepository.GetPropertyTypeByIdAsync(id.Value);

            // === New Repository Code via UnitOfWork ===
            var propertytype = await _unitOfWork.PropertyTypeRepository.GetPropertyTypeByIdAsync(id.Value);

            if (propertytype == null)
            {
                return NotFound();
            }
            return View(propertytype);
        }

        // POST: PROPERTYTYPES/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(PropertyType propertyType)
        {
            if (ModelState.IsValid)
            {
                // === New Repository Code via UnitOfWork ===
                await _unitOfWork.PropertyTypeRepository.UpdatePropertyTypeAsync(propertyType);
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(propertyType);
        }

        // GET: PROPERTYTYPES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // === Old DbContext Code (Commented Out) ===
            // var propertytype = await _context.PropertyType
            //     .FirstOrDefaultAsync(m => m.Id == id);

            // === Old Repository Code (Commented Out) ===
            // var propertytype = await _propertyTypeRepository.GetPropertyTypeByIdAsync(id.Value);

            // === New Repository Code via UnitOfWork ===
            var propertytype = await _unitOfWork.PropertyTypeRepository.GetPropertyTypeByIdAsync(id.Value);

            if (propertytype == null)
            {
                return NotFound();
            }

            return View(propertytype);
        }

        // POST: PROPERTYTYPES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // === Old DbContext Code (Commented Out) ===
            // var propertytype = await _context.PropertyType.FindAsync(id);
            // if (propertytype != null)
            // {
            //     _context.PropertyType.Remove(propertytype);
            // }
            // await _context.SaveChangesAsync();

            // === Old Repository Code (Commented Out) ===
            // await _propertyTypeRepository.DeletePropertyTypeAsync(id.Value);

            // === New Repository Code via UnitOfWork ===
            await _unitOfWork.PropertyTypeRepository.DeletePropertyTypeAsync(id.Value);

            return RedirectToAction(nameof(Index));
        }

        // === Old Helper Method (Commented Out) ===
        // private bool PropertyTypeExists(int? id)
        // {
        //     return _context.PropertyType.Any(e => e.Id == id);
        // }
    }
}