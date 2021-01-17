namespace ToLDeathnoteImage
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
            this.btn_draw = new System.Windows.Forms.Button();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.btn_select_image = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pb_original = new System.Windows.Forms.PictureBox();
            this.pb_converted = new System.Windows.Forms.PictureBox();
            this.btn_reset_draw_pos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_drawX = new System.Windows.Forms.TextBox();
            this.tb_drawY = new System.Windows.Forms.TextBox();
            this.clb_colors = new System.Windows.Forms.CheckedListBox();
            this.btn_colors_up = new System.Windows.Forms.Button();
            this.btn_colors_down = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_brushsize = new System.Windows.Forms.TrackBar();
            this.cb_only_selected_color = new System.Windows.Forms.CheckBox();
            this.lbl_brushsize = new System.Windows.Forms.Label();
            this.cb_smoothing = new System.Windows.Forms.CheckBox();
            this.trackBar_smooth_same_color = new System.Windows.Forms.TrackBar();
            this.trackBar_smooth_transparent = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_border_points_only = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_converted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_brushsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smooth_same_color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smooth_transparent)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_draw
            // 
            this.btn_draw.Enabled = false;
            this.btn_draw.Location = new System.Drawing.Point(297, 10);
            this.btn_draw.Name = "btn_draw";
            this.btn_draw.Size = new System.Drawing.Size(75, 23);
            this.btn_draw.TabIndex = 0;
            this.btn_draw.Text = "Draw";
            this.btn_draw.UseVisualStyleBackColor = true;
            this.btn_draw.Click += new System.EventHandler(this.btn_draw_Click);
            // 
            // tb_path
            // 
            this.tb_path.Location = new System.Drawing.Point(12, 12);
            this.tb_path.Name = "tb_path";
            this.tb_path.ReadOnly = true;
            this.tb_path.Size = new System.Drawing.Size(184, 20);
            this.tb_path.TabIndex = 1;
            // 
            // btn_select_image
            // 
            this.btn_select_image.Location = new System.Drawing.Point(202, 10);
            this.btn_select_image.Name = "btn_select_image";
            this.btn_select_image.Size = new System.Drawing.Size(82, 23);
            this.btn_select_image.TabIndex = 2;
            this.btn_select_image.Text = "Select Image";
            this.btn_select_image.UseVisualStyleBackColor = true;
            this.btn_select_image.Click += new System.EventHandler(this.btn_select_image_Click);
            // 
            // pb_original
            // 
            this.pb_original.Location = new System.Drawing.Point(12, 38);
            this.pb_original.Name = "pb_original";
            this.pb_original.Size = new System.Drawing.Size(175, 225);
            this.pb_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_original.TabIndex = 3;
            this.pb_original.TabStop = false;
            // 
            // pb_converted
            // 
            this.pb_converted.Location = new System.Drawing.Point(224, 42);
            this.pb_converted.Name = "pb_converted";
            this.pb_converted.Size = new System.Drawing.Size(350, 450);
            this.pb_converted.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_converted.TabIndex = 4;
            this.pb_converted.TabStop = false;
            // 
            // btn_reset_draw_pos
            // 
            this.btn_reset_draw_pos.Location = new System.Drawing.Point(460, 507);
            this.btn_reset_draw_pos.Name = "btn_reset_draw_pos";
            this.btn_reset_draw_pos.Size = new System.Drawing.Size(114, 23);
            this.btn_reset_draw_pos.TabIndex = 5;
            this.btn_reset_draw_pos.Text = "Reset Draw Position";
            this.btn_reset_draw_pos.UseVisualStyleBackColor = true;
            this.btn_reset_draw_pos.Click += new System.EventHandler(this.btn_reset_draw_pos_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 539);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "x:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(508, 539);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "y:";
            // 
            // tb_drawX
            // 
            this.tb_drawX.Location = new System.Drawing.Point(450, 536);
            this.tb_drawX.Name = "tb_drawX";
            this.tb_drawX.Size = new System.Drawing.Size(48, 20);
            this.tb_drawX.TabIndex = 18;
            this.tb_drawX.Text = "1";
            // 
            // tb_drawY
            // 
            this.tb_drawY.Location = new System.Drawing.Point(525, 536);
            this.tb_drawY.Name = "tb_drawY";
            this.tb_drawY.Size = new System.Drawing.Size(48, 20);
            this.tb_drawY.TabIndex = 19;
            this.tb_drawY.Text = "1";
            // 
            // clb_colors
            // 
            this.clb_colors.FormattingEnabled = true;
            this.clb_colors.Location = new System.Drawing.Point(12, 270);
            this.clb_colors.Name = "clb_colors";
            this.clb_colors.Size = new System.Drawing.Size(145, 169);
            this.clb_colors.TabIndex = 20;
            this.clb_colors.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_colors_ItemCheck);
            this.clb_colors.SelectedIndexChanged += new System.EventHandler(this.clb_colors_SelectedIndexChanged);
            // 
            // btn_colors_up
            // 
            this.btn_colors_up.Location = new System.Drawing.Point(163, 417);
            this.btn_colors_up.Name = "btn_colors_up";
            this.btn_colors_up.Size = new System.Drawing.Size(44, 23);
            this.btn_colors_up.TabIndex = 21;
            this.btn_colors_up.Text = "Up";
            this.btn_colors_up.UseVisualStyleBackColor = true;
            this.btn_colors_up.Click += new System.EventHandler(this.btn_colors_up_Click);
            // 
            // btn_colors_down
            // 
            this.btn_colors_down.Location = new System.Drawing.Point(163, 446);
            this.btn_colors_down.Name = "btn_colors_down";
            this.btn_colors_down.Size = new System.Drawing.Size(44, 23);
            this.btn_colors_down.TabIndex = 22;
            this.btn_colors_down.Text = "Down";
            this.btn_colors_down.UseVisualStyleBackColor = true;
            this.btn_colors_down.Click += new System.EventHandler(this.btn_colors_down_Click);
            // 
            // btn_update
            // 
            this.btn_update.Enabled = false;
            this.btn_update.Location = new System.Drawing.Point(163, 270);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(52, 23);
            this.btn_update.TabIndex = 23;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "BrushSize:";
            // 
            // trackBar_brushsize
            // 
            this.trackBar_brushsize.LargeChange = 1;
            this.trackBar_brushsize.Location = new System.Drawing.Point(468, 10);
            this.trackBar_brushsize.Maximum = 4;
            this.trackBar_brushsize.Minimum = 1;
            this.trackBar_brushsize.Name = "trackBar_brushsize";
            this.trackBar_brushsize.Size = new System.Drawing.Size(104, 45);
            this.trackBar_brushsize.TabIndex = 25;
            this.trackBar_brushsize.Value = 3;
            this.trackBar_brushsize.Scroll += new System.EventHandler(this.trackBar_brushsize_Scroll);
            // 
            // cb_only_selected_color
            // 
            this.cb_only_selected_color.AutoSize = true;
            this.cb_only_selected_color.Location = new System.Drawing.Point(12, 475);
            this.cb_only_selected_color.Name = "cb_only_selected_color";
            this.cb_only_selected_color.Size = new System.Drawing.Size(144, 17);
            this.cb_only_selected_color.TabIndex = 26;
            this.cb_only_selected_color.Text = "Show only selected color";
            this.cb_only_selected_color.UseVisualStyleBackColor = true;
            this.cb_only_selected_color.CheckedChanged += new System.EventHandler(this.cb_only_selected_color_CheckedChanged);
            // 
            // lbl_brushsize
            // 
            this.lbl_brushsize.AutoSize = true;
            this.lbl_brushsize.Location = new System.Drawing.Point(449, 15);
            this.lbl_brushsize.Name = "lbl_brushsize";
            this.lbl_brushsize.Size = new System.Drawing.Size(13, 13);
            this.lbl_brushsize.TabIndex = 27;
            this.lbl_brushsize.Text = "3";
            // 
            // cb_smoothing
            // 
            this.cb_smoothing.AutoSize = true;
            this.cb_smoothing.Location = new System.Drawing.Point(12, 498);
            this.cb_smoothing.Name = "cb_smoothing";
            this.cb_smoothing.Size = new System.Drawing.Size(102, 17);
            this.cb_smoothing.TabIndex = 28;
            this.cb_smoothing.Text = "Smooth Image if";
            this.cb_smoothing.UseVisualStyleBackColor = true;
            this.cb_smoothing.CheckedChanged += new System.EventHandler(this.cb_smoothing_CheckedChanged);
            // 
            // trackBar_smooth_same_color
            // 
            this.trackBar_smooth_same_color.LargeChange = 1;
            this.trackBar_smooth_same_color.Location = new System.Drawing.Point(186, 502);
            this.trackBar_smooth_same_color.Maximum = 6;
            this.trackBar_smooth_same_color.Name = "trackBar_smooth_same_color";
            this.trackBar_smooth_same_color.Size = new System.Drawing.Size(89, 45);
            this.trackBar_smooth_same_color.TabIndex = 29;
            this.trackBar_smooth_same_color.Scroll += new System.EventHandler(this.trackBar_smoothing_Scroll);
            // 
            // trackBar_smooth_transparent
            // 
            this.trackBar_smooth_transparent.LargeChange = 1;
            this.trackBar_smooth_transparent.Location = new System.Drawing.Point(186, 531);
            this.trackBar_smooth_transparent.Maximum = -1;
            this.trackBar_smooth_transparent.Minimum = -6;
            this.trackBar_smooth_transparent.Name = "trackBar_smooth_transparent";
            this.trackBar_smooth_transparent.Size = new System.Drawing.Size(89, 45);
            this.trackBar_smooth_transparent.TabIndex = 30;
            this.trackBar_smooth_transparent.Value = -6;
            this.trackBar_smooth_transparent.Scroll += new System.EventHandler(this.trackBar_smooth_transparent_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 517);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Adjacent same Color Points >=\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 545);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Adjacent Transparent Points <";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(181, 518);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(266, 518);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(181, 546);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "6";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(268, 546);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "1";
            // 
            // cb_border_points_only
            // 
            this.cb_border_points_only.AutoSize = true;
            this.cb_border_points_only.Checked = true;
            this.cb_border_points_only.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_border_points_only.Location = new System.Drawing.Point(12, 452);
            this.cb_border_points_only.Name = "cb_border_points_only";
            this.cb_border_points_only.Size = new System.Drawing.Size(137, 17);
            this.cb_border_points_only.TabIndex = 37;
            this.cb_border_points_only.Text = "Draw only border points";
            this.cb_border_points_only.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 564);
            this.Controls.Add(this.cb_border_points_only);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar_smooth_transparent);
            this.Controls.Add(this.trackBar_smooth_same_color);
            this.Controls.Add(this.cb_smoothing);
            this.Controls.Add(this.lbl_brushsize);
            this.Controls.Add(this.cb_only_selected_color);
            this.Controls.Add(this.trackBar_brushsize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_colors_down);
            this.Controls.Add(this.btn_colors_up);
            this.Controls.Add(this.clb_colors);
            this.Controls.Add(this.tb_drawY);
            this.Controls.Add(this.tb_drawX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_reset_draw_pos);
            this.Controls.Add(this.pb_converted);
            this.Controls.Add(this.pb_original);
            this.Controls.Add(this.btn_select_image);
            this.Controls.Add(this.tb_path);
            this.Controls.Add(this.btn_draw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_converted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_brushsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smooth_same_color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smooth_transparent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_draw;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Button btn_select_image;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pb_original;
        private System.Windows.Forms.PictureBox pb_converted;
        private System.Windows.Forms.Button btn_reset_draw_pos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_drawX;
        private System.Windows.Forms.TextBox tb_drawY;
        private System.Windows.Forms.CheckedListBox clb_colors;
        private System.Windows.Forms.Button btn_colors_up;
        private System.Windows.Forms.Button btn_colors_down;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar_brushsize;
        private System.Windows.Forms.CheckBox cb_only_selected_color;
        private System.Windows.Forms.Label lbl_brushsize;
        private System.Windows.Forms.CheckBox cb_smoothing;
        private System.Windows.Forms.TrackBar trackBar_smooth_same_color;
        private System.Windows.Forms.TrackBar trackBar_smooth_transparent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cb_border_points_only;
    }
}

