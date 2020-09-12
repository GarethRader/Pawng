using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int leftScore, rightScore;

    public static ScoreScript S;
    public Text Player1_Score;
    public Text Player2_Score;

    void Awake(){
        S = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        leftScore = rightScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scorer){
        if (scorer == 0){
            leftScore++;
            Player1_Score.text = leftScore.ToString();

        }
        if (scorer == 1){
            rightScore++;
            Player2_Score.text = rightScore.ToString();

        } 
        Debug.Log(leftScore + " : " + rightScore);

    }
    public void ResetScore(){
        leftScore = rightScore = 0;
    }
    
}
