using UnityEngine;
using System.Collections;

public class EffectSystem : SingletonMonoBehaviour<EffectSystem>
{
	public GameObject speedUpEffect;
	public GameObject speedDownEffect;
	public GameObject winEffect;
	public GameObject loseEffect;
	public GameObject catchEffect;
	public GameObject changeEffect;
	public GameObject startEffect;
	public GameObject gameOverEffect;
	public GameObject coinEffect;
	public GameObject gameClearEffect;

	private Transform speedUpEffectTrans;
	private Transform speedDownEffectTrans;
	private Transform loseEffectTrans;
	private Transform coinEffectTrans;

	new void Awake ()
	{
		base.Awake ();
		Reset ();

		speedUpEffectTrans = speedUpEffect.transform;
		speedDownEffectTrans = speedDownEffect.transform;
		loseEffectTrans = loseEffect.transform;
		coinEffectTrans = coinEffect.transform;
	}

	public void Reset ()
	{
		speedUpEffect.SetActive (false);
		speedDownEffect.SetActive (false);
		winEffect.SetActive (false);
		loseEffect.SetActive (false);
		catchEffect.SetActive (false);
		changeEffect.SetActive (false);
		startEffect.SetActive (false);
		gameOverEffect.SetActive (false);
		coinEffect.SetActive (false);
		gameClearEffect.SetActive (false);
	}

	public void PlaySpeedUpEffect (Vector3? pos = null)
	{
		speedUpEffect.SetActive (false);

		if (pos != null)
		{
			speedUpEffectTrans.parent = Player.Instance.transform;
			speedUpEffectTrans.localPosition = Vector3.zero;
		}

		speedUpEffect.SetActive (true);
		SoundManager.Instance.PlaySE ("speedup");
	}

	public void PlaySpeedDownEffect (Vector3? pos = null)
	{
		speedDownEffect.SetActive (false);

		if (pos != null)
		{
			speedDownEffectTrans.parent = Player.Instance.transform;
			speedDownEffectTrans.localPosition = Vector3.zero;
		}

		speedDownEffect.SetActive (true);
		SoundManager.Instance.PlaySE ("speeddown");
		ShakeCamera.Instance.DoShake();
	}

	public void PlayWinEffect ()
	{
		winEffect.SetActive (false);
		winEffect.SetActive (true);
		SoundManager.Instance.PlaySE ("return_or_win");
	}

	public void PlayLoseEffect (Vector3? pos = null)
	{
		// bomb
		loseEffect.SetActive (false);

		if (pos != null)
		{
			loseEffectTrans.parent = Player.Instance.transform;
			loseEffectTrans.localPosition = Vector3.zero;
		}

		loseEffect.SetActive (true);
		SoundManager.Instance.PlaySE ("die");
		ShakeCamera.Instance.DoShake();
	}

	public void PlayCatchEffect (Vector3? pos = null)
	{
		catchEffect.SetActive (false);
		catchEffect.SetActive (true);
		SoundManager.Instance.PlaySE ("return_or_win");
	}

	public void PlayChangeEffect ()
	{
		changeEffect.SetActive (false);
		changeEffect.SetActive (true);

		// 3.2.1 sound
		StartCoroutine ("PlayOneTwoThreeStartSound");
	}

	private IEnumerator PlayOneTwoThreeStartSound ()
	{
		// 3.2.1 sound
		yield return new WaitForSeconds (1.6f);
		SoundManager.Instance.PlaySE ("3.2.1");
		yield return new WaitForSeconds (1.0f);
		SoundManager.Instance.PlaySE ("3.2.1");
		yield return new WaitForSeconds (1.0f);
		SoundManager.Instance.PlaySE ("3.2.1");
		yield return new WaitForSeconds (1.0f);

		// start sound
		SoundManager.Instance.PlaySE ("start");
	}

	public void PlayStartEffect ()
	{
		startEffect.SetActive (false);
		startEffect.SetActive (true);

		StartCoroutine ("PlayOneTwoThreeStartSound");
	}

	public void PlayGameOverEffect ()
	{
		gameOverEffect.SetActive (false);
		gameOverEffect.SetActive (true);
	}

	public void PlayGameClearEffect ()
	{
		gameClearEffect.SetActive (false);
		gameClearEffect.SetActive (true);
	}

	public void PlayCoinEffect (Vector3? pos = null)
	{
		// bomb
		coinEffect.SetActive (false);

		if (pos != null)
		{
			coinEffectTrans.parent = Player.Instance.transform;
			coinEffectTrans.localPosition = Vector3.zero;
		}

		coinEffect.SetActive (true);
		SoundManager.Instance.PlaySE ("coin_get");
	}
}
