namespace InsureYouAI.Services
{
    public interface IAIService
    {
        Task<string> PredictCategoryAsync(string messageText);
        Task<string> PredictPriorityAsync(string messageText);
    }
}