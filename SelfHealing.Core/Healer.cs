using SelfHealing.Core.LLM;

namespace SelfHealing.Core
{
    public class Healer
    {
        private readonly ILlmClient _llmClient;

        public Healer(ILlmClient llmClient)
        {
            _llmClient = llmClient;
        }

        public string HealLocator(string brokenLocator, string description, string pageSource)
        {
            string snippet = pageSource.Length > 2000 ? pageSource.Substring(0, 2000) : pageSource;
            return _llmClient.SuggestNewLocator(brokenLocator, description, snippet);
        }
    }
}