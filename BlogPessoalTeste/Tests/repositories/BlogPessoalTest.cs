using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogpessoalTeste.Tests.repositories
{
    internal class BlogPessoalTest
    {
        private BlogPessoalContext _context;

        private IUser _repository;
        [TestInitialize]
        public void InitialConfig()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);
        }
        [TestMethod]
        public void CreateFourUsersAtDatabaseReturnsFourUsers()
        {
            //GIVEN - Dado que registro 4 usuarios no banco
            _repository.AddNewUser(
            new NewUserDTO(
            "Gustavo Boaz",
            "gustavo@email.com",
            "134652",
            "URLFOTO"));
            _repository.AddNewUser(
            new NewUserDTO(
            "Mallu Boaz",
            "mallu@email.com",
            "134652",
            "URLFOTO"));
            _repository.AddNewUser(
            new NewUserDTO(
            "Catarina Boaz",
            "catarina@email.com",
            "134652",
            "URLFOTO"));
            _repository.AddNewUser(
            new NewUserDTO(
            "Pamela Boaz",
            "pamela@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.User.Count());
        }
        [TestMethod]
        public void GetUserByEmailReturnsNull()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddNewUser(
            new NewUserDTO(
            "Zenildo Boaz",
            "zenildo@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmail("zenildo@email.com");
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void GetUserByIdReturnNotNullAndUserName()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddNewUser(
            new NewUserDTO(
            "Neusa Boaz",
            "neusa@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso pelo id 6
            var user = _repository.GetUserByID(6);
            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }
        [TestMethod]
        public void UpdateUserReturnsUpdateduser()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddNewUser(
            new NewUserDTO(
            "Estefânia Boaz",
            "estefania@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando atualizamos o usuario
            var old =
            _repository.GetUserByEmail("estefania@email.com");
            _repository.UpDateUser(
            new UpDateUserDTO(
            7,
            "Estefânia Moura",
            "estefania@gemail.cow",
            "123456",
            "URLFOTONOVA"));
            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura

            Assert.AreEqual(
        "Estefânia Moura",
        _context.User.FirstOrDefault(u => u.Id == old.Id).Name);
            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
            "123456",
            _context.User.FirstOrDefault(u => u.Id == old.Id).Password);
        }
    }
}

