using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anime;
    public Life life;
    public float movex;
    public GameObject projetil;
    public float speed;
    public bool isGrounded;
    public float jumpForce;
    public bool canShoot = true;
    public float fireRate = 0.5f;
    public float nextFireTime = 0f;
    public AudioSource soundFx;
    public float KBCount;
    public float KBTime;
    public float KBForce;

    public bool isKnockRigth;

    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 1f;
    public float knockbackUpwardForce = 5f;
    public float knockbackBackwardForce = 5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        life = GetComponent<Life>();
        soundFx = GetComponent<AudioSource>();
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
            soundFx.Play();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(movex * speed, rb.velocity.y);

        // Flipar o personagem
        if (movex > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anime.SetBool("isRun", true);
        }
        else if (movex < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anime.SetBool("isRun", true);
        }
        else
        {
            anime.SetBool("isRun", false);
        }
    }

    void KnockLogic(){
            if(KBCount <0){
                Move();
            }
            else{
                if( isKnockRigth == true){
                    rb.velocity = new Vector2(-KBForce,KBForce);


                }
                else if( isKnockRigth == false){
                    rb.velocity = new Vector2(KBForce,KBForce);
                     

                }
            }
            KBCount -= Time.deltaTime;
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
                bulletScript.SetDirection(true);
            }
            else
            {
                bulletScript.SetDirection(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anime.SetBool("isJump", false);
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Danger" && !isInvulnerable)
        {
            StartCoroutine(ApplyKnockback(collision));
            StartCoroutine(BecomeInvulnerable());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private IEnumerator ApplyKnockback(Collision2D collision)
    {
        rb.velocity = Vector2.zero; 
        rb.AddForce(new Vector2(0, knockbackUpwardForce), ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(1f);

        float direction = (transform.position.x < collision.transform.position.x) ? -1 : 1;
        rb.AddForce(new Vector2(direction * knockbackBackwardForce, 0), ForceMode2D.Impulse);
    }

    private IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
