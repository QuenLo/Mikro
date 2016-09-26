using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------------
// Copyright 2012-2016 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

public class AVProQuickTimeFormatConverter : System.IDisposable
{
	private int _movieHandle;
	
	// Format conversion and texture output
	private Texture2D _rawTexture;
	private RenderTexture _finalTexture;
	public Texture _outputTexture;
	private Material _conversionMaterial;
	private int _usedTextureWidth, _usedTextureHeight;
	private Vector4 _uv;	
	private int _lastFrameUploaded;
	private int _frameUploadCount;
	private int _lastFrameDisplayed;
	private int _frameDisplayCount;

	// Conversion params
	private int _width;
	private int _height;
	private bool _flipX;
	private bool _flipY;
	private AVProQuickTimePlugin.PixelFormat _sourceVideoFormat;
	private bool _requiresTextureCrop;
	private bool _requiresConversion;

	public Texture OutputTexture
	{
		get { return _outputTexture; }
	}

	public int DisplayFrame
	{
		get { return _lastFrameDisplayed; }
	}

	public int DisplayFrameCount
	{
		get { return _frameDisplayCount; }
	}
	
	public bool	ValidPicture { get; private set; }
	
	public void Reset()
	{
		ValidPicture = false;
		_lastFrameUploaded = -1;
		_frameUploadCount = 0;
		_lastFrameDisplayed = -1;
		_frameDisplayCount = 0;
	}

