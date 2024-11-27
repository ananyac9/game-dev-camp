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
        //if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y - 1 ||
        //        transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + 1)
        //{
        //    NotifyMiss();
        //    Destroy(gameObject);
        //}
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        if (transform.position.y < bottomLeft.y - 1 || transform.position.y > topRight.y + 1 ||
            transform.position.x > topRight.x + 1)
        {
            NotifyMiss();
            Destroy(gameObject);
        }
    }

    void NotifyMiss()
    {
        GameObject alien = GameObject.FindWithTag("Alien");
        if (alien != null) 
        {
            alien.GetComponent<alienscript>()?.OnMissedMissile();
        }
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
