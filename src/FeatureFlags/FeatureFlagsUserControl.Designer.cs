namespace FeatureFlags
{
    partial class FeatureFlagsUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.warningIcon = new System.Windows.Forms.PictureBox();
            this.resetAllButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.allFeatureFlagsListBox = new FeatureFlags.CustomCheckedListBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.warningIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(38, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Warning: These options control experimental or unfinished parts of the product an" +
    "d might destabilize your IDE experience.";
            // 
            // warningIcon
            // 
            this.warningIcon.Location = new System.Drawing.Point(0, 0);
            this.warningIcon.Name = "warningIcon";
            this.warningIcon.Size = new System.Drawing.Size(32, 32);
            this.warningIcon.TabIndex = 3;
            this.warningIcon.TabStop = false;
            // 
            // resetAllButton
            // 
            this.resetAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetAllButton.Location = new System.Drawing.Point(321, 39);
            this.resetAllButton.Name = "resetAllButton";
            this.resetAllButton.Size = new System.Drawing.Size(75, 23);
            this.resetAllButton.TabIndex = 4;
            this.resetAllButton.Text = "&Reset All";
            this.resetAllButton.UseVisualStyleBackColor = true;
            this.resetAllButton.Click += new System.EventHandler(this.OnResetAllButtonClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bolded items have been customized from their defaults.";
            // 
            // allFeatureFlagsListBox
            // 
            this.allFeatureFlagsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.allFeatureFlagsListBox.CheckOnClick = true;
            this.allFeatureFlagsListBox.FormattingEnabled = true;
            this.allFeatureFlagsListBox.Location = new System.Drawing.Point(0, 68);
            this.allFeatureFlagsListBox.Name = "allFeatureFlagsListBox";
            this.allFeatureFlagsListBox.Size = new System.Drawing.Size(399, 154);
            this.allFeatureFlagsListBox.TabIndex = 2;
            this.allFeatureFlagsListBox.SelectedIndexChanged += new System.EventHandler(this.AllFeatureFlagsListBox_SelectedIndexChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.Location = new System.Drawing.Point(0, 225);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(396, 41);
            this.descriptionLabel.TabIndex = 6;
            // 
            // FeatureFlagsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetAllButton);
            this.Controls.Add(this.warningIcon);
            this.Controls.Add(this.allFeatureFlagsListBox);
            this.Controls.Add(this.label1);
            this.Name = "FeatureFlagsUserControl";
            this.Size = new System.Drawing.Size(399, 266);
            ((System.ComponentModel.ISupportInitialize)(this.warningIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private CustomCheckedListBox allFeatureFlagsListBox;
        private System.Windows.Forms.PictureBox warningIcon;
        private System.Windows.Forms.Button resetAllButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label descriptionLabel;
    }
}
