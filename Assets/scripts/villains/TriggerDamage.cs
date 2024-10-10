using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TriggerDamage : MonoBehaviour
{
   
   
    void Start(){

   }
   
   private void OnCollisionEnter2D(Collision2D other){

            if(other.collider.CompareTag("Skin")){
                  Destroy(other.gameObject);
                  Invoke("Reload",2f);

            }
         }
      public void Reload(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
   }
