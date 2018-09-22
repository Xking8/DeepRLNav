using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGen : MonoBehaviour {

	public GameObject mycube;
	GameObject[] Obs;
	public int ObsRoot;
	public int ObsNum;
	public int ObsRootMax;
	public int ObsNumMax;
	public int count;
	private int start_p;

	// Use this for initialization
	void Start () {
		count = 0;
	//void Awaken () {
		ObsRootMax = 30;
		ObsNumMax = ObsRootMax*ObsRootMax;

		Obs = new GameObject[ObsNumMax];
		for (int i = 0; i < ObsNumMax; i++) {
			
			Obs [i] = Instantiate (mycube, new Vector3 (600, 600, 600), Quaternion.Euler (0, 0, 0));
		}
		start_p = -50;
		//Generate (new Vector3(0,0,0),new Vector3(0,0,0));

	}
	public int Generate(Vector3 agentPos, Vector3 targetPos) {
		for (int i = 0; i < ObsNumMax; i++) {
			Obs [i].transform.position = new Vector3 (600, 600, 600);
		}
		ObsRoot = Random.Range(0,10);
		//ObsRoot = (int)density;

		ObsNum = ObsRoot * ObsRoot;
		//print (count+ ": " + ObsRoot + ", " + ObsNum);
		count += 1;
		float itr = Mathf.Sqrt (ObsNum);
		for (int i = 0; i < itr; i++) {
			for (int j = 0; j < itr ; j++) {
				//float x = -1*ObsRoot *(ObsRoot-1)/2+ j * ObsRoot * Random.value * 2 + Random.value * 2;
				//float z = -1*ObsRoot *(ObsRoot-1)/2+ i * ObsRoot * Random.value * 2 + Random.value * 2;
				float x = start_p + -2*start_p*j/ObsRoot+ -2*start_p/ObsRoot*Random.value;
				float z = start_p + -2*start_p*i/ObsRoot;
				float scaleY = 10 + Random.value * 5 ;
				/*while (x > -3 || x < 3) {
					x = -21 + j * 7 + Random.value * 2 + 2;
				}*/
				if(!validateGen(agentPos, targetPos, x, z))
					continue;
//				if (x > -3 && x < 3 && z > -3 && z < 3)
//					continue;
//
//				/*while (z > -3 || z < 3) {
//					z = -21 + j * 7 + Random.value * 2 + 2;
//				}*/
//				if (x > -3 && x < 3 && z > -3 && z < 3)
//					continue;
				//Vector3 pos = new Vector3 (-21+j*7 +5, transform.position.y, -21+i*7+5);
				Vector3 pos = new Vector3 (x, scaleY/2 - 0.5f + targetPos.y, z);
				//Obs [i+j] = Instantiate (mycube, pos,  Quaternion.Euler (0, Random.value*360, 0));
				Obs [i*Mathf.RoundToInt(itr)+j].transform.position = pos;
				Obs [i * Mathf.RoundToInt (itr) + j].transform.rotation = Quaternion.Euler (0, Random.value * 360, 0);
				//print (i +" "+ j);
				Obs [i*Mathf.RoundToInt(itr)+j].transform.localScale =new Vector3 (0.1f + Random.value*2, scaleY, 0.1f + Random.value * 5);

				//Obs [i].layer = 10;
			}
		}
		return ObsRoot;
//		Vector3 midpoint = new Vector3 ((AgentPos.x + targetPos.x) / 2, (AgentPos.y + targetPos.y) / 2, (AgentPos.z + targetPos.z) / 2);
//
//		//Vector3 pos = new Vector3 (transform.position.x + 5*Random.value, transform.position.y, transform.position.z+ 5*Random.value);
//		for (int i = 0; i < ObsNum; i++) {
//			//Vector3 pos = new Vector3 (midpoint.x + 5 * i -10 + 5 * Random.value, transform.position.y, midpoint.z + 5 * Random.value);
////			Vector3 pos = new Vector3 (midpoint.x + 3 * Random.value , transform.position.y, midpoint.z+ 5 * i -10*(ObsNum-1) + 10 * Random.value-5);
////			Obs [i].transform.position = pos;
//			print(i +  ObsNum);
//			Vector3 pos = new Vector3(i+10, 0, i+10);
//			Obs [i] = Instantiate (mycube, pos, Quaternion.identity);
//			//Obs [i].transform.localScale =new Vector3 (1, 0.5f, 1);
//			//Obs [i].layer = 10;
//		}

	}
	public bool validateGen(Vector3 agentPos,Vector3 targetPos, float x, float z) {
		if (x > agentPos.x - 3 && x < agentPos.x + 3 && z > agentPos.z - 3 && z < agentPos.z + 3)
			return false;
		if (x > targetPos.x - 3 && x < targetPos.x + 3 && z > targetPos.z - 3 && z < targetPos.z + 3)
			return false;
		return true;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
