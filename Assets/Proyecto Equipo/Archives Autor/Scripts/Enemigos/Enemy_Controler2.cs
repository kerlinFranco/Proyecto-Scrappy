using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controler2 : EnemyControler{

    protected Animator enemyAnimator;
    [SerializeField] protected float tiempoEspera;
    protected override void Start(){
        base.Start();
        enemyAnimator = GetComponent<Animator>();
    }
    protected override void Update(){
        if (canMove){
            Movimiento();
        }else{
            rgb2d.velocity = new Vector2(0,rgb2d.velocity.y);
            enemyAnimator.SetBool("iswalk",false);
        }
    }

    protected override void Movimiento(){
        if (canMove){
        enemyAnimator.SetBool("iswalk",true);
            if (rotar){
                rgb2d.velocity = new Vector2(speed,rgb2d.velocity.y);
            }else{
                rgb2d.velocity = new Vector2(-speed,rgb2d.velocity.y);
            }
        }else{
            enemyAnimator.SetBool("iswalk",false);
        }
    }
    public override void Take_damage(float damage, Vector2 puntoGolpe){
        enemyAnimator.SetTrigger("hit");
        lifepoints -=damage;
        if (lifepoints <= 0){
           Animar_Muerte();
        }
    }
    public override void Animar_Muerte(){
        canMove = false;
        enemyAnimator.SetTrigger("dead");
        base.Animar_Muerte();
        //Destroy(gameObject,1f);
    }
    public override void MuerteporLava(){
        base.MuerteporLava();
    }
    protected override void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Ground")){
        canMove = false;
        Invoke("ReactivateMovement", tiempoEspera);
        }
    }
    protected override void ReactivateMovement(){
        base.ReactivateMovement();
        canMove = true;
    }
}
