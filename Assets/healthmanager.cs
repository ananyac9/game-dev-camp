using UnityEngine;
using UnityEngine.UI;

public class healthmanager : MonoBehaviour
{
    public GameObject[] hearts;
    public int maxHealth = 5;
    public int currentHealth;
    public alienscript alienscript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHeart()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
        }

        if (currentHealth <= 0)
        {
            alienscript.GameOver();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    public void ReplenishHeart()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHearts() ;
        }
    }
}
