using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelHistoria;
    [SerializeField] private GameObject painelTutorial;

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void AbrirHistoria()
    {
        painelMenuInicial.SetActive(false);
        painelHistoria.SetActive(true);
    }

    public void FecharHistoria()
    {
        painelHistoria.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void AbrirTutorial()
    {
        painelMenuInicial.SetActive(false);
        painelTutorial.SetActive(true);
    }

    public void FecharTutorial()
    {
        painelTutorial.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
