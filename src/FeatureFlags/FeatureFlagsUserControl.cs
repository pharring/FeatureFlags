using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FeatureFlags
{
    internal partial class FeatureFlagsUserControl : UserControl
    {
        private readonly FeatureFlagsDataModel _dataModel;

        public FeatureFlagsUserControl(FeatureFlagsDataModel dataModel)
        {
            _dataModel = dataModel;
            InitializeComponent();
            allFeatureFlagsListBox.DataModel = _dataModel;
            warningIcon.Image = SystemIcons.Warning.ToBitmap();
        }

        public void Initialize()
        {
            allFeatureFlagsListBox.Items.Clear();
            foreach (var featureFlag in _dataModel.GetFlags())
            {
                allFeatureFlagsListBox.Items.Add(featureFlag.Name, featureFlag.IsEnabled);
            }
        }

        public void WriteChanges()
        {
            for (int i = 0; i < allFeatureFlagsListBox.Items.Count; i++)
            {
                var featureName = allFeatureFlagsListBox.Items[i].ToString();
                bool currentSetting = _dataModel.IsFeatureEnabled(featureName);
                bool desiredSetting = allFeatureFlagsListBox.GetItemChecked(i);
                if (currentSetting != desiredSetting)
                {
                    _dataModel.EnableFeature(featureName, desiredSetting);
                    Telemetry.Client.TrackEvent("FeatureFlagChanged", new Dictionary<string, string> { ["FeatureName"] = featureName, ["Enabled"] = desiredSetting.ToString() });
                }
            }
        }

        private void OnResetAllButtonClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < allFeatureFlagsListBox.Items.Count; i++)
            {
                var featureName = allFeatureFlagsListBox.Items[i].ToString();
                allFeatureFlagsListBox.SetItemChecked(i, _dataModel.IsFeatureEnabledByDefault(featureName));
            }

            Telemetry.Client.TrackEvent("ResetAllButtonClicked");
        }
    }
}
