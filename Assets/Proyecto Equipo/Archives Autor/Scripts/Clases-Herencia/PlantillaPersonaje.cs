using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantillaPersonaje : MonoBehaviour, IDaño{

    protected Rigidbody2D rgb2d;
    [SerializeField]protected float speed = 1f;
    [SerializeField]protected float Damage = 1f;
    [SerializeField]protected float maxlifepoints = 100f;
    [SerializeField]protected float lifepoints;
    [SerializeField]protected bool rotar;
    [SerializeField]protected Vector2 rebote;
    protected bool canMove = true;
    protected virtual void Start(){
        rgb2d = GetComponent<Rigidbody2D>();
        lifepoints = maxlifepoints;
    }
    protected virtual void Update(){
        if (canMove){
            Movimiento();
        }
    }
    protected virtual void Movimiento(){
        if (rotar){
            rgb2d.velocity = new Vector2(speed,rgb2d.velocity.y);
        }else{
            rgb2d.velocity = new Vector2(-speed,rgb2d.velocity.y);
        }
    }
    public virtual void Take_damage(float damage, Vector2 puntoGolpe){
        canMove = false;
        lifepoints -=damage;
        StartCoroutine(TomarGolpe(puntoGolpe));
        if (lifepoints <= 0){
           Animar_Muerte();
        }
    }
    protected virtual IEnumerator TomarGolpe(Vector2 puntoGolpe){
    rgb2d.velocity = new Vector2(-rebote.x * puntoGolpe.x, rebote.y);
    yield return new WaitForSeconds(1f);
    canMove = true;
    }
    public virtual void Animar_Muerte(){
        MostrarMuerte mostrar = GetComponent<MostrarMuerte>();
        mostrar.Animar_Muerte();
        Destroy(gameObject);
    }
    public virtual void MuerteporLava(){
        canMove = false;
        lifepoints = 0;
        Animar_Muerte();
    }
    protected virtual void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            Vector2 contactNormal = other.GetContact(0).normal;
           if (contactNormal.x != 0 && contactNormal.y == 0){
            IDaño contacto = other.gameObject.GetComponent<IDaño>();
              if (contacto != null){
                contacto.Take_damage(Damage,contactNormal);
              }
            }
        }
    }

}
