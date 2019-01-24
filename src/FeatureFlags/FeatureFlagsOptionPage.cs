using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FeatureFlags
{
    [Guid("38300D6F-E7DE-4FFD-816D-DD8F95A300F8")]
    internal class FeatureFlagsOptionPage : DialogPage
    {
        private FeatureFlagsUserControl _page;

        private FeatureFlagsUserControl Page => _page ?? (_page = CreatePage());

        private FeatureFlagsUserControl CreatePage()
        {
            Telemetry.Client.TrackPageView("FeatureFlags");
            var featureFlagsDataModel = new FeatureFlagsDataModel(GetSettingsManagerService());
            return new FeatureFlagsUserControl(featureFlagsDataModel);
        }

        protected override IWin32Window Window => Page;

#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread
        private IVsSettingsManager GetSettingsManagerService() => (IVsSettingsManager)GetService(typeof(SVsSettingsManager));
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread

        public override void SaveSettingsToStorage() => Page.WriteChanges();

        public override void LoadSettingsFromStorage() => Page.Initialize();
    }
}
