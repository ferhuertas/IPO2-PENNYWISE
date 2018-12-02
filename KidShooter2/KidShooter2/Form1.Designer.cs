namespace KidShooter2
{
    partial class KidShooter
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerGameLoop = new System.Windows.Forms.Timer(this.components);
            this.timerFin = new System.Windows.Forms.Timer(this.components);
            this.labelCentral = new System.Windows.Forms.Label();
            this.labelTuto = new System.Windows.Forms.Label();
            this.labelTuto2 = new System.Windows.Forms.Label();
            this.progressBarTiempo = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerGameLoop
            // 
            this.timerGameLoop.Tick += new System.EventHandler(this.timerGameLoop_Tick);
            // 
            // timerFin
            // 
            this.timerFin.Interval = 1000;
            this.timerFin.Tick += new System.EventHandler(this.timerFin_Tick);
            // 
            // labelCentral
            // 
            this.labelCentral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCentral.AutoSize = true;
            this.labelCentral.BackColor = System.Drawing.Color.Transparent;
            this.labelCentral.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCentral.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelCentral.Location = new System.Drawing.Point(413, 296);
            this.labelCentral.Name = "labelCentral";
            this.labelCentral.Size = new System.Drawing.Size(0, 93);
            this.labelCentral.TabIndex = 0;
            // 
            // labelTuto
            // 
            this.labelTuto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTuto.AutoSize = true;
            this.labelTuto.BackColor = System.Drawing.Color.Transparent;
            this.labelTuto.Font = new System.Drawing.Font("Century Gothic", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTuto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelTuto.Location = new System.Drawing.Point(103, 489);
            this.labelTuto.Name = "labelTuto";
            this.labelTuto.Size = new System.Drawing.Size(702, 44);
            this.labelTuto.TabIndex = 1;
            this.labelTuto.Text = "Necesitas comida, ¡es hora de cazar!";
            // 
            // labelTuto2
            // 
            this.labelTuto2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTuto2.AutoSize = true;
            this.labelTuto2.BackColor = System.Drawing.Color.Transparent;
            this.labelTuto2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTuto2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelTuto2.Location = new System.Drawing.Point(61, 546);
            this.labelTuto2.Name = "labelTuto2";
            this.labelTuto2.Size = new System.Drawing.Size(765, 69);
            this.labelTuto2.TabIndex = 2;
            this.labelTuto2.Text = "Georgie intentará escapar de tu captura, disparale para conseguir alimento, \r\ntie" +
    "nes 10 segundos, el numero de comida dependera de tu punteria.\r\n ¡Buena suerte!\r" +
    "\n";
            this.labelTuto2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBarTiempo
            // 
            this.progressBarTiempo.Location = new System.Drawing.Point(171, 9);
            this.progressBarTiempo.Maximum = 15;
            this.progressBarTiempo.Name = "progressBarTiempo";
            this.progressBarTiempo.Size = new System.Drawing.Size(202, 23);
            this.progressBarTiempo.Step = 1;
            this.progressBarTiempo.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTiempo.TabIndex = 3;
            this.progressBarTiempo.Value = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tiempo restante:\r\n";
            // 
            // KidShooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::KidShooter2.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(848, 641);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarTiempo);
            this.Controls.Add(this.labelTuto2);
            this.Controls.Add(this.labelTuto);
            this.Controls.Add(this.labelCentral);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(866, 688);
            this.MinimumSize = new System.Drawing.Size(866, 688);
            this.Name = "KidShooter";
            this.Text = "KID HUNTER";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.KidShooter_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KidShooter_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerGameLoop;
        private System.Windows.Forms.Timer timerFin;
        private System.Windows.Forms.Label labelCentral;
        private System.Windows.Forms.Label labelTuto;
        private System.Windows.Forms.Label labelTuto2;
        private System.Windows.Forms.ProgressBar progressBarTiempo;
        private System.Windows.Forms.Label label1;
    }
}

