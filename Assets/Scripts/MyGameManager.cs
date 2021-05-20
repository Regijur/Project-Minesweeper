using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyGameManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] mines;
    [HideInInspector]public List<GameObject> minesList;
    public bool gameStarted = false;
    public MineGenerator _mineGenerator;
    public Button dig;

    [Header("Timer")]
    public Timer timer;

    [Header("Mine Selection")]
    public GameObject buttonSelected;
    public List<GameObject> adjacentButtons;
    public int numberOfCollumns;


    public void StartGame(GameObject button)
    {
        if (!gameStarted)
        {
            foreach (GameObject mine in mines) minesList.Add(mine);
            if (timer != null) timer.SetTimerText();
            gameStarted = true;
            _mineGenerator.GenerateMines(mines);
            dig.interactable = true;
            //Temporary
            foreach (GameObject mine in minesList)
            {
                Text buttonText = mine.GetComponentInChildren<Text>();
                buttonText.text = minesList.IndexOf(mine).ToString();
            }
        }
        else
        {
            SelectMine(button);
        }
    }

    public void SelectMine(GameObject buttonS)
    {
        buttonSelected = buttonS;
        GetAdjacentButtons(buttonS);
    }

    public void GetAdjacentButtons(GameObject button)
    {
        if (adjacentButtons != null) adjacentButtons.Clear();
        int indexOfSelectedButton = minesList.IndexOf(button);
        int rest = indexOfSelectedButton % numberOfCollumns;
        if(rest != numberOfCollumns - 1)
        {
            if (indexOfSelectedButton <= minesList.Count - numberOfCollumns - 1) adjacentButtons.Add(minesList[indexOfSelectedButton + numberOfCollumns + 1]);
            adjacentButtons.Add(minesList[indexOfSelectedButton + 1]);
            if (indexOfSelectedButton > numberOfCollumns - 1) adjacentButtons.Add(minesList[indexOfSelectedButton - numberOfCollumns + 1]);
        }
        if(rest != 0)
        {
            if (indexOfSelectedButton > numberOfCollumns - 1) adjacentButtons.Add(minesList[indexOfSelectedButton - numberOfCollumns - 1]);
            adjacentButtons.Add(minesList[indexOfSelectedButton - 1]);
            if (indexOfSelectedButton <= minesList.Count - numberOfCollumns - 1) adjacentButtons.Add(minesList[indexOfSelectedButton + numberOfCollumns - 1]);
        }
        if(indexOfSelectedButton <= minesList.Count - numberOfCollumns - 1)  adjacentButtons.Add(minesList[indexOfSelectedButton + numberOfCollumns]);
        if (indexOfSelectedButton > numberOfCollumns - 1) adjacentButtons.Add(minesList[indexOfSelectedButton - numberOfCollumns]);
    }
}
