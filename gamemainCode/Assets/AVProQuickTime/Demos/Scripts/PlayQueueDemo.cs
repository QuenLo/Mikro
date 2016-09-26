using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayQueueDemo : MonoBehaviour
{
	public AVProQuickTimeMovie _movieA;
	public AVProQuickTimeMovie _movieB;
	public string _folder;
	public List<string> _filenames;
	
	private AVProQuickTimeMovie[] _movies;
	private int _moviePlayIndex;
	private int _movieLoadIndex;
	private int _index = -1;
	private bool _loadSuccess = true;
	private int _playItemIndex = -1;
	
	public AVProQuickTimeMovie PlayingMovie  { get { return _movies[_moviePlayIndex]; } }
	public AVProQuickTimeMovie LoadingMovie  { get { return _movies[_movieLoadIndex]; } }
	public int PlayingItemIndex { get { return _playItemIndex; } }
	public bool IsPaused { get { if (PlayingMovie.MovieInstance != null) return !PlayingMovie.MovieInstance.IsPlaying; return false; } }

	void Start()
	{
		_movieA._loop = false;
		_movieB._loop = false;
		_movies = new AVProQuickTimeMovie[2];
		_movies[0] = _movieA;
		_movies[1] = _movieB;
		_moviePlayIndex = 0;
		_movieLoadIndex = 1;
		
		NextMovie();
	}
	
	void Update() 
	{
		if (PlayingMovie.MovieInstance != null)
		{
			if (PlayingMovie.MovieInstance.IsFinishedPlaying || PlayingMovie.MovieInstance.Frame >= PlayingMovie.MovieInstance.FrameCount)
			{
				NextMovie();
			}
		}
		
		if (!_loadSuccess)
		{
			_loadSuccess = true;
			NextMovie();
		}
	}
	
	void OnGUI()
	{
		bool needsFlip = false;
		Texture texture = PlayingMovie.OutputTexture;
		if (PlayingMovie.MovieInstance != null)
		{
			needsFlip = PlayingMovie.MovieInstance.RequiresFlipY;
		}
		if (texture == null)
		{
			texture = LoadingMovie.OutputTexture;		// Display the previous video until the current one has loaded the first frame
			if (LoadingMovie.MovieInstance != null)
			{
				needsFlip = LoadingMovie.MovieInstance.RequiresFlipY;
			}
		}

		if (texture != null)
		{
			Rect rect = new Rect(0, 0, Screen.width, Screen.height);

			if (needsFlip)
			{
				GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), new Vector2(0, rect.y + (rect.height / 2)));
			}

			GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit, false);
		}
	}
	
	public void Next()
	{
		NextMovie();
	}
	
	public void Previous()
	{
		_index -= 2;
		if (_index < 0)
			_index += _filenames.Count;
		
		NextMovie();
	}
	
	public void Pause()
	{
		if (PlayingMovie != null)
		{
			PlayingMovie.Pause();
		}
	}
	
	public void Unpause()
	{
		if (PlayingMovie != null)
		{
			PlayingMovie.Play();
		}
	}
	
	private void NextMovie()
	{	
		Pause();
			
		if (_filenames.Count > 0)
		{
			_index = (Mathf.Max(0, _index+1))%_filenames.Count;
		}
		else
			_index = -1;
		
		if (_index < 0)
			return;
		
	
		LoadingMovie._folder = _folder;
		LoadingMovie._filename = _filenames[_index];
		LoadingMovie._playOnStart = true;
		_loadSuccess = LoadingMovie.LoadMovie();
		_playItemIndex = _index;
		
		_moviePlayIndex = (_moviePlayIndex + 1)%2;
		_movieLoadIndex = (_movieLoadIndex + 1)%2;		
	}
}
