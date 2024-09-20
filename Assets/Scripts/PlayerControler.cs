using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    private float horizontalInput;
    [SerializeField]private float jumpForce = 5;
    [SerializeField]private float characterSpeed = 4.5f;    // "[SerializeField]" es para que se vea en el inspector //la f solo se pone con decimales

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
       
        if(Input.GetButtonDown("Jump"))
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);   //ButtonDown es para cuando lo pulsas
        }
    }

    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);   // (1,x) = (lados,arriva) <-- [hay que añadir new] // tambies se puede poner directamente la dirección "right"
    }


}
