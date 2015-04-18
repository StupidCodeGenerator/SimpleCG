namespace CGE
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxImages = new System.Windows.Forms.ListBox();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.pictureBoxFrameList = new System.Windows.Forms.PictureBox();
            this.buttonAddImage = new System.Windows.Forms.Button();
            this.buttonRemoveImage = new System.Windows.Forms.Button();
            this.buttonAddKeyFrame = new System.Windows.Forms.Button();
            this.buttonRemoveFrame = new System.Windows.Forms.Button();
            this.pictureBoxImagePreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxScreenWidth = new System.Windows.Forms.TextBox();
            this.textBoxScreenHeight = new System.Windows.Forms.TextBox();
            this.buttonRefreshPictureBox = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.listBoxCurrentFrame = new System.Windows.Forms.ListBox();
            this.buttonAddImageToFrame = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxImageSituation = new System.Windows.Forms.GroupBox();
            this.buttonRefreshImageSituation = new System.Windows.Forms.Button();
            this.textBoxAlpha = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRotate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxScaleY = new System.Windows.Forms.TextBox();
            this.textBoxScaleX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAlpha = new System.Windows.Forms.CheckBox();
            this.checkBoxRotate = new System.Windows.Forms.CheckBox();
            this.checkBoxIsScale = new System.Windows.Forms.CheckBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.textBoxFPS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonSetReferenceFrame = new System.Windows.Forms.Button();
            this.buttonClearReferenceFrame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrameList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePreview)).BeginInit();
            this.groupBoxImageSituation.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxImages
            // 
            this.listBoxImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxImages.FormattingEnabled = true;
            this.listBoxImages.ItemHeight = 12;
            this.listBoxImages.Location = new System.Drawing.Point(12, 36);
            this.listBoxImages.Name = "listBoxImages";
            this.listBoxImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxImages.Size = new System.Drawing.Size(159, 292);
            this.listBoxImages.TabIndex = 0;
            this.listBoxImages.SelectedIndexChanged += new System.EventHandler(this.listBoxImages_SelectedIndexChanged);
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMain.BackColor = System.Drawing.Color.Black;
            this.pictureBoxMain.Location = new System.Drawing.Point(357, 36);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(671, 434);
            this.pictureBoxMain.TabIndex = 1;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.Click += new System.EventHandler(this.pictureBoxMain_Click);
            this.pictureBoxMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMain_Paint);
            this.pictureBoxMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseDown);
            this.pictureBoxMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseMove);
            this.pictureBoxMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseUp);
            // 
            // pictureBoxFrameList
            // 
            this.pictureBoxFrameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxFrameList.BackColor = System.Drawing.Color.Black;
            this.pictureBoxFrameList.Location = new System.Drawing.Point(357, 476);
            this.pictureBoxFrameList.Name = "pictureBoxFrameList";
            this.pictureBoxFrameList.Size = new System.Drawing.Size(671, 150);
            this.pictureBoxFrameList.TabIndex = 2;
            this.pictureBoxFrameList.TabStop = false;
            this.pictureBoxFrameList.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFrameList_Paint);
            this.pictureBoxFrameList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFrameList_MouseDown);
            this.pictureBoxFrameList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFrameList_MouseMove);
            this.pictureBoxFrameList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFrameList_MouseUp);
            // 
            // buttonAddImage
            // 
            this.buttonAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddImage.Location = new System.Drawing.Point(15, 634);
            this.buttonAddImage.Name = "buttonAddImage";
            this.buttonAddImage.Size = new System.Drawing.Size(225, 26);
            this.buttonAddImage.TabIndex = 3;
            this.buttonAddImage.Text = "导入图片";
            this.buttonAddImage.UseVisualStyleBackColor = true;
            this.buttonAddImage.Click += new System.EventHandler(this.buttonAddImage_Click);
            // 
            // buttonRemoveImage
            // 
            this.buttonRemoveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveImage.Location = new System.Drawing.Point(15, 666);
            this.buttonRemoveImage.Name = "buttonRemoveImage";
            this.buttonRemoveImage.Size = new System.Drawing.Size(225, 26);
            this.buttonRemoveImage.TabIndex = 4;
            this.buttonRemoveImage.Text = "删除图片";
            this.buttonRemoveImage.UseVisualStyleBackColor = true;
            this.buttonRemoveImage.Click += new System.EventHandler(this.buttonRemoveImage_Click);
            // 
            // buttonAddKeyFrame
            // 
            this.buttonAddKeyFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddKeyFrame.Location = new System.Drawing.Point(924, 663);
            this.buttonAddKeyFrame.Name = "buttonAddKeyFrame";
            this.buttonAddKeyFrame.Size = new System.Drawing.Size(104, 32);
            this.buttonAddKeyFrame.TabIndex = 5;
            this.buttonAddKeyFrame.Text = "添加关键帧";
            this.buttonAddKeyFrame.UseVisualStyleBackColor = true;
            this.buttonAddKeyFrame.Click += new System.EventHandler(this.buttonAddKeyFrame_Click);
            // 
            // buttonRemoveFrame
            // 
            this.buttonRemoveFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveFrame.Location = new System.Drawing.Point(924, 628);
            this.buttonRemoveFrame.Name = "buttonRemoveFrame";
            this.buttonRemoveFrame.Size = new System.Drawing.Size(104, 32);
            this.buttonRemoveFrame.TabIndex = 6;
            this.buttonRemoveFrame.Text = "删除关键帧";
            this.buttonRemoveFrame.UseVisualStyleBackColor = true;
            this.buttonRemoveFrame.Click += new System.EventHandler(this.buttonRemoveFrame_Click);
            // 
            // pictureBoxImagePreview
            // 
            this.pictureBoxImagePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxImagePreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxImagePreview.Location = new System.Drawing.Point(15, 391);
            this.pictureBoxImagePreview.Name = "pictureBoxImagePreview";
            this.pictureBoxImagePreview.Size = new System.Drawing.Size(324, 235);
            this.pictureBoxImagePreview.TabIndex = 7;
            this.pictureBoxImagePreview.TabStop = false;
            this.pictureBoxImagePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxImagePreview_Paint);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 640);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "屏幕尺寸";
            // 
            // textBoxScreenWidth
            // 
            this.textBoxScreenWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenWidth.Location = new System.Drawing.Point(387, 637);
            this.textBoxScreenWidth.Name = "textBoxScreenWidth";
            this.textBoxScreenWidth.Size = new System.Drawing.Size(71, 21);
            this.textBoxScreenWidth.TabIndex = 9;
            // 
            // textBoxScreenHeight
            // 
            this.textBoxScreenHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenHeight.Location = new System.Drawing.Point(464, 637);
            this.textBoxScreenHeight.Name = "textBoxScreenHeight";
            this.textBoxScreenHeight.Size = new System.Drawing.Size(71, 21);
            this.textBoxScreenHeight.TabIndex = 10;
            // 
            // buttonRefreshPictureBox
            // 
            this.buttonRefreshPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRefreshPictureBox.Location = new System.Drawing.Point(330, 665);
            this.buttonRefreshPictureBox.Name = "buttonRefreshPictureBox";
            this.buttonRefreshPictureBox.Size = new System.Drawing.Size(205, 26);
            this.buttonRefreshPictureBox.TabIndex = 11;
            this.buttonRefreshPictureBox.Text = "刷新";
            this.buttonRefreshPictureBox.UseVisualStyleBackColor = true;
            this.buttonRefreshPictureBox.Click += new System.EventHandler(this.buttonRefreshPictureBox_Click);
            // 
            // listBoxCurrentFrame
            // 
            this.listBoxCurrentFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxCurrentFrame.FormattingEnabled = true;
            this.listBoxCurrentFrame.ItemHeight = 12;
            this.listBoxCurrentFrame.Location = new System.Drawing.Point(177, 36);
            this.listBoxCurrentFrame.Name = "listBoxCurrentFrame";
            this.listBoxCurrentFrame.Size = new System.Drawing.Size(147, 148);
            this.listBoxCurrentFrame.TabIndex = 13;
            this.listBoxCurrentFrame.SelectedIndexChanged += new System.EventHandler(this.listBoxCurrentFrame_SelectedIndexChanged);
            // 
            // buttonAddImageToFrame
            // 
            this.buttonAddImageToFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddImageToFrame.Location = new System.Drawing.Point(12, 346);
            this.buttonAddImageToFrame.Name = "buttonAddImageToFrame";
            this.buttonAddImageToFrame.Size = new System.Drawing.Size(159, 32);
            this.buttonAddImageToFrame.TabIndex = 14;
            this.buttonAddImageToFrame.Text = ">>";
            this.buttonAddImageToFrame.UseVisualStyleBackColor = true;
            this.buttonAddImageToFrame.Click += new System.EventHandler(this.buttonAddImageToFrame_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(177, 346);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(159, 32);
            this.buttonDelete.TabIndex = 15;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "Y:";
            // 
            // groupBoxImageSituation
            // 
            this.groupBoxImageSituation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxImageSituation.Controls.Add(this.buttonRefreshImageSituation);
            this.groupBoxImageSituation.Controls.Add(this.textBoxAlpha);
            this.groupBoxImageSituation.Controls.Add(this.label3);
            this.groupBoxImageSituation.Controls.Add(this.label5);
            this.groupBoxImageSituation.Controls.Add(this.textBoxRotate);
            this.groupBoxImageSituation.Controls.Add(this.label4);
            this.groupBoxImageSituation.Controls.Add(this.textBoxScaleY);
            this.groupBoxImageSituation.Controls.Add(this.textBoxScaleX);
            this.groupBoxImageSituation.Controls.Add(this.label2);
            this.groupBoxImageSituation.Controls.Add(this.checkBoxAlpha);
            this.groupBoxImageSituation.Controls.Add(this.checkBoxRotate);
            this.groupBoxImageSituation.Controls.Add(this.checkBoxIsScale);
            this.groupBoxImageSituation.Location = new System.Drawing.Point(177, 190);
            this.groupBoxImageSituation.Name = "groupBoxImageSituation";
            this.groupBoxImageSituation.Size = new System.Drawing.Size(174, 145);
            this.groupBoxImageSituation.TabIndex = 27;
            this.groupBoxImageSituation.TabStop = false;
            this.groupBoxImageSituation.Text = "IMAGE_SITUATION";
            // 
            // buttonRefreshImageSituation
            // 
            this.buttonRefreshImageSituation.Location = new System.Drawing.Point(6, 119);
            this.buttonRefreshImageSituation.Name = "buttonRefreshImageSituation";
            this.buttonRefreshImageSituation.Size = new System.Drawing.Size(159, 22);
            this.buttonRefreshImageSituation.TabIndex = 39;
            this.buttonRefreshImageSituation.Text = "FUCK";
            this.buttonRefreshImageSituation.UseVisualStyleBackColor = true;
            this.buttonRefreshImageSituation.Click += new System.EventHandler(this.buttonRefreshImageSituation_Click);
            // 
            // textBoxAlpha
            // 
            this.textBoxAlpha.Location = new System.Drawing.Point(60, 92);
            this.textBoxAlpha.Name = "textBoxAlpha";
            this.textBoxAlpha.Size = new System.Drawing.Size(105, 21);
            this.textBoxAlpha.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "透明度:";
            // 
            // textBoxRotate
            // 
            this.textBoxRotate.Location = new System.Drawing.Point(60, 69);
            this.textBoxRotate.Name = "textBoxRotate";
            this.textBoxRotate.Size = new System.Drawing.Size(105, 21);
            this.textBoxRotate.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "旋转角度:";
            // 
            // textBoxScaleY
            // 
            this.textBoxScaleY.Location = new System.Drawing.Point(118, 45);
            this.textBoxScaleY.Name = "textBoxScaleY";
            this.textBoxScaleY.Size = new System.Drawing.Size(47, 21);
            this.textBoxScaleY.TabIndex = 32;
            // 
            // textBoxScaleX
            // 
            this.textBoxScaleX.Location = new System.Drawing.Point(52, 45);
            this.textBoxScaleX.Name = "textBoxScaleX";
            this.textBoxScaleX.Size = new System.Drawing.Size(46, 21);
            this.textBoxScaleX.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "缩放 X:";
            // 
            // checkBoxAlpha
            // 
            this.checkBoxAlpha.AutoSize = true;
            this.checkBoxAlpha.Location = new System.Drawing.Point(114, 20);
            this.checkBoxAlpha.Name = "checkBoxAlpha";
            this.checkBoxAlpha.Size = new System.Drawing.Size(48, 16);
            this.checkBoxAlpha.TabIndex = 29;
            this.checkBoxAlpha.Text = "半透";
            this.checkBoxAlpha.UseVisualStyleBackColor = true;
            this.checkBoxAlpha.CheckedChanged += new System.EventHandler(this.checkBoxAlpha_CheckedChanged);
            // 
            // checkBoxRotate
            // 
            this.checkBoxRotate.AutoSize = true;
            this.checkBoxRotate.Location = new System.Drawing.Point(60, 20);
            this.checkBoxRotate.Name = "checkBoxRotate";
            this.checkBoxRotate.Size = new System.Drawing.Size(48, 16);
            this.checkBoxRotate.TabIndex = 28;
            this.checkBoxRotate.Text = "旋转";
            this.checkBoxRotate.UseVisualStyleBackColor = true;
            this.checkBoxRotate.CheckedChanged += new System.EventHandler(this.checkBoxRotate_CheckedChanged);
            // 
            // checkBoxIsScale
            // 
            this.checkBoxIsScale.AutoSize = true;
            this.checkBoxIsScale.Location = new System.Drawing.Point(6, 20);
            this.checkBoxIsScale.Name = "checkBoxIsScale";
            this.checkBoxIsScale.Size = new System.Drawing.Size(48, 16);
            this.checkBoxIsScale.TabIndex = 27;
            this.checkBoxIsScale.Text = "缩放";
            this.checkBoxIsScale.UseVisualStyleBackColor = true;
            this.checkBoxIsScale.CheckedChanged += new System.EventHandler(this.checkBoxIsScale_CheckedChanged);
            // 
            // buttonBuild
            // 
            this.buttonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBuild.Location = new System.Drawing.Point(544, 632);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(144, 26);
            this.buttonBuild.TabIndex = 28;
            this.buttonBuild.Text = "生成";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRun.Location = new System.Drawing.Point(544, 664);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(144, 29);
            this.buttonRun.TabIndex = 29;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // textBoxFPS
            // 
            this.textBoxFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFPS.Location = new System.Drawing.Point(718, 637);
            this.textBoxFPS.Name = "textBoxFPS";
            this.textBoxFPS.Size = new System.Drawing.Size(71, 21);
            this.textBoxFPS.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(689, 640);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "FPS";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(12, 5);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(140, 25);
            this.buttonExport.TabIndex = 32;
            this.buttonExport.Text = "导出数据";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(158, 5);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(140, 25);
            this.buttonImport.TabIndex = 33;
            this.buttonImport.Text = "导入数据";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearAll.Location = new System.Drawing.Point(888, 5);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(140, 25);
            this.buttonClearAll.TabIndex = 34;
            this.buttonClearAll.Text = "清空";
            this.buttonClearAll.UseVisualStyleBackColor = true;
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(330, 36);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(21, 55);
            this.buttonMoveUp.TabIndex = 35;
            this.buttonMoveUp.Text = "▲";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(330, 97);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(21, 55);
            this.buttonMoveDown.TabIndex = 36;
            this.buttonMoveDown.Text = "▼";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonSetReferenceFrame
            // 
            this.buttonSetReferenceFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetReferenceFrame.Location = new System.Drawing.Point(814, 628);
            this.buttonSetReferenceFrame.Name = "buttonSetReferenceFrame";
            this.buttonSetReferenceFrame.Size = new System.Drawing.Size(104, 32);
            this.buttonSetReferenceFrame.TabIndex = 37;
            this.buttonSetReferenceFrame.Text = "设定参考帧";
            this.buttonSetReferenceFrame.UseVisualStyleBackColor = true;
            this.buttonSetReferenceFrame.Click += new System.EventHandler(this.buttonSetReferenceFrame_Click);
            // 
            // buttonClearReferenceFrame
            // 
            this.buttonClearReferenceFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearReferenceFrame.Location = new System.Drawing.Point(814, 664);
            this.buttonClearReferenceFrame.Name = "buttonClearReferenceFrame";
            this.buttonClearReferenceFrame.Size = new System.Drawing.Size(104, 32);
            this.buttonClearReferenceFrame.TabIndex = 38;
            this.buttonClearReferenceFrame.Text = "清除参考帧";
            this.buttonClearReferenceFrame.UseVisualStyleBackColor = true;
            this.buttonClearReferenceFrame.Click += new System.EventHandler(this.buttonClearReferenceFrame_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 703);
            this.Controls.Add(this.buttonClearReferenceFrame);
            this.Controls.Add(this.buttonSetReferenceFrame);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.buttonClearAll);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.textBoxFPS);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.groupBoxImageSituation);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAddImageToFrame);
            this.Controls.Add(this.listBoxCurrentFrame);
            this.Controls.Add(this.buttonRefreshPictureBox);
            this.Controls.Add(this.textBoxScreenHeight);
            this.Controls.Add(this.textBoxScreenWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxImagePreview);
            this.Controls.Add(this.buttonRemoveFrame);
            this.Controls.Add(this.buttonAddKeyFrame);
            this.Controls.Add(this.buttonRemoveImage);
            this.Controls.Add(this.buttonAddImage);
            this.Controls.Add(this.pictureBoxFrameList);
            this.Controls.Add(this.pictureBoxMain);
            this.Controls.Add(this.listBoxImages);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "CGE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMain_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrameList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePreview)).EndInit();
            this.groupBoxImageSituation.ResumeLayout(false);
            this.groupBoxImageSituation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       






        #endregion

        private System.Windows.Forms.ListBox listBoxImages;
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.PictureBox pictureBoxFrameList;
        private System.Windows.Forms.Button buttonAddImage;
        private System.Windows.Forms.Button buttonRemoveImage;
        private System.Windows.Forms.Button buttonAddKeyFrame;
        private System.Windows.Forms.Button buttonRemoveFrame;
        private System.Windows.Forms.PictureBox pictureBoxImagePreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxScreenWidth;
        private System.Windows.Forms.TextBox textBoxScreenHeight;
        private System.Windows.Forms.Button buttonRefreshPictureBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ListBox listBoxCurrentFrame;
        private System.Windows.Forms.Button buttonAddImageToFrame;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxImageSituation;
        private System.Windows.Forms.TextBox textBoxAlpha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRotate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxScaleY;
        private System.Windows.Forms.TextBox textBoxScaleX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAlpha;
        private System.Windows.Forms.CheckBox checkBoxRotate;
        private System.Windows.Forms.CheckBox checkBoxIsScale;
        private System.Windows.Forms.Button buttonRefreshImageSituation;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.TextBox textBoxFPS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonSetReferenceFrame;
        private System.Windows.Forms.Button buttonClearReferenceFrame;
    }
}

