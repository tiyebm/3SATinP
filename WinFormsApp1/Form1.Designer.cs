namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.strInput = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.strOutput = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.varnb = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.process = new System.Windows.Forms.Button();
            this.arrangedInput = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputArray = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.solnb = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(34, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter 3-SAT formula:";
            // 
            // strInput
            // 
            this.strInput.Location = new System.Drawing.Point(190, 65);
            this.strInput.Name = "strInput";
            this.strInput.Size = new System.Drawing.Size(509, 79);
            this.strInput.TabIndex = 1;
            this.strInput.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(34, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Valid solutions:";
            // 
            // strOutput
            // 
            this.strOutput.Location = new System.Drawing.Point(190, 351);
            this.strOutput.Name = "strOutput";
            this.strOutput.Size = new System.Drawing.Size(509, 89);
            this.strOutput.TabIndex = 3;
            this.strOutput.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(34, 483);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "TIME:";
            // 
            // varnb
            // 
            this.varnb.AutoSize = true;
            this.varnb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.varnb.Location = new System.Drawing.Point(706, 193);
            this.varnb.Name = "varnb";
            this.varnb.Size = new System.Drawing.Size(18, 20);
            this.varnb.TabIndex = 5;
            this.varnb.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(745, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "literal(s).";
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(192, 483);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(0, 20);
            this.time.TabIndex = 7;
            // 
            // process
            // 
            this.process.Location = new System.Drawing.Point(706, 91);
            this.process.Name = "process";
            this.process.Size = new System.Drawing.Size(94, 29);
            this.process.TabIndex = 8;
            this.process.Text = "Process";
            this.process.UseVisualStyleBackColor = true;
            this.process.Click += new System.EventHandler(this.process_Click);
            // 
            // arrangedInput
            // 
            this.arrangedInput.Location = new System.Drawing.Point(190, 159);
            this.arrangedInput.Name = "arrangedInput";
            this.arrangedInput.Size = new System.Drawing.Size(509, 79);
            this.arrangedInput.TabIndex = 10;
            this.arrangedInput.Text = "";
            this.arrangedInput.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(34, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Arranged formula:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // inputArray
            // 
            this.inputArray.Location = new System.Drawing.Point(190, 249);
            this.inputArray.Name = "inputArray";
            this.inputArray.Size = new System.Drawing.Size(509, 89);
            this.inputArray.TabIndex = 12;
            this.inputArray.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(34, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Generated input:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(62, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(725, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "Solving 3-SAT in P, Study and Testing Program by Tiyeb Brahim Bellal, Engineer fr" +
    "om Mauritania";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(745, 383);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "solution(s).";
            // 
            // solnb
            // 
            this.solnb.AutoSize = true;
            this.solnb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.solnb.Location = new System.Drawing.Point(706, 383);
            this.solnb.Name = "solnb";
            this.solnb.Size = new System.Drawing.Size(18, 20);
            this.solnb.TabIndex = 14;
            this.solnb.Text = "0";
            // 
            // label9
            // 
            this.label9.AllowDrop = true;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "3 distinct literals clauses";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 539);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.solnb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inputArray);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.arrangedInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.process);
            this.Controls.Add(this.time);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.varnb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.strOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.strInput);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "3-SAT Testing";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private RichTextBox strInput;
        private Label label2;
        private RichTextBox strOutput;
        private Label label3;
        private Label varnb;
        private Label label4;
        private Label time;
        private Button process;
        private RichTextBox arrangedInput;
        private Label label5;
        private RichTextBox inputArray;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label solnb;
        private Label label9;
    }
}