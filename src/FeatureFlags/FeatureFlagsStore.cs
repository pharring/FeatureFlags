using Microsoft.VisualStudio.FeatureFlags;
using Microsoft.VisualStudio.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureFlags
{
    internal class FeatureFlagsStore : IFeatureFlagsStore
    {
        private const string CollectionRoot = @"FeatureFlags\";

        private readonly SettingsStore _settingsStore;

        public FeatureFlagsStore(SettingsStore settingsStore) => this._settingsStore = settingsStore;

        public bool? GetBoolValue(string collectionPath, string name)
        {
            string featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);

            if (_settingsStore.TryGetBoolean(featureFlagsCollectionPath, name, out var value))
            {
                return value;
            }

            return null;
        }

        public IEnumerable<string> GetSubCollections(string collectionPath)
        {
            string featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);

            try
            {
                return _settingsStore.GetSubCollectionNames(featureFlagsCollectionPath);
            }
            catch (ArgumentException)
            {
                // Collection doesn't exist
                return Enumerable.Empty<string>();
            }
        }

        protected string GetFullCollectionPath(string baseCollectionPath) => CollectionRoot + baseCollectionPath;
    }
}
