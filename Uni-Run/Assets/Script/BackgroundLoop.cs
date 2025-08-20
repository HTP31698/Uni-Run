using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float speed = 10f;

    private float width;
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        width = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
        //var col = GetComponent<BoxCollider2D>();
        //width = col.size.x;
    }

    void Update()
    {
        GameObject findgo = GameObject.FindGameObjectWithTag("GameController");
        GameManager gm = findgo.GetComponent<GameManager>();
        if (gm.isGameOver)
            return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < -width)
        {
            transform.position = new Vector3(width -0.2f, 0f, 0f);
        }
    }
}
