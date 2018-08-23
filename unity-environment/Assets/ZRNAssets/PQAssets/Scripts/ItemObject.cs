using UnityEngine;
using System.Collections;

public class ItemObject : MonoBehaviour
{
	public float baseSpeed = 5.0f;
	public float ratioSpeed = 0.4f;
	// only coin
	public Transform coinTransform;
	private Transform myTransform;

	public enum Type
	{
		None,
		SpeedUp,
		SpeedDown,
		GameOver,
		Coin,
	}

	public Type myType;

	void Awake ()
	{
		myTransform = transform;
	}

	// Use this for initialization
	void Start ()
	{
		// debug mode enemy
		if (GameMain.Instance.isDebug && myType == Type.GameOver)
		{
			myType = Type.None;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (CameraSystem.Instance.IsStop) {
			gameObject.SetActive (false);
			return;
		}

		// if (myTime > 4)
		if (myTransform.localPosition.x <= -26.8f) {
			gameObject.SetActive (false);
		}

		//myTime += Time.deltaTime;
		Move ();
	}

	private void Move ()
	{
		if (myType == Type.Coin)
		{
			float rotateSpeed = 9.5f;
			coinTransform.Rotate(new Vector3(0, rotateSpeed, 0), Space.World);
		}

		int speedLevel = SpeedManager.Instance.spedLevel;
		float speed = (baseSpeed + ratioSpeed * (speedLevel - 1)) * -1;
		myTransform.position += myTransform.right * speed * Time.deltaTime;
	}

	public void SetType (Type type)
	{
		myType = type;
	}

	void OnTriggerEnter (Collider other)
	{
		Player player;
		if (other.tag == "Player") {
			player = other.GetComponent<Player> ();
		} else {
			return;
		}

		switch (myType) {
			case Type.SpeedDown:
				player.SpeedDown ();
				gameObject.SetActive (false);
				break;
			case Type.SpeedUp:
				player.SpeedUp ();
				gameObject.SetActive (false);
				break;
			case Type.GameOver:
				player.Lose ();
				break;
			case Type.Coin:
				player.GetCoin ();
				PointManager.Instance.AddPoint ();
				gameObject.SetActive (false);
				break;
			default:
				break;
		}
	}
}
