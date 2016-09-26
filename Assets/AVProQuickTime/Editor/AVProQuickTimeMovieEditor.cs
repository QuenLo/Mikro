// Support for Editor.RequiresConstantRepaint()
#if UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_5
#define AVPROQUICKTIME_UNITYFEATURE_EDITORAUTOREFRESH
#endif
using UnityEngine;
using UnityEditor;
using System.Collections; 

//-----------------------------------------------------------------------------
// Copyright 2012-2016 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

[CustomEditor(typeof(AVProQuickTimeMovie))]
public class AVProQuickTimeMovieEditor : Editor
{
	private AVProQuickTimeMovie _movie;
	private bool _showAlpha;

#if AVPROQUICKTIME_UNITYFEATURE_EDITORAUTOREFRESH
	public override bool RequiresConstantRepaint()
	{
		return (_movie != null && _movie._editorPreview && _movie.MovieInstance != null);
	}
#endif

#if UNITY_EDITOR_WIN
	private static void ShowInExplorer(string itemPath)
	{
		itemPath = System.IO.Path.GetFullPath(itemPath.Replace(@"/", @"\"));   // explorer doesn't like front slashes
		if (System.IO.File.Exists(itemPath))
		{
			System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
		}
	}
#endif

	public override void OnInspectorGUI()
	{
		_movie = (this.target) as AVProQuickTimeMovie;

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Load Options", EditorStyles.boldLabel);
		//DrawDefaultInspector();
		_movie._useStreamingAssetsPath = EditorGUILayout.Toggle("Use StreamingAssets", _movie._useStreamingAssetsPath);
		_movie._folder = EditorGUILayout.TextField("Folder", _movie._folder);
		_movie._filename = EditorGUILayout.TextField("Filename", _movie._filename);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Source");
		_movie._source = (AVProQuickTimePlugin.MovieSource)EditorGUILayout.EnumPopup(_movie._source);
		EditorGUILayout.EndHorizontal();
		_movie._allowYUV = EditorGUILayout.Toggle("Allow YUV", _movie._allowYUV);
		if (_movie._allowYUV)
		{
			_movie._useYUVHD = EditorGUILayout.Toggle("Use YUV Rec709", _movie._useYUVHD);
		}

		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Performance", EditorStyles.boldLabel);
		_movie._ignoreFlips = EditorGUILayout.Toggle("Ignore Flips", _movie._ignoreFlips);


		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Start Options", EditorStyles.boldLabel);
		_movie._loadOnStart = EditorGUILayout.Toggle("Load On Start", _movie._loadOnStart);
		_movie._playOnStart = EditorGUILayout.Toggle("Play On Start", _movie._playOnStart);
		//_movie._loadFirstFrame = EditorGUILayout.Toggle("Load First Frame", _movie._loadFirstFrame);

		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Playback Options", EditorStyles.boldLabel);
		_movie._loop = EditorGUILayout.Toggle("Loop", _movie._loop);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Audio Volume");
		_movie._volume = EditorGUILayout.Slider(_movie._volume, 0.0f, 1.0f);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Audio Balance");
		_movie._audioBalance = EditorGUILayout.Slider(_movie._audioBalance, -1.0f, 1.0f);
		EditorGUILayout.EndHorizontal();


		GUILayout.Space(8.0f);

		AVProQuickTime media = _movie.MovieInstance;
		
		GUI.enabled = (_movie != null && _movie.MovieInstance != null);
		_movie._editorPreview = EditorGUILayout.Foldout(_movie._editorPreview, "Video Preview");
		
		GUI.enabled = true;
		if (_movie._editorPreview && _movie.MovieInstance != null)
		{
			{
				Texture texture = _movie.OutputTexture;
				if (texture == null)
					texture = EditorGUIUtility.whiteTexture;

                float ratio = (float)texture.width / (float)texture.height;

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                Rect textureRect = GUILayoutUtility.GetRect(Screen.width / 2, Screen.width / 2, (Screen.width / 2) / ratio, (Screen.width / 2) / ratio);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

				_showAlpha = GUILayout.Toggle(_showAlpha, "Show Alpha Channel");
				
				Matrix4x4 prevMatrix = GUI.matrix;
                if (_movie.MovieInstance.RequiresFlipY)
                {
                    GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0, textureRect.y + (textureRect.height / 2)));
                }
				
				if (!_showAlpha)
					GUI.DrawTexture(textureRect, texture, ScaleMode.ScaleToFit);
				else
					EditorGUI.DrawTextureAlpha(textureRect, texture, ScaleMode.ScaleToFit);

				GUI.matrix = prevMatrix;
				
				GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Select Texture", GUILayout.ExpandWidth(false)))
				{
					Selection.activeObject = texture;
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				if (Application.isPlaying && media != null)
				{
					GUILayout.Label(string.Format("{0}x{1} @ {2}fps {3} secs", media.Width, media.Height, media.FrameRate.ToString("F2"), media.DurationSeconds.ToString("F2")));
				}

				if (media != null && media.FramesTotal > 30)
				{
					GUILayout.Label("Displaying at " + media.DisplayFPS.ToString("F1") + " fps");
				}
				else
				{
					GUILayout.Label("Displaying at ... fps");	
				}
			}

			if (Application.isPlaying)
			{
				if (media != null)
				{
					GUILayout.Space(8.0f);
					
					//EditorGUILayout.LabelField("Drawn:" + AVProQuickTimePlugin.GetNumFramesDrawn(_movie.MovieInstance.Handle));

					EditorGUILayout.LabelField("Frame:");
					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("<", GUILayout.ExpandWidth(false)))
					{
						media.Frame--;
					}
					uint currentFrame = media.Frame;
					if (currentFrame != uint.MaxValue)
					{
						int newFrame = EditorGUILayout.IntSlider((int)currentFrame, 0, (int)media.FrameCount);
						if (newFrame != currentFrame)
						{
							media.Frame = (uint)newFrame;
						}
					}
					if (GUILayout.Button(">", GUILayout.ExpandWidth(false)))
					{
						media.Frame++;
					}
					EditorGUILayout.EndHorizontal();			
					
					if (!media.IsPlaying)
					{
						if (GUILayout.Button("Unpause Stream"))
						{
							_movie.Play();
						}
					}
					else
					{
						if (GUILayout.Button("Pause Stream"))
						{
							_movie.Pause();
						}
					}
#if !AVPROQUICKTIME_UNITYFEATURE_EDITORAUTOREFRESH
					if (media.IsPlaying)
					{
						this.Repaint();
					}
#endif
				}
			}
		}

		if (GUI.changed)
		{
			EditorUtility.SetDirty(_movie);
		}
	}
}