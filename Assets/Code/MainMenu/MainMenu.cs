using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public int gameSceneIndex; //loads new game scene
    public Animator anim; //reference to canvas animator

    public void PlayGame() {
        //hides and locks mouse to avoid repeated clicks
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //start fade out
        StartCoroutine(PlayGameCoroutine());
    }

    public void Quit() {
        Application.Quit();
    }

    //coroutine responsible for fade out of menu
    private IEnumerator PlayGameCoroutine() {
        anim.SetTrigger("StartGame"); //activates animation
        yield return null; //skips frame so that animator info is updated with new activated animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); //waits duration on fade out animation
        SceneManager.LoadScene(gameSceneIndex); //goes to scene
    }
}