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

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        healthSlider = (Slider)FindObjectOfType(typeof(Slider));
        currentHealth = startingHealth;
    }

    void Update() { }

    public void setHealthbar(Slider slider)
    {
        healthSlider = slider;
    }

    public void TakeDamage(int amount)
    {
        var wasAlive = isAlive;

        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();

        if (isDead && wasAlive)
        {
            Death();
        }
    }

    public void gainHealth(int amount)
    {
        currentHealth += amount;
        healthSlider.value = currentHealth;
    }

    void Death()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerController.enabled = false;
    }

    public bool isAlive
    {
        get
        {
            return this.currentHealth > 0;
        }
    }

    public bool isDead
    {
        get
        {
            return this.currentHealth <= 0;
        }
    }
}
