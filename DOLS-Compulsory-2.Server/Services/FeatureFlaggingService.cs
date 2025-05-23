﻿using Flagsmith;

namespace DOLS_Compulsory_2.Server.Services
{
    public class FeatureFlaggingService
    {
        private readonly FlagsmithClient _flagsmithClient;
        private const string FlagsmithApiKeyEnvVar = "FLAGSMITH_API_KEY";

        public FeatureFlaggingService()
        {
            var apiKey = Environment.GetEnvironmentVariable(FlagsmithApiKeyEnvVar);
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException($"Environment variable '{FlagsmithApiKeyEnvVar}' is not set or is empty.");
            }
            _flagsmithClient = new FlagsmithClient(apiKey);
        }

        public async Task<bool> IsFeatureEnabled(string featureName)
        {
            var flags = await _flagsmithClient.GetEnvironmentFlags();
            if (flags != null) 
            {
                return await flags.IsFeatureEnabled(featureName);
            }
            return false;
        }
    }
}