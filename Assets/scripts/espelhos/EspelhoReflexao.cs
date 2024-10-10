using UnityEngine;

public class EspelhoRefle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem o script de Bullet
        Bullet bullet = collision.GetComponent<Bullet>();
        if (collision.CompareTag("Bullet"))
        {
            // Define a direção da bala para cima (altera para o eixo Y)
            bullet.ReflectUp();
        }
    }
}
