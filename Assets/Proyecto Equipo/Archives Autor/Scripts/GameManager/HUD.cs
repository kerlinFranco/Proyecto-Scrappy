using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour{
    public static HUD Instance { get; private set;}
    public TextMeshProUGUI puntos;
   /* public Image lifepoint;
    private float vidaActual;
    public float VidaActual{get { return vidaActual; }}
    [SerializeField]
    private float vidaMax = 100;
    public float VidaMax{get { return vidaMax; }}*/

    private void Awake() {
        if (Instance == null){
            Instance = this;
        }else{
            Debug.Log("mas de un HUD en escena");
        }
    }
   /* void Start(){
        vidaActual = vidaMax;
    }*/

    public void ActualizarPuntos(int puntosObtenidos){
        puntos.text = puntosObtenidos.ToString();
    }
  /*  public void Death(){
        vidaActual = 0;
        lifepoint.fillAmount = 0;
    }

    public void PruebaVidaAmount(){
        lifepoint.fillAmount = vidaActual / vidaMax;
    }
    public void ActualizarVida(float life){
        vidaActual+= life;
        PruebaVidaAmount();
    }

    public bool ComprobarVida(){
        //if(vidaActual != 0 && vidaActual < vidaMax){
        if(vidaActual != 0){
            return true;
        }else{
            return false;
        }
    }
    public bool RecargarVida(){
        if(vidaActual < vidaMax){
            return true;
        }else{
            return false;
        }
    }

    public void ActualizarVidaMax(){
        vidaActual = vidaMax;
        lifepoint.fillAmount = 1;
    }
*/
}
