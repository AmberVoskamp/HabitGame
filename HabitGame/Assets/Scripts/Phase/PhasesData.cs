using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PhasesData", menuName = "Scriptable Objects/PhasesData")]
public class PhasesData : ScriptableObject
{
    public PhaseData[] PhasesOne; //The optional phase ones
    public PhaseData[] PhasesTwo;
    public PhaseData[] PhasesThree; //The order off the boss fights
    public PhaseData[] PhasesFour; //Spikes again
    
    public PhaseData[] PhasesTwoTest;

    [Serializable]
    public struct PhaseData
    {
        public Phase Phase;
        public float PhaseTime;
    }
}
