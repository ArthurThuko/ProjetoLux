using UnityEngine;

public class EspelhoPolarizacao : MonoBehaviour
{
    public GameObject bulletVermelho;
    public GameObject bulletAzul;

    private bool usarBulletVermelho = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AlternarTipoBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            if (usarBulletVermelho)
            {
                InstanciarBullet(bulletVermelho);
            }
            else
            {
                InstanciarBullet(bulletAzul);
            }
        }
    }

    private void AlternarTipoBullet()
    {
        usarBulletVermelho = !usarBulletVermelho;
        string tipoAtual = usarBulletVermelho ? "Vermelho" : "Azul";
        Debug.Log("Bullet atual: " + tipoAtual);
    }

    private void InstanciarBullet(GameObject bulletPrefab)
    {
        Vector3 spawnNewBullet = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        GameObject newBullet = Instantiate(bulletPrefab, spawnNewBullet, Quaternion.identity);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        bulletScript.subirDescer = true;
    }
}
