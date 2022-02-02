using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{

    [SerializeField] private WorldSettings worldSettings;
    [SerializeField] private PlayerStats playerStats;

    private CharacterController controller;
    public float maxSpeed = 5.8f;
    private float horizontalMov;

    private Vector2 gravity;

    public Transform actualLand;

    private bool isGrounded; //if the player is on a land or not
    private GameObject theLand; //the land where the player is in contact

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        gravity = Vector2.down;
    }

    private void Update()
    {
        //getting player input
        horizontalMov = Input.GetAxisRaw("Horizontal");

        //make the local 'y' axis aways point away from the sphere
        Vector2 pointUp = -gravity.normalized;
        transform.up = pointUp;

        if (isGrounded)
        {
            WalkOnSphere();
        }
        else
        {
            Gravity();
        }

        //DEBUG MESSAGES
        Debug.Log(isGrounded);
    }

    private void Gravity()
    {
        if(actualLand != null)
        {
            gravity = actualLand.position - transform.position;
        }
        Vector3 moveDown = new Vector3(gravity.x, gravity.y, 0);
        controller.Move(moveDown.normalized * worldSettings.gravity * Time.deltaTime);
    }

    private void WalkOnSphere()
    {
        if(horizontalMov != 0)
        {
            controller.Move(new Vector2(horizontalMov * playerStats.speed * Time.deltaTime, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Land"))
        {
            theLand = collision.gameObject;
            transform.SetParent(theLand.transform); //maybe remove this part
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
