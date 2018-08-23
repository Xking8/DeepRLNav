using UnityEngine;
using System.Collections;

public class CameraSystem : SingletonMonoBehaviour<CameraSystem>
{
	public bool IsStop { private set; get; }

	private Animator animator;
	private const float DEFAULT_SPEED = 1.2f;
	private const float RATIO = 0.5f;
	private const string ANIM_NAME = "CameraAnimation0306";
	// player is oni
	private float defaultFieldOfView;
	// boss is oni
	private const float escapeFieldOfView = 28.0f;

	new void Awake ()
	{
		base.Awake ();
		animator = GetComponent<Animator> ();
		defaultFieldOfView = Camera.main.fieldOfView;
		Stop ();
	}

	void Update ()
	{
		// for fix reverse loop
		if (GameMain.Instance.IsBossTurn) {
			if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.0f) {
				animator.Play (Animator.StringToHash (ANIM_NAME), 0, 1.0f);
			}
		}
	}

	public void Reset ()
	{
		animator.Play (Animator.StringToHash (ANIM_NAME), 0, 0.0f);
		ResetFieldOfView ();
	}

	public void ResetFieldOfView ()
	{
		Camera.main.fieldOfView = defaultFieldOfView;
	}

	public void Begin ()
	{
		ApplySpeed ();
		IsStop = false;
	}

	public void Stop ()
	{
		animator.speed = 0;
		IsStop = true;
	}

	public void Reverse ()
	{
		Begin ();
		Camera.main.fieldOfView = escapeFieldOfView;
	}

	public void ApplySpeed ()
	{
		SpeedManager sm = SpeedManager.Instance;

		if (GameMain.Instance.IsBossTurn) {
			animator.speed = -DEFAULT_SPEED - RATIO * (sm.spedLevel - 1);
		} else {
			animator.speed = DEFAULT_SPEED + RATIO * (sm.spedLevel - 1);
		}
	}
}
