using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float health = 100f;
    private bool readyToGoDown;
    public float downPower = 1f;
    public bool isGrounded = false;
    public float jumpPower = 8f;
    public float speed = 5f;
    private Rigidbody2D rb2d;
    public Canvas deathCanvas;
    //public CanvasGroup playerUICanvas;
    //public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        deathCanvas.GetComponent<Animation>().Play("sceneStartAnimation");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //healthSlider.value = health;

        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb2d.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            readyToGoDown = true;
        }
        if (Input.GetKeyDown("space") && readyToGoDown)
        {
            rb2d.AddForce(transform.up * -downPower, ForceMode2D.Impulse);
        }

        if (health <= 0)
        {
            PlayerDied();
        }
    }

    private void FixedUpdate()
    {
        float xmove = Input.GetAxisRaw("Horizontal");

        if (xmove >= 0.1f)
        {
            rb2d.velocity = new Vector2(xmove * speed, rb2d.velocity.y);
        }
        else if (xmove <= -0.1f)
        {
            rb2d.velocity = new Vector2(xmove * speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(xmove * 0.0f, rb2d.velocity.y);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDeath")
        {

            StartCoroutine(PlayerDied());
        }
    }

    public void GroundedOn()
    {
        isGrounded = true;
        readyToGoDown = false;
    }

    public void GroundedOff()
    {
        isGrounded = false;
        readyToGoDown = true;
    }

    IEnumerator PlayerDied()
    {
        deathCanvas.GetComponent<Animation>().Play("deathCanvasAnimation");
        yield return new WaitForSeconds(2);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
