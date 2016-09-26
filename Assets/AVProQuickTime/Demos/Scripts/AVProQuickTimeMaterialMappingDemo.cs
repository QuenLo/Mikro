using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AVProQuickTimeMaterialMappingDemo : MonoBehaviour
{
	public AVProQuickTimeMovie _movie;
	public GUISkin _skin;
	
	private bool _visible = false;
	private float _alpha = 1.0f;
	
	public void OnGUI()
	{
		GUI.skin = _skin;
	
		if (_visible)
		{
			GUI.color = new Color(1f, 1f, 1f, _alpha);
			GUILayout.BeginArea(new Rect(0, 0, 740, 310), GUI.skin.box);
			ControlWindow(0);
			GUILayout.EndArea();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f - _alpha);
		GUI.Box(new Rect(0, 0, 128, 32), "Demo Controls");
	}
	
	void Update()
	{
		Rect r = new Rect(0, 0, 740, 310);
		if (r.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
		{
			_visible = true;
			_alpha = 1.0f;
		}
		else
		{
			_alpha -= Time.deltaTime * 4f;
			if (_alpha <= 0.0f)
			{
				_alpha = 0.0f;
				_visible = false;
			}
		}
	}
	
	private void ControlWindow(int id)
	{
		if (_movie == null)
			return;
		
		GUILayout.Space(16f);
		
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Folder: ", GUILayout.Width(80));
		_movie._folder = GUILayout.TextField(_movie._folder, 192);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("File: ", GUILayout.Width(80));
		_movie._filename = GUILayout.TextField(_movie._filename, 128, GUILayout.Width(350));
		if (GUILayout.Button("Load File", GUILayout.Width(90)))
		{
			_movie._source = AVProQuickTimePlugin.MovieSource.LocalFile;
			_movie.LoadMovie();
		}
		if (GUILayout.Button("Load URL", GUILayout.Width(90)))
		{
			_movie._source = AVProQuickTimePlugin.MovieSource.URL;
			_movie.LoadMovie();
		}
#if !UNITY_WEBPLAYER
		if (GUILayout.Button("Load Memory", GUILayout.Width(110)))
		{
			_movie._source = AVProQuickTimePlugin.MovieSource.Memory;
			string fullPath = System.IO.Path.Combine(_movie._folder, _movie._filename);
			if (System.IO.File.Exists(fullPath))
			{
				_movie._movieData = System.IO.File.ReadAllBytes(fullPath);
				_movie.LoadMovie();
			}
		}
#endif
		GUILayout.EndHorizontal();
				
		AVProQuickTime moviePlayer = _movie.MovieInstance;
		if (moviePlayer != null)
		{	
			GUILayout.BeginHorizontal();
			GUILayout.Label("Loaded ", GUILayout.Width(80));
			GUILayout.HorizontalSlider(moviePlayer.LoadedSeconds, 0.0f, moviePlayer.DurationSeconds, GUILayout.Width(200));
			if (moviePlayer.DurationSeconds > 0f)
				GUILayout.Label(((moviePlayer.LoadedSeconds * 100f) / moviePlayer.DurationSeconds) + "%");
			else
				GUILayout.Label("0%");
			GUILayout.EndHorizontal();

			
			if (moviePlayer.LoadedSeconds > 0f)
			{
				GUILayout.Label("Resolution: " + moviePlayer.Width + "x" + moviePlayer.Height + " @ " + moviePlayer.FrameRate.ToString("F2") + "hz");
			
			
				GUILayout.BeginHorizontal();
				GUILayout.Label("Volume ", GUILayout.Width(80));
				float volume = _movie._volume;
				float newVolume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f, GUILayout.Width(200));
				if (volume != newVolume)
				{
					_movie._volume = newVolume;
				}
				GUILayout.Label(_movie._volume.ToString("F1"));
				GUILayout.EndHorizontal();
				
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Time ", GUILayout.Width(80));
				float position = moviePlayer.PositionSeconds;
				float newPosition = GUILayout.HorizontalSlider(position, 0.0f, moviePlayer.DurationSeconds, GUILayout.Width(200));
				if (position != newPosition)
				{
					moviePlayer.PositionSeconds = newPosition;
				}
				GUILayout.Label(moviePlayer.PositionSeconds.ToString("F1") + " / " + moviePlayer.DurationSeconds.ToString("F1") + "s");
				
				if (GUILayout.Button("Play"))
				{
					moviePlayer.Play();
				}
				if (GUILayout.Button("Pause"))
				{
					moviePlayer.Pause();
				}
			
				GUILayout.EndHorizontal();
				
				
				GUILayout.BeginHorizontal();
				
				GUILayout.Label("Frame " + moviePlayer.Frame.ToString() + " / " + moviePlayer.FrameCount.ToString());
		
				if (GUILayout.Button("<", GUILayout.Width(50)))
				{
					if (moviePlayer.Frame > 0)
						moviePlayer.Frame--;
				}
				if (GUILayout.Button(">", GUILayout.Width(50)))
				{
					moviePlayer.Frame++;
				}
				
				GUILayout.EndHorizontal();
				
				

				if (!moviePlayer.IsPaused)
				{				
					GUILayout.BeginHorizontal();
					GUILayout.Label("Rate: " + moviePlayer.PlaybackRate.ToString("F2") + "x");
					
					if (GUILayout.Button("Reverse", GUILayout.Width(72)))
					{
						moviePlayer.PlaybackRate = -moviePlayer.PlaybackRate;
					}
					
					if (GUILayout.Button("-", GUILayout.Width(50)))
					{
						moviePlayer.PlaybackRate = moviePlayer.PlaybackRate * 0.5f;
					}
			
					if (GUILayout.Button("+", GUILayout.Width(50)))
					{
						moviePlayer.PlaybackRate = moviePlayer.PlaybackRate * 2.0f;
					}
					
					if (GUILayout.Button("Reset", GUILayout.Width(50)))
					{
						moviePlayer.PlaybackRate = 1.0f;
					}
					
					GUILayout.EndHorizontal();
				}
			}
		}

		GUILayout.EndVertical();
	}
}