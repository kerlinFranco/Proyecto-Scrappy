

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Gravedad : byte {
    NORMAL = 1,
    DESCENSO = 5
}
public enum Daño : byte {
    NORMAL = 1,
    DESCENSO = 2
}

public class Player_Ardilla : PlantillaPersonaje{
    [SerializeField] float fuerzaSalto = 10f;
    [SerializeField] int maxSaltos = 2;
    private int saltosRestantes;
    [SerializeField] private LayerMask groundmask;
    [SerializeField] private LayerMask enemylayer;
    private BoxCollider2D boxcolision;
    private CircleCollider2D circlecolision;
    private Animator playeranimator;
    public Transform controladorSuelo;
    public Vector3 dimensionCaja;
    public float raycastDistance = 0.2f;
    public bool ensuelo;
    private bool estabaEnSuelo;//borrar desp
    [SerializeField] Image imagenVida;
    protected override void Start(){
        base.Start();
        boxcolision = GetComponent<BoxCollider2D>();
        circlecolision = GetComponent<CircleCollider2D>();
        playeranimator = GetComponent<Animator>();
        saltosRestantes = maxSaltos;
        circlecolision.enabled = false;
        rgb2d.gravityScale = (float) Gravedad.NORMAL;
        Damage = (float) Daño.NORMAL;
        estabaEnSuelo = false;//borrar desp
    }
    protected override void Update(){
        CheckSuelo();
        if (lifepoints > 0 && canMove){
            Movimiento();
            Procesar_salto();
        }
    }
    void CheckSuelo(){
        
        
        ensuelo = Physics2D.OverlapBox(controladorSuelo.position,dimensionCaja,0f,groundmask);
        // inicio de prueba
        if (ensuelo && !estabaEnSuelo)
        {
            boxcolision.enabled = true;
            circlecolision.enabled = false;
            rgb2d.gravityScale = (float)Gravedad.NORMAL;
            Damage = (float)Daño.NORMAL;
            saltosRestantes = maxSaltos;
            playeranimator.SetBool("Jump", false);  // Establecer la animación de salto en false cuando está en el suelo
        }
        else if (!ensuelo && estabaEnSuelo)
        {
            circlecolision.enabled = true;
            boxcolision.enabled = false;
            playeranimator.SetBool("Jump", true);  // Establecer la animación de salto en true cuando no está en el suelo
        }

        estabaEnSuelo = ensuelo; // fin de prueba
        /*
         if (ensuelo){
             boxcolision.enabled = true;
             circlecolision.enabled = false;
             rgb2d.gravityScale = (float) Gravedad.NORMAL;
             Damage = (float) Daño.NORMAL;
             saltosRestantes = maxSaltos;
             playeranimator.SetBool("Jump", false); //posible solución del salt
         }
         else{
             circlecolision.enabled = true;
             boxcolision.enabled = false;

         }
         */
    }
    public override void Take_damage(float damage, Vector2 puntoGolpe){
        playeranimator.SetTrigger("Hit");
        StartCoroutine(DesactivarColision());
        canMove = false;
        ActualizarVida(-damage);
        StartCoroutine(TomarGolpe(puntoGolpe));
        if (lifepoints <= 0){
           Animar_Muerte();
        }
    }
    private IEnumerator DesactivarColision(){
    Physics2D.IgnoreLayerCollision(7,8,true);
    yield return new WaitForSeconds(1f);
    Physics2D.IgnoreLayerCollision(7,8,false);
    }
    public override void Animar_Muerte(){
        playeranimator.SetTrigger("dead");
        Destroy(gameObject,1f);
    }
    public override void MuerteporLava(){
        lifepoints = 0;
        imagenVida.fillAmount = 0;
        Animar_Muerte();
    }
    public void VidaAmount(){
        imagenVida.fillAmount = lifepoints / maxlifepoints;
    }
    public void ActualizarVida(float life){
        lifepoints+= life;
        VidaAmount();
    }
    public bool RecargarVida(){
        if(lifepoints < maxlifepoints){
            return true;
        }else{
            return false;
        }
    }
    public void ActualizarVidaMax(){
        lifepoints = maxlifepoints;
        imagenVida.fillAmount = 1;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionCaja);
    }
    protected override void Movimiento(){
      float movimiento = Input.GetAxis("Horizontal");

        if (movimiento != 0f){
            playeranimator.SetBool("isRunning",true);
        }else{
            playeranimator.SetBool("isRunning",false);
        }
        rgb2d.velocity = new Vector2(movimiento * speed,rgb2d.velocity.y);
        Cambiar_orientacion(movimiento);

    }
    void Cambiar_orientacion(float orientacion){
        if (orientacion < 0){
            transform.localScale = new Vector2(-1f,transform.localScale.y);
        } else if(orientacion > 0) {
            transform.localScale = new Vector2(1f,transform.localScale.y);
        }
    }
    void Procesar_salto(){
        /*tengo que cambiar la logica de esto, si toco w o up hacer el salto
        si toco s o down aumentar la gravedad
        */

if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && saltosRestantes > 0){
saltosRestantes--;
rgb2d.velocity = new Vector2(rgb2d.velocity.x,0f);
rgb2d.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
playeranimator.SetBool("Jump", true);//intento de arreglar salto
}
if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !ensuelo){
rgb2d.gravityScale = (float) Gravedad.DESCENSO;
Damage = (float) Daño.DESCENSO;
}

if(ensuelo){
    saltosRestantes = maxSaltos;
    playeranimator.SetBool("Jump",false);
}else{
    playeranimator.SetBool("Jump",true);
}
}
protected override void OnCollisionEnter2D(Collision2D other){
if(((1 << other.gameObject.layer) & enemylayer) != 0){
    Vector2 contactNormal = other.GetContact(0).normal;
    if (!ensuelo && contactNormal.y != 0){
      IDaño contacto = other.gameObject.GetComponent<IDaño>();
      if (contacto != null){
        contacto.Take_damage(Damage,contactNormal);
        canMove = false;
        StartCoroutine(Rebotar(contactNormal));
      }
    }
}
}
private IEnumerator Rebotar(Vector2 Punto){
rgb2d.velocity = new Vector2(-rebote.x,rgb2d.velocity.y * (Punto.y * 2));
yield return new WaitForSeconds(1f);
canMove = true;
}
}


/* habia pensado en una cosa hace un rato en el mismo gameobject del personaje tener un boxcollider2d
y un circle collider2d desactivado, asi una vez que salta el personaje desactivo uno y activo el otro
despues veo si esa logica se puede implementar
*/
