using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 500.0f;
    Rigidbody rb;
    public bool hasPowerUp = false;
    float powerUpStrength = 100, powerUpDuration = 7;
    public GameObject powerUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Vertical");

        Vector3 direction = (transform.position - GameObject.Find("Main Camera").transform.position);
        direction.y = 0;
        direction = Vector3.Normalize(direction);
        
        rb.AddForce(direction * speed * Time.deltaTime * input);
        powerUpIndicator.transform.position = transform.position - new Vector3(0,0.5f,0);

        if(transform.position.y < -10)
        {
            Time.timeScale *= 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            powerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountDown());
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = Vector3.Normalize(other.transform.position - transform.position);
            enemyRB.AddForce(direction * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown(){
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
