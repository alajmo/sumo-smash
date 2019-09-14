using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    List<GameObject> players;
    EnemyHealth enemyHealth;
    NavMeshAgent navMeshAgent;

    void Awake () {
        enemyHealth = GetComponent <EnemyHealth>();
        navMeshAgent = GetComponent <NavMeshAgent>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        // Debug.Log(players[0].GetComponent<PlayerHealth>().currentHealth);
    }

    void Update () {
        List <GameObject> ps = GetAlivePlayers();
        GameObject player = GetClosestPlayer(ps);
        MoveEnemeyTowardsPlayer(player);
    }

    List <GameObject> GetAlivePlayers() {
        // 1. Filter players with health > 0
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

    void MoveEnemeyTowardsPlayer(GameObject player) {
        // 3. Move towards that player
        if (player != null) {
            navMeshAgent.SetDestination (player.transform.position);
        } else {
            navMeshAgent.enabled = false;
        }
    }
}