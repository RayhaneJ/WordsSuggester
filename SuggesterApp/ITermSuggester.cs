namespace SuggesterApp
{
    public interface ITermSuggester
    {
        IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions);
    }
}
