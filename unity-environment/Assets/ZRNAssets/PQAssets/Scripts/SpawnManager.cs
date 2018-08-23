using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : SingletonMonoBehaviour<SpawnManager>
{
	public int itemCount;
	public int bossTurnItemCount = 5;
	public float radius;
	public const float DEFAULT_DELTA_TIME = 4.0f;
	public float spwanDeltaTime;
	private float myTime;
	private Transform myTransform;
	private Vector3 defaultPos;
	private List<Transform> itemObjects;
	private List<Transform> bossTurnItemObjects;

	// Items
	private GameObject speedUpItemPrefab;
	private GameObject speedDownItemPrefab;
	// Enmeny 
	private GameObject enemyPrefab;
	private GameObject[] prefabs;
	// Coin
	private GameObject coinPrefab;

	new void Awake ()
	{
		base.Awake ();
		speedUpItemPrefab = Resources.Load ("ItemObjects/SpeedUp", typeof(GameObject)) as GameObject;
		speedDownItemPrefab = Resources.Load ("ItemObjects/SpeedDown", typeof(GameObject)) as GameObject;
		enemyPrefab = Resources.Load ("ItemObjects/Enemy", typeof(GameObject)) as GameObject;
		coinPrefab = Resources.Load ("ItemObjects/Coin", typeof(GameObject)) as GameObject;

		prefabs = new GameObject[] {
			speedUpItemPrefab,
			speedDownItemPrefab,
			enemyPrefab,
			coinPrefab
		};

		myTransform = transform;
		defaultPos = myTransform.localPosition;
	}

	// Use this for initialization
	void Start ()
	{
		// initialize
		itemObjects = new List<Transform> ();
		bossTurnItemObjects = new List<Transform> ();
		PrepareItems ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (CameraSystem.Instance.IsStop) {
			return;
		}

		myTime += Time.deltaTime;

		if (myTime > spwanDeltaTime) {
			SpawnItem ();
			myTime = 0;
		}
	}

	private int spawnedIndex;
	private int bossTurnSpawnedIndex;

	private void SpawnItem ()
	{
		DebugLabel.Instance.SetMessage ("spwanDeltaTime: " + spwanDeltaTime.ToString ());
		// boss turn
		GameMain gameMain = GameMain.Instance;
		int gameLevel = gameMain.gameLevel;
		if (GameMain.Instance.IsBossTurn && gameLevel > 3) {
			Transform bossTurnItemTrans = bossTurnItemObjects [bossTurnSpawnedIndex];
			SetSpawnItem (bossTurnItemTrans);
			bossTurnSpawnedIndex++;

			// reset
			if (bossTurnSpawnedIndex >= bossTurnItemObjects.Count) {
				bossTurnSpawnedIndex = 0;
			}
		}

		// always
		Transform itemTrans = itemObjects [spawnedIndex];
		DebugLabel.Instance.SetMessage ("SpawnItem: " + itemTrans.name);
		SetSpawnItem (itemTrans);

		spawnedIndex++;
		// reset
		if (spawnedIndex >= itemObjects.Count) {
			spawnedIndex = 0;
		}
	}

	private void SetSpawnItem (Transform itemTrans)
	{
		// (x−a)2+(y−b)2=r2
		// left-right is z axis.
		float degree = Random.Range (0, 360);
		float radian = degree * Mathf.Deg2Rad;
		float z = radius * Mathf.Sin (radian);
		float y = radius * Mathf.Cos (radian);
		Vector3 spawnPos = new Vector3 (0, y, z);
		itemTrans.localPosition = spawnPos;
		itemTrans.gameObject.SetActive (true);
	}

	private void PrepareItems ()
	{
		for (var i=0; i<itemCount; i++) {
			// all items must exist more than one object
			int itemIndex = i;

			if (i >= prefabs.Length) {
				itemIndex = Random.Range (0, prefabs.Length);
			}

			GameObject go = (GameObject)Instantiate (prefabs [itemIndex], Vector3.zero, Quaternion.identity);
			go.name = "Item_" + itemIndex.ToString ();
			Transform trans = go.transform;
			trans.parent = myTransform;
			go.SetActive (false);
			itemObjects.Add (trans);
		}

		// prepare only enemy for boss turn
		for (var i=0; i<bossTurnItemCount; i++) {
			GameObject go = (GameObject)Instantiate (prefabs [2], Vector3.zero, Quaternion.identity);
			go.name = "BossTurnItem_" + i.ToString ();
			Transform trans = go.transform;
			trans.parent = myTransform;
			go.SetActive (false);
			bossTurnItemObjects.Add (trans);
		}
	}

	public void Reset ()
	{
		spwanDeltaTime = DEFAULT_DELTA_TIME;

		foreach (var itemObject in itemObjects) {
			itemObject.gameObject.SetActive (false);
		}

		foreach (var itemObject in bossTurnItemObjects) {
			itemObject.gameObject.SetActive (false);
		}

		spawnedIndex = 0;
		myTime = 0;
		myTransform.localRotation = Quaternion.Euler (0, 0, 0);
		myTransform.localPosition = defaultPos; //new Vector3 (10f, -0.11f, 0.19f);
	}

	public void ChangeReturn ()
	{
		GameMain gameMain = GameMain.Instance;
		int gameLevel = gameMain.gameLevel;
		switch (gameLevel)
		{
			case 2:
				spwanDeltaTime = 3.0f;
				break;
			case 3:
				spwanDeltaTime = 2.5f;
				break;
			case 4:
				spwanDeltaTime = 2.3f;
				break;
			case GameMain.MAX_GAME_LEVEL:
				spwanDeltaTime = 2f;
				break;
			default:
				spwanDeltaTime = DEFAULT_DELTA_TIME;
				break;
		}

		if (GameMain.Instance.IsBossTurn) {
			myTransform.localRotation = Quaternion.Euler (0, 180, 0);
			myTransform.localPosition = new Vector3 (-15f, -0.11f, 0.19f);
		} else {
			// player turn
			myTransform.localRotation = Quaternion.identity;
			myTransform.localPosition = new Vector3 (10f, -0.11f, 0.19f);
		}
	}
}
