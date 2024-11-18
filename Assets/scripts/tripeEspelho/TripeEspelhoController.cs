using UnityEngine;

public class TripeEspelhoController : MonoBehaviour
{
    public Transform pontoEspelho;
    public GameObject espelhoNoTripe = null;

    public void ColocarEspelho(GameObject espelho)
    {
        if (espelhoNoTripe == null)
        {
            espelho.transform.SetParent(pontoEspelho);
            espelho.transform.localPosition = Vector3.zero;

            Collider2D collider = espelho.GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = true;

            Rigidbody2D rb = espelho.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
                rb.simulated = true;
            }
        }
    }

    public bool TemEspelhoNoTripe()
    {
        return espelhoNoTripe != null;
    }

    public GameObject PegarEspelho()
    {
        GameObject espelho = espelhoNoTripe;

        espelho.transform.SetParent(null); 

        return espelho;
    }
}
