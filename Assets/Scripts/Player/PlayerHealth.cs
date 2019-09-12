using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip deathClip;
    AudioSource playerAudio;
    PlayerController playerController;
    bool isDead;

    void Awake () {
        playerAudio = GetComponent <AudioSource> ();
        playerController = GetComponent <PlayerController> ();
        currentHealth = startingHealth;
        healthSlider = (Slider) FindObjectOfType(typeof (Slider));
    }

    void Update () {

    }

    public void TakeDamage (int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead) {
            Death ();
        }
    }

    public void gainHealth (int amount) {
        currentHealth += amount;
        // healthSlider.value = currentHealth;
        // playerAudio.Play ();
    }

    void Death () {
        isDead = true;

        playerAudio.clip = deathClip;
        playerAudio.Play ();
        playerController.enabled = false;
    }
}
