using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVAgent : Agent {
	public Transform StartPos;
	public bool RandomStartPos;
	//public int mysteps;
	public int vel_limit;
	public Rigidbody rb;
	public float old_distance;
	public float distance;
	public bool ckp1 = false;
	public bool ckp2 = false;
	public bool ckp3 = false;
	public bool ckp4 = false;

	public GameObject Target;
	public Rigidbody TargetRb;

	ImageSynthesis imgSyn;

	enum MenuverType{Xplus,Xminus,Yplus,Yminus,Zplus,Zminus,None}

	UAVAcademy academy;
	public bool rs_action;
	public bool request;
	public int density;
	//public override void InitializeAgent()
	void Start () 
	{
		academy = GameObject.FindObjectOfType<UAVAcademy> ();
		Application.runInBackground = true;
		//mysteps = 0;
		vel_limit = 10;
		rb = GetComponent<Rigidbody> ();
		TargetRb = Target.GetComponent<Rigidbody> ();
		old_distance = Vector3.Distance (StartPos.position, TargetRb.position);
		distance = Vector3.Distance (StartPos.position, TargetRb.position);
		if (GetComponentInChildren<ImageSynthesis> ().capturePasses [2].camera == null)
			print ("Null!");
		imgSyn = GetComponentInChildren<ImageSynthesis> ();
		//observations [0] = GetComponentInChildren<ImageSynthesis>().capturePasses[2].camera;
		agentParameters.agentCameras [0] = imgSyn.capturePasses[2].camera;
		agentParameters.agentCameras [1] = imgSyn.capturePasses[3].camera;
		//imgSyn.OnSceneChange ();
		request = false;
		density = 0;
	}

	public override void CollectObservations()
	{
//		AddVectorObs(gameObject.transform.position.x);
//		AddVectorObs(gameObject.transform.position.y - (StartPos.position.y-1) );
//		AddVectorObs(gameObject.transform.position.z);
//		AddVectorObs(Target.transform.position.x);
//		AddVectorObs(Target.transform.position.y - (StartPos.position.y-1));
//		AddVectorObs(Target.transform.position.z);
//		AddVectorObs(rb.velocity.x);
//		AddVectorObs(rb.velocity.y);
//		AddVectorObs(rb.velocity.z);
		AddVectorObs(gameObject.transform.position.x - Target.transform.position.x);
		AddVectorObs(gameObject.transform.position.y - Target.transform.position.y);
		AddVectorObs(gameObject.transform.position.z - Target.transform.position.z);
		AddVectorObs(rb.rotation.eulerAngles.x);
		AddVectorObs(rb.rotation.eulerAngles.y);
		AddVectorObs(rb.rotation.eulerAngles.z);
		AddVectorObs (density);
		/*AddVectorObs(TargetRb.velocity.x);
		AddVectorObs(TargetRb.velocity.y);
		AddVectorObs(TargetRb.velocity.z);*/
	}
	public override void AgentAction(float[] vectorAction, string textAction)
	{
		//print ("steps: " + GetStepCount ());
		rs_action = false;
		if(GetStepCount()<1)
			imgSyn.OnSceneChange ();
		if (vectorAction [0] == 0)
			Menuver (MenuverType.Xplus);
		else if (vectorAction [0] == 1)
			Menuver (MenuverType.Xminus);
		else if (vectorAction [0] == 2)
			Menuver (MenuverType.Yplus);
		else if (vectorAction [0] == 3)
			Menuver (MenuverType.Yminus);
		else if (vectorAction [0] == 4)
			Menuver (MenuverType.Zplus);

		else if (vectorAction[0]==5 ||vectorAction[0]==6) { // 5/6 = up/down resolution
			Menuver (MenuverType.None);
			//rs_action = true;
			//RequestDecision ();
		}
		/*if (vectorAction [0] == 5)
			Menuver (MenuverType.Zminus);*/
		
		distance = Vector3.Distance (transform.position, TargetRb.position);
		if (GetStepCount () >= 1) {
			//print ("old" + old_distance + "dis:" + distance);
			AddReward (0.1f * (old_distance - distance));
		}
		AddReward (-0.0002f); //timestep penalty
		old_distance = distance;
		Monitor.verticalOffset = 300f;
		Monitor.Log ("Action", vectorAction [0],MonitorType.text);





		/*
		else if (!ckp1 && transform.position.magnitude < 2 )
		{
			//AddReward (0.5f);
			ckp1 = true;
		}
		else if (!ckp2 && transform.position.magnitude < 5)
		{
			//AddReward (0.3f);
			ckp2 = true;
		}*/
	}
	private void FixedUpdate(){
		if (OutOfRange ())
		{
			SetReward (-1);
			ckp1 = false;
			ckp2 = false;
			ckp3 = false;
			ckp4 = false;
			Done ();
			Debug.Log ("Fail: Out of range:" + GetReward());
			return;
		}
		if (GetStepCount() > 5000) {
			SetReward (-1);
			ckp1 = false;
			ckp2 = false;
			ckp3 = false;
			ckp4 = false;
			Done ();
			Debug.Log ("Fail: Time out" + GetReward());
			return;
		}
		if (Succeed ())
		{
			Debug.Log ("Success");// + GetReward());
			SetReward (1);
			ckp1 = false;
			ckp2 = false;
			ckp3 = false;
			ckp4 = false;
			Done ();
			return;

		}
		if (rs_action)
			if (request) {
				request = false;
				//RequestDecision ();
			}
			else
				request = true;
		//float a = academy.resetParameters ["density"];

	}
	void OnCollisionEnter(Collision collision)
	{
		////reward += -100;
		//reward = -1;
		if (collision.gameObject.name != "Cube") {
			
			SetReward (-1);
			Debug.Log ("Fail: Collide on " + collision.gameObject.name + " "  + GetReward() );
			Done ();
		}
	}

	public override void AgentReset()
	{
		//mysteps = 0;
		if (RandomStartPos) {
			/*Vector3 offset = new Vector3 (0, 0, 3*Random.value-1.5f); 
			transform.position = StartPos.position + offset;*/
			/*last
			Vector3 offset;
			if (Random.value<=0.5f)
				offset = new Vector3 (10*Random.value+0.2f, StartPos.position.y, 16*Random.value-8f);
			else
				offset = new Vector3 (-10*Random.value-0.2f, StartPos.position.y, 16*Random.value-8f);
			
			transform.position = offset;
			*/
			//Debug.Log("pos: "+transform.position);
			/*Vector3 mySphere = Random.onUnitSphere * (11 + Random.value * 5);
			transform.position = new Vector3 (mySphere.x, StartPos.position.y + Random.value * 3 - 1.5f, mySphere.z);*/
			//Vector2 myCircle = Random.insideUnitCircle.normalized;
			Vector2 myCircle = Random.insideUnitCircle;
			transform.position = new Vector3 (myCircle.x*20*1.414f, StartPos.position.y + Random.value * 3 - 1.5f, myCircle.y*20*1.414f);
			transform.rotation =  Quaternion.Euler (0, Random.value*360, 0);


		} else {
			transform.position = StartPos.position;
			rb.rotation = Quaternion.identity;
			rb.rotation = Quaternion.Euler (0, -90, 0);
		}
		//Target.transform.position = Target.GetComponent<Vector3>();
		Vector2 targetCircle = Random.insideUnitCircle;
		Target.transform.position = new Vector3 (targetCircle.x*20, StartPos.position.y-1, targetCircle.y*20);
		TargetRb.velocity = new Vector3 (0, 0, 0);

		rb.velocity = new Vector3(0,0,0);
		if(GetComponentInParent<ObstacleGen>())
			density = GetComponentInParent<ObstacleGen>().Generate(transform.position, Target.transform.position);
		if (GetComponentInParent<RanTargetAgentPos> ())
			GetComponentInParent<RanTargetAgentPos> ().RanTA ();	
		/*last
		rb.rotation = Quaternion.identity;
		rb.rotation = Quaternion.Euler (0, -90, 0);
		*/
		//ResetReward (); must not call ResetReward()
		old_distance = Vector3.Distance (StartPos.position, TargetRb.position);
		// academy.AcademyReset ();
	}
	bool OutOfRange()
	{
		return distance > 60f;
	}
	bool Succeed()
	{
		
		//return rb.velocity.magnitude < 1f && transform.position.magnitude < 3f;
		return distance < 1.1f;
	}

	void Menuver(MenuverType type)
	{
		/*
		if (type == MenuverType.Xplus)
			rb.velocity = new Vector3 (3, 0, 0);
		if (type == MenuverType.Xminus)
			rb.velocity = new Vector3 (-3, 0, 0);
		if (type == MenuverType.Yplus)
			rb.velocity = new Vector3 (0, 3, 0);
		if (type == MenuverType.Yminus)
			rb.velocity = new Vector3 (0, -3, 0);
		if (type == MenuverType.Zplus)
			rb.velocity = new Vector3 (0, 0, 3);
		if (type == MenuverType.Zminus)
			rb.velocity = new Vector3 (0, 0, -3);
		*/

		/*float myscaling = 0.01f;
		if (type == MenuverType.Xplus && rb.velocity.x < vel_limit)
			rb.velocity += Vector3.right*myscaling;
		if (type == MenuverType.Xminus && rb.velocity.x*-1 < vel_limit)
			rb.velocity += Vector3.left*myscaling;
		if (type == MenuverType.Yplus && rb.velocity.y < vel_limit)
			rb.velocity += Vector3.up*myscaling;
		if (type == MenuverType.Yminus && rb.velocity.y*-1 < vel_limit)
			rb.velocity += Vector3.down*myscaling;
		if (type == MenuverType.Zplus && rb.velocity.z < vel_limit)
			rb.velocity += Vector3.forward*myscaling;
		if (type == MenuverType.Zminus && rb.velocity.z*-1 < vel_limit)
			rb.velocity += Vector3.back*myscaling;
		*/

		//type = MenuverType.Zplus;
		float Scaling = 2f;
		float vertiScaling = 1f;
		if (type == MenuverType.Xplus && rb.velocity.x < vel_limit) { //rotate clockwise
			rb.velocity = transform.forward*0;
			Vector3 m_EulerAngleVelocity = new Vector3(0, 10, 0) * Scaling;
			Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			//rb.rotation = Quaternion.identity;
			//print("Rotate"+mysteps);
		}
		else if (type == MenuverType.Xminus && rb.velocity.x*-1 < vel_limit) { //rotate anti clockwise
			rb.velocity = transform.forward*0;
			Vector3 m_EulerAngleVelocity = new Vector3(0, -10, 0) * Scaling;
			Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			//rb.rotation = Quaternion.identity;
			//print("Rotate reverse"+mysteps);
		}

		else if (type == MenuverType.Yplus && rb.velocity.y < vel_limit) {
			rb.velocity = Vector3.up * vertiScaling * Scaling;
			//rb.AddForce (Vector3.up * vertiScaling);
			//print ("Up"+mysteps);
		}
		else if (type == MenuverType.Yminus && rb.velocity.y * -1 < vel_limit) {
			rb.velocity = Vector3.down * vertiScaling * Scaling;
			//print ("Down"+mysteps);
		}
		else if (type == MenuverType.Zplus && rb.velocity.z < vel_limit) {
			rb.velocity = transform.forward * Scaling;
			//rb.AddForce (transform.forward);
			//print ("Forwrad"+mysteps);
		}
		else if (type == MenuverType.None)
			rb.velocity = transform.forward*0;
		/*if (type == MenuverType.Zminus && rb.velocity.z * -1 < vel_limit) {
			rb.velocity = -1 * transform.forward * Scaling;
			//rb.AddForce (-1 * transform.forward);
			//print ("Backward"+mysteps);
		}*/

		/*
		if (mysteps % 2 <1) {
			rb.AddForce (transform.forward*5);
			Debug.Log ("moving forward");
		} else {
			rb.velocity = transform.forward*0;
			Vector3 m_EulerAngleVelocity = new Vector3(0, 10, 0);
			Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
			//rb.rotation = Quaternion.identity;
			Debug.Log ("rotate");
		}
		*/
		//yield return new WaitForSeconds (1);
	}

}
