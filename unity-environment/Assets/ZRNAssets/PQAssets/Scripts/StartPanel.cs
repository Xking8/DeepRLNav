using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartPanel : SingletonMonoBehaviour<StartPanel>
{
	public PausePlane pausePlane;
	public Image blackBgImage;
	public Image bgImage;
	public Image logoImage;
	public Image leftCharaImage;
	public Image rightCharaImage;
	public GameObject startButtonGO;
	public GameObject tutorialButtonGo;

	new void Awake ()
	{
		base.Awake ();
	}

	// Use this for initialization
	void Start ()
	{
		StopGame ();
	}

	// Update is called once per frame
	void Update ()
	{
	}

	private void SetEnable (bool isActive)
	{
		blackBgImage.enabled = isActive;
		bgImage.enabled = isActive;
		startButtonGO.SetActive (isActive);
		tutorialButtonGo.SetActive (isActive);
		leftCharaImage.enabled = isActive;
		rightCharaImage.enabled = isActive;
		logoImage.enabled = isActive;
	}

	public void StopGame ()
	{
		SetEnable (true);
		pausePlane.Pause ();
	}

	public void StartGame ()
	{
		DebugLabel.Instance.SetMessage ("StartGame");
		SetEnable (false);
		pausePlane.Restart ();
		GameMain.Instance.Reset ();
	}

	public void ShowTutorial(){
		Application.LoadLevel ("TutorialScene");
	}
	
}
