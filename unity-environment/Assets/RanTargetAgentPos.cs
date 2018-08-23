using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanTargetAgentPos : MonoBehaviour {
	private const int SIZE = 30;
	public Transform []pos = new Transform[SIZE];
	public GameObject Target;
	public GameObject Agent;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void RanTA() {
		int p0 = Random.Range (0, SIZE);
		Target.transform.position = pos [p0].position;
		int p1 = Random.Range (0, SIZE);
		while (p1 == p0 || Vector3.Distance (pos[p0].position,pos[p1].position)>55) {
			//print (p0 + " " + p1);
			p1 = Random.Range (0, SIZE);
		}
		//print (p0 + " " + p1);
		Agent.transform.position = pos [p1].position;
	}
}
