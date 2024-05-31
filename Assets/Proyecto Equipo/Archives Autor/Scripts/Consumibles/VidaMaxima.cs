using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaMaxima : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            Player_Ardilla player = other.gameObject.GetComponent<Player_Ardilla>();
            if (player != null){
                if (player.RecargarVida()){
                   player.ActualizarVidaMax(); 
                   Destroy(gameObject);
                }
            }
        }
    }
/* otra logica que se podria implementar seria hacer todo esto pero en el player
y hacer una especie de animacion cual recupera toda la vida
seria igual osea hacer la comparacion de la tag vidamax actualizar el hud y eso y tirar la animacion
y llamar a un metodo para destruir el objeto
*/
}
