using System;
using UnityEngine;


public class villain : MonoBehaviour{

    public float speed;
    public Rigidbody2D enemyRb;
    public bool faceFlip;
    void Start(){

    }

    void Update(){

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void FliEnemy(){

        if(faceFlip){
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else{
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);

        }
    }
    private void OnCollisionEnter2D(Collision2D colision){

        if(colision != null && !colision.collider.CompareTag("Player") && !colision.collider.CompareTag("Ground")){

            faceFlip = !faceFlip;
        }
        FliEnemy();
    }
}
