using System.Collections.Generic;
using UnityEngine;


public class Contract : MonoBehaviour
{
    [SerializeField] private float[] requirementFactors = new float[4];

    private ContractUI contractUI;
    private string companyName;
    private int[] requirements = new int[4];
    private float buildTime;
    private string availableLetters = "ABCDEFGHIJKLMNOPQRSTUYVZ";
    private int completionReward;

    public int[] Requirements => requirements;
    public float BuildTime => buildTime;
    public int CompletionReward => completionReward;

    private void Awake()
    {
        contractUI = GetComponent<ContractUI>();
    }

    public void GenerateContract()
    {
        buildTime = 0f;

        companyName = $"COMPANY {availableLetters[Random.Range(0, availableLetters.Length)]}";

        for(int i = 0; i < requirements.Length; i++)
        {
            requirements[i] = (int)(Random.Range(0, 11) * requirementFactors[i]);
        }

        for(int i = 0; i < requirements.Length; i++)
        {
            float amount = Mathf.FloorToInt(requirementFactors[i] * 24f);
            if(requirements[i] > 0)
            {
                buildTime += amount;
            }
        }

        float randomTime = Random.Range(-4f, 4f);
        buildTime += randomTime;

        float hardness = Mathf.InverseLerp(0f, 64f, buildTime);
        completionReward = Mathf.RoundToInt(hardness * 100f);

        contractUI.SetContract(companyName, requirements, buildTime);
    }
}
