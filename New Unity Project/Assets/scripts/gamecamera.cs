using UnityEngine;
using System.Collections;

public class gamecamera : MonoBehaviour {
	private Transform target;
	private float trackspeed = 10;
	public void SetTarget(Transform t){
		target = t;
	}
	void lateUpdate(){
		if (target) {

			float x = IncrementTowards (transform.position.x,target.position.x, trackspeed);
			float y = IncrementTowards (transform.position.y,target.position.y, trackspeed);
			transform.position = new Vector3(x,y, transform.position.z);
		}
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