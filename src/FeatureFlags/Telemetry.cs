// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace FeatureFlags
{
    internal static class Telemetry
    {
        // https://portal.azure.com/#resource/subscriptions/7222a697-c5eb-4759-9f38-221791aebc7a/resourcegroups/FeatureFlags/providers/microsoft.insights/components/Feature%20Flags/overview
        private const string c_InstrumentationKey = "16d0464b-389b-4714-89cc-19c4ad7a0382";

        private readonly static TelemetryClient _telemetryClient = CreateClient();

        public static TelemetryClient Client => _telemetryClient;

        private static TelemetryClient CreateClient()
        {
            var configuration = new TelemetryConfiguration
            {
                InstrumentationKey = c_InstrumentationKey,
                TelemetryChannel = new InMemoryChannel
                {
#if DEBUG
                    DeveloperMode = true
#else
                    DeveloperMode = false
#endif
                }
            };

            var client = new TelemetryClient(configuration);
            client.Context.User.Id = Anonymize(Environment.UserDomainName + "\\" + Environment.UserName);
            client.Context.Session.Id = Guid.NewGuid().ToString();
            client.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            client.Context.Component.Version = typeof(Telemetry).Assembly.GetName().Version.ToString();

            return client;
        }

        private static string Anonymize(string str)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                byte[] inputBytes = System.Text.Encoding.Unicode.GetBytes(str);
                byte[] hash = sha1.ComputeHash(inputBytes);
                string base64 = Convert.ToBase64String(hash);
                return base64;
            }
        }
    }
}
