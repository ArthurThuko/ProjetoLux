using UnityEngine;

public class EspelhoController : MonoBehaviour
{
    public int espelho;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EspelhoRefle"))
        {
            Destroy(collision.gameObject);
            espelho++;
        }
    }
}
