using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour{

    [SerializeField] float puntosVida = 10f;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            Player_Ardilla player = other.gameObject.GetComponent<Player_Ardilla>();
            if (player != null){
                if (player.RecargarVida()){
                   player.ActualizarVida(puntosVida); 
                   Destroy(gameObject);
                }
            }
        }
    }
/* la logica seria primero comproba que la vida actual sea menor a la vida maxima si es asi
    hacer la parte de actualizar vida
    en cuyo caso que no, osea que tenga vida maxima el personaje, el script no hace nada
*/
    
}
