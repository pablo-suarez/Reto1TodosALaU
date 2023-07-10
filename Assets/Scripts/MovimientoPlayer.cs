using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2D;

    [Header("Movement")]

    private float horizontalMove = 0f;

    [SerializeField] private float moveSpeed;

    //Limite de rango de suavizado de movimiento
    [Range(0,0.3f)][SerializeField] private float moveSoft;

    private Vector3 speed = Vector3.zero;

    private bool lookRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    //Superficies aptas para salto de personaje
    [SerializeField] private LayerMask isFloor;
    //Objeto en pies para generar objeto caja
    [SerializeField] private Transform floorController; 
    //Dimensiones de la caja que identifica si el personaje esta sobre el suelo
    [SerializeField] private Vector3 boxDimensions;

    [SerializeField] private bool onFloor;

    private bool jump = false;

    private void Start(){
        rb2D = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;
    }

    private void Update() {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;

        //Si presiono el botón asignado para el salto
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
    }

    private void FixedUpdate() {
        // enSuelo mientras la cajadel personaje (floorController) toque algo que sea suelo
        onFloor = Physics2D.OverlapBox(floorController.position,boxDimensions,0f,isFloor);
        //Hace que el personaje se mueva a través del tiempo para que en equipos de alto y bajo rendimiento se miueva a la misma velocidad
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        //Salto se hace falso (actualizado cada ciclo por cuadro para que no siempre sea true)
        jump = false;
    }

    private void Move(float move, bool jump){
        /*
        * move: Velocidad definida en X
        * rb2D.velocity.y: Velocidad definida en el RigidBody para que no se cambie al saltar o caer
        */
        Vector3 targetSpeed = new Vector2(move,rb2D.velocity.y);
        /*
        * Suavizado a la hora de acelerar o frenar el personaje
        * rb2D.velocity: Velocidad en la que estamos
        * targetSpeed: Velocidad a la que queremos llegar
        * ref speed: que tan rapido queremos llegar
        * moveSoft: cantidad de suavidad
        */
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity,targetSpeed,ref speed, moveSoft);
        /*
        * Se busca SI el personaje se mueve hacia la dirección en la que nos estamos moviendo SINO que gire
        */
        if(move > 0 && !lookRight){
            TurnAround();
        }else if(move < 0 && lookRight){
            TurnAround();
        }
        if(onFloor && jump){
            onFloor = false;
            rb2D.AddForce(new Vector2(0f,jumpForce));
        }
    }

    /*
    * Coloca el personaje hacia el sentido contrario al que lo tenemos
    * Cambia la direccion del personaje multiuplicando por -1 la escala (eje x)
    */
    private void TurnAround(){
        lookRight = !lookRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(floorController.position,boxDimensions);
    }

}
