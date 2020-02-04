using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using System.Collections;
using Player;

namespace Menu
{
    public class PauseController: MonoBehaviour
    {

        public PostProcessingProfile Profile;
        public GameObject PauseMenu;
        public AudioSource MainMusicSource;
        public GameSettings Settings;

        private bool gamePaused = false;
        private GameObject[] chefs;

        public void Pause()
        {
            if (!gamePaused)
            {
                Profile.vignette.enabled = true;
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                MainMusicSource.Pause();
            }
            else
            {
                Profile.vignette.enabled = false;
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                MainMusicSource.Play();
            }
            gamePaused = !gamePaused;
        }

        public void Quit()
        {
            ChefBehaviour behaviour = chefs[0].GetComponent<ChefBehaviour>();
            behaviour.OnCancelPressed -= Pause;
            SceneManager.LoadScene("Menu");
        }

        public void Restart(){
            SceneManager.LoadScene(Settings.LevelName);
        }

        public void SubscribeToPlayer1()
        {
            chefs = GameObject.FindGameObjectsWithTag("Player");
            ChefBehaviour behaviour = chefs[0].GetComponent<ChefBehaviour>();
            behaviour.OnCancelPressed += Pause;
        }
    }

}
