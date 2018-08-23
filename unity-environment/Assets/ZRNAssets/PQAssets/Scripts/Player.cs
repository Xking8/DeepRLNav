using UnityEngine;
using System.Collections;

public class Player : SingletonMonoBehaviour<Player>
{
	public GameObject modelGO;
	public Transform axis;
	public float angle = 30f;

	private bool isStop;
	private bool isTurning;
	private int spedLevel;
	private float myAngle;
	private Transform myTransform;
	private QueryAnimationController queryAnimationController;
	// defaults
	private Vector3 defaultPosition;
	private Quaternion defaultQuaternion;

	new void Awake ()
	{
		base.Awake ();
		isTurning = false;
		myTransform = transform;
		queryAnimationController = GetComponent<QueryAnimationController> ();
		defaultPosition = myTransform.localPosition;
		defaultQuaternion = myTransform.localRotation;
	}

	// Use this for initialization
	void Start ()
	{
		Reset ();
	}

	public void Reset ()
	{
		modelGO.SetActive (true);
		isStop = false;
		axis.localEulerAngles = Vector3.zero;
		myTransform.localRotation = defaultQuaternion;
		myTransform.localPosition = defaultPosition;
		Straight ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (isTurning || isStop) {
			return;
		}

		Rotate ();
	}

	private void Rotate ()
	{
		axis.Rotate (Vector3.right * myAngle * Time.deltaTime);
	}

	public void Left ()
	{
		if (isTurning || isStop) {
			return;
		}

		// flip
		if (GameMain.Instance.IsBossTurn) {
			queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Right);
		} else {
			queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Left);
		}

		myAngle = angle * -0.8f;
	}

	public void Right ()
	{
		if (isTurning || isStop) {
			return;
		}

		// flip
		if (GameMain.Instance.IsBossTurn) {
			queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Left);
		} else {
			queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Right);
		}

		myAngle = angle * 0.8f;
	}

	public void Straight ()
	{
		if (isStop) {
			return;
		}

		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Straight);
		myAngle = 0;
	}

	private IEnumerator PlayGetItemAnim ()
	{
		yield return null;
		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.GetItem);
	}

	public void SpeedDown ()
	{
		if (isStop) {
			return;
		}

		SpeedManager.Instance.SpeedDown ();
		CameraSystem.Instance.ApplySpeed ();
		EffectSystem.Instance.PlaySpeedDownEffect (myTransform.position);
	}

	public void SpeedUp ()
	{
		if (isStop) {
			return;
		}

		GameMain.Instance.RunBlink ();
		SpeedManager.Instance.SpeedUp ();
		CameraSystem.Instance.ApplySpeed ();
		EffectSystem.Instance.PlaySpeedUpEffect (myTransform.position);
		StartCoroutine (PlayGetItemAnim ());
	}

	public void Win ()
	{
		if (isStop) {
			return;
		}

		isStop = true;
		Boss.Instance.Lose ();
		CameraSystem.Instance.Stop ();
		EffectSystem.Instance.PlayWinEffect ();
		StartCoroutine ("PlayWinAnim");
		StartCoroutine ("ResetTurningFlag");
	}

	private IEnumerator PlayWinAnim ()
	{
		yield return null;
		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Win);
	}

	public void Lose ()
	{
		if (isStop) {
			return;
		}

		modelGO.SetActive (false);
		isStop = true;
		CameraSystem.Instance.Stop ();
		EffectSystem.Instance.PlayLoseEffect (myTransform.position);
		queryAnimationController.ChangeAnimation (QueryAnimationController.QueryChanAnimationType.Lose);
		StartCoroutine ("GameOver");
	}

	public void ChangeMyTurn ()
	{
		StartCoroutine ("HalfRotate");
		isTurning = false;
		isStop = false;
	}

	// change boss's turn
	public void ChangeBossTurn ()
	{
		StartCoroutine ("PlayWinAnim");
		isTurning = true;
		CameraSystem.Instance.Stop ();
		EffectSystem.Instance.PlayCatchEffect ();
	}

	public void StartBossGame ()
	{
		EffectSystem.Instance.PlayChangeEffect ();
		StartCoroutine ("ResetTurningFlag");
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

	public void GetCoin ()
	{
		EffectSystem.Instance.PlayCoinEffect (myTransform.position);
		StartCoroutine (PlayGetItemAnim ());
	}

	private IEnumerator ResetTurningFlag ()
	{
		yield return new WaitForSeconds (2);
		isTurning = false;
	}

	private IEnumerator GameOver ()
	{
		yield return new WaitForSeconds (1.0f);

		EffectSystem.Instance.PlayGameOverEffect ();
		GameMain.Instance.SetForGameMainUIs (false);
		ResultScreen.Instance.SetActive (true);
	}
}
