using UnityEngine;

public class TripeEspelho : MonoBehaviour
{
    public GameObject espelhoNoTripe = null; // Espelho atualmente no trip�
    private EspelhoController playerController; // Refer�ncia ao script do player

    // Deslocamento vertical para posicionar o espelho acima do trip�
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
                // Player pega o espelho do trip�
                playerController.EquiparEspelho(espelhoNoTripe);
                espelhoNoTripe = null;
            }
            else if (playerController.espelhoAtual != null && espelhoNoTripe == null)
            {
                // Player coloca o espelho no trip�
                ColocarEspelhoNoTripe(playerController.espelhoAtual);
                playerController.espelhoAtual = null;
            }
        }
    }

    private void ColocarEspelhoNoTripe(GameObject espelho)
    {
        espelhoNoTripe = espelho;

        // Posiciona o espelho acima do trip� sem mudar a hierarquia
        Vector3 posicaoAcima = transform.position + new Vector3(0, alturaAcima, 0);
        espelhoNoTripe.transform.position = posicaoAcima;

        // Desativa a f�sica enquanto o espelho est� no trip�
        Rigidbody2D rb = espelhoNoTripe.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        espelhoNoTripe.GetComponent<Collider2D>().enabled = true; // Reativa o collider
    }
}
