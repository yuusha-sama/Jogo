using UnityEngine;

public class Player : MonoBehaviour
{
    public Bala balafab;
    public float velocidadeAceleracao = 1.0f;
    public float velocidadeDeVirada = 1.0f;
    private Rigidbody2D rigidbody2D;
    private bool acelerando;
    private float mudarDirecao;

    AudioManager audioManager;
    GameManager gameManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        acelerando = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A))
        {
            mudarDirecao = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            mudarDirecao = -1.0f;
        }
        else
        {
            mudarDirecao = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            audioManager.PlaySFX(audioManager.atirar);
            Atirar();
        }
    }

    private void FixedUpdate()
    {
        if (acelerando)
        {
            rigidbody2D.AddForce(transform.up * velocidadeAceleracao);
        }

        if (mudarDirecao != 0.0f)
        {
            rigidbody2D.AddTorque(mudarDirecao * velocidadeDeVirada);
        }
    }

    private void Atirar()
    {
        Bala bala = Instantiate(balafab, transform.position, transform.rotation);
        bala.Projetil(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("asteroid"))
        {
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            gameManager.playerMorreu();
        }
    }
}
