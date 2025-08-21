using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 8f;
 

    private void Update()
    {
        GameObject findgo = GameObject.FindGameObjectWithTag("GameController");
        GameManager gm = findgo.GetComponent<GameManager>();
        if (gm.isGameOver)
            return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        //if (transform.position.x < -20.48f)
        //{
        //    Destroy(gameObject);
        //}
        //if(transform.position.x < -20.4f)
        //{
        //    Vector3 mapPos = transform.position;
        //    mapPos.x = 20.4f;
        //    transform.SetPositionAndRotation(mapPos, transform.rotation);
        //}
    }
}
