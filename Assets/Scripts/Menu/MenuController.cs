using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuController: MonoBehaviour
    {
        public Animator MenuAnimator;
        public GameSettings Settings;

        void Start()
        {
            StartCoroutine("OpenCover");
            Time.timeScale = 1;
        }

        IEnumerator OpenCover()
        {
            yield return new WaitForSeconds(.2f);
            MenuAnimator.SetTrigger("Start");
        }


        public void ChangePage()
        {
            MenuAnimator.SetTrigger("Next");
        }

        // No se usa un enum porque Unity no pude usar la funcion
        // directamente en el OnClick del EventSystem...
        public void StartLevel(string level)
        {
            Settings.LevelName = level;
            SceneManager.LoadScene(level);
        }

        public void SetCoOp(bool multiplayer){
            Settings.CoOp = multiplayer; 
        }

        public void QuitGame(){
            Application.Quit();;
        }
    }
}
