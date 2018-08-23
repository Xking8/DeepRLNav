using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMain : SingletonMonoBehaviour<GameMain>
{
	public const int MAX_GAME_LEVEL = 5;
	public bool isDebug;
	public Image flashImage;
	public DebugLabel debugLabel;
	// hide object when gameover
	public GameObject distanceGO;
	public GameObject pauseGO;
	public GameObject pointGO;
	// control
	public GameObject leftButtonGO;
	public GameObject rightButtonGO;
	// 1-5
	/*
	 * 初級編. 1
	 * 中級編. 2
	 * 上級編. 3
	 * 究極編. 4
	 * 超究極編. 5
	 */
	public int gameLevel;

	public bool IsBossTurn { get; set; }

	new void Awake ()
	{
		base.Awake ();
	}

	// Use this for initialization
	void Start ()
	{
		gameLevel = 1;
	}

	public void RunBlink ()
	{
		StartCoroutine ("Blink");
	}

	private IEnumerator Blink ()
	{
		for (int i=0; i<10; i++) {
			flashImage.enabled = true;
			yield return new WaitForSeconds (0.001f);
			flashImage.enabled = false;
			yield return 0;
		}

		flashImage.enabled = false;
	}

	public void RunFadein ()
	{
		StartCoroutine ("Fadein");
	}

	private IEnumerator Fadein ()
	{
		flashImage.enabled = true;
		float fadeAlpha = 1.0f;
		float interval = 2.0f;

		float time = 0.0f;
		while (time <= interval) {
			fadeAlpha = Mathf.Lerp (1f, 0f, time / interval);
			flashImage.color = new Color (1, 1, 1, fadeAlpha);
			time += Time.deltaTime;
			yield return 0;
		}

		flashImage.enabled = false;
		flashImage.color = Color.white;
	}

	public void RaiseLevel ()
	{
		gameLevel += 1;
	}

	// it is all reset
	public void Reset (bool isFirst = true)
	{
		IsBossTurn = false;

		if (isFirst) {
			gameLevel = 1;
			Player.Instance.Reset ();
			Boss.Instance.Reset ();
		}

		// later gameLevel = 1;
		DebugLabel.Instance.SetMessage ("Level: " + GetLevelName ());

		SpeedManager.Instance.Reset ();

		// later SpeedManager
		if (isFirst) {
			CameraSystem.Instance.Reset ();
			PointManager.Instance.Reset ();
		} else {
			CameraSystem.Instance.ResetFieldOfView ();
		}

		EffectSystem.Instance.Reset ();

		StartCoroutine (PlayGame ());

		leftButtonGO.SetActive (true);
		rightButtonGO.SetActive (true);
		SpawnManager.Instance.ChangeReturn ();
	}

	public void HideButtons ()
	{
		leftButtonGO.SetActive (false);
		rightButtonGO.SetActive (false);
	}

	public IEnumerator PlayGame ()
	{
		ResultScreen.Instance.SetActive (false);
		SetForGameMainUIs (true);
		GoalObject.Instance.Reset ();
		SoundManager.Instance.PlayBGM ("PQ_bgm");
		EffectSystem.Instance.PlayStartEffect ();
		// waiting
		yield return new WaitForSeconds (5.0f);
		// real start
		CameraSystem.Instance.Begin ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			Right ();
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			Left ();
		} else {
			Straight ();
		}
	}

	public void SetForPauseUIs (bool isActive)
	{
		distanceGO.SetActive (isActive);
		pointGO.SetActive (isActive);
	}

	public void SetForGameMainUIs (bool isActive)
	{
		pauseGO.SetActive (isActive);
		SetForPauseUIs (isActive);
	}

	public void Left ()
	{
		Player.Instance.Left ();
	}

	public void Right ()
	{
		Player.Instance.Right ();
	}

	public void Straight ()
	{
		Player.Instance.Straight ();
	}

	public string GetLevelName ()
	{
		// it is for debug
		string levelName = "-";
		switch (gameLevel) {
		case 1:
			levelName = "初級編";
			break;
		case 2:
			levelName = "中級編";
			break;
		case 3:
			levelName = "上級編";
			break;
		case 4:
			levelName = "究極編";
			break;
		case 5:
			levelName = "超究極編";
			break;
		}

		return levelName;
	}
}
