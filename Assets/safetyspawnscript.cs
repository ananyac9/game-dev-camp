using UnityEngine;

public class safetyspawnscript : MonoBehaviour
{
    public GameObject safetyland;
    public float timer = 0;
    public float spawnrate = 20;
    public float heightOffset = 3f;
    public float minVerticalDistance = 3f;
    public Camera mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
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
            SpawnSafety();
            timer = 0;
        }
        
    }
    void SpawnSafety()
    {
        float screenTop = mainCam.ViewportToWorldPoint(new Vector3(0, 1, mainCam.nearClipPlane)).y;
        float screenBottom = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane)).y;

        float maxtop = screenTop - heightOffset;
        float maxbottom = screenBottom + heightOffset;

        float randomHeight = Random.Range(maxtop, maxbottom);

        Vector3 spawnLocation = transform.position;
        spawnLocation.x = transform.position.x;
        spawnLocation.y = randomHeight;

        Instantiate(safetyland, spawnLocation, transform.rotation);
    }
}
