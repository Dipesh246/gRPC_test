namespace GrpcServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Server = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Server
            // 
            this.Server.Location = new System.Drawing.Point(240, 45);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(215, 101);
            this.Server.TabIndex = 0;
            this.Server.Text = "Start Server";
            this.Server.UseVisualStyleBackColor = true;
            this.Server.Click += new System.EventHandler(this.Server_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 101);
            this.button1.TabIndex = 1;
            this.button1.Text = "Stop Server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Server);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "gRPC Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Server;
        private System.Windows.Forms.Button button1;
    }
}

