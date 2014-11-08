using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;

	private float skin = .002f;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool movementstop;
	Ray ray;
	RaycastHit hit;
	void Start() {
		collider = GetComponent<BoxCollider> ();
		s = collider.size;
		c = collider.center;
	}

	public void Move(Vector3 moveamount) {
		float deltaX = moveamount.x;
		float deltaY = moveamount.y;
		float deltaZ = moveamount.z;
		Vector3 p = transform.position;


		grounded = false;



		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign (deltaY);
			float x = (p.x + c.x - s.x / 2) + s.x / 2 * i;//left center and right of collider
			float y = p.y + c.y + s.y / 2 * dir;// the bottom of the collider
			float z = (p.z + c.z - s.z / 2) + s.x / 2 * i;//left center and right of ZAXIS
			ray = new Ray(new Vector3(x,y,z), new Vector3(0,dir));

			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaY) + skin,collisionMask)) {
				//gets distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				float dstZ = Vector3.Distance (ray.origin, hit.point);
				// stops player downwards movement after it s skin touches a collider
				if (dst > skin && dstZ > skin) {
					deltaY = -dst - skin * dir;
				}
				else {
					deltaY = 0;
				}
				grounded = true;
				break;
			}
		}



		//the guy can hit the sides
		movementstop = false;
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign (deltaX);
			float x = p.x + c.x + s.x / 2 * dir;//left center and right of collider
			float y = p.y + c.y - s.y / 2 + s.y / 2 * i;// the bottom of the collider
			
			ray = new Ray(new Vector2(x,y), new Vector2(dir,0));
			
			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaX) + skin,collisionMask)) {
				//gets distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				// stops player downwards movement after it s skin touches a collider
				if (dst > skin) {
					deltaX = -dst - skin * dir;
				}
				else {
					deltaX = 0;
				}
				movementstop = true;
				break;
			}
		}


		Vector3 finalTransform = new Vector3 (deltaX, deltaY, deltaZ);


		transform.Translate (finalTransform);
	}
}
