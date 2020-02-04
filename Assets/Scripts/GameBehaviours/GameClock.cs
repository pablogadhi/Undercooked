using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameBehaviours
{
    public class GameClock : MonoBehaviour
    {

        public float timeInSeconds;
        public Text secondsDisplay;
        public Text minutesDisplay;

        private float seconds;
        private int minutes;
        private int lastRoundedSecond;

        void Start ()
        {
            seconds = timeInSeconds;
            minutes = (int) timeInSeconds / 60;
            lastRoundedSecond = 1;
        }

        void Update ()
        {
            seconds = seconds - Time.deltaTime;
            int secondsInMinute = (int) Mathf.Round(seconds) % 60;
            if (secondsInMinute > 9)
            {
                secondsDisplay.text = secondsInMinute.ToString();
            }
            else
            {
                secondsDisplay.text = "0" + secondsInMinute.ToString();
            }

            if (secondsInMinute == 59 && secondsInMinute != lastRoundedSecond)
            {
                minutes -= 1;
                minutesDisplay.text = "0" + minutes.ToString() + ":";
            }
            lastRoundedSecond = secondsInMinute;
            if(timeInSeconds <= 0){
                SceneManager.LoadScene("Menu");
            }

        }
    }
}
