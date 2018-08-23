using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointManager : SingletonMonoBehaviour<PointManager>
{
	public Text pointText;
	public Text pointTextShadow;

	public int Point { get; set; }
	private const int POINT = 10;

	new void Awake ()
	{
		base.Awake ();
	}

	public void Reset ()
	{
		Point = 0;
		Refresh ();
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void AddPoint ()
	{
		Point += POINT;
		Refresh ();
	}

	public void Refresh ()
	{
		pointText.text = Point.ToString ();
		pointTextShadow.text = Point.ToString ();
	}
}
