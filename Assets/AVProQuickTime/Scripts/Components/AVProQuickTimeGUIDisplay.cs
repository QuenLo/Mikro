using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------
// Copyright 2012-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

[AddComponentMenu("AVPro QuickTime/IMGUI Display")]
public class AVProQuickTimeGUIDisplay : MonoBehaviour
{
	public AVProQuickTimeMovie _movie;

	public ScaleMode _scaleMode = ScaleMode.ScaleToFit;
	public Color _color = Color.white;
	public bool _alphaBlend = false;
	
	public bool _fullScreen = true;
	public int  _depth = 0;	
	public float _x = 0.0f;
	public float _y = 0.0f;
	public float _width = 1.0f;
	public float _height = 1.0f;
	
	//-------------------------------------------------------------------------
	
	public void OnGUI()
	{
		if (_movie == null)
			return;
		
		if (_movie.OutputTexture != null)
		{
			if (!_alphaBlend || _color.a > 0)
			{
				GUI.depth = _depth;
				GUI.color = _color;

				Rect rect = GetRect();

				if (_movie.MovieInstance.RequiresFlipY)
				{
					GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0, rect.y + (rect.height / 2)));
				}

				GUI.DrawTexture(rect, _movie.OutputTexture, _scaleMode, _alphaBlend);
			}
		}
	}

	public Rect GetRect()
	{
		Rect rect;
		if (_fullScreen)
			rect = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
		else
			rect = new Rect(_x * (Screen.width-1), _y * (Screen.height-1), _width * Screen.width, _height * Screen.height);
		return rect;		
	}
}