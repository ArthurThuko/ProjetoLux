using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    //Vari�veis privadas
    private Rigidbody2D rb;
    private Animator anime;
    public float movex;

    //Vari�veis p�blicas
    public GameObject projetil;
    public float speed;
    public bool isGrounded;
    public float jumpForce;
    public bool canShoot = true;
    public float fireRate = 0.5f;
    public float nextFireTime = 0f;

    

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
        if (Input.GetButtonDown("Fire1") && canShoot && Time.time > nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }

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

    public void DeathAnimation(Life vida){
        
            anime.SetBool("IsDead", true);  
       
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
                bulletScript.SetDirection(true); // Dire��o para a direita
            }
            else
            {
                bulletScript.SetDirection(false); // Dire��o para a esquerda
            }

            anime.Play("attack", -1);

            canShoot = false;
        }
    }

    void LateUpdate()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            canShoot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Verificar se � ch�o para pular
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
