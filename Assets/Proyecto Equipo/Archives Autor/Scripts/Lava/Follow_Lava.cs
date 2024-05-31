using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Lava : MonoBehaviour{
    private Transform player;
    private Rigidbody2D rgb2d;
    [SerializeField] float speed;
    void Awake(){
        player = FindObjectOfType<Player_Ardilla>().transform;
    }
    void Start(){
        rgb2d = GetComponent<Rigidbody2D>();
    }
    void Update(){
        FollowPlayer();
    }
    void FollowPlayer(){
        Vector2 directionPlayer = (player.position - transform.position).normalized;
        rgb2d.velocity = directionPlayer * speed;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
        other.gameObject.GetComponent<Player_Ardilla>().MuerteporLava();
       }
    }
}
