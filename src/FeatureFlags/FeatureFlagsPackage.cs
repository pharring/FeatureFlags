using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace FeatureFlags
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [Guid(PackageGuidString)]
    [ProvideOptionPage(typeof(FeatureFlagsOptionPage), "Environment", "FeatureFlags", 113, 114, false, 115)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class FeatureFlagsPackage : Package
    {
        /// <summary>
        /// FeatureFlagsPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "f35d0db7-2038-40e2-b0d0-13369844ca8c";

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlagsPackage"/> class.
        /// </summary>
        public FeatureFlagsPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Telemetry.Client.TrackEvent(nameof(FeatureFlagsPackage) + "." + nameof(Initialize), new Dictionary<string, string> { ["VSVersion"] = GetShellVersion() });
        }

#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread
        private string GetShellVersion()
        {
            if (GetService(typeof(SVsShell)) is IVsShell shell)
            {
                if (ErrorHandler.Succeeded(shell.GetProperty((int)__VSSPROPID5.VSSPROPID_ReleaseVersion, out var obj)) && obj != null)
                {
                    return obj.ToString();
                }
            }

            return "Unknown";
        }
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Telemetry.Client.Flush();
        }

        #endregion
    }
}
