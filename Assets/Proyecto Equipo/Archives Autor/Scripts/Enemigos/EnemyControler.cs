using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : PlantillaPersonaje{

    public override void Take_damage(float damage, Vector2 puntoGolpe){
        lifepoints -=damage;
        if (lifepoints <= 0){
           Animar_Muerte();
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Ground")){
        ReactivateMovement();
        }
    }
    protected virtual void ReactivateMovement(){
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y+180,0);
        rotar=!rotar;
    }
    
}
