using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variáveis privadas
    private Rigidbody2D rb;
    private Animator anime;
    private float movex;

    //Variáveis públicas
    public GameObject projetil;
    public float speed;
    public bool isGrounded;
    public float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        movex = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
        Attack();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(movex * speed, rb.velocity.y);

        //Flipar o pesornagem
        if (movex > 0) //Lado direito
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anime.SetBool("isRun", true);
        }

        if(movex < 0) //Lado Esquerdo
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anime.SetBool("isRun", true);
        }
        if(movex == 0)
        {
            anime.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anime.SetBool("isJump", true);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletInstance = Instantiate(projetil, transform.position, transform.rotation);

            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();

            if (transform.eulerAngles.y == 0)
            {
                bulletScript.SetDirection(true); // Direção para a direita
            }
            else
            {
                bulletScript.SetDirection(false); // Direção para a esquerda
            }

            anime.Play("attack", -1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Verificar se é chão para pular
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anime.SetBool("isJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
