using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CGE
{
    public partial class FormMain : Form
    {
        public int TIME_LINE_STEP = 10;

        // 屏幕尺寸
        public int screenWidth = 480;
        public int screenHeight = 320;

        public Bitmap previewImage; // 图片预览窗口中的预览图像
        public Dictionary<string, Bitmap> images = null; // 文件名, 图片对象
        public Dictionary<string, string> imageNames = null; // 用来保存图片的时候用
        public Dictionary<int, FrameData> keyFrames = null; // 关键帧Dic
        public FrameData[] allFrames = null; // 生成后的所有帧
        public FrameData referenceFrame = null; // 参考帧

        public ImageSituation selectedImageSituation = null;

        public int currentFrameIndex = 0; // 当前帧位置
        public int selectedFrameStart = -2; // 选择帧起始位置
        public int selectedFrameEnd = -2; // 选择帧结束位置

        public int frameListOffsetX; // 显示的X方向偏移

        public static FormMain instance = null;
        public FormMain()
        {
            instance = this;
            InitializeComponent();
            keyFrames = new Dictionary<int, FrameData>();
            images = new Dictionary<string, Bitmap>();
            imageNames = new Dictionary<string, string>();
            currentFrameBasedEnable();
            textBoxFPS.Text = "30";
            groupBoxImageSituationEnable();
            buttonRunEnable();
            buttonBuildEnable();
            screenWidth = 480;
            screenHeight = 320;
            refreshAll();
        }

        // ____ENABLES_____________________________________________________________________________

        public void currentFrameBasedEnable()
        {
            bool __enable = keyFrames.ContainsKey(currentFrameIndex);
            buttonAddImageToFrame.Enabled = __enable;
            buttonRemoveFrame.Enabled = __enable;
            listBoxCurrentFrame.Enabled = __enable;
            buttonDelete.Enabled = __enable;
            buttonSetReferenceFrame.Enabled = __enable;
        }

        public void groupBoxImageSituationEnable()
        {
            groupBoxImageSituation.Enabled = (listBoxCurrentFrame.SelectedItem != null);
            textBoxScaleX.Enabled = checkBoxIsScale.Checked;
            textBoxScaleY.Enabled = checkBoxIsScale.Checked;
            textBoxRotate.Enabled = checkBoxRotate.Checked;
            textBoxAlpha.Enabled = checkBoxRotate.Checked;
        }

        public void buttonRunEnable()
        {
            bool __result = false;
            if (allFrames != null)
                if (allFrames.Length > 0)
                    __result = true;
            buttonRun.Enabled = __result;
        }

        public void buttonBuildEnable()
        {
            bool __result = false;
            if (keyFrames != null)
                if (keyFrames.Count > 0)
                    __result = true;
            buttonBuild.Enabled = __result;
        }

        // ____PICTURE_BOXEX_______________________________________________________________________

        // 帧列表的绘制
        // 所需参数: 当前帧, 关键帧[], 选择区间[2]
        void pictureBoxFrameList_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 绘制一条时间轴, "-"为基底, 每5格显示一个"+", 用"[]"来表示已选的区间, 用"."表示边界外
            // 用"*"表示关键帧(做成红色能好些)
            for (int i = 0; i * TIME_LINE_STEP < pictureBoxFrameList.Width - frameListOffsetX; i++)
            {
                if (i == (selectedFrameStart - 1))
                {
                    drawFuck(g, "[", TIME_LINE_STEP * i, 20);
                }
                else if (i == (selectedFrameEnd + 1))
                {
                    drawFuck(g, "]", TIME_LINE_STEP * i, 20);
                }
                else if (i % 5 != 0)
                {
                    drawFuck(g, "-", TIME_LINE_STEP * i, 20);
                }
                else
                {
                    drawFuck(g, "+", TIME_LINE_STEP * i, 20);
                }
                if (i % 50 == 0)
                {
                    drawFuck(g, "|" + i, TIME_LINE_STEP * i, 10);
                }
                if (keyFrames.ContainsKey(i))
                {
                    drawRedFuck(g, "K", TIME_LINE_STEP * i, 20);
                }
            }

            // 然后需要用一个"^"表示出当前帧
            if (currentFrameIndex >= 0 && currentFrameIndex * TIME_LINE_STEP < pictureBoxFrameList.Width - frameListOffsetX)
            {
                drawFuck(g, "^", currentFrameIndex * TIME_LINE_STEP, 35);
            }

            // 绘制文字信息:
            drawStayFuck(g, "TIME_LINE", 0, 60);
            drawStayFuck(g, "使用右键拖放整个区域, 鼠标左键选择帧", 0, 80);
            drawStayFuck(g, "当前帧:" + currentFrameIndex + "; 选区[" + selectedFrameStart + "," + selectedFrameEnd + "]", 0, 100);
        }

        public void drawFuck(Graphics _g, string _fuck, int _x, int _y)
        {
            _g.DrawString(_fuck, new Font("Consolas", 12), new SolidBrush(Color.Lime), _x + frameListOffsetX, _y);
        }

        public void drawRedFuck(Graphics _g, string _fuck, int _x, int _y)
        {
            _g.DrawString(_fuck, new Font("Consolas", 12), new SolidBrush(Color.Red), _x + frameListOffsetX, _y);
        }

        public void drawStayFuck(Graphics _g, string _fuck, int _x, int _y)
        {
            _g.DrawString(_fuck, new Font("Consolas", 12), new SolidBrush(Color.Yellow), _x, _y);
        }

        public int LEFT_frameListMouseDownX = -1, LEFT_frameListMouseDownY = -1;
        public int LEFT_frameListMouseUpX = -1, LEFT_frameListMouseUpY = -1;
        public int LEFT_frameListMouseMoveX = -1, LEFT_frameListMouseMoveY = -1;

        public int RIGHT_frameListMouseDownX = -1, RIGHT_frameListMouseDownY = -1;
        public int RIGHT_frameListMouseUpX = -1, RIGHT_frameListMouseUpY = -1;
        public int RIGHT_frameListMouseMoveX = -1, RIGHT_frameListMouseMoveY = -1;

        // 在按下, 抬起, 移动的过程中, 当前帧都被改变为已选帧的最后一帧. 但是, 不能使用已选的最后一帧来
        // 指代当前帧, 因为播放的时候当前帧会随着播放进度前进

        // 右键的功能主要是决定帧上限, 

        void pictureBoxFrameList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LEFT_frameListMouseDownX = e.X;
                LEFT_frameListMouseDownY = e.Y;

                LEFT_frameListMouseDownX -= frameListOffsetX;


                selectedFrameEnd = selectedFrameStart = LEFT_frameListMouseDownX / TIME_LINE_STEP; // 选择范围的确定(开始)
                changeCurrentFrameIndex(selectedFrameEnd); // 当前帧
                pictureBoxFrameList.Refresh(); // 刷新
            }
            else if (e.Button == MouseButtons.Right)
            {
                RIGHT_frameListMouseDownX = e.X;
                RIGHT_frameListMouseDownY = e.Y;

                RIGHT_frameListMouseMoveX = e.X;
                RIGHT_frameListMouseMoveY = e.Y;

                RIGHT_frameListMouseDownX -= frameListOffsetX;

                pictureBoxFrameList.Refresh(); // 刷新
            }
        }

        void pictureBoxFrameList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (LEFT_frameListMouseDownX >= 0 && LEFT_frameListMouseDownY >= 0) // 前提 : 鼠标按下再处理. 鼠标不按下处理没有意义
                {
                    LEFT_frameListMouseMoveX = e.X;
                    LEFT_frameListMouseMoveY = e.Y;

                    LEFT_frameListMouseMoveX -= frameListOffsetX;

                    selectedFrameEnd = LEFT_frameListMouseMoveX / TIME_LINE_STEP;  // 选择范围的确定(MOVE)
                    if (selectedFrameEnd < selectedFrameStart)
                        selectedFrameEnd = selectedFrameStart;

                    changeCurrentFrameIndex(selectedFrameEnd); // 当前帧

                    pictureBoxFrameList.Refresh();   // 刷新
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (RIGHT_frameListMouseDownX >= 0 && RIGHT_frameListMouseDownY >= 0)
                {
                    int __deltaX = e.X - RIGHT_frameListMouseMoveX;

                    frameListOffsetX += __deltaX;
                    pictureBoxFrameList.Refresh();
                    if (frameListOffsetX > 0)
                        frameListOffsetX = 0;

                    RIGHT_frameListMouseMoveX = e.X;
                    RIGHT_frameListMouseMoveY = e.Y;
                }
            }
        }

        void pictureBoxFrameList_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LEFT_frameListMouseUpX = e.X;
                LEFT_frameListMouseUpY = e.Y;

                LEFT_frameListMouseUpX -= frameListOffsetX;

                // 选择范围的确定(最终)
                selectedFrameEnd = LEFT_frameListMouseUpX / TIME_LINE_STEP;
                if (selectedFrameEnd < selectedFrameStart)
                    selectedFrameEnd = selectedFrameStart;

                // 当前帧
                changeCurrentFrameIndex(selectedFrameEnd);

                // 清空鼠标数据
                LEFT_frameListMouseDownX = LEFT_frameListMouseUpX = -1;
                LEFT_frameListMouseDownY = LEFT_frameListMouseUpY = -1;
                LEFT_frameListMouseMoveX = LEFT_frameListMouseMoveY = -1;

                // 刷新
                pictureBoxFrameList.Refresh();

                // Enable
                currentFrameBasedEnable();

            }
            else if (e.Button == MouseButtons.Right)
            {
                RIGHT_frameListMouseUpX = e.X;
                RIGHT_frameListMouseUpY = e.Y;

                // 清空鼠标数据
                RIGHT_frameListMouseDownX = RIGHT_frameListMouseUpX = -1;
                RIGHT_frameListMouseDownY = RIGHT_frameListMouseUpY = -1;
                RIGHT_frameListMouseMoveX = RIGHT_frameListMouseMoveY = -1;
            }
        }

        // 主显示区的绘制
        // 绘制ImageSituation
        void pictureBoxMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics __g = e.Graphics;

            // 参考帧
            if (referenceFrame != null)
            {
                referenceFrame.paint(__g);
            }

            if (keyFrames.ContainsKey(currentFrameIndex))
            {
                // 关键帧
                FrameData __currentKeyFrame = keyFrames[currentFrameIndex];
                __currentKeyFrame.paint(e.Graphics);
                // 绘制当前已选的ImageSituation
                if (selectedImageSituation != null)
                {
                    if (selectedImageSituation.image != null)
                    {
                        int __x = (int)selectedImageSituation.x;
                        int __y = (int)selectedImageSituation.y;
                        int __w = selectedImageSituation.image.Width;
                        int __h = selectedImageSituation.image.Height;
                        __g.DrawRectangle(new Pen(Color.Red), new Rectangle(__x, __y, __w, __h));
                    }
                }
            }
            else
            {
                // 非关键帧
                if (allFrames != null)
                {
                    if (currentFrameIndex < allFrames.Length)
                    {
                        FrameData __currentFrame = allFrames[currentFrameIndex];
                        __currentFrame.paint(e.Graphics);
                    }
                }
            }

            // 绘制屏幕大小
            e.Graphics.DrawRectangle(new Pen(Color.Yellow), new Rectangle(0, 0, screenWidth, screenHeight));
            drawFuck(e.Graphics, screenWidth + "," + screenHeight, screenWidth, screenHeight);
        }

        void pictureBoxImagePreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (previewImage != null)
                e.Graphics.DrawImage(previewImage, 0, 0);
        }

        // pictureBoxMain中的鼠标操作主要是用来移动图片的

        public int LEFT_mainMouseDownX = -1, LEFT_mainMouseDownY = -1;
        public int LEFT_mainMouseUpX = -1, LEFT_mainMouseUpY = -1;
        public int LEFT_mainMouseMoveX = -1, LEFT_mainMouseMoveY = -1;

        void pictureBoxMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LEFT_mainMouseDownX = e.X;
                LEFT_mainMouseDownY = e.Y;
                LEFT_mainMouseMoveX = e.X;
                LEFT_mainMouseMoveY = e.Y;
            }
        }

        void pictureBoxMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LEFT_mainMouseUpX = e.X;
                LEFT_mainMouseUpY = e.Y;
                LEFT_mainMouseDownX = -1;
                LEFT_mainMouseDownY = -1;
                LEFT_mainMouseMoveX = -1;
                LEFT_mainMouseMoveY = -1;
            }
        }

        void pictureBoxMain_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedImageSituation != null)
                {
                    // 移动图片
                    int __deltaX = e.X - LEFT_mainMouseMoveX;
                    int __deltaY = e.Y - LEFT_mainMouseMoveY;
                    selectedImageSituation.x += __deltaX;
                    selectedImageSituation.y += __deltaY;
                    pictureBoxMain.Refresh();
                }
                LEFT_mainMouseMoveX = e.X;
                LEFT_mainMouseMoveY = e.Y;
            }
        }


        // 更改当前帧要刷新帧预览中的图像
        private void changeCurrentFrameIndex(int _newIndex)
        {
            currentFrameIndex = _newIndex;
            pictureBoxMain.Refresh();
            pictureBoxFrameList.Refresh();
            selectedImageSituation = null;
            refreshListBoxCurrentFrame();
        }



        // ____REFRESH_____________________________________________________________________________
        // 刷新图像资源列表
        public void refreshListBoxImages()
        {
            listBoxImages.Items.Clear();
            if (images != null)
            {
                string[] __keys = images.Keys.ToArray<string>();
                for (int i = 0; i < __keys.Length; i++)
                {
                    listBoxImages.Items.Add(__keys[i]);
                }
            }
        }

        // 刷新当前关键帧的图像列表
        public void refreshListBoxCurrentFrame()
        {
            listBoxCurrentFrame.Items.Clear();
            if (keyFrames != null)
            {
                if (keyFrames.ContainsKey(currentFrameIndex))
                {
                    ArrayList __imageSituations = keyFrames[currentFrameIndex].imageSituations;
                    if (__imageSituations != null)
                    {
                        for (int i = 0; i < __imageSituations.Count; i++)
                        {
                            listBoxCurrentFrame.Items.Add(((ImageSituation)__imageSituations[i]).name);
                        }
                    }
                }
            }
        }

        // 刷新所有控件
        public void refreshAll()
        {
            refreshListBoxImages();
            pictureBoxMain.Refresh();
            pictureBoxFrameList.Refresh();
            pictureBoxImagePreview.Refresh();
            textBoxScreenWidth.Text = screenWidth.ToString();
            textBoxScreenHeight.Text = screenHeight.ToString();
            refreshListBoxCurrentFrame();
            if (selectedImageSituation != null)
            {
                textBoxAlpha.Text = selectedImageSituation.getAlpha().ToString();
                textBoxRotate.Text = selectedImageSituation.rotate.ToString();
                textBoxScaleX.Text = selectedImageSituation.scaleX.ToString();
                textBoxScaleY.Text = selectedImageSituation.scaleY.ToString();
                checkBoxIsScale.Checked = selectedImageSituation.isScale;
                checkBoxRotate.Checked = selectedImageSituation.isRotate;
                checkBoxAlpha.Checked = selectedImageSituation.isAlpha;
            }
            else
            {
                textBoxAlpha.Text = "1.0";
                textBoxRotate.Text = "0";
                textBoxScaleX.Text = "1.0";
                textBoxScaleY.Text = "1.0";
                checkBoxIsScale.Checked = false;
                checkBoxRotate.Checked = false;
                checkBoxAlpha.Checked = false;
            }

            textBoxFPS.Text = "30";

            if (isTimerRunning)
            {
                stopTimer();
            }

            currentFrameBasedEnable();
            groupBoxImageSituationEnable();
            buttonRunEnable();
            buttonBuildEnable();
        }

        // ____BUTTONS_____________________________________________________________________________
        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "图片文件(*.png;*.PNG)|*.png;*.PNG";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] __fileNames = openFileDialog.FileNames;
                for (int i = 0; i < __fileNames.Length; i++)
                {
                    string __key = Path.GetFileName(__fileNames[i]);
                    if (!images.ContainsKey(__key))
                        images.Add(__key, new Bitmap(__fileNames[i]));
                    if (!imageNames.ContainsKey(__key))
                        imageNames.Add(__key, __fileNames[i]);
                }
            }
            refreshListBoxImages();
        }

        private void buttonRemoveImage_Click(object sender, EventArgs e)
        {
            if (listBoxImages.SelectedItem != null)
            {
                string __key = listBoxImages.SelectedItem.ToString();
                if (images.ContainsKey(__key))
                {
                    images.Remove(__key);
                    previewImage = null;
                    refreshListBoxImages();
                }
            }
        }

        private void buttonRefreshPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.screenWidth = int.Parse(textBoxScreenWidth.Text.ToString());
                this.screenHeight = int.Parse(textBoxScreenHeight.Text.ToString());
            }
            catch
            {
                MessageBox.Show("输入格式有误");
            }
            pictureBoxFrameList.Refresh();
            pictureBoxMain.Refresh();
        }

        private void buttonRemoveFrame_Click(object sender, EventArgs e)
        {
            for (int i = selectedFrameStart; i <= selectedFrameEnd; i++)
            {
                if (keyFrames.ContainsKey(i))
                    keyFrames.Remove(i);
            }
            pictureBoxFrameList.Refresh();
            refreshListBoxCurrentFrame();
            pictureBoxMain.Refresh();
            currentFrameBasedEnable();
        }

        // 添加关键帧
        private void buttonAddKeyFrame_Click(object sender, EventArgs e)
        {
            if (!keyFrames.ContainsKey(currentFrameIndex))
            {
                keyFrames.Add(currentFrameIndex, new FrameData());
                pictureBoxFrameList.Refresh();
                currentFrameBasedEnable();
            }
            buttonBuildEnable();
        }

        // 向当前帧添加图片
        private void buttonAddImageToFrame_Click(object sender, EventArgs e)
        {
            if (keyFrames.ContainsKey(currentFrameIndex) && listBoxImages.SelectedItems != null)
            {
                for (int i = 0; i < listBoxImages.SelectedItems.Count; i++)
                {
                    FrameData __currentFrameData = keyFrames[currentFrameIndex];
                    __currentFrameData.addImageSituation(
                        listBoxImages.SelectedItems[i].ToString(),
                        50, 50,
                        false, false, false,
                        1, 1, 0, 1
                        );
                }

                pictureBoxMain.Refresh();
                refreshListBoxCurrentFrame();
            }
        }

        // 编译其他的帧
        // 在改变了关键帧以后, 需要进行编译, 根据关键帧生成其他的帧
        public void buildFrames()
        {

        }

        private void buttonRefreshImageSituation_Click(object sender, EventArgs e)
        {
            if (selectedImageSituation != null)
            {
                selectedImageSituation.isScale = checkBoxIsScale.Checked;
                selectedImageSituation.isRotate = checkBoxRotate.Checked;
                selectedImageSituation.isAlpha = checkBoxAlpha.Checked;

                try
                {
                    if (checkBoxIsScale.Checked)
                    {
                        selectedImageSituation.scaleX = float.Parse(textBoxScaleX.Text.ToString());
                        selectedImageSituation.scaleY = float.Parse(textBoxScaleY.Text.ToString());
                    }
                    if (checkBoxRotate.Checked)
                    {
                        selectedImageSituation.rotate = float.Parse(textBoxRotate.Text.ToString());
                    }
                    if (checkBoxAlpha.Checked)
                    {
                        selectedImageSituation.setAlpha(float.Parse(textBoxAlpha.Text.ToString()));
                    }
                }
                catch
                {
                    MessageBox.Show("输入格式有误");
                }
            }
            pictureBoxMain.Refresh();
        }

        // 上移当前帧中的图片
        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (keyFrames.ContainsKey(currentFrameIndex) && listBoxCurrentFrame.SelectedItem != null)
            {
                int __selectedIndex = listBoxCurrentFrame.SelectedIndex;
                if (__selectedIndex > 0)
                {
                    keyFrames[currentFrameIndex].imageSituationMoveUp(__selectedIndex);
                    refreshListBoxCurrentFrame();
                    listBoxCurrentFrame.SelectedIndex = __selectedIndex - 1;
                    pictureBoxMain.Refresh();
                }
            }
        }

        // 下移当前帧的图片
        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (keyFrames.ContainsKey(currentFrameIndex) && listBoxCurrentFrame.SelectedItem != null)
            {
                int __selectedIndex = listBoxCurrentFrame.SelectedIndex;
                if (__selectedIndex < listBoxCurrentFrame.Items.Count - 1)
                {
                    keyFrames[currentFrameIndex].imageSituationMoveDown(__selectedIndex);
                    refreshListBoxCurrentFrame();
                    listBoxCurrentFrame.SelectedIndex = __selectedIndex - 1;
                    pictureBoxMain.Refresh();
                }
            }
        }

        // 设定参考帧
        private void buttonSetReferenceFrame_Click(object sender, EventArgs e)
        {
            if (keyFrames != null)
            {
                if (keyFrames.ContainsKey(currentFrameIndex))
                {
                    referenceFrame = (keyFrames[currentFrameIndex]).getReferenceFrame();
                    pictureBoxFrameList.Refresh();
                    pictureBoxMain.Refresh();
                }
            }
        }

        // 清除参考帧
        private void buttonClearReferenceFrame_Click(object sender, EventArgs e)
        {
            referenceFrame = null;
            pictureBoxMain.Refresh();
            pictureBoxFrameList.Refresh();
        }


        // ____LISTBOXES___________________________________________________________________________
        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxImages.SelectedItem != null)
            {
                string __key = listBoxImages.SelectedItem.ToString();
                if (images.ContainsKey(__key))
                {
                    previewImage = images[__key];
                }
                pictureBoxImagePreview.Refresh();
            }
        }

        // 选择关键帧中的图片
        private void listBoxCurrentFrame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (keyFrames.ContainsKey(currentFrameIndex) && listBoxCurrentFrame.SelectedItem != null)
            {
                FrameData __currentFrame = keyFrames[currentFrameIndex];
                selectedImageSituation = __currentFrame.getImageSituation(listBoxCurrentFrame.SelectedIndex);
                pictureBoxMain.Refresh();
            }
            else
            {
                selectedImageSituation = null;
            }
            // refresh ImageStuation Edit
            groupBoxImageSituationEnable();
        }

        private void pictureBoxMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxCurrentFrame.SelectedItem != null && keyFrames.ContainsKey(currentFrameIndex))
            {
                FrameData __currentFrame = keyFrames[currentFrameIndex];
                __currentFrame.removeImageSituation(listBoxCurrentFrame.SelectedIndex);
                selectedImageSituation = null;
                refreshListBoxCurrentFrame();
                if (listBoxCurrentFrame.Items.Count > 0)
                {
                    listBoxCurrentFrame.SelectedIndex = 0;
                }
                pictureBoxMain.Refresh();
            }
        }

        // ____CHECKBOXES__________________________________________________________________________
        private void checkBoxIsScale_CheckedChanged(object sender, EventArgs e)
        {
            textBoxScaleX.Enabled = checkBoxIsScale.Checked;
            textBoxScaleY.Enabled = checkBoxIsScale.Checked;
        }

        private void checkBoxRotate_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRotate.Enabled = checkBoxRotate.Checked;
        }

        private void checkBoxAlpha_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAlpha.Enabled = checkBoxAlpha.Checked;
        }

        // ____BUILD_&_RUN_________________________________________________________________________
        private void buttonBuild_Click(object sender, EventArgs e)
        {
            // 根据关键帧, 计算出一个帧序列.
            // 帧序列从0开始到最后一个关键帧结束.
            // 然后, 根据关键帧之间的变化, 在帧之间进行渐变, 渐变的量主要有位置, 旋转, 缩放以及透明度.

            // 首先应该根据关键帧将整个帧区间裁成以关键帧为节点的若干个小区间, 分别构造数组. 
            // 然后, 填充这些小数组的内容, 最后再将它们合在一起. 
            int[] __keyFrameKeys = keyFrames.Keys.ToArray<int>();
            FrameData[][] __frameDataArrays = new FrameData[__keyFrameKeys.Length - 1][];
            Array.Sort<int>(__keyFrameKeys);
            for (int i = 0; i < __frameDataArrays.Length; i++)
            {
                int __startIndex = i;
                int __endIndex = i + 1;
                int __startKey = __keyFrameKeys[__startIndex];
                int __endKey = __keyFrameKeys[__endIndex];
                FrameData __startKeyFrame = keyFrames[__startKey];
                FrameData __endKeyFrame = keyFrames[__endKey];
                // 给一个开区间
                int __length = __endKey - __startKey - 1;
                __frameDataArrays[i] = generateFrom2KeyFrames(__startKeyFrame, __endKeyFrame, __length);
            }
            // 合并过程
            ArrayList __finalResult = new ArrayList();
            int __startFrameIndex = __keyFrameKeys[0]; // 获取第一个关键帧
            for (int i = 0; i < __startFrameIndex; i++)
            {
                __finalResult.Add(new FrameData()); // 在第一个关键帧的前几帧, 添加空的帧数据
            }
            // 添加第一个关键帧
            __finalResult.Add(keyFrames[__startFrameIndex]);
            for (int i = 0; i < __frameDataArrays.Length; i++)
            {
                for (int j = 0; j < __frameDataArrays[i].Length; j++)
                {
                    __finalResult.Add(__frameDataArrays[i][j]);
                }
                __finalResult.Add(keyFrames[__keyFrameKeys[i + 1]]);
            }
            allFrames = new FrameData[__finalResult.Count];
            for (int i = 0; i < allFrames.Length; i++)
            {
                allFrames[i] = (FrameData)__finalResult[i];
            }

            buttonRunEnable();
        }

        // 首先要检查有没有相同的image, 然后计算这些image的差
        // length 传入的是起始关键帧和结束关键帧为界的开区间的长
        public FrameData[] generateFrom2KeyFrames(FrameData _startKeyFrame, FrameData _endKeyFrame, int length)
        {
            if (length > 0)
            {
                // 检查相同的image
                ArrayList __sameImageSituationNames = new ArrayList();
                for (int i = 0; i < _startKeyFrame.imageSituations.Count; i++)
                {
                    if (_endKeyFrame.containsImageSituation(_startKeyFrame.getImageSituation(i).name))
                    {
                        __sameImageSituationNames.Add(_startKeyFrame.getImageSituation(i).name);
                    }
                }
                // 计算image之间的差
                Dictionary<string, float> __deltaXs = new Dictionary<string, float>();
                Dictionary<string, float> __deltaYs = new Dictionary<string, float>();
                Dictionary<string, float> __deltaScaleXs = new Dictionary<string, float>();
                Dictionary<string, float> __deltaScaleYs = new Dictionary<string, float>();
                Dictionary<string, float> __deltaRotates = new Dictionary<string, float>();
                Dictionary<string, float> __deltaAlphas = new Dictionary<string, float>();

                Dictionary<string, float> __startXs = new Dictionary<string, float>();
                Dictionary<string, float> __startYs = new Dictionary<string, float>();
                Dictionary<string, float> __startScaleXs = new Dictionary<string, float>();
                Dictionary<string, float> __startScaleYs = new Dictionary<string, float>();
                Dictionary<string, float> __startRotates = new Dictionary<string, float>();
                Dictionary<string, float> __startAlphas = new Dictionary<string, float>();

                for (int i = 0; i < __sameImageSituationNames.Count; i++)
                {
                    string __name = (string)__sameImageSituationNames[i];

                    float __startX = _startKeyFrame.getImageSituation(__name).x;
                    float __startY = _startKeyFrame.getImageSituation(__name).y;
                    float __startScaleX = _startKeyFrame.getImageSituation(__name).scaleX;
                    float __startScaleY = _startKeyFrame.getImageSituation(__name).scaleY;
                    float __startRotate = _startKeyFrame.getImageSituation(__name).rotate;
                    float __startAlpha = _startKeyFrame.getImageSituation(__name).getAlpha();

                    float __deltaX = (_endKeyFrame.getImageSituation(__name).x - __startX) / (length + 1);
                    float __deltaY = (_endKeyFrame.getImageSituation(__name).y - __startY) / (length + 1);
                    float __deltaScaleX = (_endKeyFrame.getImageSituation(__name).scaleX - __startScaleX) / (length + 1);
                    float __deltaScaleY = (_endKeyFrame.getImageSituation(__name).scaleY - __startScaleY) / (length + 1);
                    float __deltaRotate = (_endKeyFrame.getImageSituation(__name).rotate - __startRotate) / (length + 1);
                    float __deltaAlpha = (_endKeyFrame.getImageSituation(__name).getAlpha() - __startAlpha) / (length + 1);

                    __startXs.Add(__name, __startX);
                    __startYs.Add(__name, __startY);
                    __startScaleXs.Add(__name, __startScaleX);
                    __startScaleYs.Add(__name, __startScaleY);
                    __startRotates.Add(__name, __startRotate);
                    __startAlphas.Add(__name, __startAlpha);

                    __deltaXs.Add(__name, __deltaX);
                    __deltaYs.Add(__name, __deltaY);
                    __deltaScaleXs.Add(__name, __deltaScaleX);
                    __deltaScaleYs.Add(__name, __deltaScaleY);
                    __deltaRotates.Add(__name, __deltaRotate);
                    __deltaAlphas.Add(__name, __deltaAlpha);
                }

                // 对每一对相同的image建立期间的imageSituations
                FrameData[] __result = new FrameData[length];
                for (int i = 0; i < __result.Length; i++)
                {
                    __result[i] = new FrameData();
                    for (int j = 0; j < __sameImageSituationNames.Count; j++)
                    {
                        string __tempSitName = (string)__sameImageSituationNames[j];
                        ImageSituation __tempSit = _startKeyFrame.getImageSituation(__tempSitName);
                        string __tempImageName = __tempSit.imageName;
                        float __tempX = __tempSit.x + __deltaXs[__tempSitName] * (i + 1);
                        float __tempY = __tempSit.y + __deltaYs[__tempSitName] * (i + 1);
                        bool __tempIsScale = __tempSit.isScale;
                        bool __tempIsRotate = __tempSit.isRotate;
                        bool __tempIsAlpha = __tempSit.isAlpha;
                        float __tempScaleX = __tempSit.scaleX + __deltaScaleXs[__tempSitName] * (i + 1);
                        float __tempScaleY = __tempSit.scaleY + __deltaScaleYs[__tempSitName] * (i + 1);
                        float __tempRotate = __tempSit.rotate + __deltaRotates[__tempSitName] * (i + 1);
                        float __tempAlpha = __tempSit.getAlpha() + __deltaAlphas[__tempSitName] * (i + 1);
                        __result[i].addImageSituation(__tempImageName, __tempX, __tempY, __tempIsScale, __tempIsRotate,
                            __tempIsAlpha, __tempScaleX, __tempScaleY, __tempRotate, __tempAlpha);
                    }
                }
                return __result;
            }

            return null;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                stopTimer();
            }
            else
            {
                startTimer();
            }
        }

        private void startTimer()
        {
            int __FPS;
            try
            {
                __FPS = int.Parse(textBoxFPS.Text.ToString());
            }
            catch
            {
                __FPS = 30;
                textBoxFPS.Text = "30";
            }

            try
            {
                timer.Interval = (1000 / __FPS);
            }
            catch
            {
                timer.Interval = 1000;
            }

            isTimerRunning = true;
            buttonRun.Text = "STOP";
            timer.Start();
            changeCurrentFrameIndex(0);
        }

        private void stopTimer()
        {
            isTimerRunning = false;
            buttonRun.Text = "RUN";
            timer.Stop();
        }


        public bool isTimerRunning;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (allFrames != null)
            {
                if (currentFrameIndex < allFrames.Length - 1)
                {
                    changeCurrentFrameIndex(currentFrameIndex + 1);
                }
                else
                {
                    stopTimer();
                }
            }
            else
            {
                stopTimer();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        public int copiedFrameIndex;
        public FrameData copiedFrameData;

        void FormMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }


        void FormMain_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
            {
                // Copy
                if (keyFrames.ContainsKey(currentFrameIndex))
                {
                    copiedFrameIndex = currentFrameIndex;
                    copiedFrameData = keyFrames[currentFrameIndex];
                }
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.V)
            {
                // Paste
                if (copiedFrameIndex != currentFrameIndex && copiedFrameData != null)
                {
                    if (keyFrames.ContainsKey(currentFrameIndex))
                        keyFrames.Remove(currentFrameIndex);

                    keyFrames.Add(currentFrameIndex, copiedFrameData.clone());
                }
            }
            pictureBoxFrameList.Refresh();
            pictureBoxMain.Refresh();
        }


        void FormMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }


        // ____INPORT_&_EXPORT_____________________________________________________________________
        private void buttonExport_Click(object sender, EventArgs e)
        {
            saveFileDialog.OverwritePrompt = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 将资源复制到目标文件夹
                    if (imageNames != null)
                    {
                        string[] __imageNamesKeys = imageNames.Keys.ToArray<string>();
                        for (int i = 0; i < __imageNamesKeys.Length; i++)
                        {
                            string __sourcePath = imageNames[__imageNamesKeys[i]];
                            string __targetPath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName) + "\\" + __imageNamesKeys[i];
                            if (!__sourcePath.Equals(__targetPath))
                            {
                                System.IO.File.Copy(__sourcePath, __targetPath, true);
                            }
                        }
                    }
                    // 数据文件(UTF8)
                    System.IO.File.WriteAllText(saveFileDialog.FileName, getDataString(), Encoding.UTF8);
                    MessageBox.Show("数据成功导出到: " + saveFileDialog.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("导出数据失败: \r\n" + ex.Data.ToString());
                }
            }
        }

        public string getDataString()
        {
            string __result = "";

            __result += "<Global>\r\n";
            __result += "\tscreenWidth=" + screenWidth + "\r\n";
            __result += "\tscreenHeight=" + screenHeight + "\r\n";
            // ImageNames
            __result += "\timageNames=";
            string[] imageNames = images.Keys.ToArray<string>();
            for (int i = 0; i < imageNames.Length; i++)
            {
                __result += imageNames[i];
                if (i < imageNames.Length - 1)
                {
                    __result += "|";
                }
            }
            __result += "\r\n";
            // KeyFrames
            __result += "\thowManyKeyFrames=" + keyFrames.Count + "\r\n";
            __result += "</Global>\r\n";
           
            __result += "<KeyFrames>\r\n";
            int[] __keysOfKeyFrames = keyFrames.Keys.ToArray<int>();
            Array.Sort<int>(__keysOfKeyFrames);
            for (int i = 0; i < __keysOfKeyFrames.Length; i++)
            {
                FrameData __tempData = keyFrames[__keysOfKeyFrames[i]];
                __result += __tempData.getDataString("\t", i, __keysOfKeyFrames[i]);
            }
            __result += "</KeyFrames>\r\n";

            return __result;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string __filePath = openFileDialog.FileName;
                string __dataString = System.IO.File.ReadAllText(__filePath);
                clearAll();
                parseDataString(__dataString, __filePath);
                refreshAll();
            }
        }

        // 处理字符串输入
        public void parseDataString(string _dataString, string _filePath)
        {
            string __globalStuff = getSubString(_dataString, "<Global>", "</Global>").Trim();
            string __frameStuff = getSubString(_dataString, "<KeyFrames>", "</KeyFrames>").Trim();
            Dictionary<string, string> __parasGlobal = analyseParas(__globalStuff);
            this.screenWidth = int.Parse(__parasGlobal["screenWidth"]);
            this.screenHeight = int.Parse(__parasGlobal["screenHeight"]);
            string[] __imageNames = __parasGlobal["imageNames"].Split(new char[] { '|' });
            for (int i = 0; i < __imageNames.Length; i++)
            {
                imageNames.Add(__imageNames[i], System.IO.Path.GetDirectoryName(_filePath) + "\\" + __imageNames[i]);
                if (images == null)
                    images = new Dictionary<string, Bitmap>();
                images.Add(__imageNames[i], new Bitmap(imageNames[__imageNames[i]]));
            }
            int __howManyKeyFrames = int.Parse(__parasGlobal["howManyKeyFrames"]);
            for (int i = 0; i < __howManyKeyFrames; i++)
            {
                string __frameDataString = getSubString(__frameStuff, "<FrameData_" + i + ">", "</FrameData_" + i + ">");
                addFrameData(__frameDataString);
            }
        }

        // 通过两个标签截取一段字符串
        public static string getSubString(string str, string tagStart, string tagEnd)
        {
            int indexStart = str.IndexOf(tagStart) + tagStart.Length;
            int indexEnd = str.IndexOf(tagEnd);
            return str.Substring(indexStart, indexEnd - indexStart);
        }

        // 将一段字符串切割后变成一张键值表.
        public static Dictionary<string, string> analyseParas(string str)
        {
            Dictionary<string, string> __result = new Dictionary<string, string>();
            string[] para_values = str.Trim().Split(new char[] { '\n' });
            for (int i = 0; i < para_values.Length; i++)
            {
                string[] fuck = para_values[i].Trim().Split(new char[] { '=' });
                if (!__result.ContainsKey(fuck[0]))
                    __result.Add(fuck[0], fuck[1]);
            }
            return __result;
        }


        public static ImageSituation createImageSituation(string _dataString)
        {
            Dictionary<string, string> __paras = analyseParas(_dataString);
            string __name = __paras["name"];
            string __imageName = __paras["imageName"];
            float __x = float.Parse(__paras["x"]);
            float __y = float.Parse(__paras["y"]);
            bool __isScale = getStringBool(__paras["isScale"]);
            bool __isRotate = getStringBool(__paras["isRotate"]);
            bool __isAlpha = getStringBool(__paras["isAlpha"]);
            float __scaleX = float.Parse(__paras["scaleX"]);
            float __scaleY = float.Parse(__paras["scaleY"]);
            float __rotate = float.Parse(__paras["rotate"]);
            float __alpha = float.Parse(__paras["alpha"]);
            return new ImageSituation(
                __name,
                __imageName,
                __x, __y,
                __isScale, __isRotate, __isAlpha,
                __scaleX, __scaleY, __rotate, __alpha);
        }

        public void addFrameData(string _dataString)
        {
            FrameData __result = new FrameData();
            string __globalStuff = getSubString(_dataString, "<FrameDataGlobal>", "</FrameDataGlobal>");
            string __imageSituationsStuff = getSubString(_dataString, "<ImageSituations>", "</ImageSituations>");
            Dictionary<string, string> __globalParas = analyseParas(__globalStuff);
            int __frameIndex = int.Parse(__globalParas["frameIndex"]);
            int __howManyImageSituations = int.Parse(__globalParas["howManyImageSituations"]);
            for (int i = 0; i < __howManyImageSituations; i++)
            {
                string __tempStuff = getSubString(__imageSituationsStuff, "<ImageSituation_" + i + ">", "</ImageSituation_" + i + ">");
                __result.addImageSituation(createImageSituation(__tempStuff));
            }
            keyFrames.Add(__frameIndex, __result);
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清空么FUCK?", "清空所有数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clearAll();
            }
        }

        // 清理所有的数据
        public void clearAll()
        {
            previewImage = null;
            images = null;
            keyFrames = new Dictionary<int, FrameData>();
            allFrames = null;
            selectedImageSituation = null;

            refreshAll();
        }

        // ____EXIT________________________________________________________________________________
        void FormMain_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DialogResult __result = MessageBox.Show("确定要退出么FUCK?", "退出程序", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (__result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (__result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        /////////////////////////////////////////////////

        public static string getBoolString(bool _value)
        {
            return _value ? "true" : "false";
        }

        public static bool getStringBool(string _str)
        {
            return _str.ToLower().Equals("true");
        }



    }
}
