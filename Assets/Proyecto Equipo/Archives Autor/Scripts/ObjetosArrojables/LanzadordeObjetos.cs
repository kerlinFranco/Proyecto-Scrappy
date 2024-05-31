using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class LanzadordeObjetos : MonoBehaviour{
    public static LanzadordeObjetos Instance { get; private set;}
    [SerializeField] Transform [] lanzadores;
    [SerializeField] GameObject objeto;
    private int numAleatorio;
    private void Awake() {
        if (Instance == null){
            Instance = this;
        }else{
            Debug.Log("mas de un LanzadordeObjetos en escena");
        }
    }
    int CrearNumRandom(){
        return UnityEngine.Random.Range(0, 10);
    }
    private IEnumerator LanzarObjeto(){
    GameObject nuevoAtaque = Instantiate(objeto, lanzadores[numAleatorio].position,lanzadores[numAleatorio].rotation);
    nuevoAtaque.transform.localScale = new Vector3(objeto.transform.localScale.x,objeto.transform.localScale.y, 1f);
    yield return new WaitForSeconds(1f); 
    }
    public void IntanciarObjeto(){
        numAleatorio = CrearNumRandom();
        Debug.Log("lanzado desde "+ numAleatorio);
        StartCoroutine(LanzarObjeto());
    }
}
