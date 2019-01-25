// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.FeatureFlags;
using Microsoft.VisualStudio.Settings;
using System;

namespace FeatureFlags
{
    internal sealed class WritableFeatureFlagsStore : FeatureFlagsStore, IWritableFeatureFlagsStore
    {
        private WritableSettingsStore _settingsStore;

        public WritableFeatureFlagsStore(WritableSettingsStore settingsStore) : base(settingsStore) => this._settingsStore = settingsStore;

        public void DeleteProperty(string collectionPath, string name)
        {
            string featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);

            _settingsStore.DeleteProperty(featureFlagsCollectionPath, name);
        }

        public void SetBool(string collectionPath, string name, bool enabled)
        {
            string featureFlagsCollectionPath = GetFullCollectionPath(collectionPath);

            try
            {
                _settingsStore.CreateCollection(featureFlagsCollectionPath);
                _settingsStore.SetBoolean(featureFlagsCollectionPath, name, enabled);
            }
            catch (Exception ex) when (!ErrorHandler.IsCriticalException(ex))
            {
            }
        }
    }
}
