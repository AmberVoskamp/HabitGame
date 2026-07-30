using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PhasesData", menuName = "Scriptable Objects/PhasesData")]
public class PhasesData : ScriptableObject
{
    public PhaseData[] PhasesOne; //The optional phase ones
    public PhaseData[] PhasesTwoTutorial; //Phase2 variants with no chest
    public PhaseData[] PhasesTwoRegular;  //Phase2 variants with the chest
    public PhaseData[] PhasesThree; //The order off the boss fights

    [Serializable]
    public struct PhaseData
    {
        public Phase Phase;
        public float PhaseTime;
    }
}
