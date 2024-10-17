using UnityEngine;

public class TripeEspelho : MonoBehaviour
{
    public GameObject espelhoNoTripe = null; // Espelho atualmente no tripé
    private EspelhoController playerController; // Referência ao script do player

    // Deslocamento vertical para posicionar o espelho acima do tripé
    public float alturaAcima = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<EspelhoController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = null;
        }
    }

    private void Update()
    {
        if (playerController != null && Input.GetKeyDown(KeyCode.E))
        {
            if (playerController.espelhoAtual == null && espelhoNoTripe != null)
            {
                // Player pega o espelho do tripé
                playerController.EquiparEspelho(espelhoNoTripe);
                espelhoNoTripe = null;
            }
            else if (playerController.espelhoAtual != null && espelhoNoTripe == null)
            {
                // Player coloca o espelho no tripé
                ColocarEspelhoNoTripe(playerController.espelhoAtual);
                playerController.espelhoAtual = null;
            }
        }
    }

    private void ColocarEspelhoNoTripe(GameObject espelho)
    {
        espelhoNoTripe = espelho;

        // Posiciona o espelho acima do tripé sem mudar a hierarquia
        Vector3 posicaoAcima = transform.position + new Vector3(0, alturaAcima, 0);
        espelhoNoTripe.transform.position = posicaoAcima;

        // Desativa a física enquanto o espelho está no tripé
        Rigidbody2D rb = espelhoNoTripe.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        espelhoNoTripe.GetComponent<Collider2D>().enabled = true; // Reativa o collider
    }
}
