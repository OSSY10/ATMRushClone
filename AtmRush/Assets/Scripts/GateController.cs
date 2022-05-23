using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField] TMP_Text gateNumberText = null;
    [SerializeField] enum GateType
    {
        PositiveGate,

        NegativeGate
    }

    [SerializeField] GateType gateType;
    [SerializeField] int gateNumber;
    

    public int GetGateNumber()
    {
        return gateNumber;
    }
    void Start()
    {
        RandomGateNumber();
    }
    
    void RandomGateNumber()
    {
        switch (gateType)
        {
            case GateType.PositiveGate : gateNumber = Random.Range(1, 10);
                gateNumberText.text = gateNumber.ToString();
                break;
            case GateType.NegativeGate : gateNumber = Random.Range(-1, -10);
                gateNumberText.text = gateNumber.ToString();
                break;
        } 
            
    }
}
