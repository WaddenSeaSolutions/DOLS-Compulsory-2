using Flagsmith;

namespace DOLS_Compulsory_2.Server.Services
{
    public class FeatureFlaggingService
    {

static FlagsmithClient _flagsmithClient;
        private const string FlagsmithApiKeyEnvVar = "FLAGSMITH_API_KEY";

        public FeatureFlaggingService()
        {
            var apiKey = Environment.GetEnvironmentVariable(FlagsmithApiKeyEnvVar);
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException($"Enviorment variable not set '{FlagsmithApiKeyEnvVar}' ");
            }
            _flagsmithClient = new FlagsmithClient(apiKey);
        }

        public async Task<bool> IsFeatureEnabled(string featureName)
        {
            var flags = await _flagsmithClient.GetEnvironmentFlags();
            var isEnabled = await flags.IsFeatureEnabled(featureName);
            return isEnabled;
        }
    }
}
