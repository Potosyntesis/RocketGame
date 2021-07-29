using UnityEngine.SceneManagement;
using UnityEngine;

public class RocketObject : MonoBehaviour
{
    [SerializeField]public float rocketThrust = 2f;
    [SerializeField] public float rocketRotate = 2f;

    AudioSource audio;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rocketControls();

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Obstacle":
                Debug.Log("HIT");
                SceneManager.LoadScene("SampleScene");
                break;
            case "Finish":
                Debug.Log("Game End");
                break;
            case "Respawn":
                Debug.Log("Reload");
                break;
        }
    }

    private void rocketControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //rb.AddForce(0, thrust, 0, ForceMode.Impulse);
            rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);

            if (!audio.isPlaying)
            {
                audio.Play();
            }

            Debug.Log("Key press hold");
        }
        else
        {
            audio.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Rotate(Vector3.right * rocketRotate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Rotate(Vector3.left * rocketRotate * Time.deltaTime);
        }
    }


}
