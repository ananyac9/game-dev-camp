using UnityEngine;

public class asteroidspawnscript : MonoBehaviour
{
    public GameObject asteroid;
    public float timer = 0;
    public float spawnrate = 2;
    public float heightOffset = 2f;
    public float minVerticalDistance = 3f;
    public Camera mainCam;
    private float lastSpawnHeight;
    public GameObject safeLand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
        lastSpawnHeight = transform.position.y;
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnAsteroid();
            timer = 0;
        }
    }
    void SpawnAsteroid()
    {
        float screenTop = mainCam.ViewportToWorldPoint(new Vector3(0, 1, mainCam.nearClipPlane)).y;
        float screenBottom = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane)).y;

        float maxtop = screenTop - heightOffset;
        float maxbottom = screenBottom + heightOffset;

        float randomHeight;

        do
        {
            randomHeight = Random.Range(maxtop, maxbottom);
        } while (Mathf.Abs(randomHeight - lastSpawnHeight)  < minVerticalDistance);

        lastSpawnHeight = randomHeight;

        Vector3 spawnLocation = transform.position;
        spawnLocation.x = transform.position.x;
        spawnLocation.y = randomHeight;

        Instantiate(asteroid, spawnLocation, transform.rotation);
    }
}
