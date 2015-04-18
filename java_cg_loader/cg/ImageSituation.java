package com.cg;

import java.util.Hashtable;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;

public class ImageSituation
{

    public String name; // ������imageName֮���name��Ϊ�˱���֡�в���ʹ���ظ���Ԫ��
    public Bitmap image;
    public String imageName;
    public float x, y;
    public boolean isScale, isRotate, isAlpha;
    // 0 ~ 1, 0 ~ 1, 0 ~ 360, 0 ~ 1; �õ�ʱ����ת255Ҳ��, ͳһ�˱ȽϷ���
    public float scaleX, scaleY, rotate, alpha;

    public int centerX, centerY; // ���������������ļ���

    private CG cg;

    // �������������ҪCG�е�Image��Դ�б��֧��.
    public ImageSituation(String _name, String _imageName, float _x, float _y, boolean _isScale, boolean _isRotate, boolean _isAlpha, float _scaleX, float _scaleY, float _rotate, float _alpha, CG _cg)
    {
	this.cg = _cg;

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
	alpha = _alpha;

	image = cg.images.get(imageName);

	this.centerX = image.getWidth() / 2;
	this.centerY = image.getHeight() / 2;
    }

    public ImageSituation(String _dataString, CG _cg)
    {
	this.cg = _cg;

	Hashtable<String, String> __paras = Statics.parseParas(_dataString);
	name = __paras.get("name");
	imageName = __paras.get("imageName");
	x = Float.parseFloat(__paras.get("x"));
	y = Float.parseFloat(__paras.get("y"));
	isScale = Statics.stringToBoolean(__paras.get("isScale"));
	isRotate = Statics.stringToBoolean(__paras.get("isRotate"));
	isAlpha = Statics.stringToBoolean(__paras.get("isAlpha"));
	scaleX = Float.parseFloat(__paras.get("scaleX"));
	scaleY = Float.parseFloat(__paras.get("scaleY"));
	rotate = Float.parseFloat(__paras.get("rotate"));
	alpha = Float.parseFloat(__paras.get("alpha"));
	alpha = 255f * alpha;

	image = cg.images.get(imageName);

	this.centerX = image.getWidth() / 2;
	this.centerY = image.getHeight() / 2;
    }

    public void paint(Canvas canvas, Paint paint)
    {
	if (image != null)
	{
	    canvas.save();
	    canvas.scale(scaleX, scaleY, x + centerX, y + centerY);
	    canvas.rotate(rotate, x + centerX, y + centerY);
	    paint.setAlpha((int) alpha);
	    canvas.drawBitmap(image, x, y, paint);
	    canvas.restore();
	    paint.setAlpha(255);
	}
    }

    public ImageSituation clone()
    {
	return new ImageSituation(this.name, this.imageName, this.x, this.y, this.isScale, this.isRotate, this.isAlpha, this.scaleX, this.scaleY, this.rotate, this.alpha, this.cg);
    }

    public ImageSituation getReferenceSituation()
    {
	return new ImageSituation(this.name, this.imageName, this.x, this.y, this.isScale, this.isRotate, this.isAlpha, this.scaleX, this.scaleY, this.rotate, this.alpha * 0.3f, this.cg);
    }
}
