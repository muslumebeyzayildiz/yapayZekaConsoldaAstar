namespace yapayzekaOdev1Deneme2
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonNextStep = new System.Windows.Forms.Button();
            this.labelState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonNextStep
            // 
            this.buttonNextStep.Location = new System.Drawing.Point(128, 122);
            this.buttonNextStep.Name = "buttonNextStep";
            this.buttonNextStep.Size = new System.Drawing.Size(75, 23);
            this.buttonNextStep.TabIndex = 0;
            this.buttonNextStep.Text = "buttonNextStep";
            this.buttonNextStep.UseVisualStyleBackColor = true;
            this.buttonNextStep.Click += new System.EventHandler(this.buttonNextStep_Click);
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(292, 105);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(68, 16);
            this.labelState.TabIndex = 1;
            this.labelState.Text = "labelState";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.buttonNextStep);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNextStep;
        private System.Windows.Forms.Label labelState;
    }
}

