using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------
// Copyright 2012-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

[AddComponentMenu("AVPro QuickTime/Material Apply")]
public class AVProQuickTimeMaterialApply : MonoBehaviour 
{
	public Material _material;
	public AVProQuickTimeMovie _movie;
	public string _textureName;
    public Texture2D _defaultTexture;
    private static Texture2D _blackTexture;

    private static void CreateTexture()
    {
        _blackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false, false);
        _blackTexture.name = "AVProQuickTime-BlackTexture";
        _blackTexture.filterMode = FilterMode.Point;
        _blackTexture.wrapMode = TextureWrapMode.Clamp;
        _blackTexture.SetPixel(0, 0, Color.black);
        _blackTexture.Apply(false, true);
    }

    void OnDestroy()
    {
        _defaultTexture = null;

        if (_blackTexture != null)
        {
            Texture2D.Destroy(_blackTexture);
            _blackTexture = null;
        }
    }

    void Start()
    {
        if (_blackTexture == null)
            CreateTexture();

        if (_defaultTexture == null)
        {
            _defaultTexture = _blackTexture;
        }

        Update();
    }
	
	void Update()
	{
        if (_movie != null)
        {
            if (_movie.OutputTexture != null)
                ApplyMapping(_movie.OutputTexture, _movie.MovieInstance.RequiresFlipY);
            else
                ApplyMapping(_defaultTexture, false);
        }
	}
	
	private void ApplyMapping(Texture texture, bool flipY)
	{
		if (_material != null)
		{
            if (string.IsNullOrEmpty(_textureName))
            {
                if (flipY)
                {
                    _material.mainTextureOffset = new Vector2(0f, 1f);
                    _material.mainTextureScale = new Vector2(1f, -1f);
                }
                _material.mainTexture = texture;
            }
            else
            {
                if (_material.HasProperty(_textureName))
                {
                    if (flipY)
                    {
                        _material.SetTextureOffset(_textureName, new Vector2(0f, 1f));
                        _material.SetTextureScale(_textureName, new Vector2(1f, -1f));
                    }
                    _material.SetTexture(_textureName, texture);
                }
            }
		}
	}
	
	public void OnDisable()
	{
		ApplyMapping(null, false);
	}
}
