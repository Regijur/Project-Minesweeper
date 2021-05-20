using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyGameManager : MonoBehaviour
{
    [Header("Components")]
    private GameObject[] mines;
    public List<GameObject> minesList;
    public bool gameStarted = false;
    public MineGenerator _mineGenerator;

    [Header("Timer")]
    public Timer timer;

    [Header("Mine Selection")]
    public GameObject buttonSelected;
    public List<GameObject> adjacentButtons;

    void Start()
    {
        mines = GameObject.FindGameObjectsWithTag("Mine");
        foreach (GameObject mine in mines) minesList.Add(mine);
    }

    public void StartGame(GameObject button)
    {
        if (!gameStarted)
        {
            if (timer != null) timer.SetTimerText();
            gameStarted = true;
            _mineGenerator.GenerateMines(mines);
        }
        else
        {
            SelectMine(button);
        }
    }

    public void SelectMine(GameObject buttonS)
    {
        if (buttonSelected != null)
        {
            buttonSelected.GetComponent<Image>().color = Color.white;
            foreach(GameObject b in adjacentButtons)
            {
                b.GetComponent<Image>().color = Color.white;
            } 
        }
        buttonSelected = buttonS;
        GetAdjacentButtons(buttonS);
        foreach (GameObject b in adjacentButtons)
        {
            Mine bomb = b.GetComponent<Mine>();
            if(bomb != null)
            {
                if(bomb.isMine) b.GetComponent<Image>().color = Color.yellow;
                else b.GetComponent<Image>().color = Color.blue;
            }
        }
        Mine isBomb = buttonSelected.GetComponent<Mine>();
        if (isBomb != null)
        {
            if (isBomb.isMine) buttonSelected.GetComponent<Image>().color = Color.red;
            else buttonSelected.GetComponent<Image>().color = Color.green;
        }
    }

    public void GetAdjacentButtons(GameObject button)
    {
        if (adjacentButtons != null) adjacentButtons.Clear();
        int indexOfSelectedButton = minesList.IndexOf(button);
        int rest = indexOfSelectedButton % 8;
        if(rest != 7)
        {
            if (indexOfSelectedButton <= 71) adjacentButtons.Add(minesList[indexOfSelectedButton + 9]);
            adjacentButtons.Add(minesList[indexOfSelectedButton + 1]);
            if (indexOfSelectedButton > 7) adjacentButtons.Add(minesList[indexOfSelectedButton - 7]);
        }
        if(rest != 0)
        {
            if (indexOfSelectedButton > 7) adjacentButtons.Add(minesList[indexOfSelectedButton - 9]);
            adjacentButtons.Add(minesList[indexOfSelectedButton - 1]);
            if (indexOfSelectedButton <= 71) adjacentButtons.Add(minesList[indexOfSelectedButton + 7]);
        }
        if(indexOfSelectedButton <= 71)  adjacentButtons.Add(minesList[indexOfSelectedButton + 8]);
        if (indexOfSelectedButton > 7) adjacentButtons.Add(minesList[indexOfSelectedButton - 8]);
    }
}
