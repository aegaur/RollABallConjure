using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private const int NB_PICK_UP = 12;
    private const string COUNT_TEXT_BASE = "Count: ";
    private const string WIN_TEXT = "You win!";
    private const string LOST_TEXT = "You lost!";

    public static GameManager Instance {
        get;
        private set;
    }


    public Text countText;
    public Text globalMsgText;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        if (Instance == null)
        {
            Instance = this;
        }

        globalMsgText.text = "";
        countText.text = COUNT_TEXT_BASE + "0/" + NB_PICK_UP.ToString();
        GameObject playerObject = GameObject.Find("Player");
        System.Diagnostics.Debug.WriteLine(playerObject.name);
        player = playerObject.GetComponent<PlayerController>();
    }

    public void CheckGameState()
    {
        Debug.Log(player.IsDead);
        Debug.Log(player.Count);

        if (player.IsDead)
        {
            globalMsgText.text = LOST_TEXT;
            FreezeAndRestart();
        }
        else
        {
            countText.text = COUNT_TEXT_BASE + player.Count + "/" + NB_PICK_UP.ToString();

            if (player.Count >= NB_PICK_UP)
            {
                globalMsgText.text = WIN_TEXT;
                FreezeAndRestart();
            }
        }
    }

    private void FreezeAndRestart()
    {
        StartCoroutine(ResetAfterSeconds(5));
        Time.timeScale = 0f;
    }

    private IEnumerator ResetAfterSeconds(int seconds)

    {

        float pauseEndTime = Time.realtimeSinceStartup + seconds;

        while (Time.realtimeSinceStartup < pauseEndTime)

        {

            yield return null; // Attend un frame

        }

        ResetGame();

    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
}
