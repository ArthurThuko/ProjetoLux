using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float livingTimer = 3f;

    private Vector3 move = Vector3.zero;
    private bool isMovingRight;

    public bool direcao = true;
    public bool subirDescer = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, livingTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (subirDescer)
        {
            move.y = speed * Time.deltaTime;
            transform.position += move;
        } else
        {
            if (direcao)
            {
                move.x = (isMovingRight ? 1 : -1) * speed * Time.deltaTime;
                transform.position += move;
            }
            else
            {
                move.x = (!isMovingRight ? 1 : -1) * speed * Time.deltaTime;
                transform.position += move; transform.position += move;
            }
        }
    }

    public void SetDirection(bool movingRight)
    {
        isMovingRight = movingRight;

        if (!isMovingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    public void Direction()
    {
        speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("EspelhoRefle"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("EspelhoDifra"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("EspelhoInfer"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("EspelhoPola"))
        {
            Destroy(gameObject);
        }
    }
}
