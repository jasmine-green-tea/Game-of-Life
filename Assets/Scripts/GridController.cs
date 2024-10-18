using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    [Range(10, 100)]
    private int gridSize;
    [SerializeField]
    [Range(-30, -15)]
    private int randNumber;

    [SerializeField]
    private float updatePeriod;
    private float updateCounter = 0f;

    private bool[,] stateMatrix;

    private List<bool[,]> matrixList;
    private bool gameOver = false;
    private int generationCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        stateMatrix = new bool[gridSize, gridSize];
        matrixList = new List<bool[,]>();
        

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                int yCoord = Random.Range(randNumber, -7);
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
        if (gameOver)
            return;
        updateCounter += Time.deltaTime;
        if (updateCounter < updatePeriod)
            return;
        updateCounter = 0f;

        bool[,] tempStateMatrix;
        tempStateMatrix = new bool[gridSize, gridSize];

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tempStateMatrix[i, j] = GetNewStatus(CountNeighbours(i, j), stateMatrix[i, j]);
                if (!stateMatrix[i, j] && tempStateMatrix[i, j])
                    transform.GetChild(i * gridSize + j).Translate(new Vector3(0, 10, 0));
                else if (stateMatrix[i, j] && !tempStateMatrix[i, j])
                    transform.GetChild(i * gridSize + j).Translate(new Vector3(0, -10, 0));
            }
        }
        stateMatrix = tempStateMatrix;

        if (FindExistingMatrix())
            gameOver = true;

        matrixList.Add(stateMatrix);

        generationCount++;
    }

    private int CountNeighbours(int x, int z)
    {
        int neighbourCount = 0;

        if (x > 0)
        {
            if (z > 0)
            {
                if (stateMatrix[x - 1, z - 1])
                    neighbourCount++;
            }
            if (z < gridSize - 1)
            {
                if (stateMatrix[x - 1, z + 1])
                    neighbourCount++;
            }
            if (stateMatrix[x - 1, z])
                neighbourCount++;
        }

        if (x < gridSize - 1)
        {
            if (z > 0)
            {
                if (stateMatrix[x + 1, z - 1])
                    neighbourCount++;
            }
            if (z < gridSize - 1)
            {
                if (stateMatrix[x + 1, z + 1])
                    neighbourCount++;
            }
            if (stateMatrix[x + 1, z])
                neighbourCount++;
        }

        if (z > 0 && stateMatrix[x, z - 1])
            neighbourCount++;

        if (z < gridSize - 1 && stateMatrix[x, z + 1])
            neighbourCount++;

        return neighbourCount;
    }

    private bool GetNewStatus(int neighbourCount, bool oldStatus)
    {
        bool newStatus;
        if (oldStatus)
        {
            if (neighbourCount == 2 || neighbourCount == 3)
                newStatus = true;
            else
                newStatus = false;
        }
        else
        {
            if (neighbourCount == 3)
                newStatus = true;
            else
                newStatus = false;
        }

        return newStatus;
    }

    private bool FindExistingMatrix()
    {
        foreach (bool[,] m in matrixList)
            if (CompareMatrix(m, stateMatrix))
                return true;

        return false;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public int GetGenerationCount()
    {
        return generationCount;
    }

    private bool CompareMatrix(bool[,] l, bool[,] r)
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (l[i, j] != r[i, j])
                    return false;
            }
        }



        return true;
    }
}
