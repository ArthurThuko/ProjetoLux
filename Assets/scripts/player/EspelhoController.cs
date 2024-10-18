using UnityEngine;

public class EspelhoController : MonoBehaviour
{
    public Transform handSlot; // Slot onde o espelho ficará na mão
    private GameObject espelhoAtual = null; // Espelho atualmente em mãos
    private GameObject tripeEspelho = null; // Tripé em contato

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tripeEspelho != null) // Se estiver perto de um tripé
            {
                InteragirComTripe();
            }
            else if (espelhoAtual != null) // Se não houver tripé, solta o espelho
            {
                SoltarEspelho();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TripeEspelho"))
        {
            tripeEspelho = collision.gameObject; // Guarda referência ao tripé
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
            tripeEspelho = null; // Saiu do contato com o tripé
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
        espelho.transform.SetParent(handSlot); // Coloca na mão
        espelho.transform.localPosition = Vector3.zero;

        // Desativa física e colisão
        Rigidbody2D rb = espelho.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
        espelho.GetComponent<Collider2D>().enabled = false;
    }

    private void SoltarEspelho()
    {
        espelhoAtual.transform.SetParent(null); // Remove o espelho da mão

        // Define a posição à frente do player
        Vector3 direcao = transform.right; // Usa a direção do player (pode ser ajustada conforme necessário)
        Vector3 novaPosicao = transform.position + direcao * 1.5f; // Ajusta a distância para soltar

        espelhoAtual.transform.position = novaPosicao; // Define a nova posição

        // Reativa física e colisão
        Rigidbody2D rb = espelhoAtual.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.velocity = Vector2.zero; // Garante que o espelho não receba impulso anterior
        }

        Collider2D colisor = espelhoAtual.GetComponent<Collider2D>();
        if (colisor != null) colisor.enabled = true;

        // Sincroniza as transformações para evitar problemas de física
        Physics2D.SyncTransforms();

        espelhoAtual = null; // Libera a referência
    }


    private void InteragirComTripe()
    {
        TripeEspelhoController tripeController = tripeEspelho.GetComponent<TripeEspelhoController>();

        if (espelhoAtual != null) // Coloca o espelho no tripé
        {
            tripeController.ColocarEspelho(espelhoAtual);
            tripeController.espelhoNoTripe = espelhoAtual;
            espelhoAtual = null; // Libera a mão
        }
        else if (tripeController.TemEspelhoNoTripe()) // Pega o espelho do tripé
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
