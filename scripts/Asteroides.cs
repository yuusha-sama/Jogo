using UnityEngine;

public class Asteroides : MonoBehaviour
{
    public float minSize = 0.5f;
    public float size = 1.0f;
    public float maxSize = 1.5f;
    public Sprite[] sprites;
    public float velocidade = 50.0f;  
    public float tempoMaximo = 30.0f;  
    private SpriteRenderer render;
    private Rigidbody2D rigidbody2D;

    

    private void Awake()
    {
        
        render = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        render.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
        rigidbody2D.mass = this.size;
    }

    public void setarTrajetoria(Vector2 direction)  
    {
        rigidbody2D.AddForce(direction * this.velocidade);
        Destroy(this.gameObject, this.tempoMaximo);
    }

    private void OnCollisionEnter2D(Collision2D colisao) {
        
        if(colisao.gameObject.tag == "Bala") {
            if(this.size * 0.5f > this.minSize) {
                CriarSeparacao();
                CriarSeparacao();
            }
            
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().AsteroidDestruido(this);
        }
    } 

    private void CriarSeparacao () {

        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroides metade = Instantiate(this, position, this.transform.rotation);
        metade.size = this.size * 0.5f;
        metade.setarTrajetoria(Random.insideUnitCircle.normalized * this.velocidade);
    }

}

