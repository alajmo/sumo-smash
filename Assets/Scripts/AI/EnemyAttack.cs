using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.

     List<GameObject> players;
     

    void Awake ()
    {
        //player = GameObject.FindGameObjectWithTag ("Player");
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        player = GetClosestPlayer(players);
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        
    }

    void OnTriggerEnter (Collider other)
    {   
        player = GetClosestPlayer(players);
        if(other.gameObject == player)
        {   
            Debug.Log("player triggered" + other.gameObject);
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        GameObject player = GetClosestPlayer(players);

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Debug.Log(player + "attacked");
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }

    GameObject GetClosestPlayer(List <GameObject> ps) {
        // 2. Get player that is closest
        GameObject closestPlayer = null;
        float distance = 99999f;
        foreach (GameObject player in ps) {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < distance) {
                closestPlayer = player;
                distance = dist;
            }
        }

        return closestPlayer;
    }


    void Attack ()
    {
        timer = 0f;

        // If the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
