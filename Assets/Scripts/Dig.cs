using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dig : MonoBehaviour
{
    public MyGameManager manager;
    public GameObject buttonSlctd;

    public void ToDig()
    {
        buttonSlctd = manager.buttonSelected;
        if (buttonSlctd.GetComponent<Mine>().isMine) buttonSlctd.GetComponent<Image>().color = Color.red;
        int minesAround = 0;
        foreach(GameObject button in manager.adjacentButtons)
        {
            if (button.GetComponent<Mine>().isMine)
            {
                minesAround++;
            }
        }
        buttonSlctd.GetComponentInChildren<Text>().text = minesAround.ToString();
    }
}
