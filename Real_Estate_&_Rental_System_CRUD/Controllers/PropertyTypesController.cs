
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Data;

public class PropertyTypesController : Controller
{
    private readonly AppDbContext _context;

    public PropertyTypesController(AppDbContext context) // context == db
    {
        _context = context;
    }

    // GET: PROPERTYTYPES
    // IEnumerable<PropertyType> propertytypes = await _context.PropertyType.ToListAsync(); return View(propertytype);
    // edit 
    public async Task<IActionResult> Index()    
    {
        IEnumerable<PropertyType> properties = await _context.PropertyType.Include(p=>p.Properties).ToListAsync();
        return View(properties);
      //return View(await _context.PropertyType.ToListAsync());
    }

    // GET: PROPERTYTYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var propertytype = await _context.PropertyType
            .FirstOrDefaultAsync(m => m.Id == id);
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
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Properties")] PropertyType propertytype)
    {
        if (ModelState.IsValid)
        {
            _context.Add(propertytype);
            await _context.SaveChangesAsync();
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

        var propertytype = await _context.PropertyType.FindAsync(id);
        if (propertytype == null)
        {
            return NotFound();
        }
        return View(propertytype);
    }

    // POST: PROPERTYTYPES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Properties")] PropertyType propertytype)
    {
        if (id != propertytype.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(propertytype);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTypeExists(propertytype.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(propertytype);
    }

    // GET: PROPERTYTYPES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var propertytype = await _context.PropertyType
            .FirstOrDefaultAsync(m => m.Id == id);
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
        var propertytype = await _context.PropertyType.FindAsync(id);
        if (propertytype != null)
        {
            _context.PropertyType.Remove(propertytype);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PropertyTypeExists(int? id)
    {
        return _context.PropertyType.Any(e => e.Id == id);
    }
}
