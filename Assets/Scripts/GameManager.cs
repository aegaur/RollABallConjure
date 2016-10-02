using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private int NB_PICK_UP;
    private const string COUNT_TEXT_BASE = "Count: ";
    private const string DEATH_TEXT_BASE = "Death: ";
    private const string WIN_TEXT = "You win!";
    private const string LOST_TEXT = "You lost!";

    public static GameManager Instance {
        get;
        private set;
    }


    public Text countText;
    public Text deathText;
    public Text globalMsgText;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        if (Instance == null)
        {
            Instance = this;
        }

        GameObject pickUpsContainer = GameObject.Find("Pick Ups");
        NB_PICK_UP = pickUpsContainer.transform.childCount;

        globalMsgText.text = "";
        countText.text = COUNT_TEXT_BASE + "0/" + NB_PICK_UP.ToString();
        deathText.text = DEATH_TEXT_BASE + PersistantValues.Instance.GetValue("Death Count");


        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame(true);
        }
    }

    public void CheckGameState()
    {
        Debug.Log(player.IsDead);
        Debug.Log(player.Count);

        if (player.IsDead)
        {
            globalMsgText.text = LOST_TEXT;
            PersistantValues.Instance.SetValue("Death Count", PersistantValues.Instance.GetValue("Death Count") + 1);
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

        ResetGame(false);

    }

    private void ResetGame(bool resetPersistant)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;

        if (resetPersistant)
        {
            PersistantValues.Instance.ResetValues();
        }
    }
}
