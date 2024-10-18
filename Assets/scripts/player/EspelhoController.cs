using UnityEngine;

public class EspelhoController : MonoBehaviour
{
    public Transform handSlot; // Slot onde o espelho ficar� na m�o
    private GameObject espelhoAtual = null; // Espelho atualmente em m�os
    private GameObject tripeEspelho = null; // Trip� em contato

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tripeEspelho != null) // Se estiver perto de um trip�
            {
                InteragirComTripe();
            }
            else if (espelhoAtual != null) // Se n�o houver trip�, solta o espelho
            {
                SoltarEspelho();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TripeEspelho"))
        {
            tripeEspelho = collision.gameObject; // Guarda refer�ncia ao trip�
        }
        else if (espelhoAtual == null && EspelhoValido(collision.gameObject))
        {
            EquiparEspelho(collision.gameObject); // Equipar espelho
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TripeEspelho"))
        {
            tripeEspelho = null; // Saiu do contato com o trip�
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
        espelho.transform.SetParent(handSlot); // Coloca na m�o
        espelho.transform.localPosition = Vector3.zero;

        // Desativa f�sica e colis�o
        Rigidbody2D rb = espelho.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
        espelho.GetComponent<Collider2D>().enabled = false;
    }

    private void SoltarEspelho()
    {
        espelhoAtual.transform.SetParent(null); // Remove o espelho da m�o

        // Define a posi��o � frente do player
        Vector3 direcao = transform.right; // Usa a dire��o do player (pode ser ajustada conforme necess�rio)
        Vector3 novaPosicao = transform.position + direcao * 1.5f; // Ajusta a dist�ncia para soltar

        espelhoAtual.transform.position = novaPosicao; // Define a nova posi��o

        // Reativa f�sica e colis�o
        Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.velocity = Vector2.zero; // Garante que o espelho n�o receba impulso anterior
        }

        Collider2D colisor = espelhoAtual.GetComponent<Collider2D>();
        if (colisor != null) colisor.enabled = true;

        // Sincroniza as transforma��es para evitar problemas de f�sica
        Physics2D.SyncTransforms();

        espelhoAtual = null; // Libera a refer�ncia
    }


    private void InteragirComTripe()
    {
        TripeEspelhoController tripeController = tripeEspelho.GetComponent<TripeEspelhoController>();

        if (espelhoAtual != null) // Coloca o espelho no trip�
        {
            tripeController.ColocarEspelho(espelhoAtual);
            tripeController.espelhoNoTripe = espelhoAtual;
            espelhoAtual = null; // Libera a m�o
        }
        else if (tripeController.TemEspelhoNoTripe()) // Pega o espelho do trip�
        {
            espelhoAtual = tripeController.PegarEspelho();
            espelhoAtual = tripeController.espelhoNoTripe;
            tripeController.espelhoNoTripe.transform.SetParent(handSlot);
            tripeController.espelhoNoTripe.transform.localPosition = Vector3.zero;

            espelhoAtual.GetComponent<Collider2D>().enabled = false;

            Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
