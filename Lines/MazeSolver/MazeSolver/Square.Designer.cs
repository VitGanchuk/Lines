namespace MazeSolver {
    partial class Square {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lbWeight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbWeight
            // 
            this.lbWeight.BackColor = System.Drawing.Color.Transparent;
            this.lbWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbWeight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWeight.Location = new System.Drawing.Point(0, 0);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(60, 60);
            this.lbWeight.TabIndex = 0;
            this.lbWeight.Text = "0";
            this.lbWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Square
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbWeight);
            this.Name = "Square";
            this.Size = new System.Drawing.Size(60, 60);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbWeight;
    }
}
