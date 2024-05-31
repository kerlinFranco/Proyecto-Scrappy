using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_Ascendente : MonoBehaviour{
    private Rigidbody2D rgb2d;
    [SerializeField] float speed;
    void Start() {
        rgb2d = GetComponent<Rigidbody2D>();
    }
    void Update(){
        Ascender();
    }
    void Ascender(){
        rgb2d.velocity = new Vector2(rgb2d.velocity.x, speed);
    }
    void OnTriggerEnter2D(Collider2D other){
     /*PlantillaPersonaje plantilla = other.gameObject.GetComponent<PlantillaPersonaje>();
        if (plantilla != null){
            plantilla.MuerteporLava();
        }*/
       if (other.gameObject.CompareTag("Player")){
        Player_Ardilla player = other.gameObject.GetComponent<Player_Ardilla>();
            if (player != null) {
                player.MuerteporLava();
            } 
        //llamamos al metodo muerte por lava del jugador, y capas tirar un evento del gamemanager
       }
    }
}
