using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDamage : MonoBehaviour
{
    public Life vida;
    private PlayerController player;
    private bool isReloading = false;
    private

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Skin"))
        {
            vida.vida--;

        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (vida.vida <= 0 && !isReloading)
        {
            isReloading = true;
            vida.coracao[0].sprite = vida.vazio;

            Invoke("Reload", 2f);
        }
    }
}
