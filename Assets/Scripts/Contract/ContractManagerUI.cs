using System.Collections.Generic;
using UnityEngine;


public class ContractManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject contractSelectPanel;

    public void SetActivePanel(bool cond)
    {
        contractSelectPanel.SetActive(cond);
    }
}
