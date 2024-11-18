using UnityEngine;

public class EspelhoController : MonoBehaviour
{
    public Transform handSlot;
    private GameObject espelhoAtual = null;
    private GameObject tripeEspelho = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tripeEspelho != null)
            {
                InteragirComTripe();
            }
            else if (espelhoAtual != null)
            {
                SoltarEspelho();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TripeEspelho"))
        {
            tripeEspelho = collision.gameObject;
        }
        else if (espelhoAtual == null && EspelhoValido(collision.gameObject))
        {
            EquiparEspelho(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TripeEspelho"))
        {
            tripeEspelho = null;
        }
    }

    private bool EspelhoValido(GameObject obj)
    {
        return obj.CompareTag("EspelhoRefle") || obj.CompareTag("EspelhoDifra") ||
               obj.CompareTag("EspelhoInfer") || obj.CompareTag("EspelhoPola");
    }

    private void EquiparEspelho(GameObject espelho)
    {
        espelhoAtual = espelho;
        espelho.transform.SetParent(handSlot);
        espelho.transform.localPosition = Vector3.zero;

        Rigidbody2D rb = espelho.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
        espelho.GetComponent<Collider2D>().enabled = false;
    }

    private void SoltarEspelho()
    {
        espelhoAtual.transform.SetParent(null);
        Vector3 direcao = transform.right;
        Vector3 novaPosicao = transform.position + direcao * 1.5f;

        espelhoAtual.transform.position = novaPosicao; 

        Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.simulated = true;
            rb.velocity = Vector2.zero;
        }

        Collider2D colisor = espelhoAtual.GetComponent<Collider2D>();
        if (colisor != null) colisor.enabled = true;

        Physics2D.SyncTransforms();

        espelhoAtual = null;
    }


    private void InteragirComTripe()
    {
        TripeEspelhoController tripeController = tripeEspelho.GetComponent<TripeEspelhoController>();

        if (espelhoAtual != null)
        {
            tripeController.ColocarEspelho(espelhoAtual);
            tripeController.espelhoNoTripe = espelhoAtual;
            espelhoAtual = null;
        }
        else if (tripeController.TemEspelhoNoTripe())
        {
            espelhoAtual = tripeController.PegarEspelho();
            espelhoAtual = tripeController.espelhoNoTripe;
            tripeController.espelhoNoTripe.transform.SetParent(handSlot);
            tripeController.espelhoNoTripe.transform.localPosition = Vector3.zero;

            tripeController.espelhoNoTripe = null;

            Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = false;
            espelhoAtual.GetComponent<Collider2D>().enabled = false;
        }
    }
}
