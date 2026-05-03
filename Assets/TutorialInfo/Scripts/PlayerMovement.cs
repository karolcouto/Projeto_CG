using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f; // Nova variável para controlar a força do pulo
    public Transform modelo;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded; // Variável para checar se o jogador está tocando o chão

    public Transform pontoLanterna;
    private GameObject lanternaAtual;
    private GameObject lanternaPerto;
public Light luzLanterna;
public bool temLanterna = false;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        luzLanterna.enabled = false;

    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float moveAmount = new Vector3(moveX, 0f, moveZ).magnitude;
        

        anim.SetFloat("Speed", moveAmount);

        // Flip do personagem (2.5D)
        if (moveX > 0)
        {
            modelo.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (moveX < 0)
        {
            modelo.rotation = Quaternion.Euler(0, -90, 0);
        }

        // Lógica do Pulo
        // Verifica se o botão de pulo (Espaço por padrão) foi pressionado E se o personagem está no chão
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Pular();
        }

        if (Input.GetKeyDown(KeyCode.E) && lanternaPerto != null)
{
    PegarLanterna(lanternaPerto);
}
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveX, 0f, 0f);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Pular()
    {
        // Aplica uma força de impulso para cima no Rigidbody
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Assim que pula, ele não está mais no chão
        
        // Se você tiver uma animação de pulo no Animator, pode ativá-la aqui:
        // anim.SetTrigger("Jump"); 

        

    anim.SetBool("isJumping", true);
    }

    // Detecção de colisão para saber se voltou ao chão
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto em que o jogador encostou tem a Tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter(Collider other)
{
    Debug.Log("Entrou em trigger com: " + other.name);

    if (other.CompareTag("Lanterna"))
    {
        lanternaPerto = other.gameObject;
        Debug.Log("Aperte E para pegar");
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Lanterna"))
    {
        lanternaPerto = null;
    }
}


    void PegarLanterna(GameObject lanternaObj)
{

    luzLanterna.enabled = true;

    temLanterna = true;

    lanternaAtual = lanternaObj;
    anim.SetBool("temLanterna", true);
    // torna filha do player (na mão)
    lanternaObj.transform.SetParent(pontoLanterna);

    // posiciona certinho
    lanternaObj.transform.localPosition = Vector3.zero;
    lanternaObj.transform.localRotation = Quaternion.identity;

    // desliga física
    Rigidbody rbLanterna = lanternaObj.GetComponent<Rigidbody>();
    if (rbLanterna != null)
        rbLanterna.isKinematic = true;

    Collider colLanterna = lanternaObj.GetComponent<Collider>();
    if (colLanterna != null)
        colLanterna.isTrigger = false;
}

}