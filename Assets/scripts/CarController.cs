using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speedSensitivity = 15f;
    public float smoothSpeed = 0.125f;
    public GameObject maincamera;
    public GameObject[] redcars;
    public GameObject[] myCoin;
    public Vector3 offset = new Vector3(15f, 10f, -5f);
    public static int score = 0;
    float force = -50f;
    float x = 0f; 
    
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
    }

    void FixedUpdate()
    {
        float z = Input.GetAxis("Horizontal") * speedSensitivity * Time.deltaTime;

        // Mouvement vertical contrôlé par les touches du clavier
        x = Input.GetAxis("Vertical") * force;

        this.GetComponent<Rigidbody>().AddForce(x, 0, z * 100);
        this.transform.Rotate(new Vector3(0, z * 5, 0));

        if (Input.GetKeyDown(KeyCode.Space) && score > 5)
        {
            force = -100f;
            Invoke("resetNitro", 5f);
        }

        MoveRedCars(); 
    }

    public void resetNitro()
    {
        force = -50f;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = this.transform.position + offset;
        maincamera.transform.position = desiredPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "leftSide" || collision.gameObject.tag == "rightSide" || collision.gameObject.tag == "redCar")
        {
            score = (score == 0) ? 0 : score - 1;
            Debug.Log(score);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            score++;
            Debug.Log(score);
            Destroy(other.gameObject);
        }
    }

    void MoveRedCars()
    {
        redcars = GameObject.FindGameObjectsWithTag("redcar");
        foreach (GameObject redcar in redcars)
        {
            redcar.GetComponent<Rigidbody>().AddForce(-40, 0, 0); 
        }
    }
}
