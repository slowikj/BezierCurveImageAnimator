namespace BezierCurveImageAnimator
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
            this.components = new System.ComponentModel.Container();
            this.bezierCurveLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pointsNumberTextbox = new System.Windows.Forms.TextBox();
            this.generateBezierButton = new System.Windows.Forms.Button();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.naiveRotatingButton = new System.Windows.Forms.RadioButton();
            this.filteringRotatingButton = new System.Windows.Forms.RadioButton();
            this.rotationAnimationButton = new System.Windows.Forms.RadioButton();
            this.onCurveAnimation = new System.Windows.Forms.RadioButton();
            this.startAnimationButton = new System.Windows.Forms.Button();
            this.stopAnimationButton = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.visiblePolylineCheckbox = new System.Windows.Forms.CheckBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.imageView = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageView)).BeginInit();
            this.SuspendLayout();
            // 
            // bezierCurveLabel
            // 
            this.bezierCurveLabel.AutoSize = true;
            this.bezierCurveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bezierCurveLabel.Location = new System.Drawing.Point(12, 25);
            this.bezierCurveLabel.Name = "bezierCurveLabel";
            this.bezierCurveLabel.Size = new System.Drawing.Size(107, 20);
            this.bezierCurveLabel.TabIndex = 0;
            this.bezierCurveLabel.Text = "Bezier\'s curve";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of points:";
            // 
            // pointsNumberTextbox
            // 
            this.pointsNumberTextbox.Location = new System.Drawing.Point(126, 57);
            this.pointsNumberTextbox.Name = "pointsNumberTextbox";
            this.pointsNumberTextbox.Size = new System.Drawing.Size(40, 20);
            this.pointsNumberTextbox.TabIndex = 2;
            // 
            // generateBezierButton
            // 
            this.generateBezierButton.Location = new System.Drawing.Point(107, 83);
            this.generateBezierButton.Name = "generateBezierButton";
            this.generateBezierButton.Size = new System.Drawing.Size(59, 23);
            this.generateBezierButton.TabIndex = 3;
            this.generateBezierButton.Text = "Generate";
            this.generateBezierButton.UseVisualStyleBackColor = true;
            this.generateBezierButton.Click += new System.EventHandler(this.generateBezierButton_Click);
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(114, 46);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(59, 23);
            this.loadImageButton.TabIndex = 5;
            this.loadImageButton.Text = "Load";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // naiveRotatingButton
            // 
            this.naiveRotatingButton.AutoSize = true;
            this.naiveRotatingButton.Checked = true;
            this.naiveRotatingButton.Location = new System.Drawing.Point(13, 19);
            this.naiveRotatingButton.Name = "naiveRotatingButton";
            this.naiveRotatingButton.Size = new System.Drawing.Size(53, 17);
            this.naiveRotatingButton.TabIndex = 7;
            this.naiveRotatingButton.TabStop = true;
            this.naiveRotatingButton.Text = "Naive";
            this.naiveRotatingButton.UseVisualStyleBackColor = true;
            // 
            // filteringRotatingButton
            // 
            this.filteringRotatingButton.AutoSize = true;
            this.filteringRotatingButton.Location = new System.Drawing.Point(13, 53);
            this.filteringRotatingButton.Name = "filteringRotatingButton";
            this.filteringRotatingButton.Size = new System.Drawing.Size(83, 17);
            this.filteringRotatingButton.TabIndex = 8;
            this.filteringRotatingButton.Text = "With filtering";
            this.filteringRotatingButton.UseVisualStyleBackColor = true;
            // 
            // rotationAnimationButton
            // 
            this.rotationAnimationButton.AutoSize = true;
            this.rotationAnimationButton.Checked = true;
            this.rotationAnimationButton.Location = new System.Drawing.Point(18, 31);
            this.rotationAnimationButton.Name = "rotationAnimationButton";
            this.rotationAnimationButton.Size = new System.Drawing.Size(65, 17);
            this.rotationAnimationButton.TabIndex = 10;
            this.rotationAnimationButton.TabStop = true;
            this.rotationAnimationButton.Text = "Rotation";
            this.rotationAnimationButton.UseVisualStyleBackColor = true;
            // 
            // onCurveAnimation
            // 
            this.onCurveAnimation.AutoSize = true;
            this.onCurveAnimation.Location = new System.Drawing.Point(18, 67);
            this.onCurveAnimation.Name = "onCurveAnimation";
            this.onCurveAnimation.Size = new System.Drawing.Size(123, 17);
            this.onCurveAnimation.TabIndex = 11;
            this.onCurveAnimation.Text = "Moving on the curve";
            this.onCurveAnimation.UseVisualStyleBackColor = true;
            // 
            // startAnimationButton
            // 
            this.startAnimationButton.Location = new System.Drawing.Point(24, 531);
            this.startAnimationButton.Name = "startAnimationButton";
            this.startAnimationButton.Size = new System.Drawing.Size(59, 23);
            this.startAnimationButton.TabIndex = 12;
            this.startAnimationButton.Text = "Start";
            this.startAnimationButton.UseVisualStyleBackColor = true;
            this.startAnimationButton.Click += new System.EventHandler(this.startAnimationButton_Click);
            // 
            // stopAnimationButton
            // 
            this.stopAnimationButton.Location = new System.Drawing.Point(104, 531);
            this.stopAnimationButton.Name = "stopAnimationButton";
            this.stopAnimationButton.Size = new System.Drawing.Size(59, 23);
            this.stopAnimationButton.TabIndex = 13;
            this.stopAnimationButton.Text = "Stop";
            this.stopAnimationButton.UseVisualStyleBackColor = true;
            this.stopAnimationButton.Click += new System.EventHandler(this.stopAnimationButton_Click);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(241, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(919, 700);
            this.canvas.TabIndex = 14;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // visiblePolylineCheckbox
            // 
            this.visiblePolylineCheckbox.AutoSize = true;
            this.visiblePolylineCheckbox.Checked = true;
            this.visiblePolylineCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visiblePolylineCheckbox.Location = new System.Drawing.Point(33, 117);
            this.visiblePolylineCheckbox.Name = "visiblePolylineCheckbox";
            this.visiblePolylineCheckbox.Size = new System.Drawing.Size(94, 17);
            this.visiblePolylineCheckbox.TabIndex = 15;
            this.visiblePolylineCheckbox.Text = "Visible polyline";
            this.visiblePolylineCheckbox.UseVisualStyleBackColor = true;
            this.visiblePolylineCheckbox.CheckedChanged += new System.EventHandler(this.visiblePolylineCheckbox_CheckedChanged);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.naiveRotatingButton);
            this.groupBox1.Controls.Add(this.filteringRotatingButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 304);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotating";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rotationAnimationButton);
            this.groupBox2.Controls.Add(this.onCurveAnimation);
            this.groupBox2.Location = new System.Drawing.Point(12, 410);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Animation";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.imageView);
            this.groupBox3.Controls.Add(this.loadImageButton);
            this.groupBox3.Location = new System.Drawing.Point(12, 178);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 105);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image";
            // 
            // imageView
            // 
            this.imageView.Location = new System.Drawing.Point(21, 31);
            this.imageView.Name = "imageView";
            this.imageView.Size = new System.Drawing.Size(50, 50);
            this.imageView.TabIndex = 6;
            this.imageView.TabStop = false;
            this.imageView.Paint += new System.Windows.Forms.PaintEventHandler(this.imageView_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 717);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.visiblePolylineCheckbox);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.stopAnimationButton);
            this.Controls.Add(this.startAnimationButton);
            this.Controls.Add(this.generateBezierButton);
            this.Controls.Add(this.pointsNumberTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bezierCurveLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label bezierCurveLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pointsNumberTextbox;
        private System.Windows.Forms.Button generateBezierButton;
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.RadioButton naiveRotatingButton;
        private System.Windows.Forms.RadioButton filteringRotatingButton;
        private System.Windows.Forms.RadioButton rotationAnimationButton;
        private System.Windows.Forms.RadioButton onCurveAnimation;
        private System.Windows.Forms.Button startAnimationButton;
        private System.Windows.Forms.Button stopAnimationButton;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.CheckBox visiblePolylineCheckbox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox imageView;
    }
}

