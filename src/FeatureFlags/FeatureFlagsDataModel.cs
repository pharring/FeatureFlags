// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FeatureFlags
{
    internal class FeatureFlagsDataModel
    {
        // Validate that the feature name consists of 'word's (at least 1 \w) divided by dots, e.g. JavaScript.LanguageService.LightBulb
        private const string c_featureNameRegExPattern = @"^(\w+\.)+\w+$";

        // The value names represent the property names to use in the collection of the flags store
        private const string c_flagValueName = "Value";
        private const string c_flagDescription = "Description";

        private static readonly Regex s_featureNameRegex = new Regex(c_featureNameRegExPattern);

        private readonly FeatureFlagsStore _defaultsStore;
        private readonly WritableFeatureFlagsStore _customizationsStore;

        public FeatureFlagsDataModel(IVsSettingsManager settingsManagerService)
        {
            var settingsManager = new ShellSettingsManager(settingsManagerService);
            var settingsStore = settingsManager.GetReadOnlySettingsStore(SettingsScope.Configuration);
            var writableSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            _defaultsStore = new FeatureFlagsStore(settingsStore);
            _customizationsStore = new WritableFeatureFlagsStore(writableSettingsStore);
        }

        public bool IsFeatureEnabled(string featureName)
        {
            if (featureName == null)
            {
                throw new ArgumentNullException(nameof(featureName));
            }

            if (!s_featureNameRegex.IsMatch(featureName))
            {
                throw new ArgumentException("The feature flag should consist of words separated by dots.", "featureName");
            }

            string path = GetCollectionPath(featureName);

            return _customizationsStore.GetBoolValue(path, c_flagValueName)
                ?? _defaultsStore.GetBoolValue(path, c_flagValueName)
                ?? false;
        }

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

        public IEnumerable<FeatureFlag> GetFlags()
        {
            var flags = new List<FeatureFlag>();
            CollectFlagsRecursive("", flags);
            return flags;
        }

        private void CollectFlagsRecursive(string collectionPath, List<FeatureFlag> flags)
        {
            bool? enabled = _defaultsStore.GetBoolValue(collectionPath, c_flagValueName);
            if (enabled.HasValue)
            {
                // If an FlagValueName value exists in this collection then we assume the collection represents a leaf key
                // and we will not iterate through additional subcollections (i.e. leaves are feature flags)
                var flagName = collectionPath.Replace('\\', '.');

                // It's unlikely this could be anything other than a flag name, but validate and ignore in
                // case the collection has been corrupted by illegal paths
                if (s_featureNameRegex.IsMatch(flagName))
                {
                    var isEnabledByDefault = enabled.Value;
                    var isEnabled = _customizationsStore.GetBoolValue(collectionPath, c_flagValueName) ?? isEnabledByDefault;
                    var description = _defaultsStore.GetString(collectionPath, c_flagDescription);

                    flags.Add(new FeatureFlag(flagName, isEnabled, isEnabledByDefault, description));
                }
            }

            foreach (var subCollection in _defaultsStore.GetSubCollections(collectionPath))
            {
                var path = string.IsNullOrEmpty(collectionPath) ? subCollection : collectionPath + "\\" + subCollection;
                CollectFlagsRecursive(path, flags);
            }
        }

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