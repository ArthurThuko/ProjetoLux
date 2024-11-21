using UnityEngine;

public class ButtonTriggerVermelho : MonoBehaviour
{
    public Transform porta;
    public float alturaDestino = 20f;
    public float velocidade = 2f;
    private bool ativarPorta = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletVermelho"))
        {
            ativarPorta = true;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (ativarPorta && porta.position.y < alturaDestino)
        {
            porta.position = Vector2.MoveTowards(porta.position, new Vector2(porta.position.x, alturaDestino), velocidade * Time.deltaTime);
        }
    }
}
