#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Services.Marcas;
using RevendaVeiculos.Web.Models;

namespace RevendaVeiculos.Web.Controllers
{
    public class MarcasController : Controller
    {
        private readonly RevendaVeiculosContext _context;
        private readonly IMarcasService _marcasService;
        private readonly IMapper _mapper;  


        public MarcasController(RevendaVeiculosContext context, IMarcasService marcasService, IMapper mapper)
        {
            _context = context;
            _marcasService = marcasService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var marcas = await _marcasService.ListPagedAsync(c => c.Id, 1, 10);
            var marcasVM = _mapper.Map<PagedQuery<MarcaVM>>(marcas);
            return View(marcasVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _marcasService.FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<MarcaVM>(marca));
        }

        public IActionResult Create()
        {
            return View(new MarcaVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarcaVM marcaVM)
        {
            if (ModelState.IsValid)
            {
                await _marcasService.AddAsync(_mapper.Map<Marca>(marcaVM));
                return RedirectToAction(nameof(Index));
            }
            return View(marcaVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _marcasService.GetByIdAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<MarcaVM>(marca));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MarcaVM marcaVM)
        {
            if (id != marcaVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _marcasService.UpdateAsync(_mapper.Map<Marca>(marcaVM));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _marcasService.ExistsByIdAsync(marcaVM.Id))
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
            return View(marcaVM);
        }
    }
}
