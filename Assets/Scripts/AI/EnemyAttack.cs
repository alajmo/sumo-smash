using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 20;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

     List<GameObject> players;

     List <GameObject> ps;


    void Awake ()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        ps = GetAlivePlayers();
        player = GetClosestPlayer(ps);
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();

    }

    void OnTriggerEnter (Collider other)
    {
        player = GetClosestPlayer(ps);
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
            ps = GetAlivePlayers();
            playerInRange = false;
        }
         player = GetClosestPlayer(ps);
         playerHealth = player.GetComponent <PlayerHealth> ();
    }

     List <GameObject> GetAlivePlayers() {
        List <GameObject> ps = players.Where(player =>
            player.GetComponent<PlayerHealth>() != null &&
            player.GetComponent<PlayerHealth>().currentHealth > 0).ToList();
        return ps;
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
        Debug.Log(player + " attacked" + playerHealth.currentHealth);
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
