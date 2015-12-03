using UnityEngine;
using System.Collections;

public class StartTekst : MonoBehaviour {
	
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	
	void OnMouseEnter() {

	}
	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("Winkel"); 
		}
	}
	void OnMouseExit() {
	}
}