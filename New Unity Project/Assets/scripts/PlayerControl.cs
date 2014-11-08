using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerControl : MonoBehaviour {

	// Player handling
	public float gravity = 20;
	public float speed;
	public float acceleration;
	public float jumpheight;


	private float currentspeedz;
	private float targetspeedz;

	private float currentspeed;
	private float targetspeed;
	private Vector3 amounttomove;

	private PlayerPhysics playerPhysics;

	
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerPhysics.movementstop) {
			targetspeed = 0;
			currentspeed = 0;
		}
		//Input
		targetspeed = Input.GetAxisRaw ("Horizontal") * speed;
		currentspeed = IncrementTowards (currentspeed, targetspeed, acceleration);
		targetspeedz = Input.GetAxisRaw ("Vertical") * speed;
		currentspeedz = IncrementTowards (currentspeedz, targetspeedz, acceleration);
		if (playerPhysics.grounded) {
			amounttomove.y = 0;
			//Makes this fucker jump
			if (Input.GetButtonDown ("Jump")) {
				amounttomove.y = jumpheight;
			}
		}
		amounttomove.x = currentspeed;
		amounttomove.z = currentspeedz;
		amounttomove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amounttomove * Time.deltaTime);
	}
	//Increase n towards target by speed
	private float IncrementTowards(float n,float target, float a) {
		if(n == target) {
			return n;
		}
		else {
			float dir = Mathf.Sign(target - n);//n must be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign (target-n))? n: target;//if n has now passed the target then return the target, otherwise return n
		}
	}
}
