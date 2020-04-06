using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Healthbar healthBar;
    private bool flashActive;
    private float flashCounter = 0.5f;
    private float flashLength = 0.5f;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashActive)
        {
            player.GetComponent<Animator>().SetBool("hurt", true);
            if(flashCounter <= 0)
            {
                player.GetComponent<Animator>().SetBool("hurt", false);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        flashActive = true;
        healthBar.SetHealth(currentHealth);
        flashCounter = flashLength;
        if(currentHealth <= 0)
        {
            Invoke("Respawn", 1.4f);
            player.GetComponent<Animator>().SetBool("death", true);
        }
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
