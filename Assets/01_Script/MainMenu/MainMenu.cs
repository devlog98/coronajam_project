using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public int gameSceneIndex; //loads new game scene
    public Animator anim; //reference to canvas animator

    //is being used for every scene change
    public void PlayGame() {
        //hides and locks mouse to avoid repeated clicks
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;

        //start fade out
        StartCoroutine(PlayGameCoroutine(gameSceneIndex));
    }

    //loads scene
    public void LoadScene(int sceneIndex) {
        //hides and locks mouse to avoid repeated clicks
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;

        //start fade out
        StartCoroutine(PlayGameCoroutine(sceneIndex));
    }

    //coroutine responsible for fade out of menu
    private IEnumerator PlayGameCoroutine(int sceneIndex) {
        anim.SetTrigger("StartGame"); //activates animation
        yield return null; //skips frame so that animator info is updated with new activated animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); //waits duration on fade out animation

        MusicManager.instance.StopEvent();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneIndex); //goes to scene
    }

    public void PauseGame() {
        Pause.instance.TogglePause();
    }

    //increases menu scale in order to show onscreen
    public void ShowMenu(RectTransform rectTransform) {
        rectTransform.localScale = new Vector2(1, 1);
    }

    //decreases menu scale in order to hide onscreen
    public void HideMenu(RectTransform rectTransform) {
        rectTransform.localScale = new Vector2(0, 0);
    }

    public void Quit() {
        Application.Quit();
    }
}