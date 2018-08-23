using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PausePlane : MonoBehaviour
{
	public GameObject restartButtonGO;
	public GameObject quitButtonGO;
	public GameObject textGO;
	public GameObject pauseGO;
	public Image bgImage;

	// Use this for initialization
	void Start ()
	{
		ActiveItems (false);
	}

	// Update is called once per frame
	void Update ()
	{
	}

	/// <summary>
	/// Actives the items.
	/// </summary>
	/// <param name="isActive">If set to <c>true</c> is active.</param>
	private void ActiveItems (bool isActive)
	{
		restartButtonGO.SetActive (isActive);
		quitButtonGO.SetActive (isActive);
		textGO.SetActive (isActive);
		bgImage.enabled = isActive;
	}

	/// <summary>
	/// Pause the specified pauseGO.
	/// </summary>
	public void Pause ()
	{
		GameMain.Instance.SetForPauseUIs (false);
		pauseGO.SetActive (false);
		ActiveItems (true);
		Time.timeScale = 0;
	}

	public void Restart ()
	{
		GameMain.Instance.SetForPauseUIs (true);
		ActiveItems (false);
		pauseGO.SetActive (true);
		Time.timeScale = 1;
		SpawnManager.Instance.Reset ();
	}

	public void Quit ()
	{
		StartPanel.Instance.StopGame ();
		SoundManager.Instance.StopBGM ();
	}
}
