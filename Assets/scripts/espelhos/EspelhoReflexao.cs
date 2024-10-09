using UnityEngine;

public class EspelhoRefle : MonoBehaviour
{
    public GameObject projetil;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            Vector3 spawNewBullet = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

            GameObject newBullet = Instantiate(projetil, spawNewBullet, Quaternion.identity);

            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            bulletScript.direcao = false;
        }
    }
}
