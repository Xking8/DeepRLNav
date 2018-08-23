using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
	public int MaxSE = 10;
	public List<AudioClip> bgmList;
	public List<AudioClip> seList;

	private AudioSource bgmAudioSource;
	private List<AudioSource> seAudioSources;
	private Dictionary<string, AudioClip> bgmDict;
	private Dictionary<string, AudioClip> seDict;

	new void Awake ()
	{
		base.Awake ();
		Setup ();
	}

	private void Setup ()
	{
		// create listener
		if (FindObjectsOfType (typeof(AudioListener)).All (o => !((AudioListener)o).enabled)) {
			this.gameObject.AddComponent<AudioListener> ();
		}

		// create audio sources
		this.bgmAudioSource = this.gameObject.AddComponent<AudioSource> ();
		this.seAudioSources = new List<AudioSource> ();

		// create clip dictionaries
		this.bgmDict = new Dictionary<string, AudioClip> ();
		this.seDict = new Dictionary<string, AudioClip> ();

		Action<Dictionary<string,AudioClip>,AudioClip> addClipDict = (dict, c) => {
			if (!dict.ContainsKey (c.name)) {
				dict.Add (c.name, c); 
			}
		};

		this.bgmList.ForEach (bgm => addClipDict (this.bgmDict, bgm));
		this.seList.ForEach (se => addClipDict (this.seDict, se));
	}

	public void PlaySE (string seName)
	{
		if (!this.seDict.ContainsKey (seName))
		{
			throw new ArgumentException (seName + " not found", "seName");
		}

		AudioSource source = this.seAudioSources.FirstOrDefault (s => !s.isPlaying);
		if (source == null) {
			if (this.seAudioSources.Count >= this.MaxSE) {
				Debug.Log ("SE AudioSource is full");
				return;
			}

			source = this.gameObject.AddComponent<AudioSource> ();
			this.seAudioSources.Add (source);
		}

		source.clip = this.seDict [seName];
		source.Play ();
	}

	public void StopSE ()
	{
		this.seAudioSources.ForEach (s => s.Stop ());
	}

	public void PlayBGM (string bgmName)
	{
		if (!this.bgmDict.ContainsKey (bgmName))
		{
			throw new ArgumentException (bgmName + " not found", "bgmName");
		}

		if (this.bgmAudioSource.clip == this.bgmDict [bgmName])
		{
			return;
		}

		this.bgmAudioSource.Stop ();
		this.bgmAudioSource.clip = this.bgmDict [bgmName];
		// BGM is loop
		this.bgmAudioSource.loop = true;
		this.bgmAudioSource.Play ();
	}

	public void StopBGM ()
	{
		this.bgmAudioSource.Stop ();
		this.bgmAudioSource.clip = null;
	}
}
