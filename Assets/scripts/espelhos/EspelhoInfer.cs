using System.Collections;
using UnityEngine;

public class EspelhoInferencia : MonoBehaviour
{
    public GameObject projetil;
    private int bulletCount = 0;
    private Coroutine timerCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            bulletCount++;

            if (bulletCount == 2)
            {
                InstantiateNewBullet();
                ResetCount();
            }
            else
            {
                if (timerCoroutine == null)
                {
                    timerCoroutine = StartCoroutine(ResetCountAfterTime());
                }
            }
        }
    }

    private void InstantiateNewBullet()
    {
        Vector3 spawNewBullet = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
        GameObject newBullet = Instantiate(projetil, spawNewBullet, Quaternion.identity);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        bulletScript.subirDescer = true;
    }

    private void ResetCount()
    {
        bulletCount = 0;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private IEnumerator ResetCountAfterTime()
    {
        yield return new WaitForSeconds(2);
        ResetCount();
    }
}
