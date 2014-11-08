using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	private gamecamera cam;
	// Use this for initialization
	void Start () {
		cam = GetComponent<gamecamera> ();
		SpawnPlayer ();
	}


	private void SpawnPlayer() {
		cam.SetTarget ((Instantiate (Player, Vector3.zero, Quaternion.identity)as GameObject).transform);
	}
}
