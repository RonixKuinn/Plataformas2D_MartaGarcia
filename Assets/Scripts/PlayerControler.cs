using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator;
    private float horizontalInput;
    private bool isAttacking;
    [SerializeField]private float jumpForce = 5;
    [SerializeField]private float characterSpeed = 4.5f;    // "[SerializeField]" es para que se vea en el inspector //la f solo se pone con decimales
    [SerializeField]private int healtPoints = 5;
    [SerializeField]private Transform attackHitBox;
    [SerializeField]private float attackRadius;

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
        Movement();

        if(Input.GetButtonDown("Jump")/*para GroundSensor -->*/ && GroundSensor.isGrounded && /*no atacar cuando salte-->*/ isAttacking == false)
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded && isAttacking == false)
        {
            Attack();
        }
        
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
            //if(isAttacking)
            //{
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //}

            characterAnimator.SetBool("IsRunning", true);
        }
        else if(horizontalInput > 0)
        {
            //if(isAttacking)
            //{
                transform.rotation = Quaternion.Euler(0, 0, 0);
            //}

            characterAnimator.SetBool("IsRunning", true);
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);   //ButtonDown es para cuando lo pulsas
        characterAnimator.SetBool("IsJumping", true);
    }

    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y); // (1,x) = (lados,arriva) <-- [hay que añadir new] // tambien se puede poner directamente la dirección "right"

    }
    
    void Attack()
    {
        StartCoroutine(AttackAnimation());      //llamar a co-rutina
        characterAnimator.SetTrigger("Attack");
    }

    //co-rutina para que no salte mientras ataca
    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.2f);
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage();
            }
        }
        
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    void TakeDamage()
        {
            healtPoints--; // -- es para que se resten de uno en uno y si quieres más sería -= x
            

            if(healtPoints <= 0)
            {
                Die();
            }
            else
            {
                characterAnimator.SetTrigger("IsHurt"); 
            }
        }

    void Die()
    {
        characterAnimator.SetBool("IsDead", true);
        Destroy(gameObject, 0.7f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            TakeDamage();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
    
}

// Extension -- Night Pink (tiene murcielagos) // Pink Candy Theme (parece una nube rizada)