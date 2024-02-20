using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteRepositorioTestes()
        {
            //Injetando dependências no contrutor;
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _clienteRepositorio = provedor.GetService<IClienteRepositorio>();
            
        }

        [Fact]
         public void TestaObterTodosClientes()
        {
            //Arrange
            //Act
            List<Cliente> lista = _clienteRepositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
            Assert.Equal(3, lista.Count);

        }

        [Fact]
        public void TestaObterClientesPorId()
        {
            //Arrange
            //Act
            var cliente = _clienteRepositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(cliente);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestaObterClientesPorVariosId(int id)
        {
            //Arrange
            //Act
            var cliente = _clienteRepositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(cliente);

        }

        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadoCliente()
        {
            //Arrange
            var cliente = _clienteRepositorio.ObterPorId(2);
            var nomeNovo = "João Pedro";
            cliente.Nome = nomeNovo;

            //Act
            var atualizado = _clienteRepositorio.Atualizar(2,cliente);

            //Assert
            Assert.True(atualizado);
        }

        // Testes com Mock
        [Fact]
        public void TestaObterClientesMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarClientes();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarClientes());
        }


    }
}
