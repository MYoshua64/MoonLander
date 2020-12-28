using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text winVelocityText;
    [SerializeField] private Text loseVelocityText;
    private Vector2 inputMovement;
    private Rigidbody2D rocketRB;
    private float currentVel;

    void Start()
    {
        rocketRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float upMovement = Input.GetAxisRaw("Vertical") * force;
        upMovement = Mathf.Max(0, upMovement);

        float torque = Input.GetAxisRaw("Horizontal");

        inputMovement = new Vector2(torque, upMovement);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rocketRB.AddForce(inputMovement.y * transform.up);
        rocketRB.AddTorque(-inputMovement.x);

        currentVel = rocketRB.velocity.magnitude;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (currentVel <= 2)
            {
                //successful landing, display win screen
                winPanel.gameObject.SetActive(true);
                winVelocityText.text = "Your velocity was: " + currentVel + "[m/s]";
            }
            else
            {
                losePanel.gameObject.SetActive(true);
                loseVelocityText.text = "Your velocity was: " + currentVel + "[m/s]";
            }

            Time.timeScale = 0;
        }
    }
}
