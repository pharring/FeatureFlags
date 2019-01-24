using System.Drawing;
using System.Windows.Forms;

namespace FeatureFlags
{
    internal class CustomCheckedListBox : CheckedListBox
    {
        public FeatureFlagsDataModel DataModel { get; set; }

        private Font _drawingFont;

        public override Font Font
        {
            get => _drawingFont ?? base.Font;
            set => base.Font = value;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (!DesignMode)
            {
                var flagName = Items[e.Index].ToString();
                if (GetItemChecked(e.Index) != DataModel.IsFeatureEnabledByDefault(flagName))
                {
                    // Changing the Font in the DrawItemEventArgs doesn't work -
                    // the underlying control always using the control's font.
                    _drawingFont = new Font(e.Font, FontStyle.Bold);
                }
            }

            base.OnDrawItem(e);

            if (_drawingFont != null)
            {
                _drawingFont.Dispose();
                _drawingFont = null;
            }
        }
    }
}
