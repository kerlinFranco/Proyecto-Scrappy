using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow_fly : PlantillaPersonaje{
    protected Transform player;
    [SerializeField] protected float distanciaMin = 4f;
    protected bool isFollow = false;
    private Animator enemyAnimator;
    protected virtual void Awake(){
        player = FindObjectOfType<Player_Ardilla>().transform;
    }
    protected override void Start(){
        base.Start();
        enemyAnimator = GetComponent<Animator>();
    }
    protected override void Update(){
        bool GiroPlayer = transform.position.x > player.transform.position.x;
       if((PruebaDistancia() > distanciaMin) && !isFollow){
           
        }else{
            FollowPlayer();
            Girar(GiroPlayer);
        }
        Animaciones();
    }
    private void Animaciones(){
        if(!isFollow){
            enemyAnimator.SetBool("isFollow",false);
        }else{
            enemyAnimator.SetBool("isFollow",true);
        }
    }
    protected virtual void FollowPlayer(){
        isFollow = true;
        Vector2 directionPlayer = (player.position - transform.position).normalized;
        rgb2d.velocity = directionPlayer * speed;
    }

    protected virtual void Girar(bool giro){
        if ((rotar && !giro) || (!rotar && giro)){
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y+180,0);
        rotar=!rotar;
        }
    }
    protected virtual float PruebaDistancia(){
        return Vector2.Distance(transform.position,player.position);
    }
    public override void Take_damage(float damage, Vector2 puntoGolpe){
        base.Take_damage(damage,puntoGolpe);
    }
}
