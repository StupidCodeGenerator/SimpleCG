package com.cg;

import java.util.ArrayList;
import java.util.Hashtable;

import com.base.Render;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;

public class CG
{

    public int screenWidth;
    public int screenHeight;
    String[] imageNames;
    Hashtable<String, Bitmap> images; // 主资源表. CG中所有的资源都通过这个表来管理

    // 关键帧
    public FrameData[] keyFrames;
    public int[] keyFrameIndexes;

    // 全部帧
    public FrameData[] allFrames;

    // 播放相关
    public int currentFrameIndex;
    public boolean isRunning;

    // 在加载的时候直接就生成了, 拿来就用
    public CG(String _path, Activity _mainActivity)
    {
	String __dir = Statics.getDir(_path); 
	if (__dir.equals("/"))
	    __dir = "";
	
	String __rootStuff = Statics.readTextFile(_path, "utf8");

	String __globalStuff = Statics.getStringSub(__rootStuff, "<Global>", "</Global>").trim();
	String __keyFramesStuff = Statics.getStringSub(__rootStuff, "<KeyFrames>", "</KeyFrames>").trim();

	Hashtable<String, String> __globalParas = Statics.parseParas(__globalStuff);
	screenWidth = Integer.parseInt(__globalParas.get("screenWidth").trim());
	screenHeight = Integer.parseInt(__globalParas.get("screenHeight").trim());
	imageNames = __globalParas.get("imageNames").trim().split("\\|");
	
	// 加载图片
	images = new Hashtable<String, Bitmap>();
	for (int i = 0; i < imageNames.length ;i ++)
	{
	   String __imagePath = __dir + imageNames[i];
	   images.put(imageNames[i], Statics.loadBitmap(__imagePath, _mainActivity));
	}
	
	int __howManyFrames = Integer.parseInt(__globalParas.get("howManyKeyFrames").trim());

	// 关键帧
	getKeyFrames(__keyFramesStuff, __howManyFrames);

	// 生成过程
	allFrames = build(keyFrameIndexes, keyFrames, this);

	init();
    }
    
    public void init()
    {
	currentFrameIndex = 0;
	isRunning = true;
    }

    private void getKeyFrames(String _keyFramesString, int _howManyKeyFrames)
    {
	keyFrames = new FrameData[_howManyKeyFrames];
	keyFrameIndexes = new int[_howManyKeyFrames];
	for (int i = 0; i < keyFrames.length; i++)
	{
	    String __frameDataStr = Statics.getStringSub(_keyFramesString, "<FrameData_" + i + ">", "</FrameData_" + i + ">");
	    String __frameDataGlobal = Statics.getStringSub(__frameDataStr, "<FrameDataGlobal>", "</FrameDataGlobal>");
	    String __strImageSituations = Statics.getStringSub(__frameDataStr, "<ImageSituations>", "</ImageSituations>");
	    
	    keyFrames[i] = new FrameData(this);
	    Hashtable<String, String> __parasFrameDataGlobal = Statics.parseParas(__frameDataGlobal);
	    keyFrameIndexes[i] = Integer.parseInt(__parasFrameDataGlobal.get("frameIndex"));
	    int __howManyImageSituations = Integer.parseInt(__parasFrameDataGlobal.get("howManyImageSituations"));
	    for (int j = 0; j < __howManyImageSituations; j++)
	    {
		String __imageSituation_j = Statics.getStringSub(__strImageSituations, "<ImageSituation_" + j + ">", "</ImageSituation_" + j + ">");
		keyFrames[i].addImageSituation(new ImageSituation(__imageSituation_j, this));
	    }
	}
    }

    // 根据关键帧计算一个帧序列
    // 帧序列从0开始到最后一个关键帧结束
    // 传入参数表示关键帧的位置以及关键帧, 要保证一一对应
    public static FrameData[] build(int[] _frameIndexs, FrameData[] _keyFrames, CG _cg)
    {
	FrameData[] __result = null;

	if (_frameIndexs.length == _keyFrames.length)
	{

	    FrameData[][] __frameDataArrays = new FrameData[_keyFrames.length - 1][];

	    for (int i = 0; i < _frameIndexs.length - 1; i++)
	    {
		int __startFrameIndex = _frameIndexs[i];
		int __endFrameIndex = _frameIndexs[i + 1];
		FrameData __startKeyFrame = _keyFrames[i];
		FrameData __endKeyFrame = _keyFrames[i + 1];
		int __length = __endFrameIndex - __startFrameIndex - 1;
		__frameDataArrays[i] = Statics.generatoFrom2KeyFrames(__startKeyFrame, __endKeyFrame, __length, _cg);
	    }

	    // 数组合并过程
	    ArrayList<FrameData> __finalResult = new ArrayList<FrameData>();
	    int __startFrameIndex = _frameIndexs[0]; // 获取第一个关键帧
	    for (int i = 0; i < __startFrameIndex; i++)
	    {
		__finalResult.add(new FrameData(_cg)); // 在第一个关键帧的前几帧, 添加空的帧数据
	    }

	    __finalResult.add(_keyFrames[0]);
	    for (int i = 0; i < __frameDataArrays.length; i++)
	    {
		for (int j = 0; j < __frameDataArrays[i].length; j++)
		{
		    __finalResult.add(__frameDataArrays[i][j]);
		}
		__finalResult.add(_keyFrames[i + 1]);
	    }
	    __result = new FrameData[__finalResult.size()];
	    for (int i = 0; i < __result.length; i++)
	    {
		__result[i] = __finalResult.get(i);
	    }
	}

	return __result;
    }
    
    // return: 是否播放完毕
    // 抗锯齿需要自己去开, 方法详见公司Wiki : http://42.121.78.119:81/wiki/index.php/%E6%8A%97%E9%94%AF%E9%BD%BF
    public boolean paint(Canvas _canvas, Paint _paint)
    {
	if (currentFrameIndex >= 0 && currentFrameIndex < allFrames.length)
	{
	    allFrames[currentFrameIndex].paint(_canvas, _paint);
	    if (isRunning)
		currentFrameIndex ++;
	    return false;
	}
	else
	{
	    allFrames[allFrames.length - 1].paint(_canvas, _paint);
	    return true;
	}
    }

    public void pause()
    {
	isRunning = false;
    }
    
    public void resume()
    {
	isRunning = true;
    }
    
    // <0 则 =0, >= length 则 = length - 1
    public void setFrame(int _index)
    {
	if (_index < 0)
	    _index = 0;
	if (_index >= allFrames.length)
	    _index = allFrames.length - 1;
	
	currentFrameIndex = _index;
    }
    
}
