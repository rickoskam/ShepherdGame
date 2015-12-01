using UnityEngine;
using System.Collections;

public class Rondlopen : MonoBehaviour {
	public float rotatespeed; 
	public float speed; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			WeideScript.openhekje();
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(new Vector3(0,rotatespeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(new Vector3(0,-rotatespeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
		}
	}
}
