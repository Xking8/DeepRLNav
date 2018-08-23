using UnityEngine;
using System.Collections;

public class Boss : SingletonMonoBehaviour<Boss>
{
	public float returnBackDeltaTime = 4;
	private bool isAlive;
	private float myTime;
	private Transform myTransform;
	private QueryAnimationController queryAnimationController;
	private Vector3 defaultPosition;
	private CapsuleCollider myCollider;
	private Quaternion defaultQuaternion;

	new void Awake ()
	{
		base.Awake ();
		myTransform = transform;
		queryAnimationController = GetComponent<QueryAnimationController> ();
		defaultPosition = myTransform.localPosition;
		defaultQuaternion = myTransform.localRotation;
		myCollider = GetComponent<CapsuleCollider> ();
	}

	// Use this for initialization
	void Start ()
	{
	}

	public void Reset ()
	{
		myTime = 0;
		Straight ();
		isAlive = true;
		myTransform.localPosition = defaultPosition;
		myTransform.localRotation = defaultQuaternion;
		myCollider.enabled = true;
	}

	public void Straight ()
	{
		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Straight);
	}

	// Update is called once per frame
	void Update ()
	{
		if (isAlive) {
			if (GameMain.Instance.IsBossTurn) {
				Stoke ();
			} else {
				Escape ();
			}
		}
	}

	private void Escape ()
	{
		myTime += Time.deltaTime;
		if (myTime > returnBackDeltaTime) {
			ReturnBack ();
			myTime = 0;
		}
	}

	private void Stoke ()
	{
		// Debug.Log ("Stoke");
	}

	private void ReturnBack ()
	{
		DebugLabel.Instance.SetMessage ("Black Query: ReturnBack");
		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.ReturnBack);
	}

	private void ChangeTurn ()
	{
		StartCoroutine ("HalfRotate");
	}

	private IEnumerator HalfRotate ()
	{
		int timetodothisloop = 20;
		for (var i = 0; i < timetodothisloop; i++) {
			myTransform.Rotate (0, 180 / timetodothisloop, 0);
			yield return null;
		}
	}

	void OnTriggerStay (Collider other)
	//void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			myCollider.enabled = false;
			if (GameMain.Instance.IsBossTurn) {
				RunLoseAnimPlayer (other);
			} else {
				StartCoroutine (RunChangeTurn (other));
			}
		}
	}

	public void Lose ()
	{
		StartCoroutine (RunLoseAnim ());
	}

	private IEnumerator RunLoseAnim ()
	{
		yield return null;
		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Lose);
	}

	private void RunLoseAnimPlayer (Collider other)
	{
		Player player = other.GetComponent<Player> ();
		player.Lose ();
	}

	private IEnumerator RunChangeTurn (Collider other)
	{
		GameMain.Instance.IsBossTurn = !GameMain.Instance.IsBossTurn;
		if (GameMain.Instance.IsBossTurn) {
			GoalObject.Instance.SetUp ();
			GameMain.Instance.HideButtons ();
		} else {
			GoalObject.Instance.Reset ();
		}

		Player player = other.GetComponent<Player> ();
		SpeedManager.Instance.ResetForBossTurn ();
		// rotation
		ChangeTurn ();
		player.ChangeBossTurn ();
		yield return new WaitForSeconds (2.0f);
		player.StartBossGame ();
		yield return new WaitForSeconds (5.0f);
		myCollider.enabled = true;
		// real start
		CameraSystem.Instance.Reverse ();
		SpawnManager.Instance.ChangeReturn ();
	}
}
