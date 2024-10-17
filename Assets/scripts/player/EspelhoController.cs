using UnityEngine;

public class EspelhoController : MonoBehaviour
{
    public Transform handSlot; // Slot na m�o do player
    public GameObject espelhoAtual = null; // Espelho atualmente na m�o

    // Prefabs dos espelhos (atribu�dos via Inspector)
    public GameObject espelhoReflePrefab;
    public GameObject espelhoDifraPrefab;
    public GameObject espelhoInferPrefab;
    public GameObject espelhoPolaPrefab;

    private GameObject tripeEspelho = null; // Refer�ncia ao trip�
    private SpriteRenderer playerSprite; // SpriteRenderer do player para verificar orienta��o

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer do player
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && espelhoAtual != null)
        {
            if (tripeEspelho != null)
            {
                PosicionarEspelhoNoTripe();
            }
            else
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

        if (espelhoAtual == null)
        {
            if (collision.gameObject.CompareTag("EspelhoRefle"))
            {
                EquiparEspelho(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("EspelhoDifra"))
            {
                EquiparEspelho(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("EspelhoInfer"))
            {
                EquiparEspelho(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("EspelhoPola"))
            {
                EquiparEspelho(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TripeEspelho"))
        {
            tripeEspelho = null;
        }
    }

    public void EquiparEspelho(GameObject espelho)
    {
        espelhoAtual = espelho;
        espelhoAtual.transform.SetParent(handSlot);
        espelhoAtual.transform.localPosition = Vector3.zero;

        // Desativa f�sica enquanto o espelho est� na m�o
        Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        espelhoAtual.GetComponent<Collider2D>().enabled = false; // Desativa o collider
    }

    private void SoltarEspelho()
    {
        espelhoAtual.transform.SetParent(null); // Remove da m�o

        // Determina a dire��o baseada na orienta��o do player
        Vector3 dropPosition = transform.position + new Vector3(1.5f, 0, 0); // Ajuste de 1 unidade � frente

        espelhoAtual.transform.position = dropPosition; // Define a posi��o � frente do player

        // Reativa f�sica ao soltar
        Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
        }

        espelhoAtual.GetComponent<Collider2D>().enabled = true; // Reativa o collider
        espelhoAtual = null; // Libera a refer�ncia
    }

    private void PosicionarEspelhoNoTripe()
    {
        espelhoAtual.transform.SetParent(tripeEspelho.transform);
        espelhoAtual.transform.localPosition = Vector3.zero;

        // Desativa f�sica ao posicionar no trip�
        Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        espelhoAtual.GetComponent<Collider2D>().enabled = true; // Reativa o collider
        espelhoAtual = null; // Libera a refer�ncia
    }
}
