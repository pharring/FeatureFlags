// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.VisualStudio.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureFlags
{
    internal class FeatureFlagsStore
    {
        private const string c_collectionRoot = @"FeatureFlags\";

        private readonly SettingsStore _settingsStore;

        public FeatureFlagsStore(SettingsStore settingsStore) => this._settingsStore = settingsStore;

        public bool? GetBoolValue(string collectionPath, string name)
        {
            var featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);
            if (_settingsStore.TryGetBoolean(featureFlagsCollectionPath, name, out var value))
            {
                return value;
            }

            return null;
        }

        public IEnumerable<string> GetSubCollections(string collectionPath)
        {

            try
            {
                var featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);
                return _settingsStore.GetSubCollectionNames(featureFlagsCollectionPath);
            }
            catch (ArgumentException)
            {
                // Collection doesn't exist
                return Enumerable.Empty<string>();
            }
        }

        public string GetString(string collectionPath, string name)
        {
            try
            {
                var featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);
                return _settingsStore.GetString(featureFlagsCollectionPath, name);
            }
            catch (ArgumentException)
            {
                // Property doesn't exist.
                return null;
            }
        }

        protected string GetFullCollectionPath(string baseCollectionPath) => c_collectionRoot + baseCollectionPath;
    }
}
