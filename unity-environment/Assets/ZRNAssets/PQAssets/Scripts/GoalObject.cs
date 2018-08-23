using UnityEngine;
using System.Collections;

public class GoalObject : SingletonMonoBehaviour<GoalObject>
{
	private Transform myTransform;
	private SphereCollider mySphereCollider;

	new void Awake ()
	{
		base.Awake ();
		myTransform = transform;
		mySphereCollider = GetComponent<SphereCollider> ();
		Reset ();
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public void Reset ()
	{
		mySphereCollider.enabled = false;
	}

	public void SetUp ()
	{
		Vector3 bossPos = Boss.Instance.transform.position;
		myTransform.position = new Vector3 (bossPos.x, 1.0f, bossPos.z);
		StartCoroutine ("LaterEnable");
	}

	private IEnumerator LaterEnable ()
	{
		// it is enough to changing time
		yield return new WaitForSeconds (10.0f);
		mySphereCollider.enabled = true;
	}

	void OnTriggerEnter (Collider other)
	{
		// change to Player turn
		if (other.tag == "Player") {
			Player player = Player.Instance;
			mySphereCollider.enabled = false;
			player.Win ();
			Boss.Instance.Reset ();

			CameraSystem.Instance.Stop();
			StartCoroutine ("StartPlayerTurn");
		}
	}

	private IEnumerator StartPlayerTurn ()
	{
		GameMain gameMain  = GameMain.Instance;
		// check last level or not
		int level = GameMain.Instance.gameLevel;

		yield return new WaitForSeconds (4.0f);

		if (level == GameMain.MAX_GAME_LEVEL)
		{
			GameMain.Instance.SetForGameMainUIs (false);
			ResultScreen.Instance.SetActive (true);
			EffectSystem.Instance.PlayGameClearEffect ();
		}
		else
		{
			// fading in
			gameMain.RunFadein ();
			Player.Instance.ChangeMyTurn ();
			gameMain.RaiseLevel ();
			// RaiseLevel later
			//CameraSystem.Instance.Reset ();

			// fix turn spawn... and camera moving
			SpeedManager.Instance.Reset ();
			SpawnManager.Instance.ChangeReturn ();
			// it is not first
			bool isFirst = false;
			gameMain.Reset (isFirst);
		}
	}
}
