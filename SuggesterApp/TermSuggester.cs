namespace SuggesterApp
{

    public class TermSuggester : ITermSuggester
    {
        public IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions)
        {
            if (string.IsNullOrEmpty(term))
            {
                return [];
            }

            var termLower = term.ToLower();
            var candidates = new List<(string mot, int score, int longueur)>();

            foreach (var word in choices)
            {
                var wordLower = word.ToLower();

                if (wordLower.Length < termLower.Length)
                {
                    continue;
                }
                    
                int score = GetDifferenceScore(termLower, wordLower);
                candidates.Add((word, score, wordLower.Length));
            }

            var resultats = candidates
                .OrderBy(c => c.score)
                .ThenBy(c => c.longueur)
                .ThenBy(c => c.mot)
                .Take(numberOfSuggestions)
                .Select(c => c.mot)
                .ToList();

            return resultats;
        }

        private int GetDifferenceScore(string term, string candidate)
        {
            int minDiff = int.MaxValue;

            for (int offset = 0; offset <= candidate.Length - term.Length; offset++)
            {
                int diff = 0;

                // on compare le terme avec cette fenêtre du candidat
                for (int i = 0; i < term.Length; i++)
                {
                    if (term[i] != candidate[offset + i])
                    {
                        diff++;
                    }
                }

                // on garde le meilleur score
                if (diff < minDiff)
                {
                    minDiff = diff;
                }
            }

            return minDiff;
        }
    }
}
