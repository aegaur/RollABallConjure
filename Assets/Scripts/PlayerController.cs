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
        Debug.Log("PlayerStart");
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
        Debug.Log(other.gameObject.tag);
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
}
