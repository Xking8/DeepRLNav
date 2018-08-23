using UnityEngine;
using System.Collections;

public class QueryAnimationController : MonoBehaviour
{
	public enum  QueryChanAnimationType
	{
		Left,
		Right,
		Straight,
		GetItem,
		Win,
		Lose,
		ReturnBack,
		ChangeTurn,
	}

	private const string CHANGE_TURN_ANIM = "is_change_turn";

	/*
	 * PQ_damage　　…敵にぶつかった際のモーション.
	 * PQ_fly_idle　　…待機.
	 * PQ_fly_left　　…左へ曲がる.
	 * PQ_fly_right　　…右へ曲がる.
	 * PQ_fly_straight　　…まっすぐ突き進む.
	 * PQ_get　　…アイテムゲットした際.
	 * PQ_idle　　…待機(地面).
	 * PQ_loose　　…敗北(地面).
	 * PQ_win　…地面(地面).
	 * PQ_return …振り返る.
	 * PQ_change_turn …ターン交代.
	 */
	private Animator myAnimator;

	void Awake ()
	{
		myAnimator = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	private void ResetBoolKeys ()
	{
		myAnimator.SetBool ("is_straight", false);
		myAnimator.SetBool ("is_right", false);
		myAnimator.SetBool ("is_left", false);
		myAnimator.SetBool ("is_get", false);
		myAnimator.SetBool ("is_win", false);
		myAnimator.SetBool ("is_lose", false);
		myAnimator.SetBool ("is_return_back", false);
	}

	private IEnumerator UnSetAnimationFlag (string flagName, float time = 0.02f)
	{
		yield return new WaitForSeconds (time);
		myAnimator.SetBool (flagName, false);
	}

	public void ChangeAnimation (QueryChanAnimationType type)
	{
		if (myAnimator.GetBool (CHANGE_TURN_ANIM)) {
			return;
		}

		switch (type) {
			case QueryChanAnimationType.Right:
				ResetBoolKeys ();
				myAnimator.SetBool ("is_right", true);
				break;
			case QueryChanAnimationType.Left:
				ResetBoolKeys ();
				myAnimator.SetBool ("is_left", true);
				break;
			case QueryChanAnimationType.Straight:
				ResetBoolKeys ();
				myAnimator.SetBool ("is_straight", true);
				break;
			case QueryChanAnimationType.GetItem:
				ResetBoolKeys ();
				string getFlagName = "is_get";
				myAnimator.SetBool (getFlagName, true);
				StartCoroutine (UnSetAnimationFlag (getFlagName, 0.5f));
				break;
			case QueryChanAnimationType.Win:
				ResetBoolKeys ();
				string winFlagName = "is_win";
				myAnimator.SetBool (winFlagName, true);
				StartCoroutine (UnSetAnimationFlag (winFlagName, 0.5f));
				break;
			case QueryChanAnimationType.Lose:
				ResetBoolKeys ();
				string loseFlagName = "is_lose";
				myAnimator.SetBool (loseFlagName, true);
				StartCoroutine (UnSetAnimationFlag (loseFlagName, 0.5f));
				break;
			case QueryChanAnimationType.ReturnBack:
				ResetBoolKeys ();
				string retutnFlagName = "is_return_back";
				myAnimator.SetBool (retutnFlagName, true);
				StartCoroutine (UnSetAnimationFlag (retutnFlagName, 0.5f));
				break;
			case QueryChanAnimationType.ChangeTurn:
				ResetBoolKeys ();
				myAnimator.SetBool (CHANGE_TURN_ANIM, true);
				StartCoroutine (UnSetAnimationFlag (CHANGE_TURN_ANIM, 1.0f));
				break;
			default:
				break;
		}
	}
}
