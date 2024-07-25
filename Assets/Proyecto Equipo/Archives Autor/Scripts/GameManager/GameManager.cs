using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public int PuntosTotales {get {return puntosTotales;} }
    private int puntosTotales;
    [SerializeField] float TiempoEspera = 5f;
    private float temporizador;
    private void Awake() {
        if (Instance == null){
            Instance = this;
        }else{
            Debug.Log("mas de un gamemanager en escena");
        }
    }

    public void SumarPuntos(int puntosaSumar){
        Debug.Log("sumo los puntos");
        puntosTotales+=puntosaSumar;
        HUD.Instance.ActualizarPuntos(puntosTotales);
    }

    private void Update() {
        temporizador += Time.deltaTime;
        if (temporizador >= TiempoEspera){
          Lanzar();
            temporizador = 0.0f;
        }
    }
    void Lanzar(){
        LanzadordeObjetos.Instance.IntanciarObjeto();
    }
   /* public void PruebaActualizarVidaGM(float life){
        if(HUD.Instance.ComprobarVida()){
            HUD.Instance.ActualizarVida(life);
        }
        if(HUD.Instance.VidaActual == 0){
            HUD.Instance.Death();
            Debug.Log("Personaje muerto HUD");
        }
    }*/
}
