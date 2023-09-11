using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public float TempoRespawn = 3.0f;
    public Player player;
    public float Invunerabilidade = 3.0f;
    public GameObject gameOverUI;

     public int pontos { get; private set; }
    public Text TextoDePontuacao;

    public int vidas { get; private set; }
    public Text vida;

     AudioManager audioManager;

     private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (vidas <= 0 && Input.GetKeyDown(KeyCode.Space)) {
            NewGame();
        }
    }

    public void NewGame()
    {
        Asteroides[] Asteroids = FindObjectsOfType<Asteroides>();

        for (int i = 0; i < Asteroids.Length; i++) {
            Destroy(Asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);

        SetPontos(0);
        SetVidas(3);
        Respawn();
    }



    public void AsteroidDestruido (Asteroides Asteroids) {
        
        this.explosion.transform.position = Asteroids.transform.position;
        this.explosion.Play();
        audioManager.PlaySFX(audioManager.destruir);

       if (Asteroids.size < 0.7f) {
            SetPontos(pontos + 1000); 
        } else if (Asteroids.size < 1.0f) {
            SetPontos(pontos + 250); 
        } else {
            SetPontos(pontos + 125); 
        }


    }

    public void playerMorreu () {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        SetVidas(vidas - 1);
        audioManager.PlaySFX(audioManager.morrer);

        if(this.vidas <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.TempoRespawn);
        }

        
    }

    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("ignorarColisao");
        this.player.gameObject.SetActive(true);
        
        Invoke(nameof(LigarColisao), this.Invunerabilidade);
    }

    private void LigarColisao () {
        this.player.gameObject.layer = LayerMask.NameToLayer("jogador");
    }

    private void GameOver () {
        gameOverUI.SetActive(true);
    }

    private void SetPontos(int pontos)
    {
        this.pontos = pontos;
        TextoDePontuacao.text = pontos.ToString();
    }

    private void SetVidas(int vidas)
    {
        this.vidas = vidas;
        vida.text = vidas.ToString();
    }
    
}
