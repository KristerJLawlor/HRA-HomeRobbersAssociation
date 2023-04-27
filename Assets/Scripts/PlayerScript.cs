using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject SpawnPoint;
    public GameObject StartPoint;

    public Text Score;
    public Text Timer;
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
        Score.text = "Score: 0";
        Timer.text = "Time: 120S";

        AudioScript = FindObjectOfType<GameAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeLeft <= 0)
        {
            //GameObject.Find("Lose").SetActive(true);
            this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);

            this.transform.position = SpawnPoint.transform.position;
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

        }
        if (other.gameObject.tag == "Window")
        {
            //Will end the game and show a new overlay depending on score
            if(PlayerScore >= 5)
            {
                
                this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                //GameObject.Find("Lose").SetActive(true);
                this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
            }

        }

        if (other.gameObject.tag == "Money")
        {
            PlayerScore++;
            Score.text = "Score: " + PlayerScore.ToString();

            Destroy(other.gameObject);

            AudioScript.PlayMoneyAudio();
        }

    }

    public IEnumerator Countdown()
    {
        while(TimeLeft > 0)
        {
            Timer.text = "Time: " + TimeLeft.ToString() + "S";
            --TimeLeft;
            yield return new WaitForSecondsRealtime(1.0f);
            
        }
        Timer.text = TimeLeft.ToString();
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
