using UnityEngine;

public class Life : MonoBehaviour
{
    PlayerController playerController;
    public bool isDead;
    public int vida = 3;    
    public int vidaMax = 3;
    public UnityEngine.UI.Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (vida > vidaMax)
            vida = vidaMax;
    }

    void Update()
    {
        HealthLogic();
        CheckDeath();
    }

    void HealthLogic()
    {
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }

            coracao[i].enabled = i < vidaMax;
        }
    }

    void CheckDeath()
    {
        if (vida <= 0 && !isDead)
        {
            isDead = true;
            playerController.enabled = false;
            playerController.anime.SetBool("isDead", true);
        }
    }
}
