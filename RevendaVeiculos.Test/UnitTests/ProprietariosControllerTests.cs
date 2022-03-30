using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Service.Services.Proprietarios;
using RevendaVeiculos.Web.Controllers;
using RevendaVeiculos.Web.Maps;
using RevendaVeiculos.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RevendaVeiculos.Test.UnitTests
{
    public class ProprietariosControllerTests
    {
        #region snippet_Index_ReturnsAViewResult_Proprietarios
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfProprietarios()
        {
            var mockService = new Mock<IProprietariosService>();
            mockService.Setup(repo => repo.ListPagedAsync(c => c.Id, 1, 10))
                .ReturnsAsync(GetTestProprietarios());

            IMapper mapper = ConfigAutoMapper();
            var controller = new ProprietariosController(mockService.Object, mapper);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagedQuery<ProprietarioVM>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Total);
        }

        [Fact]
        public void IndexPost_ReturnInvalidForRequiredFields()
        {
            var mockService = new Mock<IProprietariosService>();
            IMapper mapper = ConfigAutoMapper();

            var controller = new ProprietariosController(mockService.Object, mapper);
            ModelStateValidator.ValidateViewModel(controller, new ProprietarioVM());

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

        private PagedQuery<Proprietario> GetTestProprietarios()
        {
            var proprietarios = new List<Proprietario>();
            proprietarios.Add(new Proprietario()
            {
                Id = 1,
                Nome = "Teste Nome 1"
            });

            proprietarios.Add(new Proprietario()
            {
                Id = 2,
                Nome = "Teste Nome 3"
            });

            proprietarios.Add(new Proprietario()
            {
                Id = 3,
                Nome = "Teste Nome 3"
            });


            return new PagedQuery<Proprietario>()
            {
                CurrentPage = 1,
                PageSize = 10,
                Data = proprietarios,
                Total = proprietarios.Count
            };
        }

        #endregion
    }
}