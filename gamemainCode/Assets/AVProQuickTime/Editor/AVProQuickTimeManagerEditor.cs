// Support for Editor.RequiresConstantRepaint()
#if UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_5
	#define AVPROQUICKTIME_UNITYFEATURE_EDITORAUTOREFRESH
#endif
// Supports Unity 4.x features
#if UNITY_5 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0_1 || UNITY_4_0 
	#define AVPRO_UNITY_4_X
#endif
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//-----------------------------------------------------------------------------
// Copyright 2012-2016 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

[CustomEditor(typeof(AVProQuickTimeManager))]
public class AVProQuickTimeManagerEditor : Editor
{
	private AVProQuickTimeManager _manager;
	private AVProQuickTimeMovie[] _movies;

	private void UpdateMovies()
	{
		_movies = (AVProQuickTimeMovie[])FindObjectsOfType(typeof(AVProQuickTimeMovie));
	}

#if AVPROQUICKTIME_UNITYFEATURE_EDITORAUTOREFRESH
	public override bool RequiresConstantRepaint()
	{
		return (_movies != null && _movies.Length > 0 && Application.isPlaying);
	}
#endif

	public override void OnInspectorGUI()
	{
		_manager = (this.target) as AVProQuickTimeManager;

		if (!Application.isPlaying)
		{
			this.DrawDefaultInspector();
		}

		if (GUILayout.Button ("Update"))
		{
			UpdateMovies();
		}

		if (_movies != null && _movies.Length > 0)
		{
			for (int i = 0; i < _movies.Length; i++)
			{
				GUILayout.BeginHorizontal();

				GUI.color = Color.white;
				if (!_movies[i].enabled ||
#if !AVPRO_UNITY_4_X
				    !_movies[i].gameObject.active
#else
				    !_movies[i].gameObject.activeInHierarchy
#endif
				    )
					GUI.color = Color.grey;

				AVProQuickTime media = _movies[i].MovieInstance;
				if (media != null)
				{
					GUI.color = Color.yellow;
					if (media.IsPlaying)
						GUI.color = Color.green;
				}

				if (GUILayout.Button("S"))
				{
					Selection.activeObject = _movies[i];
				}
				GUILayout.Label(i.ToString("D2") + " " + _movies[i].name, GUILayout.MinWidth(128f));
				//GUILayout.FlexibleSpace();
				if (media != null)
				{
					GUILayout.Label(media.Width + "x" + media.Height);
					GUILayout.FlexibleSpace();
					GUILayout.Label(string.Format("{0:00.0}", media.DisplayFPS) + " FPS");
					//GUILayout.FlexibleSpace();
				}
				else
				{
					GUILayout.FlexibleSpace();
				}



				GUILayout.EndHorizontal();

				if (media != null)
				{
					GUILayout.HorizontalSlider(media.PositionSeconds, 0f, media.DurationSeconds, GUILayout.MinWidth(128f), GUILayout.ExpandWidth(true));
				}
			}

#if !AVPROQUICKTIME_UNITYFEATURE_EDITORAUTOREFRESH
			if (Application.isPlaying)
			{
				this.Repaint();
			}
#endif
		}
		else
		{
			if (Event.current.type.Equals(EventType.Repaint))
			{
				UpdateMovies();
			}
		}

		if (GUI.changed)
		{
			EditorUtility.SetDirty(_manager);
		}		
	}
}