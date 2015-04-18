package com.cg;

import java.util.ArrayList;

import android.graphics.Canvas;
import android.graphics.Paint;

public class FrameData {
    public ArrayList<ImageSituation> imageSituations = null;
    
    private CG cg;
    public FrameData(CG _cg)
    {
	cg = _cg;
        imageSituations = new ArrayList<ImageSituation>();
    }
    
    public void paint(Canvas _canvas, Paint _paint)
    {
        for (int i = 0; i < imageSituations.size(); i++)
        {
            imageSituations.get(i).paint(_canvas, _paint);
        }
    }

    public void addImageSituation(String _name, String _imageName, float _x, float _y,
        boolean _isScale, boolean _isRotate, boolean _isAlpha,
        float _scaleX, float _scaleY, float _rotate, float _alpha)
    {
        ImageSituation __newImageSituation = new ImageSituation(_name, _imageName, _x, _y,
            _isScale, _isRotate, _isAlpha,
            _scaleX, _scaleY, _rotate, _alpha, cg);
        imageSituations.add(__newImageSituation);
    }

    public void addImageSituation(ImageSituation _newSit)
    {
        this.imageSituations.add(_newSit);
    }

    public ImageSituation getImageSituation(int _index)
    {
        if (_index < imageSituations.size())
            return imageSituations.get(_index);
        return null;
    }

    public ImageSituation getImageSituation(String _name)
    {
        if (imageSituations != null)
        {
            for (int i = 0; i < imageSituations.size(); i++)
            {
                if (imageSituations.get(i).name.equals(_name))
                    return imageSituations.get(i);
            }
        }
        return null;
    }

    public void removeImageSituation(int _index)
    {
        if (_index < imageSituations.size())
            imageSituations.remove(_index);
    }

    public boolean containsImageSituation(String _name)
    {
        if (imageSituations != null)
        {
            for (int i = 0; i < imageSituations.size(); i++)
            {
                if (imageSituations.get(i).name.equals(_name))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public FrameData clone()
    {
        FrameData __result = new FrameData(cg);

        for (int i = 0; i < imageSituations.size(); i++)
        {
            __result.addImageSituation(imageSituations.get(i).clone());
        }

        return __result;
    }

    // 获取一个参考
    public FrameData getReferenceFrame()
    {
        FrameData __result = new FrameData(cg);

        for (int i = 0; i < imageSituations.size(); i++)
        {
            __result.addImageSituation(imageSituations.get(i).getReferenceSituation());
        }

        return __result;
    }
    
}
