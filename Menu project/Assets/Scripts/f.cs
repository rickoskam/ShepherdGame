using UnityEngine;
using System.Collections;

public class f : MonoBehaviour {
    public static int aaa;
    public string level;


    public void set(){
        aaa = 900;
        Application.LoadLevel(level);
}


}