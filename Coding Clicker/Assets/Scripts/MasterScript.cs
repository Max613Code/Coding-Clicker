using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{

    private void Start()
    {
        UIHandler.UpdateMoney(LevelHandler.money);
    }

    public void ComputerClick()
    {
        ComputerHandler.ComputerClicked();
    }

}
