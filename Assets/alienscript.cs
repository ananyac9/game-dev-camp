using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class alienscript : MonoBehaviour
{
    public float clickPower = 3;
    public Rigidbody2D alienRigid;
    public GameObject missilePF;
    public float missileSpeed = 10f;
    public Transform missileSpawnPoint;
    private bool isLanded = false;
    public float landedZ = -1;
    public float unlandedZ = 0;
    public GameObject gameOverPanel;
    public Camera mainCam;
    public GameObject logicManager;
    //public healthmanager healthmanager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(isLanded);
        if (!isLanded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                alienRigid.linearVelocity = Vector2.up * clickPower;
                //MoveAlien(Vector2.up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                alienRigid.linearVelocity = Vector2.down * clickPower;
                //MoveAlien(Vector2.down);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShootMissileUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ShootMissileDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ShootMissileRight();
        }

        if (isLanded && Input.GetKeyDown(KeyCode.Space))
        {
            DetachFromSafety();
        }

        CheckOutOfBounds();
    }

    void ShootMissileRight()
    {
        GameObject missile = Instantiate(missilePF, missileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D missileRigidBody = missile.GetComponent<Rigidbody2D>();
        if (missileRigidBody != null )
        {
            missileRigidBody.linearVelocity = Vector2.right * missileSpeed;
        }

    }

    void ShootMissileUp()
    {
        GameObject missile = Instantiate(missilePF, missileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D missileRigidBody = missile.GetComponent<Rigidbody2D>();
        if (missileRigidBody != null)
        {
            missileRigidBody.linearVelocity = Vector2.up * missileSpeed;
        }

    }
    void ShootMissileDown()
    {
        GameObject missile = Instantiate(missilePF, missileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D missileRigidBody = missile.GetComponent<Rigidbody2D>();
        if (missileRigidBody != null)
        {
            missileRigidBody.linearVelocity = Vector2.down * missileSpeed;
        }

    }
    void MoveAlien(Vector2 direction)
    {
        float newYPosition = alienRigid.position.y + direction.y * clickPower;

        alienRigid.position = new Vector2(alienRigid.position.x, newYPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SafetyLand"))
        {
            LandOnSafety(collision.transform);
        }
        if (collision.CompareTag("Asteroid"))
        {
            GameOver();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SafetyLand"))
        {
            DetachFromSafety();
        }
    }

    void LandOnSafety(Transform safetyLand)
    {
        isLanded = true;
        alienRigid.linearVelocity = Vector2.zero;
        alienRigid.bodyType = RigidbodyType2D.Kinematic;
        //transform.position = new Vector3(transform.position.x, safetyLand.position.y, landedZ);
        float safetyTopY = safetyLand.position.y + (safetyLand.localScale.y / 2); 
        float alienHeight = transform.localScale.y / 2; 
        transform.position = new Vector3(transform.position.x, safetyTopY + alienHeight, landedZ);
        healthmanager healthManager = logicManager.GetComponent<healthmanager>();
        if (healthManager != null )
        {
            healthManager.ReplenishHeart();
        }
    }

    void DetachFromSafety()
    {
        isLanded = false;
        alienRigid.linearVelocity = Vector2.down * clickPower;
        alienRigid.bodyType = RigidbodyType2D.Dynamic;
        transform.position = new Vector3(transform.position.x, transform.position.y, unlandedZ);
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CheckOutOfBounds()
    {
        Vector3 alienPosition = transform.position;
        Vector3 screenBottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane));
        Vector3 screenTopRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));

        if (alienPosition.y < screenBottomLeft.y || alienPosition.y > screenTopRight.y)
        {
            GameOver();
        }

    }

    public void OnMissedMissile()
    {
        healthmanager healthManager = logicManager.GetComponent<healthmanager>();
        if (healthManager != null)
        {
            healthManager.LoseHeart();
        }
    }
}
