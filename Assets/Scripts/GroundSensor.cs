using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public static bool isGrounded;  // "static" es un añadido importante :)

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6) //esto hay que hacerlo después de añadirle el lyer "ground" a las plataformas
        {
            isGrounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
