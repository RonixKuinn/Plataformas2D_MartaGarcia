using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator;
    private float horizontalInput;
    [SerializeField]private float jumpForce = 5;
    [SerializeField]private float characterSpeed = 4.5f;    // "[SerializeField]" es para que se vea en el inspector //la f solo se pone con decimales

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }
       
        if(Input.GetButtonDown("Jump")/*para GroundSensor -->*/ && GroundSensor.isGrounded)
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);   //ButtonDown es para cuando lo pulsas
            characterAnimator.SetBool("IsJumping", true);
        }
        
    }

    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);   // (1,x) = (lados,arriva) <-- [hay que añadir new] // tambies se puede poner directamente la dirección "right"
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            characterAnimator.SetTrigger("IsDead");
        }
    }
}
