namespace Core.UI.Forms
{
    partial class PasswordChangeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.currentPasswordTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newPasswordTextbox = new System.Windows.Forms.TextBox();
            this.repeatPasswordTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(35, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Текущий пароль*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentPasswordTextbox
            // 
            this.currentPasswordTextbox.Location = new System.Drawing.Point(169, 41);
            this.currentPasswordTextbox.Name = "currentPasswordTextbox";
            this.currentPasswordTextbox.Size = new System.Drawing.Size(200, 20);
            this.currentPasswordTextbox.TabIndex = 1;
            this.currentPasswordTextbox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(35, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Новый пароль*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // newPasswordTextbox
            // 
            this.newPasswordTextbox.Location = new System.Drawing.Point(169, 67);
            this.newPasswordTextbox.Name = "newPasswordTextbox";
            this.newPasswordTextbox.Size = new System.Drawing.Size(200, 20);
            this.newPasswordTextbox.TabIndex = 3;
            this.newPasswordTextbox.UseSystemPasswordChar = true;
            // 
            // repeatPasswordTextbox
            // 
            this.repeatPasswordTextbox.Location = new System.Drawing.Point(169, 93);
            this.repeatPasswordTextbox.Name = "repeatPasswordTextbox";
            this.repeatPasswordTextbox.Size = new System.Drawing.Size(200, 20);
            this.repeatPasswordTextbox.TabIndex = 5;
            this.repeatPasswordTextbox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(35, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Повторите пароль*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Смена пароля";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.currentPasswordTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.repeatPasswordTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.newPasswordTextbox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel2.Controls.Add(this.cancelBtn);
            this.splitContainer1.Panel2.Controls.Add(this.applyBtn);
            this.splitContainer1.Size = new System.Drawing.Size(427, 196);
            this.splitContainer1.SplitterDistance = 134;
            this.splitContainer1.TabIndex = 7;
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelBtn.Location = new System.Drawing.Point(157, 15);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(105, 31);
            this.cancelBtn.TabIndex = 11;
            this.cancelBtn.TabStop = false;
            this.cancelBtn.Text = "Отменить";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.applyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.applyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.applyBtn.Location = new System.Drawing.Point(36, 15);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(105, 31);
            this.applyBtn.TabIndex = 10;
            this.applyBtn.TabStop = false;
            this.applyBtn.Text = "Применить";
            this.applyBtn.UseVisualStyleBackColor = false;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // PasswordChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 196);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PasswordChangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox currentPasswordTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newPasswordTextbox;
        private System.Windows.Forms.TextBox repeatPasswordTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button applyBtn;
    }
}