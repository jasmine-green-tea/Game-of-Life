using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private uint gridSize;

    [SerializeField]
    private float updatePeriod;
    private float updateCounter = 0f;

    private bool[,] stateMatrix;

    // Start is called before the first frame update
    void Start()
    {
        stateMatrix = new bool[gridSize, gridSize];

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                int yCoord = Random.Range(-30, -7);
                if (yCoord == -8)
                    yCoord = 0;
                else
                    yCoord = -10;
                GameObject newCube = Instantiate(prefab, transform, false);

                newCube.transform.Translate(new Vector3(i, yCoord, j));


                if (yCoord == 0)
                    stateMatrix[i, j] = true;
                else
                    stateMatrix[i, j] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateCounter += Time.deltaTime;
        if (updateCounter < updatePeriod)
            return;
        updateCounter = 0f;
        Debug.LogError("tick");

        // проходим по мертвым клеткам
        // считаем кол-во соседей каждой мертвой клетки
        // меняем статус клетки
        // меняем координату кубика (поднимаем) или оставляем

        // проходим по живым клеткам
        // считаем соседей
        // меняем статус
        // меняем коорд (опускаем) или оставляем
    }

    private int CountNeighbours(int x, int z)
    {
        return new int();
    }

    private bool GetNewStatus(int neighbourCount)
    {
        return new bool();
    }

}
