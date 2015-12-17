using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class hover : MonoBehaviour
{
    
    public Texture NewTexture;
    private RawImage img;

    void Start()
    {
      //  img = (RawImage)start_game_2.GetComponent<RawImage>();
      //  img.texture = (Texture)NewTexture;
    }

    void onMouseEnter()
    {
  

    }

    void onMouseExit()
    {

    }


    void Update()
    {

        if (Input.mousePosition.x > 10 && Input.mousePosition.y > 20 && Input.mousePosition.x < 10 + 100 && Input.mousePosition.y < 20 + 200)
        {

        }
    }

}


    