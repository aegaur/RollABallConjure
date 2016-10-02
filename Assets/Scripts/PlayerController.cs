using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private int count;
    private bool isDead;



    public int Count
    {
        get { return count; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        isDead = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pick Up":
                other.gameObject.SetActive(false);
                count++;
                break;
            case "Hole":
                isDead = true;
                break;
        }

        GameManager.Instance.CheckGameState();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bumper"))
        {
            ContactPoint contactPoint = collision.contacts[0];
            rb.AddForce(contactPoint.normal * speed, ForceMode.Impulse);
        }
        switch (collision.gameObject.tag)
        {
            case "Bumper":
                ContactPoint contactPoint = collision.contacts[0];
                rb.AddForce(contactPoint.normal * speed, ForceMode.Impulse);
                break;
            case "MrBadGuy":
                isDead = true;
                GameManager.Instance.CheckGameState();
                break;
        }
    }
}
