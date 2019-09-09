using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public static event Action<Food> OnRemove = delegate {};
    PlayerHealth playerHealth;
    GameObject player;

    [Header("Attributes")]
    public float secondsUntilRemove = 5f;

    void Awake() {
        StartCoroutine(removeFood());
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    IEnumerator removeFood() {
        yield return new WaitForSeconds(secondsUntilRemove);
        OnRemove(this);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            //playerHealth.gainHealth(10);
            OnRemove(this);
            Destroy(this.gameObject);
        }
    }
}
