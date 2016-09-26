#if UNITY_5
	#if !UNITY_5_0 && !UNITY_5_1
		#define AVPROVIDEO_ISSUEPLUGINEVENT_UNITY52
	#endif
#endif
using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

//-----------------------------------------------------------------------------
// Copyright 2012-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

[AddComponentMenu("AVPro QuickTime/Manager (required)")]
public class AVProQuickTimeManager : MonoBehaviour
{
	private static AVProQuickTimeManager _instance;

	public bool _logVideoLoads = true;

	public bool _updateUsingCoroutine = true;

	// Format conversion
	private Shader _shaderBGRA;
	private Shader _shaderYUV2;
	private Shader _shaderYUV2_709;
	private Shader _shaderCopy;
	private Shader _shaderHap_YCoCg;

	private bool _isInitialised;

#if AVPROVIDEO_ISSUEPLUGINEVENT_UNITY52
	private static System.IntPtr _nativeFunction_UpdateAllTextures;
#endif
	
	//-----------------------------------------------------------------------------
	
	public static AVProQuickTimeManager Instance  
	{
		get
		{
			if (_instance == null)
			{
				_instance = (AVProQuickTimeManager)GameObject.FindObjectOfType(typeof(AVProQuickTimeManager));
				if (_instance == null)
				{
					Debug.LogWarning("AVProQuickTimeManager component required - adding dynamically now");
					GameObject go = new GameObject("AVProQuickTimeManager");
					_instance = go.AddComponent<AVProQuickTimeManager>();
				}
			}

			if (_instance == null)
			{
				Debug.LogError("AVProQuickTimeManager component required");
			}
			else
			{
				if (!_instance._isInitialised)
				{
					_instance.Init();
				}
			}

			return _instance;
		}
	}
	
	//-------------------------------------------------------------------------
	
	void Awake()
	{
		if (!_isInitialised)
		{
			_instance = this;
			Init();
		}
	}
	
	void OnDestroy()
	{
		Deinit();
	}
	
	protected bool Init()
	{
		if (_isInitialised)
		{
			return true;
		}

		try
		{
			if (AVProQuickTimePlugin.Init())
			{
				Debug.Log("[AVProQuickTime] Initialised (plugin version " + AVProQuickTimePlugin.GetPluginVersion().ToString("F2") + ", script version " + AVProQuickTimePlugin.ScriptVersion + ")");
			}
			else
			{
				Debug.LogError("[AVProQuickTime] failed to initialise.");
				this.enabled = false;
				Deinit();
				return false;
			}
		}
		catch (DllNotFoundException e)
		{
			Debug.LogError("[AVProQuickTime] Unity couldn't find the DLL.  Please move the 'Plugins' folder to the root of your project, and then restart Unity.");
			Debug.LogException(e);
			return false;
		}

#if AVPROVIDEO_ISSUEPLUGINEVENT_UNITY52
		if (_nativeFunction_UpdateAllTextures == System.IntPtr.Zero)
		{
			_nativeFunction_UpdateAllTextures = AVProQuickTimePlugin.GetRenderEventFunc();
		}
#endif

		LoadShaders();
		GetConversionMethod();
		SetUnityFeatures();

		if (_updateUsingCoroutine)
		{
			StartCoroutine("FinalRenderCapture");
		}

		_isInitialised = true;

		return _isInitialised;
	}

