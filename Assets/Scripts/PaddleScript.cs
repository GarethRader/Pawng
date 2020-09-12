using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {
    private float yPos, xPos;
    public float  paddleSpeed = .05f;
    public float topWall, bottomWall, leftWall, rightWall;

    public KeyCode upArrow, downArrow, rightArrow, leftArrow;

    public float rotateVelocity = .5f;
    // Start is called before the first frame update
    void Start() {
        if(this.gameObject.name == "LeftPaddle1"){
            xPos = -5f;
            yPos = 3.5f;
        }
        if(this.gameObject.name == "LeftPaddle2"){
            xPos = -5f;
            yPos = -3.5f;
        }
        if(this.gameObject.name == "RightPaddle1"){
            xPos = 5f;
            yPos = 3.5f;
        }        
        if(this.gameObject.name =="RightPaddle2"){
            xPos = 5f;
            yPos = -3.5f;
        }
    }


    // Update is called once per frame
    void Update() {
        CheckInputMovement();
        CheckRotated();
    }

    
    void CheckInputMovement(){
        if (Input.GetKey(downArrow)) {
            if (yPos > bottomWall) {
                yPos -= paddleSpeed;
            }
        }

        if (Input.GetKey(upArrow)) {
            if (yPos < topWall) {
                yPos += paddleSpeed;
            }
        }
        if (Input.GetKey(leftArrow)){
            if(xPos > leftWall){
                xPos -= paddleSpeed;
            }
        }
        if (Input.GetKey(rightArrow)){
            if(xPos < rightWall){
                xPos += paddleSpeed;
            }
        }
        // to allow rotating paddle for different angles
        if(Input.GetKey(upArrow) && Input.GetKey(leftArrow) || Input.GetKey(downArrow) && Input.GetKey(leftArrow)){
            this.transform.Rotate(new Vector3 (0 ,0, rotateVelocity ));
            //Debug.Log("rotating");
            if(this.transform.rotation == Quaternion.Euler(0,0,180)){
                this.transform.Rotate(Vector3.zero);
            }
        }
        if(Input.GetKey(upArrow) && Input.GetKey(rightArrow)|| Input.GetKey(downArrow) && Input.GetKey(rightArrow)){
            this.transform.Rotate(new Vector3 (0, 0, -1 * rotateVelocity));
            if(this.transform.rotation == Quaternion.Euler(0,0,-180)){
                this.transform.Rotate(Vector3.zero);
            }
        }
        transform.localPosition = new Vector3(xPos, yPos, 0);
    }
    void CheckRotated(){
       // if(this.gameObject.transform.rotation.eulerAngles.y == 90){
       //     this.gameObject.transform.rotation.eulerAngles.y = 0;
       // }
    }
}

