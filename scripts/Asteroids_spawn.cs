using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids_spawn : MonoBehaviour
{
    public float varianciaDaTrajetoria = 15.0f;
    public Asteroides asteroidesfab;
    public float distanciaDeAparecimento = 15.0f;
    public float tempoDeAparecimento = 2.0f;
    public float aparecaQuantidade = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.tempoDeAparecimento, this.tempoDeAparecimento);  

    }

    private void Spawn()  
    {
        for (int i = 0; i < this.aparecaQuantidade; i++)
        {
            Vector3 aparecimentoAleatorio = Random.insideUnitCircle.normalized * this.distanciaDeAparecimento;  
            Vector3 pontoDeAparecimento = this.transform.position + aparecimentoAleatorio;  

            float variancia = Random.Range(-this.varianciaDaTrajetoria, this.varianciaDaTrajetoria);
            Quaternion rotation = Quaternion.AngleAxis(variancia, Vector3.forward);  

            Asteroides asteroides = Instantiate(this.asteroidesfab, pontoDeAparecimento, rotation);  
            asteroides.size = Random.Range(asteroides.minSize, asteroides.maxSize);
            asteroides.setarTrajetoria(rotation * -aparecimentoAleatorio);  

        }
    }
}
