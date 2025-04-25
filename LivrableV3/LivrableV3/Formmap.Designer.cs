namespace LivrableV3
{
    partial class Formmap
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
            this.gMapControlmap = new GMap.NET.WindowsForms.GMapControl();
            this.label1 = new System.Windows.Forms.Label();
            this.btnretour = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gMapControlmap
            // 
            this.gMapControlmap.Bearing = 0F;
            this.gMapControlmap.CanDragMap = true;
            this.gMapControlmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControlmap.GrayScaleMode = false;
            this.gMapControlmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControlmap.LevelsKeepInMemmory = 5;
            this.gMapControlmap.Location = new System.Drawing.Point(5, 84);
            this.gMapControlmap.MarkersEnabled = true;
            this.gMapControlmap.MaxZoom = 2;
            this.gMapControlmap.MinZoom = 2;
            this.gMapControlmap.MouseWheelZoomEnabled = true;
            this.gMapControlmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControlmap.Name = "gMapControlmap";
            this.gMapControlmap.NegativeMode = false;
            this.gMapControlmap.PolygonsEnabled = true;
            this.gMapControlmap.RetryLoadTile = 0;
            this.gMapControlmap.RoutesEnabled = true;
            this.gMapControlmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControlmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControlmap.ShowTileGridLines = false;
            this.gMapControlmap.Size = new System.Drawing.Size(771, 667);
            this.gMapControlmap.TabIndex = 0;
            this.gMapControlmap.Zoom = 0D;
            this.gMapControlmap.Load += new System.EventHandler(this.gMapControlmap_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map";
            // 
            // btnretour
            // 
            this.btnretour.Location = new System.Drawing.Point(8, 7);
            this.btnretour.Name = "btnretour";
            this.btnretour.Size = new System.Drawing.Size(96, 52);
            this.btnretour.TabIndex = 2;
            this.btnretour.Text = "Retour";
            this.btnretour.UseVisualStyleBackColor = true;
            this.btnretour.Click += new System.EventHandler(this.btnretour_Click);
            // 
            // Formmap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(785, 762);
            this.Controls.Add(this.btnretour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gMapControlmap);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Formmap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formmap";
            this.Load += new System.EventHandler(this.Formmap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControlmap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnretour;
    }
}