using UnityEngine;
using System.Collections;

public class ControlPlayerScript : MonoBehaviour {

	public xPlayer p;

		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (p.health < 0) {
			Destroy(p.gameObject);
		}


	}
}
