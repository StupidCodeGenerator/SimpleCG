using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace CGE
{
    // 帧数据
    public class FrameData
    {
        public int renameId = 0;

        public ArrayList imageSituations = null;
        public FrameData()
        {
            imageSituations = new ArrayList();
        }

        // 有一点容易混淆. index是输出数据的顺序, frameIndex是这一帧在时间轴上的位置
        public string getDataString(string _prefix, int _index, int _frameIndex)
        {
            string __result = "";

            __result += _prefix + "<FrameData_" + _index + "> \r\n";
            __result += _prefix + "\t<FrameDataGlobal>\r\n";
            __result += _prefix + "\t\tframeIndex=" + _frameIndex + "\r\n";
            __result += _prefix + "\t\thowManyImageSituations=" + imageSituations.Count + "\r\n";
            __result += _prefix + "\t</FrameDataGlobal>\r\n";
            __result += _prefix + "\t<ImageSituations>\r\n";
            for (int i = 0; i < imageSituations.Count; i++)
            {
                __result += ((ImageSituation)imageSituations[i]).getDataString(_prefix + "\t\t", i);
            }
            __result += _prefix + "\t</ImageSituations>\r\n";
            __result += _prefix + "</FrameData_" + _index + "> \r\n";

            return __result;
        }

        public void paint(Graphics g)
        {
            for (int i = 0; i < imageSituations.Count; i++)
            {
                ((ImageSituation)imageSituations[i]).paint(g);
            }
        }

        // 这里需要对已经存在的imageSituation进行判定, 如果已经存在的话需要进行重命名. 
        // 可以简单的使用一个int变量解决这个问题.
        public void addImageSituation(string _imageName, float _x, float _y,
            bool _isScale, bool _isRotate, bool _isAlpha,
            float _scaleX, float _scaleY, float _rotate, float _alpha)
        {
            // 进行重复性检测
            // 如果未发生重复, name即为imageName. 
            // 如果已经发生重复, name = imageName + renameId++;
            bool __isRepeat = false;
            string __newName = _imageName;
            for (int i = 0; i < imageSituations.Count; i++)
            {
                string __targetName = ((ImageSituation)imageSituations[i]).name;
                __isRepeat = true;
                break;
            }
            if (__isRepeat)
            {
                // 已经发生重复
                __newName = _imageName + "_" + renameId++;
            }

            ImageSituation __newImageSituation = new ImageSituation(__newName, _imageName, _x, _y,
                _isScale, _isRotate, _isAlpha,
                _scaleX, _scaleY, _rotate, _alpha);
            imageSituations.Add(__newImageSituation);
        }

        public void addImageSituation(ImageSituation _newSit)
        {
            this.imageSituations.Add(_newSit);
        }

        public ImageSituation getImageSituation(int _index)
        {
            if (_index < imageSituations.Count)
                return (ImageSituation)imageSituations[_index];
            return null;
        }

        public ImageSituation getImageSituation(string _name)
        {
            if (imageSituations != null)
            {
                for (int i = 0; i < imageSituations.Count; i++)
                {
                    if (((ImageSituation)imageSituations[i]).name.Equals(_name))
                        return (ImageSituation)imageSituations[i];
                }
            }
            return null;
        }

        public void removeImageSituation(int _index)
        {
            if (_index < imageSituations.Count)
                imageSituations.RemoveAt(_index);
        }

        public bool containsImageSituation(string _name)
        {
            if (imageSituations != null)
            {
                for (int i = 0; i < imageSituations.Count; i++)
                {
                    if (((ImageSituation)imageSituations[i]).name.Equals(_name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void imageSituationMoveUp(int _index)
        {
            if (imageSituations != null)
            {
                if (_index > 0 && _index < imageSituations.Count)
                {
                    ImageSituation __currentImageSituation = (ImageSituation)imageSituations[_index];
                    imageSituations.RemoveAt(_index);
                    imageSituations.Insert(_index - 1, __currentImageSituation);
                }
            }
        }

        public void imageSituationMoveDown(int _index)
        {
            if (imageSituations != null)
            {
                if (_index >= 0 && _index < imageSituations.Count - 1)
                {
                    ImageSituation __currentImageSituation = (ImageSituation)imageSituations[_index];
                    imageSituations.RemoveAt(_index);
                    imageSituations.Insert(_index + 1, __currentImageSituation);
                }
            }
        }

        public FrameData clone()
        {
            FrameData __result = new FrameData();

            for (int i = 0; i < imageSituations.Count; i++)
            {
                __result.addImageSituation(((ImageSituation)imageSituations[i]).clone());
            }

            return __result;
        }

        // 获取一个参考
        public FrameData getReferenceFrame()
        {
            FrameData __result = new FrameData();

            for (int i = 0; i < imageSituations.Count; i++)
            {
                __result.addImageSituation(((ImageSituation)imageSituations[i]).getReferenceSituation());
            }

            return __result;
        }

    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    // 考虑到关键帧需要进行许多除法运算, 所以数据类型大量的使用float
    public class ImageSituation
    {
        public Bitmap image;
        public string name; // 考虑到帧中对一个图片的重复利用, 所以需要一个独立于image之外的name
        public string imageName;
        public float x, y;
        public bool isScale, isRotate, isAlpha;
        // 0 ~ 1, 0 ~ 1, 0 ~ 360, 0 ~ 1; 用的时候在转255也好, 统一了比较方便
        public float scaleX, scaleY, rotate;
        private float alpha;

        public ImageSituation(string _name, string _imageName, float _x, float _y, bool _isScale, bool _isRotate, bool _isAlpha,
            float _scaleX, float _scaleY, float _rotate, float _alpha)
        {
            name = _name;
            imageName = _imageName;
            x = _x;
            y = _y;
            isScale = _isScale;
            isRotate = _isRotate;
            isAlpha = _isAlpha;
            scaleX = _scaleX;
            scaleY = _scaleY;
            rotate = _rotate;

            image = FormMain.instance.images[_imageName];

            // 图片半透明处理
            setAlpha(_alpha);
        }

        // _prefix: 前缀, 一般传入的是若干个"\t", 用来制造缩进
        public string getDataString(string _prefix, int _index)
        {
            string __result = "";

            __result += _prefix + "<ImageSituation_" + _index + "> \r\n";

            __result += _prefix + "\tname=" + name + "\r\n";
            __result += _prefix + "\timageName=" + imageName + "\r\n";
            __result += _prefix + "\tx=" + x + "\r\n";
            __result += _prefix + "\ty=" + y + "\r\n";
            __result += _prefix + "\tisScale=" + FormMain.getBoolString(isScale) + "\r\n";
            __result += _prefix + "\tisRotate=" + FormMain.getBoolString(isRotate) + "\r\n";
            __result += _prefix + "\tisAlpha=" + FormMain.getBoolString(isAlpha) + "\r\n";
            __result += _prefix + "\tscaleX=" + scaleX + "\r\n";
            __result += _prefix + "\tscaleY=" + scaleY + "\r\n";
            __result += _prefix + "\trotate=" + rotate + "\r\n";
            __result += _prefix + "\talpha=" + alpha + "\r\n";

            __result += _prefix + "</ImageSituation_" + _index + "> \n";

            return __result;
        }

        public void paint(Graphics g)
        {
            GraphicsState gstate = g.Save();
            g.TranslateTransform(x + image.Width / 2, y + image.Height / 2);
            g.ScaleTransform(scaleX, scaleY);
            g.RotateTransform(rotate);
            g.TranslateTransform(-x - image.Width / 2, -y - image.Height / 2);
            g.DrawImage(image, x, y);
            g.Restore(gstate);
        }

        // 一个从网上Down来的创建半透明图片的方法. 这个过程会创建一个新的图片对象, 所以对原来的Image没有影响
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
        }

        public void setAlpha(float _value)
        {
            this.alpha = _value;
            if (alpha < 1.0f)
                image = ChangeOpacity(image, alpha);
        }

        public float getAlpha()
        {
            return alpha;
        }

        public ImageSituation clone()
        {
            return new ImageSituation(
                this.name,
                this.imageName,
                this.x, this.y,
                this.isScale, this.isRotate, this.isAlpha,
                this.scaleX, this.scaleY,
                this.rotate,
                this.alpha);
        }

        public ImageSituation getReferenceSituation()
        {
            return new ImageSituation(
                this.name,
                this.imageName,
                this.x, this.y,
                this.isScale, this.isRotate, this.isAlpha,
                this.scaleX, this.scaleY,
                this.rotate,
                this.alpha * 0.3f);
        }
    }
}