	private void GetConversionMethod()
	{
		bool swapRedBlue = false;

        if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11"))
        {
#if UNITY_5
			// DX11 has red and blue channels swapped around
			if (!SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
				swapRedBlue = true;
#else
            swapRedBlue = true;
#endif
        }

		if (swapRedBlue)
		{
			Shader.DisableKeyword("SWAP_RED_BLUE_OFF");
			Shader.EnableKeyword("SWAP_RED_BLUE_ON");
		}
		else
		{
			Shader.DisableKeyword("SWAP_RED_BLUE_ON");
			Shader.EnableKeyword("SWAP_RED_BLUE_OFF");
		}

        if (QualitySettings.activeColorSpace == ColorSpace.Linear)
        {
            Shader.DisableKeyword("AVPRO_GAMMACORRECTION_OFF");
            Shader.EnableKeyword("AVPRO_GAMMACORRECTION");
        }
        else
        {
            Shader.DisableKeyword("AVPRO_GAMMACORRECTION");
            Shader.EnableKeyword("AVPRO_GAMMACORRECTION_OFF");
        }
	}

	private void SetUnityFeatures()
	{
		AVProQuickTimePlugin.SetUnityFeatures(false);
	}

	void Update()
	{
#if UNITY_EDITOR
		if (_instance == null)
			return;
#endif
		if (!_updateUsingCoroutine)
		{
			int flags = AVProQuickTimePlugin.PluginID | (int)AVProQuickTimePlugin.PluginEvent.UpdateAllTextures;
#if AVPROVIDEO_ISSUEPLUGINEVENT_UNITY52
			GL.IssuePluginEvent(_nativeFunction_UpdateAllTextures, flags);
#else
			GL.IssuePluginEvent(flags);
#endif
		}
	}

	private IEnumerator FinalRenderCapture()
	{
		while (Application.isPlaying)
		{
			yield return new WaitForEndOfFrame();

			int flags = AVProQuickTimePlugin.PluginID | (int)AVProQuickTimePlugin.PluginEvent.UpdateAllTextures;
#if AVPROVIDEO_ISSUEPLUGINEVENT_UNITY52
			GL.IssuePluginEvent(_nativeFunction_UpdateAllTextures, flags);
#else
		GL.IssuePluginEvent(flags);
#endif
		}
	}

	public void Deinit()
	{
		if (_updateUsingCoroutine)
		{
			StopCoroutine("FinalRenderCapture");
		}

		// Clean up any open movies
		AVProQuickTimeMovie[] movies = (AVProQuickTimeMovie[])FindObjectsOfType(typeof(AVProQuickTimeMovie));
		if (movies != null && movies.Length > 0)
		{
			for (int i = 0; i < movies.Length; i++)
			{
				movies[i].UnloadMovie();
			}
		}

		_instance = null;
		_isInitialised = false;

#if AVPROVIDEO_ISSUEPLUGINEVENT_UNITY52
		_nativeFunction_UpdateAllTextures = System.IntPtr.Zero;
#endif
		
		AVProQuickTimePlugin.Deinit();
	}

	private void LoadShaders()
	{
		_shaderBGRA = (Shader)Resources.Load("AVProQuickTime_RedBlueSwap", typeof(Shader));
		_shaderYUV2 = (Shader)Resources.Load("AVProQuickTime_YUV2RGB", typeof(Shader));
		_shaderYUV2_709 = (Shader)Resources.Load("AVProQuickTime_YUV7092RGB", typeof(Shader));
		_shaderCopy = (Shader)Resources.Load("AVProQuickTime_Copy", typeof(Shader));
		_shaderHap_YCoCg = (Shader)Resources.Load("AVProQuickTime_YCoCg2RGB", typeof(Shader));

		if (_shaderBGRA == null ||
			_shaderYUV2 == null ||
			_shaderYUV2_709 == null ||
			_shaderCopy == null ||
			_shaderHap_YCoCg == null)
		{
			Debug.LogError("[AVProQuickTime] Failed to load shader resources", this);
		}
	}

	public Shader GetPixelConversionShader(AVProQuickTimePlugin.PixelFormat format, bool yuvHD)
	{
		Shader result = null;
		switch (format)
		{
		case AVProQuickTimePlugin.PixelFormat.RGBA32:
			result = _shaderBGRA;
			break;
		case AVProQuickTimePlugin.PixelFormat.YCbCr:
			result = _shaderYUV2;
			if (yuvHD)
				result = _shaderYUV2_709;
			break;
		case AVProQuickTimePlugin.PixelFormat.Hap_RGB:
			result = _shaderCopy;
			break;
		case AVProQuickTimePlugin.PixelFormat.Hap_RGBA:
			result = _shaderCopy;
			break;
		case AVProQuickTimePlugin.PixelFormat.Hap_RGB_HQ:
			result = _shaderHap_YCoCg;
			break;
		default:
			Debug.LogError("[AVProQuickTime] Unknown video format '" + format);
			break;
		}
		return result;
	}
}