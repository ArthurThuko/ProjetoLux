using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    public int vida;
    public int vidaMax;
    public UnityEngine.UI.Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;
    

    void Start(){
      
    }
    void Update()
    {
        HealthLogic();
        
    }

    void  HealthLogic(){

        if( vida > vidaMax){
            vida = vidaMax;
        }

        for(int i =0; i< coracao.Length; i++){

            if(i<vida){
                coracao[i].sprite = cheio;

            }

            else {
                coracao[i].sprite = vazio;
            }


            if(i<vidaMax){
                coracao[i].enabled =true;
            }
            else{
                coracao[i].enabled =false;
            }
        }
       
    }
   
}
