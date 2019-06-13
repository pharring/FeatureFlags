// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

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
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes an instance of FeatureFlag with the given name and state. FeatureFlag
        /// objects are immutable.
        /// </summary>
        /// <param name="name">The name. A string of the form ^(\w+\.)+\w+$, following a pattern of [AreaPath].[Name].</param>
        /// <param name="isEnabled">True if the feature is enabled, otherwise false.</param>
        /// <param name="isEnabledByDefault">True if the feature is be enabled by default, otherwise false.</param>
        /// <param name="description">A description of the feature. May be null.</param>
        public FeatureFlag(string name, bool isEnabled, bool isEnabledByDefault, string description = null)
        {
            Name = name;
            IsEnabled = isEnabled;
            IsEnabledByDefault = isEnabledByDefault;
            Description = description;
        }

        /// <summary>
        /// Provide a string for rendering.
        /// </summary>
        /// <returns>The name.</returns>
        public override string ToString() => Name;
    }
}
