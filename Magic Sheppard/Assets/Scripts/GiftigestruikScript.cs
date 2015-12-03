using UnityEngine;
using System.Collections;

public class GiftigestruikScript : MonoBehaviour {
    private GameObject schaap;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Schaap"))
        {
            schaap = other.gameObject;
            schaap.tag = "ZiekSchaap";
            schaap.GetComponent<Renderer>().material.color = Color.white;
            gameObject.SetActive(false);
            
        }
    }

}
