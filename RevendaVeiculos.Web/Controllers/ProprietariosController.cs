#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Services.Proprietarios;
using RevendaVeiculos.Web.Models;

namespace RevendaVeiculos.Web.Controllers
{
    public class ProprietariosController : Controller
    {
        private readonly RevendaVeiculosContext _context;
        private readonly IProprietariosService _proprietariosService;
        private readonly IMapper _mapper;


        public ProprietariosController(RevendaVeiculosContext context, IProprietariosService proprietariosService, IMapper mapper)
        {
            _context = context;
            _proprietariosService = proprietariosService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var proprietarios = await _proprietariosService.ListPagedAsync(c => c.Id, 1, 10);
            var proprietariosVM = _mapper.Map<PagedQuery<ProprietarioVM>>(proprietarios);
            return View(proprietariosVM);
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

            return View(_mapper.Map<ProprietarioVM>(proprietario));
        }

        public IActionResult Create()
        {
            return View(new ProprietarioVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProprietarioVM proprietarioVM)
        {
            if (ModelState.IsValid)
            {
                await _proprietariosService.AddAsync(_mapper.Map<Proprietario>(proprietarioVM));
                return RedirectToAction(nameof(Index));
            }
            return View(proprietarioVM);
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
            return View(_mapper.Map<ProprietarioVM>(proprietario));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProprietarioVM proprietarioVM)
        {
            if (id != proprietarioVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _proprietariosService.UpdateAsync(_mapper.Map<Proprietario>(proprietarioVM));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _proprietariosService.ExistsByIdAsync(proprietarioVM.Id))
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
            return View(proprietarioVM);
        }
    }
}
