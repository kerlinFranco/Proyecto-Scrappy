using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : EnemyFollow_fly{
    [SerializeField] Transform [] PuntosdeControl;
    private int ActualPunto;
    bool estaEsperando;

    protected override void Awake(){
        base.Awake();
    }
    protected override void Update(){
        bool GiroPlayer = transform.position.x < player.transform.position.x;
       if((PruebaDistancia() > distanciaMin) && !isFollow){
            Movimiento();
        }else{
            FollowPlayer();
            Girar(GiroPlayer);
        }
    }
    protected override void Movimiento(){
        /*aqui hacer la logica de moverse entre 4 ejes que establezcamos
        si se detecta al jugador con la distania min que estableci esto dejaria de hacerse
        */
        if (transform.position != PuntosdeControl[ActualPunto].position){
            transform.position = Vector2.MoveTowards(transform.position,PuntosdeControl[ActualPunto].position, speed * Time.deltaTime);
        }else if (!estaEsperando){
            StartCoroutine(Espera());
        }
    }
    private IEnumerator Espera(){
        estaEsperando = true;
        yield return new WaitForSeconds(1f);
        ActualPunto++;
        if (ActualPunto == PuntosdeControl.Length){
            ActualPunto = 0;    
        }
        estaEsperando = false;
        Girar(transform.position.x < PuntosdeControl[ActualPunto].position.x);
    }
    public override void Take_damage(float damage, Vector2 puntoGolpe){
        base.Take_damage(damage,puntoGolpe);
    }
  /*  void GirarporPunto(){
        if (transform.position.x > PuntosdeControl[ActualPunto].position.x){
            transform.rotation = Quaternion.Euler(0f,180f,0f); 
        }else{
            transform.rotation = Quaternion.Euler(0f,0f,0f); 
        }
    }*/
}
