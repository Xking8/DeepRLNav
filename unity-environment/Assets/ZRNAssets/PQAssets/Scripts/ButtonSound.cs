using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour
{
	public void PlayPositiveSound () {
		SoundManager.Instance.PlaySE ("positive");
	}

	public void PlayNegativeSound () {
		SoundManager.Instance.PlaySE ("negative");
	}
}
