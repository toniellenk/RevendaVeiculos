using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Data.Enums;
using RevendaVeiculos.Message.Consumers;
using RevendaVeiculos.Message.Producers;
using RevendaVeiculos.Service.Services.Marcas;
using RevendaVeiculos.Service.Services.Proprietarios;
using RevendaVeiculos.Service.Services.Veiculos;
using RevendaVeiculos.Web.Controllers;
using RevendaVeiculos.Web.Maps;
using RevendaVeiculos.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RevendaVeiculos.Test.UnitTests
{
    public class VeiculosControllerTests
    {
        #region snippet_Index_ReturnsAViewResult_Veiculos
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfVeiculos()
        {
            var mockService = new Mock<IVeiculosService>();
            mockService.Setup(repo => repo.ListPagedAsync(1, 10))
                .ReturnsAsync(GetTestVeiculos());

            var controller = ConfigController(mockService.Object);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PagedQuery<VeiculoVM>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Total);
        }

        [Fact]
        public void Post_ReturnInvalidFields()
        {
            var controller = ConfigController();
            ModelStateValidator.ValidateViewModel(controller, new VeiculoVM());
            var erros = controller.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

            Assert.Contains("The Nome field is required.", erros);
            Assert.Contains("Informe um valor válido", erros);
            Assert.Contains("The Modelo field is required.", erros);
            Assert.Contains("The RENAVAM field is required.", erros);
            Assert.Contains("The field Ano Modelo must be between 1000 and 99999.", erros);
            Assert.Contains("The field Ano Fabricação must be between 1000 and 99999.", erros);
        }

        [Fact]
        public async Task Post_ReturnAlreadyExistsRENAVAN()
        {
            var veiculoFake = new VeiculoVM()
            {
                AnoFabricacao = 1990,
                AnoModelo = 1990,
                MarcaId = 1,
                ProprietarioId = 2,
                Nome = "Jaguar seminovo",
                Modelo = "Jaguar 5541",
                StatusVeiculo = StatusVeiculoEnum.Disponivel,
                Valor = "45521,00",
                Renavam = "4448516841845"
            };


            var mockContext = new Mock<RevendaVeiculosContext>();
            var mockNotificacaoProducer = new Mock<INotificacaoEmailProducer>();
            IList<Veiculo> entities = new List<Veiculo>() { new Veiculo() { Renavam = "4448516841845" }, new Veiculo() };
            mockContext.Setup(x => x.Set<Veiculo>()).ReturnsDbSet(entities);


            var mockService = new VeiculosService(mockContext.Object, mockNotificacaoProducer.Object);
            var controller = ConfigController(mockService);

            await controller.Create(veiculoFake);

            var erros = controller.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

            Assert.Contains("Este RENAVAM já está vinculado à um veículo.", erros);
        }


        [Fact]
        public async Task Post_ReturnInvalidChangeStatus()
        {
            var veiculoFake = new VeiculoVM()
            {
                Id = 4512,
                AnoFabricacao = 1990,
                AnoModelo = 1990,
                MarcaId = 1,
                ProprietarioId = 2,
                Nome = "Jaguar seminovo",
                Modelo = "Jaguar 5541",
                StatusVeiculo = StatusVeiculoEnum.Disponivel,
                Valor = "45521,00",
                Renavam = "4448516841845"
            };

            IList<Veiculo> veiculosEntitiesFake = new List<Veiculo>()
            { new Veiculo() {
                Id = 4512,
                StatusVeiculo = StatusVeiculoEnum.Indisponivel
            },
            new Veiculo() };


            var mockContext = new Mock<RevendaVeiculosContext>();
            var mockNotificacaoProducer = new Mock<INotificacaoEmailProducer>();

            mockContext.Setup(x => x.Set<Veiculo>()).ReturnsDbSet(veiculosEntitiesFake);


            var mockService = new VeiculosService(mockContext.Object, mockNotificacaoProducer.Object);
            var controller = ConfigController(mockService);

            await controller.Edit(4512, veiculoFake);

            var erros = controller.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

            Assert.Contains("Não é permitido voltar o status para Disponível", erros);
        }
        #endregion


        #region snippet_GetTest

        private VeiculosController ConfigController(IVeiculosService mockVeiculosService)
        {
            IMapper mapper = ConfigAutoMapper();

            var mockMarcasService = new Mock<IMarcasService>();
            var mockProprietariosService = new Mock<IProprietariosService>();

            return new VeiculosController(mapper, mockVeiculosService, mockProprietariosService.Object, mockMarcasService.Object);
        }

        private VeiculosController ConfigController()
        {
            IMapper mapper = ConfigAutoMapper();

            var mockVeiculosService = new Mock<IVeiculosService>();
            var mockMarcasService = new Mock<IMarcasService>();
            var mockProprietariosService = new Mock<IProprietariosService>();

            return new VeiculosController(mapper, mockVeiculosService.Object, mockProprietariosService.Object, mockMarcasService.Object);
        }

        private IMapper ConfigAutoMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }

        private PagedQuery<Veiculo> GetTestVeiculos()
        {
            var veiculos = new List<Veiculo>();
            veiculos.Add(new Veiculo()
            {
                Id = 1,
                Nome = "Teste Nome 1"
            });

            veiculos.Add(new Veiculo()
            {
                Id = 2,
                Nome = "Teste Nome 3"
            });

            veiculos.Add(new Veiculo()
            {
                Id = 3,
                Nome = "Teste Nome 3"
            });


            return new PagedQuery<Veiculo>()
            {
                CurrentPage = 1,
                PageSize = 10,
                Data = veiculos,
                Total = veiculos.Count
            };
        }

        #endregion
    }
}