using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrojadizo : MonoBehaviour{
    
 
    private Rigidbody2D rgb2;
    private Transform player;
    public float speed;
    public float destroytime = 4f;
    private bool hasLanded = false;// para rastrear si la piedra ha aterrizado.
    //[SerializeField] float damage = 1f;
    void Start(){
        Resize();
        rgb2 = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();
        //intento de arreglar el rebote de la roca
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        PhysicsMaterial2D noBounceMaterial = new PhysicsMaterial2D
        {
            friction = 0.4f, // Ajusta según sea necesario
            bounciness = 0f
        };
        collider.sharedMaterial = noBounceMaterial;

        player = FindAnyObjectByType<Player_Ardilla>().transform;
        LaunchProyectile();
        rgb2.bodyType = RigidbodyType2D.Dynamic;
    }
    private void LaunchProyectile(){
        Vector2 directionPlayer = (player.position - transform.position).normalized;
        rgb2.velocity = directionPlayer * speed;
        //StartCoroutine(DestroyProyectile());
    }
    private void Resize(){
        float scaleX = 2f;//CrearNumRandom();
        Debug.Log("scaleX"+ scaleX);
        float scaleY = 2f;//CrearNumRandom();
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
    //inico de función para que no se mueva las piedras cuando toque el piso u otra piedra
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLanded)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Arrojadizo"))
            {
                rgb2.bodyType = RigidbodyType2D.Static;
                hasLanded = true;
            }
        }
    }
    //fin 

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
