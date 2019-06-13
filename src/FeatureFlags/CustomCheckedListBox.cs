// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using System.Drawing;
using System.Windows.Forms;

namespace FeatureFlags
{
    internal class CustomCheckedListBox : CheckedListBox
    {
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
                var index = e.Index;
                if (index >= 0 && index < Items.Count)
                {
                    var featureFlag = (FeatureFlag)Items[index];
                    if (GetItemChecked(index) != featureFlag.IsEnabledByDefault)
                    {
                        // Changing the Font in the DrawItemEventArgs doesn't work -
                        // the underlying control always using the control's font.
                        _drawingFont = new Font(e.Font, FontStyle.Bold);
                    }
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
