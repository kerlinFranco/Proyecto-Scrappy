using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrojadizo : MonoBehaviour{
    private Rigidbody2D rgb2;
    private Transform player;
    public float speed;
    public float destroytime = 4f;
    [SerializeField] float damage = 1f;
    void Start(){
        Resize();
        rgb2 = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();
        player = FindAnyObjectByType<Player_Ardilla>().transform;
        LaunchProyectile();
    }
    private void LaunchProyectile(){
        Vector2 directionPlayer = (player.position - transform.position).normalized;
        rgb2.velocity = directionPlayer * speed;
        //StartCoroutine(DestroyProyectile());
    }
    private void Resize(){
        float scaleX = CrearNumRandom();
        Debug.Log("scaleX"+ scaleX);
        float scaleY = CrearNumRandom();
        Debug.Log("scaleY"+ scaleY);
        transform.localScale = new Vector2(scaleX,scaleY);
    }
    float CrearNumRandom(){
        return Random.Range(1f, 5f);
    }
    IEnumerator DestroyProyectile(){
        yield return new WaitForSeconds(destroytime);
        Destroy(gameObject);
    }
    /*void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            Vector2 contactNormal = other.GetContact(0).normal;
            IDaño contacto = other.gameObject.GetComponent<IDaño>();
              if (contacto != null){
                contacto.Take_damage(damage,contactNormal);
              }
        }
    }*/
}
