using UnityEngine;

public class ShakeCamera : SingletonMonoBehaviour<ShakeCamera>
{
	public bool isShaking;

	private float shakeDecay;
	private float shakeIntensity;
	private Vector3 originalLocalPos;
	private Vector3 originalPos;
	private Transform myTransform;
	private Quaternion originalRot;

	new void Awake ()
	{
		base.Awake ();
		myTransform = transform;
		originalLocalPos = myTransform.localPosition;
	}

	void Start ()
	{
		isShaking = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (shakeIntensity > 0) {
			myTransform.position = originalPos + Random.insideUnitSphere * shakeIntensity;
			shakeIntensity -= shakeDecay;
		} else if (isShaking) {
			// last
			isShaking = false;
			myTransform.localPosition = originalLocalPos;
		}
	}

	public void DoShake ()
	{
		originalPos = myTransform.position;
		shakeIntensity = 0.3f;
		shakeDecay = 0.02f;
		isShaking = true;
	}
}
