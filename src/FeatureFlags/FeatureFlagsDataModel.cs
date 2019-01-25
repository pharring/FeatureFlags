// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.VisualStudio.FeatureFlags;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using VsFeatureFlags = Microsoft.VisualStudio.FeatureFlags.FeatureFlags;

namespace FeatureFlags
{
    internal class FeatureFlagsDataModel
    {
        private readonly VsFeatureFlags _featureFlags;
        private readonly IFeatureFlagsStore _defaultsStore;
        private readonly IWritableFeatureFlagsStore _customizationsStore;

        public FeatureFlagsDataModel(IVsSettingsManager settingsManagerService)
        {
            var settingsManager = new ShellSettingsManager(settingsManagerService);
            var settingsStore = settingsManager.GetReadOnlySettingsStore(SettingsScope.Configuration);
            var writableSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            _defaultsStore = new FeatureFlagsStore(settingsStore);
            _customizationsStore = new WritableFeatureFlagsStore(writableSettingsStore);
            _featureFlags = new VsFeatureFlags(_defaultsStore, _customizationsStore);
        }

        public bool IsFeatureEnabled(string featureName) => _featureFlags.IsFeatureEnabled(featureName, defaultValue: false);

        public bool IsFeatureEnabledByDefault(string featureName)
        {
            var collectionPath = GetCollectionPath(featureName);
            var boolValue = _defaultsStore.GetBoolValue(collectionPath, "Value");
            if (!boolValue.HasValue)
            {
                throw new ArgumentException("Unrecognized feature name.", nameof(featureName));
            }

            return (bool)boolValue;
        }

        public IEnumerable<FeatureFlag> GetFlags() => _featureFlags.GetFlags();

        private static string GetCollectionPath(string featureName) => featureName.Replace(".", "\\");

        public void EnableFeature(string featureName, bool enabled)
        {
            var collectionPath = GetCollectionPath(featureName);
            var boolValue = _defaultsStore.GetBoolValue(collectionPath, "Value");
            if (!boolValue.HasValue)
            {
                throw new ArgumentException("Unrecognized feature name.", nameof(featureName));
            }

            if (boolValue.Value == enabled)
            {
                _customizationsStore.DeleteProperty(collectionPath, "Value");
            }
            else
            {
                _customizationsStore.SetBool(collectionPath, "Value", enabled);
            }
        }
    }
}