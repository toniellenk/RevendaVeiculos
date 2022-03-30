using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Services.Marcas;
using RevendaVeiculos.Web.Controllers;
using RevendaVeiculos.Web.Maps;
using RevendaVeiculos.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RevendaVeiculos.Test.UnitTests
{
    public class MarcasControllerTests
    {
        #region snippet_Index_ReturnsAViewResult_Marcas
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfMarcas()
        {
            var mockService = new Mock<IMarcasService>();
            mockService.Setup(repo => repo.ListPagedAsync(c => c.Id, 1, 10))
                .ReturnsAsync(GetTestMarcas());

            IMapper mapper = ConfigAutoMapper();
            var controller = new MarcasController(mockService.Object, mapper);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagedQuery<MarcaVM>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Total);
        }

        [Fact]
        public void IndexPost_ReturnInvalidForRequiredFields()
        {
            var mockService = new Mock<IMarcasService>();
            IMapper mapper = ConfigAutoMapper();

            var controller = new MarcasController(mockService.Object, mapper);
            ModelStateValidator.ValidateViewModel(controller, new MarcaVM());

            var erros = controller.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            Assert.Contains("The Nome field is required.", erros);
        }
        #endregion


        #region snippet_GetTest
        private IMapper ConfigAutoMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;

        }

        private PagedQuery<Marca> GetTestMarcas()
        {
            var marcas = new List<Marca>();
            marcas.Add(new Marca()
            {
                Id = 1,
                Nome = "Teste Nome 1"
            });

            marcas.Add(new Marca()
            {
                Id = 2,
                Nome = "Teste Nome 3"
            });

            marcas.Add(new Marca()
            {
                Id = 3,
                Nome = "Teste Nome 3"
            });


            return new PagedQuery<Marca>()
            {
                CurrentPage = 1,
                PageSize = 10,
                Data = marcas,
                Total = marcas.Count
            };
        }

        #endregion
    }
}