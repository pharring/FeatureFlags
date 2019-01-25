// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Settings;
using System;

namespace FeatureFlags
{
    internal static class SettingsStoreExtensions
    {
        public static bool TryGetBoolean(this SettingsStore store, string collectionPath, string propertyName, out bool value)
        {
            try
            {
                if (!store.PropertyExists(collectionPath, propertyName))
                {
                    value = default(bool);
                    return false;
                }

                value = store.GetBoolean(collectionPath, propertyName);
                return true;
            }
            catch (Exception ex) when (!ErrorHandler.IsCriticalException(ex))
            {
                value = default(bool);
                return false;
            }
        }
    }
}
