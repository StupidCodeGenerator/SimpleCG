package com.cg;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Locale;

import org.apache.http.util.EncodingUtils;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.res.AssetManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;

import com.base.MainActivity;

// 这里和Util应该会有一些重复的函数, 目的是避免代码依赖性, 让这段代码任何人都可用
@SuppressLint("DefaultLocale")
public class Statics {

    public static String readTextFile(String path, String format) {
	String result = "";
	InputStream is;
	try {
	    is = MainActivity.instance.getResources().getAssets().open(path);
	    int length = is.available();
	    byte[] buffer = new byte[length];
	    is.read(buffer);
	    result = EncodingUtils.getString(buffer, format);
	} catch (IOException e1) {
	    // TODO Auto-generated catch block
	    e1.printStackTrace();
	}

	return result;
    }

    public static Hashtable<String, String> parseParas(String _dataString) {
	Hashtable<String, String> __result = new Hashtable<String, String>();

	String[] __paraStrings = _dataString.trim().split("\\\n");
	for (int i = 0; i < __paraStrings.length; i++) {
	    String[] __splited = __paraStrings[i].split("\\=");
	    if (!__result.containsKey(__splited[0].trim()))
		__result.put(__splited[0].trim(), __splited[1].trim());
	}

	return __result;
    }

    public static String getStringSub(String text, String keyWordStart, String keyWordEnd) {
	String str = "";
	int s = 0;
	int e = text.length();

	if (!keyWordStart.equals("")) {
	    s = text.indexOf(keyWordStart);
	}
	if (!keyWordEnd.equals("")) {
	    e = text.indexOf(keyWordEnd, s);
	}

	if (s != -1 && e != -1) {
	    str = text.substring(s + keyWordStart.length(), e);
	}
	return str.trim();
    }

    // 参数 : 起始的关键帧, 结束的关键帧, 两帧之间的距离(开区间中包含的元素个数)
    public static FrameData[] generatoFrom2KeyFrames(FrameData _startKeyFrame, FrameData _endKeyFrame, int _length, CG _cg) {
	if (_length > 0) {
	    // 检查相同的image
	    ArrayList<String> __sameImageSituationNames = new ArrayList<String>();
	    for (int i = 0; i < _startKeyFrame.imageSituations.size(); i++) {
		if (_endKeyFrame.containsImageSituation(_startKeyFrame.getImageSituation(i).name)) {
		    __sameImageSituationNames.add(_startKeyFrame.getImageSituation(i).name);
		}
	    }
	    // 计算image之间的差
	    Hashtable<String, Float> __deltaXs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __deltaYs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __deltaScaleXs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __deltaScaleYs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __deltaRotates = new Hashtable<String, Float>();
	    Hashtable<String, Float> __deltaAlphas = new Hashtable<String, Float>();

	    Hashtable<String, Float> __startXs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __startYs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __startScaleXs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __startScaleYs = new Hashtable<String, Float>();
	    Hashtable<String, Float> __startRotates = new Hashtable<String, Float>();
	    Hashtable<String, Float> __startAlphas = new Hashtable<String, Float>();

	    for (int i = 0; i < __sameImageSituationNames.size(); i++) {
		String __name = __sameImageSituationNames.get(i);

		float __startX = _startKeyFrame.getImageSituation(__name).x;
		float __startY = _startKeyFrame.getImageSituation(__name).y;
		float __startScaleX = _startKeyFrame.getImageSituation(__name).scaleX;
		float __startScaleY = _startKeyFrame.getImageSituation(__name).scaleY;
		float __startRotate = _startKeyFrame.getImageSituation(__name).rotate;
		float __startAlpha = _startKeyFrame.getImageSituation(__name).alpha;

		float __deltaX = (_endKeyFrame.getImageSituation(__name).x - __startX) / (_length + 1);
		float __deltaY = (_endKeyFrame.getImageSituation(__name).y - __startY) / (_length + 1);
		float __deltaScaleX = (_endKeyFrame.getImageSituation(__name).scaleX - __startScaleX) / (_length + 1);
		float __deltaScaleY = (_endKeyFrame.getImageSituation(__name).scaleY - __startScaleY) / (_length + 1);
		float __deltaRotate = (_endKeyFrame.getImageSituation(__name).rotate - __startRotate) / (_length + 1);
		float __deltaAlpha = (_endKeyFrame.getImageSituation(__name).alpha - __startAlpha) / (_length + 1);

		__startXs.put(__name, __startX);
		__startYs.put(__name, __startY);
		__startScaleXs.put(__name, __startScaleX);
		__startScaleYs.put(__name, __startScaleY);
		__startRotates.put(__name, __startRotate);
		__startAlphas.put(__name, __startAlpha);

		__deltaXs.put(__name, __deltaX);
		__deltaYs.put(__name, __deltaY);
		__deltaScaleXs.put(__name, __deltaScaleX);
		__deltaScaleYs.put(__name, __deltaScaleY);
		__deltaRotates.put(__name, __deltaRotate);
		__deltaAlphas.put(__name, __deltaAlpha);
	    }

	    // 对每一对相同的image建立期间的imageSituations
	    FrameData[] __result = new FrameData[_length];
	    for (int i = 0; i < __result.length; i++) {
		__result[i] = new FrameData(_cg);
		for (int j = 0; j < __sameImageSituationNames.size(); j++) {
		    String __tempSitName = __sameImageSituationNames.get(j);
		    ImageSituation __tempSit = _startKeyFrame.getImageSituation(__tempSitName);
		    String __tempImageName = __tempSit.imageName;
		    float __tempX = __tempSit.x + __deltaXs.get(__tempSitName) * (i + 1);
		    float __tempY = __tempSit.y + __deltaYs.get(__tempSitName) * (i + 1);
		    boolean __tempIsScale = __tempSit.isScale;
		    boolean __tempIsRotate = __tempSit.isRotate;
		    boolean __tempIsAlpha = __tempSit.isAlpha;
		    float __tempScaleX = __tempSit.scaleX + __deltaScaleXs.get(__tempSitName) * (i + 1);
		    float __tempScaleY = __tempSit.scaleY + __deltaScaleYs.get(__tempSitName) * (i + 1);
		    float __tempRotate = __tempSit.rotate + __deltaRotates.get(__tempSitName) * (i + 1);
		    float __tempAlpha = __tempSit.alpha + __deltaAlphas.get(__tempSitName) * (i + 1);
		    __result[i].addImageSituation(__tempSitName, __tempImageName, __tempX, __tempY, __tempIsScale, __tempIsRotate, __tempIsAlpha, __tempScaleX, __tempScaleY, __tempRotate, __tempAlpha);
		}
	    }
	    return __result;
	}

	return null;
    }

    

    public static String booleanToString(boolean _value) {
	return _value ? "true" : "false";
    }

    public static boolean stringToBoolean(String _value) {
	return _value.toLowerCase(Locale.ENGLISH).equals("true");
    }
    
    public static String getDir(String path) {
	String __result = "";
	char[] __charArray = path.toCharArray();
	for (int i = __charArray.length - 1; i >= 0; i--) {
	    if (__charArray[i] == '/') {
		__result = path.substring(0, i);
	    }
	}
	return __result + "/";
    }
    
    public static Bitmap loadBitmap(String path, Activity mainActivity) {
	Bitmap img = null;
	if (path != null) {
	    try {
		AssetManager am = mainActivity.getAssets();
		InputStream is = am.open(path);
		img = BitmapFactory.decodeStream(is);
		is.close();
	    } catch (Exception e) {
		Log.i("TARG", "CREATE IMAGE ERROR" + path);
	    }
	}
	return img;
    }
}
