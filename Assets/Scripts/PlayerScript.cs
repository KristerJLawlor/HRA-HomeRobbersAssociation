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

    public bool TimesUp = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = SpawnPoint.transform.position;
        Score.text = "Score: 0";
        Timer.text = "Time: 120S";
    }

    // Update is called once per frame
    void Update()
    {
        if(TimesUp)
        {
            GameObject.Find("Lose").SetActive(true);
            this.transform.position = SpawnPoint.transform.position;
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

        }
        if (other.gameObject.tag == "Window")
        {
            //Will end the game and show a new overlay depending on score
            if(PlayerScore >= 5)
            {
                GameObject.Find("Win").SetActive(true);
            }
            else
            {
                GameObject.Find("Poor").SetActive(true);
            }

        }

        if (other.gameObject.tag == "Money")
        {
            PlayerScore++;
            Score.text = "Score: " + PlayerScore.ToString();

            Destroy(other.gameObject);
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
}
