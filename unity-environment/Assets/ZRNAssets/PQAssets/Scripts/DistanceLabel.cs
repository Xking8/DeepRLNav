using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DistanceLabel : SingletonMonoBehaviour<DistanceLabel>
{
	public Text meterLabel;
	public Text meterShadowLabel;
	public Image level1;
	public Image level2;
	public Image level3;
	public Image level4;
	public Image level5;

	private const int BASE_MATRE = 100;

	new void Awake ()
	{
		base.Awake ();
	}

	// Use this for initialization
	void Start ()
	{
		Refresh ();
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public void Refresh ()
	{
		SpeedManager sm = SpeedManager.Instance;
		SetGauge (sm.spedLevel);

		if (GameMain.Instance.IsBossTurn) {
			float distance = (sm.spedLevel - 1) * BASE_MATRE;
			meterLabel.text = distance.ToString ();
			meterShadowLabel.text = distance.ToString ();
		} else {
			float distance = (SpeedManager.MAX_SPEED_LEVEL - sm.spedLevel) * BASE_MATRE;
			meterLabel.text = distance.ToString ();
			meterShadowLabel.text = distance.ToString ();
		}
	}

	public void SetGauge (int level)
	{
		level1.enabled = true;
		level2.enabled = level > 1;
		level3.enabled = level > 2;
		level4.enabled = level > 3;
		level5.enabled = level > 4;
	}
}
