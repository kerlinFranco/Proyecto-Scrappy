using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarMuerte : MonoBehaviour{
    [SerializeField] private Transform controladorMuerte;
    [SerializeField] private GameObject dead;
    public void Animar_Muerte(){
    GameObject muerte = Instantiate(dead,controladorMuerte.position, controladorMuerte.rotation);
    muerte.transform.localScale = new Vector3(dead.transform.localScale.x,dead.transform.localScale.y, 1f);
    }
}
