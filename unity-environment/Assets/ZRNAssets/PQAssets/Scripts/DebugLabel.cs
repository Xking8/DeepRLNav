using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugLabel : SingletonMonoBehaviour<DebugLabel>
{
	public bool isHide;
	public Text debugLabel;

	new void Awake ()
	{
		base.Awake ();

		if (isHide) {
			GetComponent<Text> ().text = string.Empty;
		}
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SetMessage (string message)
	{
		if (isHide) {
			return;
		}

		if (debugLabel.text.Length > 300) {
			debugLabel.text = string.Empty;
		}

		debugLabel.text += "\n" + message;
	}
}
