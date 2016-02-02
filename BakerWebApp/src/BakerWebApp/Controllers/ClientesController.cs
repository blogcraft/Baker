using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using BakerWebApp.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;

namespace BakerWebApp.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
            return View(await _context.Clientes.Where(item => item.Usuario.Id == usuario.Id).ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Cliente cliente = await _context.Clientes.SingleAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            cliente.Usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
            cliente.UsuarioId = cliente.Usuario.Id;
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Cliente cliente = await _context.Clientes.Include(u => u.Usuario).SingleAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Cliente cliente = await _context.Clientes.SingleAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await _context.Clientes.SingleAsync(m => m.ClienteId == id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
