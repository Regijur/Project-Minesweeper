using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGenerator : MonoBehaviour
{
    [Header("Mine Generation")]
    public int numberOfMines;
    public int maxOfMines;
    [Range(1, 100)] public int probability;
    public void GenerateMines(GameObject[] mines)
    {
        probability = (probability <= 0) ? 1 : probability;

        foreach (GameObject mine in mines)
        {
            Mine mineScript = mine.GetComponent<Mine>();
            if (mineScript != null)
            {
                if (numberOfMines < maxOfMines && Random.Range(0, Mathf.CeilToInt(100 / probability)) == 0 && !mineScript.already)
                {
                    mineScript.isMine = true;
                    numberOfMines++;
                }
                else
                {
                    mineScript.isMine = false;
                }
            }
        }
    }
}
