namespace SelfHealing.Core.LLM
{
    public interface ILlmClient
    {
        string SuggestNewLocator(string brokenLocator, string targetElementDescription, string pageSourceSnippet);
    }
}