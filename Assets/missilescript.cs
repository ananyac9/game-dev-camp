using UnityEngine;

public class missilescript : MonoBehaviour
{
    public int points = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            scorescript.instance.AddScore(points);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
