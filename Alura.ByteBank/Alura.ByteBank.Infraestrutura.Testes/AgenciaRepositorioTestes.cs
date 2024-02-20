using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _agenciaRepositorio;

        public AgenciaRepositorioTestes()
        {
            //Injetando dependências no contrutor;
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _agenciaRepositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaObterTodasAgencias()
        {
            //Arrange
            //Act
            List<Agencia> lista = _agenciaRepositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
            Assert.Equal(2, lista.Count);
            Assert.True(lista.Count != 0);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            //Act
            var agencia = _agenciaRepositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(agencia);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciasPorVariosId(int id)
        {
            //Arrange
            //Act
            var agencia = _agenciaRepositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(agencia);

        }

        [Fact]
        public void TesteInsereUmaNovaAgenciaNaBaseDeDados()
        {
            //Arrange
            string nome = "Agência Guarapari";
            int numero = 125982;
            Guid identificador = Guid.NewGuid();
            string endereco = "Rua: 7 de setembro - Centro";

            var agencia = new Agencia()
            {
                Nome = nome,
                Identificador = identificador,
                Endereco = endereco,
                Numero = numero
            };

            //Act
            var retorno = _agenciaRepositorio.Adicionar(agencia);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadaAgencia()
        {
            //Arrange
            var agencia = _agenciaRepositorio.ObterPorId(2);
            var nomeNovo = "Agencia Nova";
            agencia.Nome = nomeNovo;

            //Act
            var atualizado = _agenciaRepositorio.Atualizar(2, agencia);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadaAgencia()
        {
            //Arrange
            //Act
            var atualizado = _agenciaRepositorio.Excluir(3);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaExcecaoConsultaPorAgenciaPorId()
        {
            //Act
            //Assert
            Assert.Throws<FormatException>(() => _agenciaRepositorio.ObterPorId(33));
        }

        // Testes com Mock
        [Fact]
        public void TestaObterAgenciasMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarAgencias();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }

        [Fact]
        public void TestaAdiconarAgenciaMock()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            //Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            //Assert
            Assert.True(adicionado);
        }
    }
}
