using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    private AudioSource audioSource;

    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator;
    private float horizontalInput;
    private bool isAttacking;
    [SerializeField]private float jumpForce = 5;
    [SerializeField]private float characterSpeed = 4.5f;    // "[SerializeField]" es para que se vea en el inspector //la f solo se pone con decimales
    [SerializeField]public int currentHealth {get; private set;}
    [SerializeField]private Transform attackHitBox;
    [SerializeField]private float attackRadius;
    [SerializeField]public int maxHealth {get; private set;} = 5;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
        currentHealth = maxHealth;
        GameManager.instance.SetHealthBar(maxHealth);
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
            //Attack();
            StartAttack();
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.Pause();
        }
    }

    void Movement()
    {
        
        if(isAttacking && horizontalInput == 0)
            {
                horizontalInput = 0;
            }
            else
            {
                horizontalInput = Input.GetAxis("Horizontal");
            }

        if(horizontalInput < 0)
        {
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
            //SoundManager.instance.PlaySFX(SoundManager.instance.runAudio);
            
        }
        else if(horizontalInput > 0)
        {
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
            //SoundManager.instance.PlaySFX(SoundManager.instance.runAudio);
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);        //ButtonDown es para cuando lo pulsas
        characterAnimator.SetBool("IsJumping", true);
        SoundManager.instance.PlaySFX(SoundManager.instance.audioSource, SoundManager.instance.jumpAudio);
    }

    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);         // (1,x) = (lados,arriva) <-- [hay que añadir new] // tambien se puede poner directamente la dirección "right"

    }

    void StartAttack()
    {
        isAttacking = true;
        characterAnimator.SetTrigger("Attack");
        SoundManager.instance.PlaySFX(SoundManager.instance.audioSource, SoundManager.instance.atackAudio);
    }

    void Attack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage();
            }
        }
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    void TakeDamage()
    {
        currentHealth--;          // -- es para que se resten de uno en uno y si quieres más sería -= x

        GameManager.instance.UpdateHealthBar(currentHealth);
        
        if(currentHealth <= 0)
        {
            Die();
            LoadGameOver();
        }
        else
        {
            characterAnimator.SetTrigger("IsHurt");
            SoundManager.instance.PlaySFX(SoundManager.instance.audioSource, SoundManager.instance.hurtAudio);
        }

    }

    public void AddHealth()
    {
        currentHealth++;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        GameManager.instance.UpdateHealthBar(currentHealth);
    }

    void Die()
    {
        characterAnimator.SetBool("IsDead", true);
        Destroy(gameObject, 0.7f);
        SoundManager.instance.PlaySFX(SoundManager.instance.audioSource, SoundManager.instance.dieAudio);
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
    
    public void LoadGameOver()
    {
        //WaitForSecondsRealtime(0.12);
        SceneManager.LoadScene("Game Over");
    }
}

// Extension -- Night Pink (tiene murcielagos) // Pink Candy Theme (parece una nube rizada)  //  Merko's green theme (octagono verde y blanco)