	public bool Build(int movieHandle, int width, int height, AVProQuickTimePlugin.PixelFormat format, bool yuvHD, bool flipX, bool flipY)
	{
		Reset();

        _outputTexture = null;
		_movieHandle = movieHandle;
		
		_width = width;
		_height = height;
		_sourceVideoFormat = format;
		_flipX = flipX;
		_flipY = flipY;
		
		CreateTexture();

		if (_rawTexture != null)
		{
			_requiresConversion = false;
			_requiresTextureCrop = (_usedTextureWidth != _rawTexture.width || _usedTextureHeight != _rawTexture.height);
			if (_requiresTextureCrop)
			{
				CreateUVs(_flipX, _flipY);
				_requiresConversion = true;
			}

			AVProQuickTimePlugin.SetTexturePointer(_movieHandle, _rawTexture.GetNativeTexturePtr());

			if (!_requiresConversion)
			{
				bool isDX11 = SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11");
				if (_flipX || _flipY)
				{
					_requiresConversion = true;
				}
				else if (_sourceVideoFormat == AVProQuickTimePlugin.PixelFormat.RGBA32 && isDX11)
				{
#if UNITY_5
				// DX11 has red and blue channels swapped around
				if (!SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
					_requiresConversion = true;
#else
                _requiresConversion = true;
#endif
				}
				else if (_sourceVideoFormat != AVProQuickTimePlugin.PixelFormat.Hap_RGB &&
						_sourceVideoFormat != AVProQuickTimePlugin.PixelFormat.Hap_RGBA &&
						_sourceVideoFormat != AVProQuickTimePlugin.PixelFormat.RGBA32)
				{
					_requiresConversion = true;
				}
			}

			if (_requiresConversion)
			{
				if (CreateMaterial(yuvHD))
				{
					CreateRenderTexture();
					_outputTexture = _finalTexture;

					_conversionMaterial.mainTexture = _rawTexture;
					if (!_requiresTextureCrop)
					{
						// Flip and then offset back to get back to normalised range
						Vector2 scale = new Vector2(1f, 1f);
						Vector2 offset = new Vector2(0f, 0f);
						if (_flipX)
						{
							scale = new Vector2(-1f, scale.y);
							offset = new Vector2(1f, offset.y);
						}
						if (_flipY)
						{
							scale = new Vector2(scale.x, -1f);
							offset = new Vector2(offset.x, 1f);
						}

						_conversionMaterial.mainTextureScale = scale;
						_conversionMaterial.mainTextureOffset = offset;
						// Since Unity 5.3 Graphics.Blit ignores mainTextureOffset/Scale
#if UNITY_5 && !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2
						_conversionMaterial.SetVector("_MainTex_ST2", new Vector4(scale.x, scale.y, offset.x, offset.y));
#endif
					}
					bool formatIs422 = (_sourceVideoFormat == AVProQuickTimePlugin.PixelFormat.YCbCr);
					if (formatIs422)
					{
						_conversionMaterial.SetFloat("_TextureWidth", _finalTexture.width);
					}
				}
			}
			else
			{
			    bool formatIs422 = (_sourceVideoFormat == AVProQuickTimePlugin.PixelFormat.YCbCr);
			    if (formatIs422)
			    {
                    if (CreateMaterial(yuvHD))
                    {
                        _conversionMaterial.SetFloat("_TextureWidth", _width);
                        _rawTexture.filterMode = FilterMode.Point;
                        _outputTexture = _rawTexture;
                    }
			    }
			    else
			    {
				    _rawTexture.filterMode = FilterMode.Bilinear;
                    _outputTexture = _rawTexture;
			    }
			    //_rawTexture.wrapMode = TextureWrapMode.Repeat;
			    
			}
		}

		return (_outputTexture != null); ;
	}

	public void Resize(int width, int height)
	{
		if (width > 0 && height > 0)
		{
			AVProQuickTimePlugin.SetTexturePointer(_movieHandle, System.IntPtr.Zero);
			Build(_movieHandle, width, height, _sourceVideoFormat, true, _flipX, _flipY);
		}
	}
		
	public bool Update()
	{
		bool result = UpdateTexture();
		if (_requiresConversion)
		{
			if (result)
			{
				DoFormatConversion();
			}
			else
			{
				if (_finalTexture != null && !_finalTexture.IsCreated())
				{
					Reset();
				}
			}
		}
		else
		{
			if (result)
				ValidPicture = true;
		}
		return result;
	}

	private bool UpdateTexture()
	{
		bool result = false;

		// We update all the textures from AVProQuickTimeManager.Update()
		// so just check if the update was done
		int frameUploadCount = AVProQuickTimePlugin.GetFrameUploadCount(_movieHandle);
		if (_frameUploadCount != frameUploadCount)
		{
            _frameUploadCount = frameUploadCount;
            _lastFrameUploaded = AVProQuickTimePlugin.GetLastFrameUploaded(_movieHandle); ;
			result = true;
		}
		
		return result;
	}

	public void Dispose()
	{
		ValidPicture = false;
		_width = _height = 0;
		
		if (_conversionMaterial != null)
		{
			_conversionMaterial.mainTexture = null;
			Material.Destroy(_conversionMaterial);
			_conversionMaterial = null;
		}

		_outputTexture = null;
		
		if (_finalTexture != null)
		{
			RenderTexture.ReleaseTemporary(_finalTexture);
			_finalTexture = null;
		}
		if (_rawTexture != null)
		{			
			Texture2D.Destroy(_rawTexture);
			_rawTexture = null;
		}
	}

	private bool CreateMaterial(bool yuvHD)
	{	
		Shader shader = AVProQuickTimeManager.Instance.GetPixelConversionShader(_sourceVideoFormat, yuvHD);
		if (shader)
		{
			if (_conversionMaterial != null)
			{
				if (_conversionMaterial.shader != shader)
				{
					Material.Destroy(_conversionMaterial);
					_conversionMaterial = null;
				}
			}
			
			if (_conversionMaterial == null)
			{
				_conversionMaterial = new Material(shader);
				_conversionMaterial.name = "AVProQuickTime-Material";
			}
		}
		
		return (_conversionMaterial != null);
	}

	private void CreateTexture()
	{	
		_usedTextureWidth = _width;
		_usedTextureHeight = _height;

		// Calculate texture size and format
		int textureWidth = _usedTextureWidth;
		int textureHeight = _usedTextureHeight;
		TextureFormat textureFormat = TextureFormat.RGBA32;
		switch (_sourceVideoFormat)
		{
        case AVProQuickTimePlugin.PixelFormat.RGBA32:
            if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11"))
            {
#if UNITY_5
			    // DX11 has red and blue channels swapped around
			    if (SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
				    textureFormat = TextureFormat.BGRA32;
#endif
            }
            break;
		case AVProQuickTimePlugin.PixelFormat.Hap_RGBA:
		case AVProQuickTimePlugin.PixelFormat.Hap_RGB_HQ:
			textureFormat = TextureFormat.DXT5;
			break;
		case AVProQuickTimePlugin.PixelFormat.Hap_RGB:
			textureFormat = TextureFormat.DXT1;
			break;
		case AVProQuickTimePlugin.PixelFormat.YCbCr:
			textureFormat = TextureFormat.RGBA32;
            if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11"))
            {
#if UNITY_5
			    // DX11 has red and blue channels swapped around
			    if (SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
				    textureFormat = TextureFormat.BGRA32;
#endif
            }
			_usedTextureWidth /= 2;	// YCbCr422 modes need half width
			textureWidth = _usedTextureWidth;
			break;
		}

        // If the texture isn't a power of 2
        bool requiresPOT = (SystemInfo.npotSupport == NPOTSupport.None);
		if (requiresPOT)
		{
			// We use a power-of-2 texture as Unity makes these internally anyway and not doing it seems to break things for texture updates
			if (!Mathf.IsPowerOfTwo(_width) || !Mathf.IsPowerOfTwo(_height))
			{
				textureWidth = Mathf.NextPowerOfTwo(textureWidth);
				textureHeight = Mathf.NextPowerOfTwo(textureHeight);
			}
		}
		
		// Create texture that stores the initial raw frame
		// If there is already a texture, only destroy it if it's not the same requirements
		if (_rawTexture != null)
		{
			if (_rawTexture.width != textureWidth || 
				_rawTexture.height != textureHeight || 
				_rawTexture.format != textureFormat)
			{
				Texture2D.Destroy(_rawTexture);
				_rawTexture = null;
			}
		}
		
		if (_rawTexture == null)
		{
            bool isLinear = true;
            if (!_requiresConversion)
            {
                if (AVProQuickTimePlugin.PixelFormat.Hap_RGBA == _sourceVideoFormat ||
                    AVProQuickTimePlugin.PixelFormat.Hap_RGB == _sourceVideoFormat ||
                    AVProQuickTimePlugin.PixelFormat.RGBA32 == _sourceVideoFormat)
                {
                    isLinear = false;
                }
            }
			_rawTexture = new Texture2D(textureWidth, textureHeight, textureFormat, false, isLinear);
			_rawTexture.wrapMode = TextureWrapMode.Clamp;
			_rawTexture.filterMode = FilterMode.Point;
			_rawTexture.name = "AVProQuickTime-RawTexture";
			_rawTexture.Apply(false, true);
		}
	}
	
	private void CreateRenderTexture()
	{	
		// Create RenderTexture for post transformed frames
		// If there is already a renderTexture, only destroy it smaller than desired size
		if (_finalTexture != null)
		{
			if (_finalTexture.width != _width || _finalTexture.height != _height)
			{
				RenderTexture.ReleaseTemporary(_finalTexture);
				_finalTexture = null;
			}
		}

		if (_finalTexture == null)
		{
			ValidPicture = false;
            _finalTexture = RenderTexture.GetTemporary(_width, _height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			_finalTexture.wrapMode = TextureWrapMode.Clamp;
			_finalTexture.filterMode = FilterMode.Bilinear;
			_finalTexture.name = "AVProQuickTime-FinalTexture";
			_finalTexture.Create();
		}
	}

	private void DoFormatConversion()
	{
		if (_finalTexture == null)
			return;

		_finalTexture.DiscardContents();

		RenderTexture prev = RenderTexture.active;
		if (!_requiresTextureCrop)
		{
			Graphics.Blit(_rawTexture, _finalTexture, _conversionMaterial, 0);
		}
		else
		{
			RenderTexture.active = _finalTexture;

			_conversionMaterial.SetPass(0);

			GL.PushMatrix();
			GL.LoadOrtho();
			DrawQuad(_uv);
			GL.PopMatrix();
			
		}
		
		RenderTexture.active = prev;

		_lastFrameDisplayed = _lastFrameUploaded;
		_frameDisplayCount = _frameUploadCount;
		ValidPicture = true;
	}

	private void CreateUVs(bool invertX, bool invertY)
	{				
		float x1, x2;
		float y1, y2;
		if (invertX)
		{
			x1 = 1.0f; x2 = 0.0f;
		}
		else
		{
			x1 = 0.0f; x2 = 1.0f;
		}
		if (invertY)
		{
			y1 = 1.0f; y2 = 0.0f;
		}
		else
		{
			y1 = 0.0f; y2 = 1.0f;
		}
		
		// Alter UVs if we're only using a portion of the texture
		if (_usedTextureWidth != _rawTexture.width)
		{
			float xd = _usedTextureWidth / (float)_rawTexture.width;
			x1 *= xd; x2 *= xd;
		}
		if (_usedTextureHeight != _rawTexture.height)
		{
			float yd = _usedTextureHeight / (float)_rawTexture.height;
			y1 *= yd; y2 *= yd;
		}
			
		_uv = new Vector4(x1, y1, x2, y2);
	}	

	private static void DrawQuad(Vector4 uv)
	{
		GL.Begin(GL.QUADS);
		
		GL.TexCoord2(uv.x, uv.y);
		GL.Vertex3(0.0f, 0.0f, 0.1f);
		
		GL.TexCoord2(uv.z, uv.y);
		GL.Vertex3(1.0f, 0.0f, 0.1f);
		
		GL.TexCoord2(uv.z, uv.w);		
		GL.Vertex3(1.0f, 1.0f, 0.1f);
		
		GL.TexCoord2(uv.x, uv.w);
		GL.Vertex3(0.0f, 1.0f, 0.1f);
		
		GL.End();
	}
}