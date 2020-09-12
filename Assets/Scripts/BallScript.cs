using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float ballSpeed;

    private int[] dirOptions =  {-1,1};
    private int hDir, vDir; 
    private bool isMoving = true;
    public AudioSource scoreSound;
    public AudioSource blip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rb
        StartCoroutine("Launch"); //StartCoroutine(Launch());
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > 10){
            rb.velocity = new Vector2(10, rb.velocity.y);
        }
        if(rb.velocity.y > 10){
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
        CheckIsMoving();
        if(!isMoving){
            rb.velocity = new Vector2(UnityEngine.Random.Range(1,10),UnityEngine.Random.Range(1,10));
            isMoving = true;
        }
    }

    private void CheckIsMoving(){
        //ball will always be moving no matter what
        if (rb.velocity == Vector2.zero){
            isMoving = false;
        }
    }
    //coroutine -- like a function, but has its timing outside of the main Unity loop

    public IEnumerator Launch(){
        
        yield return new WaitForSeconds(1.5f);
        // figure out directions
        // ball always seems to shoot right side with this code
        hDir = dirOptions[(UnityEngine.Random.Range(0,dirOptions.Length))];
        vDir = dirOptions[(UnityEngine.Random.Range(0,dirOptions.Length))];

        // add a horizontal force
        rb.AddForce(transform.right * ballSpeed * hDir); // transform.right = 1,0
        // add a vertical force
        rb.AddForce(transform.up * ballSpeed * vDir); // transform.up = 0,1
    }

    private void Reset(){
        transform.position = new Vector2(0,0);
        rb.velocity = Vector2.zero;
        StartCoroutine("Launch");
    }

    float hitFactor(Vector2 ballPos, Vector2 paddlePos){
        // used to find the area where the ball hit the paddle
        // to find angle to bounce off of
        // if ball hits top of paddle, it will angle up, likewise at the bottom...
        return(ballPos.y - paddlePos.y);
    }
    private void OnCollisionEnter2D(Collision2D other){
        // did we hit a wall
        // change pitch
        // keep pitch
        // play sound
        if(other.gameObject.tag == "Wall"){
            blip.pitch = .75f;
            blip.Play();
        }
        if(other.gameObject.tag == "LeftBounds"){ // if left player wins
            Debug.Log("Hit Left Bounds");
            ScoreScript.S.UpdateScore(0);
            Reset();
            scoreSound.Play();
        }
        if(other.gameObject.tag == "RightBounds"){ // if right player wins
            Debug.Log("Hit Right Bounds");
            ScoreScript.S.UpdateScore(1);
            Reset();
            scoreSound.Play();
        }
        if( other.gameObject.tag=="LeftPaddle"){ 
            // allows the ball to be angled more effectively when hit with paddle
            // ex: bounces towards top if hit at the top of the paddle and towards bottom if at the bottom of the paddle
            float y = hitFactor(this.transform.position, other.transform.position);
            // will always hit in the opposite direction of your goal
            Vector2 dir = new Vector2(1,y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * ballSpeed;
            blip.pitch= 1f;
            blip.Play();
        }
        if (other.gameObject.tag=="RightPaddle"){
            float y = hitFactor(this.transform.position, other.transform.position);
            // will always hit in the opposite direction of your goal
            Vector2 dir = new Vector2(-1,y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * ballSpeed;
            blip.pitch= 1f;
            blip.Play();
        }

    }
    // start the ball moving
    // if the ball goes out of bounds
    // which side?
    // award point
    // reset the ball back to the middle
}
