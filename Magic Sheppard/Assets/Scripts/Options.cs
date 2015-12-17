using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour
{

    public void disable()
    {
        gameObject.SetActive(false);
    }

   public void enable()
    {
        gameObject.SetActive(true);
    }
}
