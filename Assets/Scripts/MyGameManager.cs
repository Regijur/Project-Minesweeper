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
            dig.interactable = true;
            //Temporary
            CaveZeros(button);
            _mineGenerator.GenerateMines(mines);
            foreach (GameObject mine in minesList)
            {
                if (!mine.GetComponent<Mine>().already)
                {
                    Text buttonText = mine.GetComponentInChildren<Text>();
                    buttonText.text = minesList.IndexOf(mine).ToString();
                }
            }
            SelectMine(button);
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

    public List<GameObject> GetAdjacentButtons(GameObject button, bool toOverwrite = true)
    {
        List<GameObject> adjacentButtonsTemp = new List<GameObject>();

        int indexOfSelectedButton = minesList.IndexOf(button);
        int rest = indexOfSelectedButton % numberOfCollumns;
        if(rest != numberOfCollumns - 1)
        {
            if (indexOfSelectedButton <= minesList.Count - numberOfCollumns - 1) adjacentButtonsTemp.Add(minesList[indexOfSelectedButton + numberOfCollumns + 1]);
            adjacentButtonsTemp.Add(minesList[indexOfSelectedButton + 1]);
            if (indexOfSelectedButton > numberOfCollumns - 1) adjacentButtonsTemp.Add(minesList[indexOfSelectedButton - numberOfCollumns + 1]);
        }
        if(rest != 0)
        {
            if (indexOfSelectedButton > numberOfCollumns - 1) adjacentButtonsTemp.Add(minesList[indexOfSelectedButton - numberOfCollumns - 1]);
            adjacentButtonsTemp.Add(minesList[indexOfSelectedButton - 1]);
            if (indexOfSelectedButton <= minesList.Count - numberOfCollumns - 1) adjacentButtonsTemp.Add(minesList[indexOfSelectedButton + numberOfCollumns - 1]);
        }
        if(indexOfSelectedButton <= minesList.Count - numberOfCollumns - 1)  adjacentButtonsTemp.Add(minesList[indexOfSelectedButton + numberOfCollumns]);
        if (indexOfSelectedButton > numberOfCollumns - 1) adjacentButtonsTemp.Add(minesList[indexOfSelectedButton - numberOfCollumns]);

        if (adjacentButtons != null) 
        { 
            adjacentButtons.Clear();
            adjacentButtons = adjacentButtonsTemp;
        }

        return adjacentButtonsTemp;
    }

    public void CaveZeros(GameObject button)
    {
        List<GameObject> buttons = GetAdjacentButtons(button);
        buttons.Add(button);
        foreach(GameObject b in buttons)
        {
            Mine mine = b.GetComponent<Mine>();
            if(mine!= null) 
            {
                mine.already = true;
                b.GetComponentInChildren<Text>().text = "";
            }
        }
    }
    
}
