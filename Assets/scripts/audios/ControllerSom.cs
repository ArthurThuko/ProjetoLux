using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSom : MonoBehaviour
{
    private bool estadoSom = true;
    [SerializeField] private AudioSource fundoMusica;

    [SerializeField] private Sprite somLigadoSprinte;
    [SerializeField] private Sprite somDesligadoSprite;

    [SerializeField] private Image muteImage;

    public void LigarDesligarSom()
    {
        estadoSom = !estadoSom;
        fundoMusica.enabled = estadoSom;

        if (estadoSom){
            muteImage.sprite = somLigadoSprinte;
        }
        else
        {
            muteImage.sprite = somDesligadoSprite;
        }
    }
}
