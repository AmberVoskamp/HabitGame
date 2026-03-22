using UnityEngine;

[CreateAssetMenu(fileName = "PhasesData", menuName = "Scriptable Objects/PhasesData")]
public class PhasesData : ScriptableObject
{
    public Phase[] phasesOne; //The optional phase ones
    public Phase phasesTwo;
    public Phase[] phasesThree; //The order off the boss fights
}
