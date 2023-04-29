using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public GameObject SpawnPoint;
    public GameObject StartPoint;

    public Canvas Results;
    public TMP_Text ScoreText;
    public TMP_Text TimerText;

    public Canvas FinalResults;
    public TMP_Text FinalScore;



    //public Text Score;
    //public Text Timer;
    public float TimeLeft = 120.0f;
    public int PlayerScore = 0;

    public GameAudio AudioScript;

    public bool TimesUp = false;


    public bool canWalk = true;
    public Vector3 previousLoc;
    public float movDiff;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = SpawnPoint.transform.position;
        //Score.text = "Score: 0";
        //Timer.text = "Time: 120S";

        ScoreText.text = "Score: 0";
        TimerText.text = "Time: 120S";

        AudioScript = FindObjectOfType<GameAudio>();

        FinalResults.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeLeft <= 0 && TimesUp)
        {
            //GameObject.Find("Lose").SetActive(true);
            Results.transform.GetChild(0).gameObject.SetActive(true);

            this.transform.position = SpawnPoint.transform.position;

            FinalResults.transform.GetChild(0).gameObject.SetActive(true);
            FinalScore.text = "Score: " + PlayerScore.ToString();

            TimeLeft = 120.0f;

            AudioScript.PlayNightAudio();
        }


        //Check if the player is moving at all
        movDiff = ((transform.position - previousLoc).magnitude) / Time.deltaTime;
        previousLoc = transform.position;

        if(movDiff > 0.2f && canWalk)
        {
            canWalk = false;
            StartCoroutine(Walk());
            
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Door")
        {
            //Move player indoors (No returning outside)
            this.transform.position = StartPoint.transform.position;

            //Start the timer
            StartCoroutine(Countdown());

            AudioScript.PlayClockAudio();


            //Undo the results display
            Results.transform.GetChild(0).gameObject.SetActive(false);
            Results.transform.GetChild(1).gameObject.SetActive(false);
            Results.transform.GetChild(2).gameObject.SetActive(false);

            //Reset player score to 0
            PlayerScore = 0;

        }
        if (other.gameObject.tag == "Window")
        {
            //Will end the game and show a new UI depending on score
            if(PlayerScore >= 5)
            {
                
                Results.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                //GameObject.Find("Lose").SetActive(true);
                Results.transform.GetChild(2).gameObject.SetActive(true);
            }
            FinalResults.transform.GetChild(0).gameObject.SetActive(true);
            FinalScore.text = "Score: " + PlayerScore.ToString();

            this.transform.position = SpawnPoint.transform.position;

            TimeLeft = 120.0f;

            AudioScript.PlayNightAudio();

        }

        if (other.gameObject.tag == "Money")
        {
            PlayerScore++;
            //Score.text = "Score: " + PlayerScore.ToString();
            ScoreText.text = "Score: " + PlayerScore.ToString();

            Destroy(other.gameObject);

            AudioScript.PlayMoneyAudio();
        }

        if (other.gameObject.tag == "TestMoney")
        {

            AudioScript.PlayMoneyAudio();
        }

    }

    public IEnumerator Countdown()
    {
        while(TimeLeft > 0)
        {
            //Timer.text = "Time: " + TimeLeft.ToString() + "S";
            TimerText.text = "Time: " + TimeLeft.ToString() + "S";

            --TimeLeft;
            yield return new WaitForSecondsRealtime(1.0f);
            
        }
        //Timer.text = TimeLeft.ToString();
        TimesUp = true;
    }

    public IEnumerator Walk()
    {
        AudioScript.PlayWalkAudio();
        yield return new WaitForSecondsRealtime(0.7f);
        AudioScript.PlayWalkAudio();
        yield return new WaitForSeconds(0.7f);
        canWalk = true;
    }
}
