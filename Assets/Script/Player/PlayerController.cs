using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // VARIAVEIS PRIVADAS
    private Rigidbody2D rb;
    private Animator anim;  
    private float moveX;

    // VARIAVEIS PUBLICAS
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveX = Input.GetAxisRaw("Horizontal");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Move(); 
        Attack();

        if(isGrounded == true)
        {
            addJumps = 2;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        else
        {
            if (Input.GetButtonDown("Jump")  && addJumps > 0 )
            {
                addJumps--;
                Jump();
            }
        }
        
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (moveX > 0)
        {
             transform.eulerAngles = new Vector3(0f, 0f, 0f);
             anim.SetBool("isRun", true); //setando valor na booleana isRum p/ animação de correr.
        }

        if (moveX < 0) //se o player estiver olhando para o lado esquerdo!
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
             anim.SetBool("isRun", true);
        }
        if (moveX == 0)
        {
             anim.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x , jumpForce);
        anim.SetBool("isJump", true); //setando valor em booleana isJump p/ animação de pular.
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("Attack", -1); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isJump", false); // parar a animação de pular. 
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}