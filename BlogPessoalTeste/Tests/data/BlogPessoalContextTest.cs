using BlogPessoal.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.models;
using System.Linq;

namespace BlogPessoalTeste.Tests.data
{
    [TestClass]
    public class BlogPessoalContextTest
    {
        private BlogPessoalContext _context;
        [TestInitialize]
        public void Start()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _context = new BlogPessoalContext(opt);

        }

        [TestMethod]
        public void InsertNewUserAtDatabaseReturnUser()
        {

            UserModel user = new UserModel();

            user.Name = "Julio Cesar";
            user.Email = "julio@outlook.com";
            user.Password = "12345";
            user.Picture = "picturelink";

            _context.User.Add(user);

            _context.SaveChanges();

            Assert.IsNotNull(_context.User.FirstOrDefault(u => u.Email == "julio@outlook.com"));

            //Assert.AreEqual(1, 1);        
        }

    }
}
