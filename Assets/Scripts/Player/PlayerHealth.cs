using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip deathClip;
    AudioSource playerAudio;
    Animator animator;
    PlayerController playerController;
    bool isDead;
    void Awake () {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent <AudioSource> ();
        playerController = GetComponent <PlayerController> ();
        healthSlider = (Slider) FindObjectOfType(typeof (Slider));
        currentHealth = startingHealth;
    }

    void Update () {}

    public void setHealthbar(Slider slider) {
        healthSlider = slider;
    }
    public void TakeDamage (int amount){
            currentHealth -= amount;
            healthSlider.value = currentHealth;
            playerAudio.Play ();

            if(currentHealth <= 0 && !isDead) {
                Death ();
            }
    }

    public void gainHealth (int amount) {

            currentHealth += amount;
            healthSlider.value = currentHealth;

            if(currentHealth <= 0 && !isDead) {
                Death ();
            }

            //playerAudio.Play ();
    }

    void Death () {
        isDead = true;

        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play ();
        playerController.enabled = false;
    }
}
