using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int currentHealth;
    public Slider[] healthSliders;
    public AudioClip deathClip;
    AudioSource playerAudio;
    Animator animator;
    PlayerController playerController;
    bool isDead;

    void Awake () {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent <AudioSource> ();
        playerController = GetComponent <PlayerController> ();
        currentHealth = startingHealth;
        healthSliders = (Slider[]) FindObjectsOfType(typeof (Slider));
    }

    void Update () {}

    public void TakeDamage (int amount){

            currentHealth -= amount;
            foreach(Slider slider in healthSliders) {
                slider.value = currentHealth;
                 playerAudio.Play ();
                Debug.Log("player health " + slider.value);
            }
            
           
            if(currentHealth <= 0 && !isDead) {
                Death ();
            }
    }

    public void gainHealth (int amount) {

            currentHealth += amount;
            //healthSliders.value = currentHealth;

            if(currentHealth <= 0 && !isDead) {
                Death ();
            }
        
     // playerAudio.Play ();
    }

    void Death () {
        isDead = true;

        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play ();
        playerController.enabled = false;
    }
}
