using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScreen : SingletonMonoBehaviour<ResultScreen>
{
	public Text pointText;
	public Text pointTextShadow;
	public Image bgImage;
	public GameObject restartButtonGO;
	public GameObject quitButtonGO;
	public GameObject pointGO;

	new void Awake ()
	{
		base.Awake ();
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public void SetActive (bool isEnable)
	{
		pointGO.SetActive (isEnable);
		restartButtonGO.SetActive (isEnable);
		quitButtonGO.SetActive (isEnable);

		int point = PointManager.Instance.Point;
		pointText.text = point.ToString ();
		pointTextShadow.text = point.ToString ();
	}

	public void Restart ()
	{
		GameMain.Instance.Reset ();
	}

	public void Quit ()
	{
		StartPanel.Instance.StopGame ();
		SoundManager.Instance.StopBGM ();
	}
}
