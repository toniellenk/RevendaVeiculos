#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Services.Marcas;
using RevendaVeiculos.Service.Services.Proprietarios;
using RevendaVeiculos.Service.Services.Veiculos;
using RevendaVeiculos.Web.Models;

namespace RevendaVeiculos.Web.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly RevendaVeiculosContext _context;
        private readonly IMapper _mapper;  
        private readonly IVeiculosService _veiculosService;
        private readonly IProprietariosService _proprietariosService;
        private readonly IMarcasService _marcasService;


        public VeiculosController(RevendaVeiculosContext context, 
            IMapper mapper,
            IVeiculosService veiculosService, 
            IProprietariosService proprietariosService,
            IMarcasService marcasService)
        {
            _context = context;
            _mapper = mapper;
            _veiculosService = veiculosService;
            _proprietariosService = proprietariosService;
            _marcasService = marcasService;
        }

        public async Task<IActionResult> Index()
        {
            var veiculos = await _veiculosService.ListPagedAsync(c => c.Id, 1, 10);
            var veiculosVM = _mapper.Map<PagedQuery<VeiculoVM>>(veiculos);
            return View(veiculosVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _veiculosService.FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VeiculoVM>(veiculo));
        }

        public async Task<IActionResult> Create()
        {
            await PopularSelectLists();
            return View(new VeiculoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoVM veiculoVM)
        {
            if (ModelState.IsValid)
            {
                await _veiculosService.AddAsync(_mapper.Map<Veiculo>(veiculoVM));
                return RedirectToAction(nameof(Index));
            }

            await PopularSelectLists();
            return View(veiculoVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _veiculosService.GetByIdAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            await PopularSelectLists();
            return View(_mapper.Map<VeiculoVM>(veiculo));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeiculoVM veiculoVM)
        {
            if (id != veiculoVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _veiculosService.UpdateAsync(_mapper.Map<Veiculo>(veiculoVM));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _veiculosService.ExistsByIdAsync(veiculoVM.Id))
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
            await PopularSelectLists();
            return View(veiculoVM);
        }

        private async Task PopularSelectLists()
        {
            var proprietarios = await _proprietariosService.GetAllActiveListAsync();
            ViewData["Proprietarios"] = new SelectList(proprietarios, "Id", "Nome");

            var marcas = await _marcasService.GetAllActiveListAsync();
            ViewData["Marcas"] = new SelectList(marcas, "Id", "Nome");
        }
    }
}
