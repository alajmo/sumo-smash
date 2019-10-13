using System;
using System.Collections;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public int playerId = 0;
    public float score = 0;

    [Header("Movement")]
    public float movementSpeed = 6f;
    Vector3 movement;

    [Header("Mass")]
    public float growthSize = 0.05f;

    [Header("Collision")]
    public float collisionMultiplier = 400;
    public CapsuleCollider playerCollider;

    [Header("Dash")]
    public float dashDistance = 3;
    public float dashTimeDisabled = 1;
    public bool dashEnabled = true;

    [Header("Fire")]
    public float fireTimeDisabled = 1;
    public float fireTime = 0;
    public bool fireEnabled = true;
    public bool fireStarted = true;
    public System.Diagnostics.Stopwatch fireStopWatch = new System.Diagnostics.Stopwatch();

    [Header("Jump")]
    public float jumpHeight = 200;
    public float fallMultiplier = 10;
    public float jumpTimeDisabled = 0.5f;
    public bool jumpEnabled = true;

     [Header("Push")]
    public float pushLength = 1;
    public float pushMultiplier = 1000;
    public float pushTimeDisabled = 0.2f;
    public bool pushEnabled = true;
    public float pushRadius = 0.5f;
    public float pushDistance = 1f;

    [Header("Controller")]
    public string _VerticalAxisName;
    public string _HorizontalAxisName;
    public string _JumpName;
    public string _DashName;
    public string _PushName;
    public string _FireName;

    [Header("Layers")]
    public LayerMask groundLayers;
    public LayerMask playerLayer;

    [Header("Prefabs")]
    public GameObject grenadePrefab;

    [Header("Scripts")]
    private Rigidbody playerRigidBody;
    private Player playerControllers;
    private Animator playAnimator;
    // Events

    public static event Action<PlayerController> OnFallOff = delegate {};

    void Awake()
    {
        InitPlayerController(playerId);

        playerRigidBody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        playAnimator = GetComponent<Animator>();
        groundLayers = LayerMask.GetMask("Default");
    }

    void Start()
    {
        _VerticalAxisName = "Move Vertical";
        _HorizontalAxisName = "Move Horizontal";
        _JumpName = "Jump";
        _DashName = "Dash";
        _PushName = "Push";
        _FireName = "Fire";
    }

    void Update()
    {
        PlayerAction();

        if (playerRigidBody.transform.position.y < -12) {
            // Debug.Log("Player " + playerId + " fell of map");
            gameObject.SetActive(false);
            OnFallOff(this);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (playerRigidBody.velocity.y < -0.25)
        {
            //playerRigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //playerRigidBody.AddForce(Vector3.down * jumpHeight * -1f * Physics.gravity.y);
            //playerRigidBody.AddForce(Vector3.down * 30f);
            playerRigidBody.AddForce(Vector3.down * Mathf.Sqrt(jumpHeight/800 * -1f * Physics.gravity.y), ForceMode.Impulse);
        }

        Animating();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Vector3 position = transform.position;
        position.y = position.y + 0.9f;
        Gizmos.DrawRay(position, direction);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onCollide(collision);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food")) {
            eatFood(other);
        }
    }

    /**
    / Methods
    */

    public float timeHeld = 0;

    void PlayerAction()
    {
        bool jump = playerControllers.GetButtonDown(_JumpName);
        bool dash = playerControllers.GetButtonDown(_DashName);
        bool push = playerControllers.GetButtonDown(_PushName);
        bool fireDown = playerControllers.GetButtonDown(_FireName);
        bool fireUp = playerControllers.GetButtonUp(_FireName);

        if (jumpEnabled && jump && isGrounded())
        {
            playerJump();
        }

        if (dashEnabled && dash)
        {
            playerDash();
        }

        if (fireEnabled && fireDown)
        {
            fireStopWatch.Start();
            fireStarted = true;
        }

        if (fireEnabled && fireStarted && fireUp)
        {
            fireStarted = false;
            fireStopWatch.Stop();
            float ts = (float) fireStopWatch.ElapsedMilliseconds / (long) 1000;
            fireStopWatch.Reset();
            fireWeapon(ts);
        }

        if (pushEnabled && push)
        {
            pushPlayer();
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = playerControllers.GetAxisRaw(_HorizontalAxisName);
        float moveVertical = playerControllers.GetAxisRaw(_VerticalAxisName);

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(movement);
            Vector3 move = movement.normalized * movementSpeed * Time.deltaTime;
            playerRigidBody.MovePosition (transform.position + move);
        }
    }

    void Animating () {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = movement != Vector3.zero;
        if (walking) {
            playAnimator.SetInteger("condition", 1);
        } else {
            // playAnimator.SetInteger("condition", 0);
        }
        // playAnimator.SetBool ("IsWalking", walking);
    }

    void onCollide(Collision collision)
    {
        Vector3 direction = collision.contacts[0].point - transform.position;
        direction = -direction.normalized;
        direction = Vector3.Scale(direction, -playerRigidBody.velocity * playerRigidBody.mass);
        direction.y = 0;
        Rigidbody otherPlayer = collision.gameObject.GetComponent<Rigidbody>();

        float angle = Vector3.Angle(otherPlayer.transform.forward, direction);
        float forceX = (playerRigidBody.mass - otherPlayer.mass);

        int otherPlayerNumber = otherPlayer.GetComponent<PlayerController>().playerId;
        // Debug.Log("P: " + otherPlayerNumber + " Colliding with " + playerId);
        // Debug.Log("P: " + playerId + ", V: " + playerRigidBody.velocity + ", Colliding with: " + otherPlayerNumber + ", V: " + otherPlayer.velocity);
        otherPlayer.AddForce(direction * collisionMultiplier);
    }

    void playerDash()
    {
        dashEnabled = false;
        Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * playerRigidBody.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * playerRigidBody.drag + 1)) / -Time.deltaTime)));
        playerRigidBody.AddForce(dashVelocity, ForceMode.VelocityChange);
        StartCoroutine(EnableDash());
    }

    void playerJump()
    {
        jumpEnabled = false;
        playerRigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -1f * Physics.gravity.y), ForceMode.VelocityChange);
        // player.AddForce(Vector3.up * 7f, ForceMode.Impulse);
        StartCoroutine(EnableJump());
    }

    void fireWeapon(float fireTime)
    {
        fireEnabled = false;

        GrenadeLauncher grenadeLauncher = GetComponent<GrenadeLauncher>();
        Vector3 grenadePosition = transform.position + Vector3.Scale(Vector3.one, transform.forward) + new Vector3(0, 1f, 0);
        GameObject grenade = Instantiate(grenadePrefab, grenadePosition, Quaternion.identity) as GameObject;
        grenadeLauncher.fire(grenade, grenadePosition, transform.forward, fireTime);

        StartCoroutine(EnableFire());
    }

    void pushPlayer()
    {
        pushEnabled = false;

        Vector3 pos = transform.position;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        pos.y = pos.y + 1f;

        RaycastHit hitInfo;

        Ray ray = new Ray(pos, forward);
        // Physics.Raycast(ray, out hitInfo, 2)
        /* if (Physics.SphereCast(pos, 2, forward, out hitInfo, 2)) */
        if (Physics.SphereCast(pos, pushRadius, forward, out hitInfo, pushDistance))
        {
            Debug.DrawRay(pos, forward, Color.red, 10);
            Rigidbody otherPlayer = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            otherPlayer.AddForce(forward * pushMultiplier);
        }
        else
        {
            Debug.DrawRay(pos, forward, Color.green, 10);
        }

        playerRigidBody.AddForce(-forward * pushMultiplier / 2);
        StartCoroutine(EnablePush());
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y, playerCollider.bounds.center.z), playerCollider.radius * 0.9f, groundLayers);
    }

    void eatFood(Collider other)
    {
        // GameManager.instance.IncrementScore();
        // score += 1;
        // playerRigidBody.mass += 0.001f;
        //playerRigidBody.transform.localScale += new Vector3(growthSize, growthSize, growthSize);
    }

    public void InitPlayerController(int id) {
        playerId = id;
        playerControllers = ReInput.players.GetPlayer(playerId);
    }

    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(jumpTimeDisabled);
        jumpEnabled = true;
    }

    IEnumerator EnableDash()
    {
        yield return new WaitForSeconds(dashTimeDisabled);
        dashEnabled = true;
    }

    IEnumerator EnablePush()
    {
        yield return new WaitForSeconds(pushTimeDisabled);
        pushEnabled = true;
    }

    IEnumerator EnableFire()
    {
        yield return new WaitForSeconds(fireTimeDisabled);
        fireEnabled = true;
    }
}
