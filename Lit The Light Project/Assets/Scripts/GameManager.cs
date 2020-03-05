using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GenericEvent<Progress> onGameStarted = new GenericEvent<Progress>();

    public static PositionEvent onAssignCheckpoint = new PositionEvent();

    public static BoolEvent onDoorOpened = new BoolEvent();

    public static EmptyEvent onLanternLited = new EmptyEvent();

    [SerializeField] private Transform[] checkPoints;

    private Progress currentProgress;

    private static Settings currentSettings;
    public static Settings CurrentSettings => currentSettings;

    private int currentLevel => SceneManager.GetActiveScene().buildIndex;

    void Start()
    {
        currentSettings = SaveLoader.LoadSettings();
        currentProgress = SaveLoader.LoadProgress();

        SubscribeToEvents();

        if (currentProgress.Level == currentLevel)
        {
            // for (int i = 0; i < currentProgress.COINs; i++) onLanternLited?.Invoke();
            if (currentProgress.COINs == 3) OpenLevelDoor();
        }
        else
        {
            OnNextLevelEnter();
        }

        onAssignCheckpoint?.Invoke(currentProgress.LastCheckpoint);

        onGameStarted?.Invoke(currentProgress);

        Debug.Log(currentProgress.COINs);
    }

    private void SubscribeToEvents()
    {
        Lantern.onWithPlayerCollided.AddListener((lantern) =>
        {
            if (currentProgress.HasKey && !currentProgress.LitedLanterns[lantern.Number])
            {
                currentProgress.LanternLit(lantern.Number);
                SaveLoader.SaveProgress(currentProgress);
                onLanternLited?.Invoke();
                lantern.PlayLited();

                if (currentProgress.COINs == 3) OpenLevelDoor();
            }
        });

        Crystal.onPickUp.AddListener(() =>
        {
            currentProgress.HasKey = true;
            SaveLoader.SaveProgress(currentProgress);
        });

        Checkpoint.onNewCheckPoint.AddListener(SetCheckpoint);

        LevelDoor.onWithPlayerCollided.AddListener(TryToGetNextLevel);
    }

    void OpenLevelDoor()
    {
        // if (currentProgress.Level == currentLevel) currentProgress.Level++;
        onDoorOpened?.Invoke(currentProgress.Level >= SceneManager.sceneCountInBuildSettings);
    }

    void TryToGetNextLevel()
    {
        if (currentProgress.COINs < 3) return;
        if (currentProgress.Level + 1 < SceneManager.sceneCountInBuildSettings)
        {
            currentProgress.Level++;
            SaveLoader.SaveProgress(currentProgress);
            SceneManager.LoadScene(currentProgress.Level);
            OnNextLevelEnter();
        }
        else
        {
            Time.timeScale = .5f;
            currentProgress.Level++;
            PlayerController.onPlayerDisappeared.AddListener(() => { SceneManager.LoadScene(0); });
        }
    }

    void SetCheckpoint(Vector3 point)
    {
        currentProgress.LastCheckpoint = point;
        SaveLoader.SaveProgress(currentProgress);

        onAssignCheckpoint?.Invoke(point);
    }

    void OnNextLevelEnter()
    {
        currentProgress = new Progress()
        {
            Level = currentLevel,
            LastCheckpoint = checkPoints[0].position
        };
        SaveLoader.SaveProgress(currentProgress);
    }
}
