using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        FinishCheckpoint.GameFinishedEvent += OnGameFinished;
    }

    void OnGameFinished()
    {
        Cursor.visible = true;

        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        FinishCheckpoint.GameFinishedEvent -= OnGameFinished;
    }
}
