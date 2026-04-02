using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PhasesData", menuName = "Scriptable Objects/PhasesData")]
public class PhasesData : ScriptableObject
{
    public PhaseData[] phasesOne; //The optional phase ones
    public PhaseData phasesTwo;
    public PhaseData[] phasesThree; //The order off the boss fights

    [Serializable]
    public struct PhaseData
    {
        public Phase phase;
        public float phaseTime;
    }
}
