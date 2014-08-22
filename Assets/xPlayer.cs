using UnityEngine;
using System.Collections;

public class xPlayer : MonoBehaviour {

	public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		loosingH (10);


	}


	void loosingH(int h)
	{
		health -= h;
		}
}
