using UnityEngine;
using System.Collections;

public class SpeedManager : SingletonMonoBehaviour<SpeedManager>
{
	public int spedLevel;
	public static int MAX_SPEED_LEVEL = 5;
	// player and enemy distance
	private const float DISTANCE = -3.2f;
	private float defaultX;
	private Transform myTransform;
	private Vector3 defaultPos;

	new void Awake ()
	{
		base.Awake ();
		myTransform = transform;
		defaultPos = myTransform.localPosition;
		// 1 is default
		Reset ();
	}

	// Use this for initialization
	void Start ()
	{
		// debug mode enemy
		if (GameMain.Instance.isDebug)
		{
			MAX_SPEED_LEVEL = 2;
		}
	}

	public void Reset ()
	{
		spedLevel = 1;
		myTransform.localPosition = defaultPos;
		DistanceLabel.Instance.Refresh ();
	}

	public void ResetForBossTurn ()
	{
		spedLevel = MAX_SPEED_LEVEL;
		DistanceLabel.Instance.Refresh ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameMain.Instance.IsBossTurn) {
			BossTurnUpdate ();
		} else {
			PlayerTurnUpdate ();
		}
	}

	private void PlayerTurnUpdate ()
	{
		// 1 is default
		float to = DISTANCE * (spedLevel - 1) / (MAX_SPEED_LEVEL - 1);

		float from = myTransform.localPosition.x;
		// big is slow. small is high
		float split = 100f;
		float speed = 200f;
		myTransform.localPosition = new Vector3 (Mathf.Lerp (from, to, Time.deltaTime * speed / split), 0, 0);
	}

	private void BossTurnUpdate ()
	{
		float to = DISTANCE - DISTANCE * (spedLevel - 1) / (MAX_SPEED_LEVEL - 1);

		float from = myTransform.localPosition.x;
		// big is slow. small is high
		float split = 100f;
		float speed = 200f;
		myTransform.localPosition = new Vector3 (Mathf.Lerp (from, to, Time.deltaTime * speed / split), 0, 0);
	}

	public void SpeedUp ()
	{
		spedLevel = Mathf.Min (MAX_SPEED_LEVEL, spedLevel + 1);
		DebugLabel.Instance.SetMessage ("SPEED: " + spedLevel.ToString ());
		DistanceLabel.Instance.Refresh ();
	}

	public void SpeedDown ()
	{
		spedLevel = Mathf.Max (1, spedLevel - 1);
		DebugLabel.Instance.SetMessage ("SPEED: " + spedLevel.ToString ());
		DistanceLabel.Instance.Refresh ();
	}
}
