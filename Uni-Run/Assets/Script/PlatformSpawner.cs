using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    ////////////////////////////////// 내가 한거 //////////////////////////////////
    //public GameObject platformprefab;

    //public GameObject last;

    //public Vector3 spawnPoint;

    //private int spawnCount = 0;

    //private void Update()
    //{
    //    if (spawnCount == 0)
    //    {
    //        spawnCount++;
    //        spawnPoint = new Vector3(Random.Range(15f, 20f), Random.Range(-1.5f, 1.5f), 0f);
    //        GameObject go = Instantiate(platformprefab, spawnPoint, platformprefab.transform.rotation);
    //    }
    //    else if (spawnCount == 1)
    //    {
    //        spawnCount++;
    //        spawnPoint = new Vector3(Random.Range(35f, 40f), Random.Range(-1.5f, 1.5f), 0f);
    //        GameObject go = Instantiate(platformprefab, spawnPoint, platformprefab.transform.rotation);
    //    }
    //    else if (spawnCount == 2)
    //    {
    //        spawnCount++;
    //        spawnPoint = new Vector3(Random.Range(55f, 60f), Random.Range(-1.5f, 1.5f), 0f);
    //        GameObject go = Instantiate(platformprefab, spawnPoint, platformprefab.transform.rotation);
    //    }
    //    else if (spawnCount == 3)
    //    {
    //        spawnCount++;
    //        spawnPoint = new Vector3(Random.Range(75f, 80f), Random.Range(-1.5f, 1.5f), 0f);
    //        last = Instantiate(platformprefab, spawnPoint, platformprefab.transform.rotation);
    //    }
    //    else if (spawnCount == 4)
    //    {
    //        spawnCount++;
    //        spawnPoint = new Vector3(Random.Range(95f, 100f), Random.Range(-1.5f, 1.5f), 0f);
    //        GameObject go = Instantiate(platformprefab, spawnPoint, platformprefab.transform.rotation);
    //    }
    //    else if (last.transform.position.x < -20)
    //    {
    //        spawnCount = 0;
    //    }
    //}


    ////////////////////////////////교수님///////////////////////////////////

    public GameObject prefab;
    public int poolSize = 10;

    public float intervalMin = 1.5f;
    public float intervalMax = 3f;

    public float ymin = -1f;
    public float ymax = -1f;

    private GameObject[] platforms;
    private int currentIndex;

    private float interval = 1f;
    private float timer = 0;

    private GameManager gameManager;

    private void Awake()
    {
        platforms = new GameObject[poolSize];
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = Instantiate(prefab);
            platforms[i].SetActive(false);
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        Spawn();
    }

    private void Update()
    {
        if (gameManager.isGameOver)
            return;


        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer = 0f;
            Spawn();
        }
    }
    private void Spawn()
    {
        var position = transform.position;
        position.y = Random.Range(ymin, ymax);

        platforms[currentIndex].transform.position = position;
        platforms[currentIndex].SetActive(true);
        currentIndex = (currentIndex + 1) % platforms.Length;
        interval = Random.Range(intervalMin, intervalMax);
    }
}

