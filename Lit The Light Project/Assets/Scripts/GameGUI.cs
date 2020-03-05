using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuWindow;
    [SerializeField] private GameObject doorText;
    [SerializeField] private GameObject keyImage;

    [SerializeField] private Transform coinArea;

    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private int coinOffset = 20;

    private List<GameObject> coins = new List<GameObject>();

    private float actualTimeScale;

    void Awake()
    {
        GameManager.onGameStarted.AddListener((progress) =>
        {
            if (progress.HasKey) DisplayKey();
            for (int i = 0; i < progress.COINs; i++) DisplayCoin();
        });
        GameManager.onDoorOpened.AddListener(ShowDoorText);
        GameManager.onLanternLited.AddListener(DisplayCoin);
        Crystal.onPickUp.AddListener(DisplayKey);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuWindow.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    private void ShowDoorText(bool furtherRoadExists)
    {
        doorText.SetActive(true);
        if (!furtherRoadExists)
        {
            LevelDoor.onWithPlayerCollided.AddListener(() =>
            {
                doorText.GetComponent<Text>().text = "There is no further road!";
            });
        }
    }

    public void Pause()
    {
        actualTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pauseMenuWindow.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = actualTimeScale;
        pauseMenuWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void DisplayKey()
    {
        keyImage.SetActive(true);
    }

    public void DisplayCoin()
    {
        int offset = coinOffset * coins.Count;
        coins.Add(Instantiate(coinPrefab, coinArea));
        coins[coins.Count - 1].transform.position += new Vector3(offset, 0, 0);
    }
}
