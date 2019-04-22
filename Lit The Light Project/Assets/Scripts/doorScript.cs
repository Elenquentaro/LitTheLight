using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class doorScript : MonoBehaviour
{
    public Sprite spriteSecond, spriteSecret, spriteOpened, spriteEmpty;
    private SpriteRenderer currentSprite;
    public bool isOpened = false;
    public int LastSceneNumber = 2;
    public GameObject DoorText;
    private int currentLevel;

    void Awake()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.GetInt("Level") == currentLevel && PlayerPrefs.GetInt("COINCounter") == 3)
        {
            Opened();
        }
        else if (currentLevel == 1)
        {
            currentSprite.sprite = spriteSecond;
        }
        else
        {
            currentSprite.sprite = spriteSecret;
        }
    }

    public void Opened()
    {
        isOpened = true;
        if (PlayerPrefs.GetInt("LevelAccess") <= currentLevel)
            PlayerPrefs.SetInt("LevelAccess", currentLevel + 1);
        currentSprite.sprite = spriteOpened;
        if (currentLevel == LastSceneNumber)
        {
            currentSprite.sprite = spriteEmpty;
        }
        DoorText.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isOpened && collider.gameObject.tag == "Player")
        {
            if (currentLevel < LastSceneNumber)
                SceneManager.LoadScene(currentLevel + 1);
            else
            {
                DoorText.GetComponent<Text>().text = "There is no further road!";
                Time.timeScale = 0.5f;
                collider.GetComponent<PlayerScript>().isEnded = true;
            }
        }
    }

}
