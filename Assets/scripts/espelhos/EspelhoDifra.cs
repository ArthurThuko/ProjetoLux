using UnityEngine;

public class EspelhoDifra : MonoBehaviour
{
    public GameObject projetil;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet originalBullet = collision.GetComponent<Bullet>();
            bool originalDirection = originalBullet.direcao;

            Destroy(collision.gameObject);
            
            //Bullet para frente
            Vector3 spawNewBullet1 = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            GameObject bulletSameDirection = Instantiate(projetil, spawNewBullet1, Quaternion.identity);
            Bullet bulletSameScript = bulletSameDirection.GetComponent<Bullet>();
            bulletSameScript.direcao = originalDirection;
            
            //Bullet para tras
            Vector3 spawNewBullet2 = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);
            GameObject bulletOppoDirection = Instantiate(projetil, spawNewBullet2, Quaternion.identity);
            Bullet bulletOppoScript = bulletOppoDirection.GetComponent<Bullet>();
            bulletOppoScript.direcao = !originalDirection;
            
            //Bullet para cima
            Vector3 spawNewBullet3 = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
            GameObject bulletUp = Instantiate(projetil, spawNewBullet3, Quaternion.identity);
            Bullet bulletUpScript = bulletUp.GetComponent<Bullet>();
            bulletUpScript.subirDescer = true;
        }
    }
}
