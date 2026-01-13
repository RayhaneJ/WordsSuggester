using SuggesterApp;

var suggester = new TermSuggester();

Console.WriteLine("=== Exemple de suggestions de termes ===\n");

var terme = "gros";
var choix = new List<string> { "gros", "gras", "graisse", "agressif", "go", "ros", "gro" };

Console.WriteLine($"Terme recherché: '{terme}'");
Console.WriteLine($"Liste de choix: {string.Join(", ", choix)}");
Console.WriteLine($"Nombre de suggestions demandées: 2\n");

var resultats = suggester.GetSuggestions(terme, choix, 2);

Console.WriteLine("Résultats:");

foreach (var r in resultats)
{
    Console.WriteLine($"  - {r}");
}

Console.WriteLine("\n" + new string('-', 50) + "\n");

terme = "chat";
choix = ["chaton", "chat", "acheter", "chien", "chateau"];

Console.WriteLine($"Terme recherché: '{terme}'");
Console.WriteLine($"Liste de choix: {string.Join(", ", choix)}");
Console.WriteLine($"Nombre de suggestions demandées: 3\n");

resultats = suggester.GetSuggestions(terme, choix, 3);

Console.WriteLine("Résultats:");

foreach (var r in resultats)
{
    Console.WriteLine($"  - {r}");
}