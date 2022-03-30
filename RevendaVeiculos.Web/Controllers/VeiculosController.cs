#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Data.Enums;
using RevendaVeiculos.Service.Services.Marcas;
using RevendaVeiculos.Service.Services.Proprietarios;
using RevendaVeiculos.Service.Services.Veiculos;
using RevendaVeiculos.Web.Models;

namespace RevendaVeiculos.Web.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVeiculosService _veiculosService;
        private readonly IProprietariosService _proprietariosService;
        private readonly IMarcasService _marcasService;


        public VeiculosController(
            IMapper mapper,
            IVeiculosService veiculosService,
            IProprietariosService proprietariosService,
            IMarcasService marcasService)
        {
            _mapper = mapper;
            _veiculosService = veiculosService;
            _proprietariosService = proprietariosService;
            _marcasService = marcasService;
        }

        public async Task<IActionResult> Index()
        {
            var veiculos = await _veiculosService.ListPagedAsync(1, 10);
            var veiculosVM = _mapper.Map<PagedQuery<VeiculoVM>>(veiculos);
            return View(veiculosVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _veiculosService.GetDetailsAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VeiculoVM>(veiculo));
        }

        public async Task<IActionResult> Create()
        {
            return await ViewWithSelectLists(new VeiculoVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoVM veiculoVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _veiculosService.AddAsync(_mapper.Map<Veiculo>(veiculoVM));

                if (result.PossueErro)
                {
                    foreach (var item in result.Erros)
                        ModelState.AddModelError(item.Key, item.Value);

                    return await ViewWithSelectLists(veiculoVM);
                }

                return RedirectToAction(nameof(Index));
            }

            return await ViewWithSelectLists(veiculoVM);
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

            return await ViewWithSelectLists(_mapper.Map<VeiculoVM>(veiculo));
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
                    var result = await _veiculosService.UpdateAsync(_mapper.Map<Veiculo>(veiculoVM));

                    if (result.PossueErro)
                    {
                        foreach (var item in result.Erros)
                            ModelState.AddModelError(item.Key, item.Value);

                        return await ViewWithSelectLists(veiculoVM);
                    }
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
            return await ViewWithSelectLists(veiculoVM);
        }
        private async Task<IActionResult> ViewWithSelectLists(VeiculoVM veiculoVM)
        {
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
