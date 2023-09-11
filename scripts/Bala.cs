using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidadeBala = 500.0f;
    public float distanciaMaxima = 5.0f;
    private Rigidbody2D rigidbody2D;

    

    private void Awake() {
        
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    public void Projetil(Vector2 direction) {
        
        rigidbody2D.AddForce(direction * this.velocidadeBala);
        Destroy(this.gameObject, this.distanciaMaxima);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        Destroy(this.gameObject);

    }

}
