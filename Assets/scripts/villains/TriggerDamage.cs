using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TriggerDamage : MonoBehaviour
{
   public Life vida;
   PlayerController player;   
    
   private void OnCollisionEnter2D(Collision2D other){

            if(other.collider.CompareTag("Skin")){
                  vida.vida--;
                 if(vida.vida == 0){
                   vida.coracao[0].sprite = vida.vazio;
                  // player.DeathAnimation(vida);
                   Destroy(other.gameObject);
                    Invoke("Reload",2f);
                 }

            }
         }
        public void Reload(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
     
   }
