#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Services.Proprietarios;

namespace RevendaVeiculos.Web.Controllers
{
    public class ProprietariosController : Controller
    {
        private readonly RevendaVeiculosContext _context;
        private readonly IProprietariosService _proprietariosService;

        public ProprietariosController(RevendaVeiculosContext context, IProprietariosService proprietariosService)
        {
            _context = context;
            _proprietariosService = proprietariosService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _proprietariosService.ListPagedAsync(c => c.Id, 1, 10));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietario = await _proprietariosService.FirstOrDefaultAsync(m => m.Id == id);
            if (proprietario == null)
            {
                return NotFound();
            }

            return View(proprietario);
        }

        public IActionResult Create()
        {
            return View(new Proprietario());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proprietario proprietario)
        {
            if (ModelState.IsValid)
            {
                await _proprietariosService.AddAsync(proprietario);
                return RedirectToAction(nameof(Index));
            }
            return View(proprietario);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietario = await _proprietariosService.GetByIdAsync(id);
            if (proprietario == null)
            {
                return NotFound();
            }
            return View(proprietario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proprietario proprietario)
        {
            if (id != proprietario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _proprietariosService.UpdateAsync(proprietario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _proprietariosService.ExistsByIdAsync(proprietario.Id))
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
            return View(proprietario);
        }
    }
}
