using UnityEngine;
using System.Collections;

public class DeurScript : MonoBehaviour {

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
            GameObject schaap = other.gameObject;
            schaap.tag="GevangenSchaap";
        }
    }
}
