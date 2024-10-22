using System.Collections;
using UnityEngine;

public class EspelhoInferencia : MonoBehaviour
{
    public GameObject projetil; // Prefab do bullet
    private int bulletCount = 0; // Contador de bullets recebidos
    private Coroutine timerCoroutine; // Coroutine para rastrear o tempo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            bulletCount++; // Incrementa o número de bullets recebidos

            if (bulletCount == 2) // Se dois bullets colidirem
            {
                InstantiateNewBullet();
                ResetCount(); // Reseta o contador e a coroutine
            }
            else
            {
                // Se for o primeiro bullet, começa a contagem regressiva
                if (timerCoroutine == null)
                {
                    timerCoroutine = StartCoroutine(ResetCountAfterTime());
                }
            }
        }
    }

    // Método para instanciar um novo bullet para cima
    private void InstantiateNewBullet()
    {
        Vector3 spawNewBullet = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
        GameObject newBullet = Instantiate(projetil, spawNewBullet, Quaternion.identity);

        // Configura o novo bullet para se mover no eixo Y (para cima)
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        bulletScript.subirDescer = true; // O bullet se move no eixo Y
    }

    // Reseta o contador de bullets e interrompe a contagem de tempo
    private void ResetCount()
    {
        bulletCount = 0;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    // Reseta o contador após o intervalo de tempo (2 segundos)
    private IEnumerator ResetCountAfterTime()
    {
        yield return new WaitForSeconds(2); // Espera o tempo definido
        ResetCount(); // Reseta o contador de bullets
    }
}
