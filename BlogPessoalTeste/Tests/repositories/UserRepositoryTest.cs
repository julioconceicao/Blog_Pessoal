using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{
    public class UserRepositoryTest
    {
        private BlogPessoalContext _context;
        private IUser _repository;

        [TestMethod]
        public async Task CreateFourUsersReturnsFourUsers()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco
            await _repository.NewUserAsync(
                new NewUserDTO("Gustavo Boaz","gustavo@email.com","134652","URLFOTO", UserType.USER)
            );
            
            await _repository.NewUserAsync(
                new NewUserDTO("Mallu Boaz","mallu@email.com","134652","URLFOTO", UserType.USER)
            );
            
            await _repository.NewUserAsync(
                new NewUserDTO("Catarina Boaz","catarina@email.com","134652","URLFOTO", UserType.USER)
            );
 
            await _repository.NewUserAsync(
                new NewUserDTO("Pamela Boaz","pamela@email.com","134652","URLFOTO", UserType.USER)
            );
            
			//WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.User.Count());
        }
        
        [TestMethod]
        public async Task GetUserByEmailReturnsNotNull()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO("Zenildo Boaz","zenildo@email.com","134652","URLFOTO", UserType.USER)
            );
            
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = await _repository.GetUserByEmailAsync("zenildo@email.com");
			
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task GetUserByIdReturnNotNullAndUserName()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO("Neusa Boaz","neusa@email.com","134652","URLFOTO", UserType.USER)
            );
            
			//WHEN - Quando pesquiso pelo id 1
            var user = await _repository.GetUserByIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }

        [TestMethod]
        public async Task UpDateUserReturnUpdatedUser()
        {
            // Definindo o contexto
            var opt= new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO("Estefânia Boaz","estefania@email.com","134652","URLFOTO", UserType.USER)
            );
            
            //WHEN - Quando atualizamos o usuario
            await _repository.UpDateUserAsync(
                new UpDateUserDTO(1,"Estefânia Moura", "estefania@outlook.com", "123456","URLFOTONOVA")
            );
            
            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            var old = await _repository.GetUserByEmailAsync("estefania@email.com");

            Assert.AreEqual(
                "Estefânia Moura",
                _context.User.FirstOrDefault(u => u.Id == old.Id).Name
            );
            
            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
                "123456",
                _context.User.FirstOrDefault(u => u.Id == old.Id).Password
            );
        }

    }
}
    