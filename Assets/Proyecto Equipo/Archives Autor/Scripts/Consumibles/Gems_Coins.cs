using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems_Coins : MonoBehaviour
{
    [SerializeField]
    private int valor = 1;
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);
        }
    }
}
