using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;// player's position.

    PlayerHealth playerHealth;
    // EnemyHealth enemyHealth;
    NavMeshAgent nav;

    void Awake ()
    {

        // enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();

    }


    void Update ()
    {
        // if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        // {
            //nav.Warp(player.position);
            nav.SetDestination (player.position);
            Debug.Log("Enemy chases " + player.position);
        // }
        // else
        // {
            // nav.enabled = false;
        // }
    }
}