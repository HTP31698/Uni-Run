using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 8f;
    public int jumpCountMax = 2;

    public AudioClip dieaudioclip;

    private int jumpCount;
    private Animator animator; //animator �� ����ؼ� ���� ����
    private Rigidbody2D rb; // Rigidbody2D �� ����ؼ� ���� ����
    private AudioSource audioSource;
   

    private bool isGrounded = true;
    private bool isDead = false;
    //animator�� �ִ� Run���� true�϶� �ִϸ��̼� ���
    //animator�� �ִ� Jump���� false�϶� �ִϸ��̼� ���

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GameObject findgo = GameObject.FindGameObjectWithTag("GameController");
        GameManager gm = findgo.GetComponent<GameManager>();
        if (gm.isGameOver)
            return;

        if (Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax)
        {
            audioSource.Play();
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
            ++jumpCount;
        }

        if(Input.GetMouseButtonUp(0) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity *= 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gm.AddScore(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") &&
            collision.contacts[0].normal.y > 0.7f)
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
        rb.bodyType = RigidbodyType2D.Kinematic; //���� �ù� X
        rb.linearVelocity = Vector2.zero; // �����ڸ����� ���߰� �ϱ�
        isDead = true;
        audioSource.PlayOneShot(dieaudioclip);

        gm.OnPlayerDead();
    }
}
