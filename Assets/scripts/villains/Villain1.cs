using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V : MonoBehaviour
{
    public float speed;
    public Rigidbody2D enemyRb;
    public bool faceFlip;
    public float flipInterval = 2f;
    public Animator anime;


    void Start()
    {
        StartCoroutine(AutoFlip()); 
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void FliEnemy()
    {
        if (faceFlip)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private IEnumerator AutoFlip()
    {
        while (true)
        {
            yield return new WaitForSeconds(flipInterval);
            faceFlip = !faceFlip;
            FliEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && !collision.CompareTag("Player") && !collision.CompareTag("Ground"))
        {
            faceFlip = !faceFlip;
            FliEnemy();
        }

        if (collision.CompareTag("Bullet"))
        {
            transform.Translate(Vector2.left * 0);
            anime.SetBool("isDeade", true);
            Destroy(gameObject, 1.1f);
        }
    }
    }
