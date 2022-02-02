using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private WorldSettings worldSettings;
    [SerializeField] private PlayerStats playerStats;

    private Rigidbody2D rb;
    public float maxSpeed = 5.8f;
    private float horizontalMov;

    private Vector2 gravity;

    public Transform actualLand;

    private bool isGrounded; //if the player is on a land or not
    private GameObject theLand; //the land where the player is in contact

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = Vector2.down;
    }

    private void Update()
    {
        //getting player input
        horizontalMov = Input.GetAxisRaw("Horizontal");
        
        //make the local 'y' axis aways point away from the sphere
        Vector2 pointUp = -gravity.normalized;
        transform.up = pointUp;


        //DEBUG MESSAGES
        Debug.Log(rb.velocity.x);
    }

    private void FixedUpdate()
    {
        Gravity();

        if (isGrounded)
        {
            WalkOnSphere();
            Jump();
        }

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Gravity()
    {
        if (actualLand != null)
        {
            gravity = actualLand.position - transform.position;
        }
        rb.AddForce(gravity * worldSettings.gravity * Time.deltaTime);
    }

    private void WalkOnSphere()
    {
        if(horizontalMov != 0)
        {
            rb.AddForce(transform.right * horizontalMov * playerStats.speed * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Horizontal") == 1) //gambiarra horrores
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetAxisRaw("Jump") > 0)
        {
            Vector2 invertedGravity = -gravity;
            rb.AddForce(invertedGravity * playerStats.jumpHeight * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Land"))
        {
            theLand = collision.gameObject;
            transform.SetParent(theLand.transform);
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Land"))
        {
            theLand = null;
            transform.parent = null;
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Detector")
        {
            actualLand = collision.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Detector")
        {
            actualLand = null;
        }
    }
}
