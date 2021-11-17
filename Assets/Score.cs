using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent < TMPro.TextMeshProUGUI>();
        GameController.OnCubeSpawned += GameController_OnCubeSpawned;

    }

    private void OnDestroy()
    {
        GameController.OnCubeSpawned -= GameController_OnCubeSpawned;
    }
    private void GameController_OnCubeSpawned()
    {
        score++;
        text.text = "Score: " + score;
    }
}


