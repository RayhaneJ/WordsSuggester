using SuggesterApp;

namespace Suggester.Tests
{
    [TestClass]
    public sealed class SuggesterTests
    {
        [TestMethod]
        public void Should_Return_Gros_And_Gras_For_Example()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "gros", "gras", "graisse", "agressif", "go", "ros", "gro" };

            var result = suggester.GetSuggestions("gros", words, 2).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("gros", result[0]);
            Assert.AreEqual("gras", result[1]);
        }

        [TestMethod]
        public void Should_Return_Exact_Match_First()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "chat", "chaton", "chien" };

            var result = suggester.GetSuggestions("chat", words, 1).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("chat", result[0]);
        }

        [TestMethod]
        public void Should_Prefer_Shorter_When_Same_Differences()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "agressif", "gras" };

            var result = suggester.GetSuggestions("gros", words, 2).ToList();

            Assert.AreEqual("gras", result[0]);
        }

        [TestMethod]
        public void Should_Sort_Alphabetically_When_Same_Length()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "gris", "gras", "gros" };

            var result = suggester.GetSuggestions("gros", words, 3).ToList();

            Assert.AreEqual("gros", result[0]);
            Assert.AreEqual("gras", result[1]);
            Assert.AreEqual("gris", result[2]);
        }

        [TestMethod]
        public void Should_Ignore_Words_Too_Short()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "go", "ros", "gro", "gros" };

            var result = suggester.GetSuggestions("gros", words, 10).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("gros", result[0]);
        }

        [TestMethod]
        public void Should_Return_Empty_For_Empty_Term()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "test", "exemple" };

            var result = suggester.GetSuggestions("", words, 5).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Should_Be_Case_Insensitive()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "GROS", "Gras", "GrAiSsE" };

            var result = suggester.GetSuggestions("gros", words, 2).ToList();

            Assert.AreEqual("GROS", result[0]);
            Assert.AreEqual("Gras", result[1]);
        }

        [TestMethod]
        public void Should_Handle_Multiple_Differences()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "table", "cable", "sable" };

            var result = suggester.GetSuggestions("table", words, 3).ToList();

            Assert.AreEqual("table", result[0]);
            Assert.AreEqual("cable", result[1]);
        }

        [TestMethod]
        public void Should_Give_Correct_Score_For_Graisse()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "gros", "gras", "graisse" };

            var result = suggester.GetSuggestions("gros", words, 3).ToList();

            Assert.AreEqual("gros", result[0]);
            Assert.AreEqual("gras", result[1]);
            Assert.AreEqual("graisse", result[2]);
        }

        [TestMethod]
        public void Should_Use_Sliding_Window_For_Agressif()
        {
            var suggester = new TermSuggester();
            var words = new List<string> { "gros", "gras", "agressif" };

            var result = suggester.GetSuggestions("gros", words, 3).ToList();

            Assert.AreEqual("gros", result[0]);
            Assert.AreEqual("gras", result[1]);
            Assert.AreEqual("agressif", result[2]);
        }
    }
}
