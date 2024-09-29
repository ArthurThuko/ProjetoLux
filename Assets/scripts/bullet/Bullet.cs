using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    [SerializeField] private float damage = 1f;

    [SerializeField] private float livingTimer = 3f;

    private  Vector3 move = Vector3.zero;

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
        move.x = speed * Time.deltaTime;
        transform.position += move;
    }
    public void Direction()
    {
        speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Classe Health
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Espelho")
        {
            //Classe Health
            Destroy(gameObject);
        }
    }
}
