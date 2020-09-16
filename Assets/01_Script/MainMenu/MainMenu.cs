using Locallies.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public int gameSceneIndex; //loads new game scene
    public Animator anim; //reference to canvas animator

    //is being used for every scene change
    public void PlayGame() {
        if (!LanguageSelector.instance.IsLoading) {
            //hides and locks mouse to avoid repeated clicks
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;

            //start fade out
            StartCoroutine(PlayGameCoroutine(gameSceneIndex));
        }
    }

    //coroutine responsible for fade out of menu
    private IEnumerator PlayGameCoroutine(int sceneIndex) {
        MusicManager.instance.StopEvent();

        anim.SetTrigger("StartGame"); //activates animation
        yield return null; //skips frame so that animator info is updated with new activated animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); //waits duration on fade out animation        

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneIndex); //goes to scene
    }

    public void PauseGame() {
        Pause.instance.TogglePause();
    }

    //increases menu scale in order to show onscreen
    public void ShowMenu(RectTransform rectTransform) {
        if (!LanguageSelector.instance.IsLoading) {
            rectTransform.localScale = new Vector2(1, 1);
        }
    }

    //decreases menu scale in order to hide onscreen
    public void HideMenu(RectTransform rectTransform) {
        if (!LanguageSelector.instance.IsLoading) {
            rectTransform.localScale = new Vector2(0, 0);
        }
    }

    public void Quit() {
        if (!LanguageSelector.instance.IsLoading) {
            Application.Quit();
        }
    }
}