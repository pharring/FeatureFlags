// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;

namespace FeatureFlags
{
    /// <summary>
    /// Contains metadata and state information for a feature flag.
    /// </summary>
    internal class FeatureFlag
    {
        /// <summary>
        /// Gets a flag saying whether the feature is enabled.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Gets a flag saying whether the feature is enabled by default.
        /// </summary>
        public bool IsEnabledByDefault { get; }

        /// <summary>
        /// Gets the name. The name is a string of the form ^(\w+\.)+\w+$,
        /// following a pattern of [AreaPath].[Name].
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the (non-localized) description. May be null.
        /// Also, it may be a reference to a package string. (e.g. #1001)
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Optional package GUID. If <see cref="Description"/> is a package
        /// string, then this is the GUID of the corresponding package that
        /// can be used to fetch the actual string.
        /// </summary>
        public Guid? PackageGuid { get; }

        /// <summary>
        /// Initializes an instance of FeatureFlag with the given name and state. FeatureFlag
        /// objects are immutable.
        /// </summary>
        /// <param name="name">The name. A string of the form ^(\w+\.)+\w+$, following a pattern of [AreaPath].[Name].</param>
        /// <param name="isEnabled">True if the feature is enabled, otherwise false.</param>
        /// <param name="isEnabledByDefault">True if the feature is be enabled by default, otherwise false.</param>
        /// <param name="description">A description of the feature. May be null.</param>
        /// <param name="packageGuid">Optional package GUID, if present.</param>
        public FeatureFlag(string name, bool isEnabled, bool isEnabledByDefault, string description = null, Guid? packageGuid = null)
        {
            Name = name;
            IsEnabled = isEnabled;
            IsEnabledByDefault = isEnabledByDefault;
            Description = description;
            PackageGuid = packageGuid;
        }

        /// <summary>
        /// Provide a string for rendering.
        /// </summary>
        /// <returns>The name.</returns>
        public override string ToString() => Name;

        /// <summary>
        /// If the description is a package string, then extract the resource ID and package GUID.
        /// </summary>
        /// <param name="resourceId">The resource ID</param>
        /// <param name="packageGuid">The package GUID</param>
        /// <returns>True if the description is actually a package string.</returns>
        public bool TryParseDescriptionResourceId(out uint resourceId, out Guid packageGuid)
        {
            resourceId = default;
            if (!PackageGuid.HasValue)
            {
                packageGuid = default;
                return false;
            }

            packageGuid = PackageGuid.Value;
            string description = Description;
            return description?.Length > 1
                && (description[0] == '#' || description[0] == '@')
                && uint.TryParse(description.Substring(1), out resourceId);
        }
    }
}
