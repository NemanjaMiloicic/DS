namespace April25Client
{
    partial class Form1
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
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.puta = new System.Windows.Forms.Button();
            this.podeljeno = new System.Windows.Forms.Button();
            this.izraz = new System.Windows.Forms.TextBox();
            this.rezultatTb = new System.Windows.Forms.TextBox();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // plus
            // 
            this.plus.Location = new System.Drawing.Point(46, 148);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(75, 23);
            this.plus.TabIndex = 0;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // minus
            // 
            this.minus.Location = new System.Drawing.Point(167, 148);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(75, 23);
            this.minus.TabIndex = 0;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // puta
            // 
            this.puta.Location = new System.Drawing.Point(296, 148);
            this.puta.Name = "puta";
            this.puta.Size = new System.Drawing.Size(75, 23);
            this.puta.TabIndex = 0;
            this.puta.Text = "*";
            this.puta.UseVisualStyleBackColor = true;
            this.puta.Click += new System.EventHandler(this.puta_Click);
            // 
            // podeljeno
            // 
            this.podeljeno.Location = new System.Drawing.Point(439, 148);
            this.podeljeno.Name = "podeljeno";
            this.podeljeno.Size = new System.Drawing.Size(75, 23);
            this.podeljeno.TabIndex = 0;
            this.podeljeno.Text = "/";
            this.podeljeno.UseVisualStyleBackColor = true;
            this.podeljeno.Click += new System.EventHandler(this.podeljeno_Click);
            // 
            // izraz
            // 
            this.izraz.Location = new System.Drawing.Point(46, 42);
            this.izraz.Name = "izraz";
            this.izraz.Size = new System.Drawing.Size(504, 22);
            this.izraz.TabIndex = 1;
            // 
            // rezultatTb
            // 
            this.rezultatTb.Location = new System.Drawing.Point(659, 42);
            this.rezultatTb.Name = "rezultatTb";
            this.rezultatTb.Size = new System.Drawing.Size(100, 22);
            this.rezultatTb.TabIndex = 1;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(46, 89);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.rezultatTb);
            this.Controls.Add(this.izraz);
            this.Controls.Add(this.podeljeno);
            this.Controls.Add(this.puta);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.plus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button puta;
        private System.Windows.Forms.Button podeljeno;
        private System.Windows.Forms.TextBox izraz;
        private System.Windows.Forms.TextBox rezultatTb;
        private System.Windows.Forms.NumericUpDown numericUpDown;
    }
}

