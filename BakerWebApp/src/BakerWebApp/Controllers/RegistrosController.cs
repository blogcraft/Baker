using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BakerWebApp.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;

namespace BakerWebApp.Controllers
{
    [Authorize]
    public class RegistrosController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistrosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Registros
        public async Task<IActionResult> Index()
        {
            var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var applicationDbContext = _context.Registros.Where(item => item.Usuario.Id == usuario.Id).Include(r => r.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Registros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Registro registro = await _context.Registros.SingleAsync(m => m.RegistroId == id);
            if (registro == null)
            {
                return HttpNotFound();
            }

            return View(registro);
        }

        // GET: Registros/Create
        public IActionResult Create()
        {
            ViewBag.Cliente = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            return View();
        }

        // POST: Registros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registro registro)
        {
            registro.Usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
            registro.UsuarioId = registro.Usuario.Id;

            if (ModelState.IsValid)
            {
                _context.Registros.Add(registro);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(_context.Clientes, "ClienteId", "Nombre", registro.ClienteId);
            return View(registro);
        }

        // GET: Registros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Registro registro = await _context.Registros.SingleAsync(m => m.RegistroId == id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente = new SelectList(_context.Clientes, "ClienteId", "Nombre", registro.ClienteId);
            return View(registro);
        }

        // POST: Registros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Registro registro)
        {
            if (ModelState.IsValid)
            {
                _context.Update(registro);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(_context.Clientes, "ClienteId", "Cliente", registro.ClienteId);
            return View(registro);
        }

        // GET: Registros/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Registro registro = await _context.Registros.SingleAsync(m => m.RegistroId == id);
            if (registro == null)
            {
                return HttpNotFound();
            }

            return View(registro);
        }

        // POST: Registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Registro registro = await _context.Registros.SingleAsync(m => m.RegistroId == id);
            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
