using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GridController gridController;

    [SerializeField]
    private GameObject gameOverScreen;

    private bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
            if (gridController.IsGameOver())
                SetGameOver();
    }

    private void SetGameOver()
    {
        gameEnd = true;
        gameOverScreen.SetActive(true);
        gameOverScreen.transform.GetChild(0).GetComponent<TMP_Text>().SetText("GAME OVER\n GENERATIONS: " + gridController.GetGenerationCount().ToString());

    }
}
