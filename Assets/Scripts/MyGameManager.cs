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

    [Header("Colors")]
    public Color par;
    public Color impar;

    private void Start()
    {
        foreach (GameObject mine in mines)
        {
            minesList.Add(mine);
            Image buttonIMG = mine.GetComponent<Image>();
            buttonIMG.color = (minesList.IndexOf(mine) % 2 == 0) ? par : impar;
        }
        SetCollumLine(minesList);
    }

    public void StartGame(GameObject button)
    {
        if (!gameStarted)
        {
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
                    
                }
            }
            SelectMine(button);
            //InicialStage(button);
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
        Mine buttonMine = button.GetComponent<Mine>();
        foreach(GameObject mine in minesList)
        {
            Mine mineScript = mine.GetComponent<Mine>();
            if (mineScript.collumn == buttonMine.collumn + 1)
            {
                if (mineScript.line == buttonMine.line + 1) adjacentButtonsTemp.Add(mine);
                if (mineScript.line == buttonMine.line) adjacentButtonsTemp.Add(mine);
                if (mineScript.line == buttonMine.line - 1) adjacentButtonsTemp.Add(mine);
            }
            if (mineScript.collumn == buttonMine.collumn)
            {
                if (mineScript.line == buttonMine.line + 1) adjacentButtonsTemp.Add(mine);
                if (mineScript.line == buttonMine.line - 1) adjacentButtonsTemp.Add(mine);
            }
            if (mineScript.collumn == buttonMine.collumn - 1)
            {
                if (mineScript.line == buttonMine.line + 1) adjacentButtonsTemp.Add(mine);
                if (mineScript.line == buttonMine.line) adjacentButtonsTemp.Add(mine);
                if (mineScript.line == buttonMine.line - 1) adjacentButtonsTemp.Add(mine);
            }
            
        }

        if (adjacentButtons != null)
        {
            adjacentButtons.Clear();
            if(toOverwrite) adjacentButtons = adjacentButtonsTemp;
        }

        return adjacentButtonsTemp;
    }

    int MinesAround(GameObject button)
    {
        int numberOfMines = 0;
        List<GameObject> minesADJ = GetAdjacentButtons(button, false);
        foreach(GameObject mine in minesADJ)
        {
            Mine mineScript = mine.GetComponent<Mine>();
            if (mineScript.isMine)
            {
                numberOfMines++;
            }
        }

        return numberOfMines;
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
    
    public void InicialStage(GameObject button)
    {
        List<GameObject> buttonsADJ = GetAdjacentButtons(button, false);
        
        foreach(GameObject b in buttonsADJ)
        {
            Text mineText = b.GetComponentInChildren<Text>();
            mineText.text = MinesAround(b).ToString();
            if(MinesAround(b) == 0)
            {
                InicialStage(b);
            }
        }
    }

    public void SetCollumLine(List<GameObject> minesL)
    {
        foreach(GameObject mine in minesL)
        {
            int line = Mathf.CeilToInt(minesL.IndexOf(mine) / numberOfCollumns) + 1;
            int collumn = (minesL.IndexOf(mine) + 1) % numberOfCollumns;

            Mine mineScript = mine.GetComponent<Mine>();
            if (mineScript != null)
            {
                mineScript.collumn = collumn;
                mineScript.line = line;
            }
        }
    }
}
