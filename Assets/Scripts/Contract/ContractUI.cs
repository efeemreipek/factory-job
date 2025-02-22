using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContractUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI companyNameText;
    [SerializeField] private List<TextMeshProUGUI> requirementTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI buildTimeText;

    public void SetContract(string name, int[] requirement, float buildTime)
    {
        companyNameText.text = name;

        for(int i = 0; i < requirementTextList.Count; i++)
        {
            requirementTextList[i].text = requirement[i].ToString();
        }

        int minutes = Mathf.FloorToInt(buildTime / 60);
        int seconds = Mathf.FloorToInt(buildTime % 60);
        buildTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
