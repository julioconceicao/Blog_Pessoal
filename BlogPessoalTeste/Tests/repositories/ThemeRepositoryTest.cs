using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{
    public class ThemeRepositoryTest
    {
        private BlogPessoalContext _context;
        private ITheme _repository;
        
        [TestMethod]
        public async Task CreateFourThemesAtDataBaseReturnsFourThemes()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco
            await _repository.NewThemeAsync(new NewThemeDTO("C#"));
            await _repository.NewThemeAsync(new NewThemeDTO("Java"));
            await _repository.NewThemeAsync(new NewThemeDTO("Python"));
            await _repository.NewThemeAsync(new NewThemeDTO("JavaScript"));

            var themes = await _repository.GetAllThemesAsync();

            //THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, themes.Count);
        }

        [TestMethod]
        public async Task GetThemeByIdReturnsTheme1()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal11")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro C# no banco
            await _repository.NewThemeAsync(new NewThemeDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var theme = await _repository.GetThemeByIdAsync(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", theme.Description);
        }
        
        [TestMethod]
        public async Task GetThemeByDescriptionReturnsTwoThemes()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal12")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Java no banco
            await _repository.NewThemeAsync(new NewThemeDTO("Java"));
            //AND - E que registro JavaScript no banco
            await _repository.NewThemeAsync(new NewThemeDTO("JavaScript"));

            //WHEN - Quando que pesquiso pela descricao Java
            var themes = await _repository.GetThemeByDescriptionAsync("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, themes.Count);
        }
        
        [TestMethod]
        public async Task AlternatePythonThemeReturnsCobolTheme()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal13")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Python no banco
            await _repository.NewThemeAsync(new NewThemeDTO("Python"));

            //WHEN - Quando passo o Id 1 e a descricao COBOL
            await _repository.UpDateThemeAsync(new UpDateThemeDTO(1, "COBOL"));

            var theme = await _repository.GetThemeByIdAsync(1);

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL", theme.Description);
        }
        
        [TestMethod]
        public async Task DeleteThemesReturnsNull()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal14")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 temas no banco
            await _repository.NewThemeAsync(new NewThemeDTO("C#"));

            //WHEN - quando deleto o Id 1
            await _repository.DeleteThemeAsync(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(await _repository.GetThemeByIdAsync(1));
        }
    }
}    
    