using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 8f;
    public int jumpCountMax = 2;

    private int jumpCount;
    private Animator animator; //animator 을 사용해서 변수 선언
    private Rigidbody2D rb; // Rigidbody2D 를 사용해서 변수 선언

    private bool isGrounded = true;
    private bool isDead = false;
    //animator에 있는 Run에서 true일때 애니메이션 출력
    //animator에 있는 Jump에서 false일때 애니메이션 출력

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GameObject findgo = GameObject.FindGameObjectWithTag("GameController");
        GameManager gm = findgo.GetComponent<GameManager>();
        if (gm.isGameOver)
            return;

        if (Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax)
        {
            rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
            ++jumpCount;
        }

        animator.SetBool("Grounded", isGrounded);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gm.AddScore(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject findgo = GameObject.FindGameObjectWithTag("GameController");
        GameManager gm = findgo.GetComponent<GameManager>();
        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Kinematic; //물리 시뮬 X
        rb.linearVelocity = Vector2.zero; // 죽은자리에서 멈추게 하기
        isDead = true;

        gm.OnPlayerDead();
    }
}
