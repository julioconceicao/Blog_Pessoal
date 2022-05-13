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
    [TestClass]
    public class PostingRepositoryTest
    {
        private BlogPessoalContext _context;
        private IUser _repositoryU;
        private ITheme _repositoryT;
        private IPosting _repositoryP;

        [TestMethod]
        public async Task CreateThreePostsReturnsThree()
        {
            //Set the context
           var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal21")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostingRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO("Julio","juju@email.com","134652", "picurl", UserType.USER)
            );
            
            await _repositoryU.NewUserAsync(
            new NewUserDTO("Lud Mila","milnha@email.com","134652","picurl", UserType.USER)
            );
            
            // AND - E que registro 2 temas
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
            new NewPostDTO(
                    "C# is the best lang",
                    "The best in the world",
                    "picurl",
                    "microfitinho@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );
            
            // WHEN - Quando eu busco todas as postagens
            var postings = await _repositoryP.GetAllPostsAsync();

            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, postings.Count());
        }

        [TestMethod]
        public async Task UpDatePostReturnUpDatedPost()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostingRepository(_context);

            // GIVEN - Dado que registro 1 usuarios
            await _repositoryU.NewUserAsync(
            new NewUserDTO("Gustavo Boaz","gustavo@email.com","134652", "URLDAFOTO", UserType.USER)
            );
            
            // AND - E que registro 1 tema
            await _repositoryT.NewThemeAsync(new NewThemeDTO("COBOL"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));

            // AND - E que registro 1 postagem
            await _repositoryP.NewPostAsync(
            new NewPostDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            await _repositoryP.UpDatePostAsync(
            new UpDatePostDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            var posting = await _repositoryP.GetPostByIdAsync(1);

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual("C# é muito massa", posting.Title);
            Assert.AreEqual("C# é muito utilizada no mundo", posting.Description);
            Assert.AreEqual("URLDAFOTOATUALIZADA", posting.Picture);
            Assert.AreEqual("C#", posting.Theme.Description);
        }

        [TestMethod]
        public async Task GetPostBySearchReturnsCustomize()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostingRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO("Gustavo Boaz","gustavo@email.com","134652", "URLDAFOTO", UserType.USER)
            );
            
            await _repositoryU.NewUserAsync(
                new NewUserDTO("Catarina Boaz","catarina@email.com","134652","URLDAFOTO", UserType.USER)
            );
            
            // AND - E que registro 2 temas
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            var postingTest1 = await _repositoryP.GetPostBySearchAsync("massa", null, null);
            var postingTest2 = await _repositoryP.GetPostBySearchAsync(null, "C#", null);
            var postingTest3 = await _repositoryP.GetPostBySearchAsync(null, null, "Gustavo Boaz");

            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(2, postingTest1.Count);
            Assert.AreEqual(2, postingTest2.Count);
            Assert.AreEqual(2, postingTest3.Count);
        }
    }
}