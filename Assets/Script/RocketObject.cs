using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class RocketObject : MonoBehaviour
{
    [SerializeField]public float rocketThrust = 2f;
    [SerializeField] public float rocketRotate = 2f;
    bool isDead = false;
    AudioSource audio;
    public Rigidbody rb;
    [SerializeField]ParticleSystem rocketExhaust;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            rocketControls();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Obstacle":
                Debug.Log("HIT");
                isDead = true;
                StartCoroutine(RelodGame());
                break;
            case "Finish":
                Debug.Log("Game End");
                StartCoroutine(NextGame());
                break;
            case "Respawn":
                Debug.Log("Reload");
                break;
        }
    }

    IEnumerator RelodGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SceneSomething");
    }

    private void rocketControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //rb.AddForce(0, thrust, 0, ForceMode.Impulse);
            rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);

            if (!rocketExhaust.isEmitting)
            {
                rocketExhaust.Play();

            }
            if (!audio.isPlaying)
            {
                audio.Play();
            }

            Debug.Log("Key press hold");
        }
        else
        {
            audio.Stop();
            rocketExhaust.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;
            rb.transform.Rotate(Vector3.right * rocketRotate * Time.deltaTime);
            rb.freezeRotation = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;
            rb.transform.Rotate(Vector3.left * rocketRotate * Time.deltaTime);
            rb.freezeRotation = false;
        }
    }


}
