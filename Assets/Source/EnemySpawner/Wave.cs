using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
  public int MeleeCount;
  public int RangeCount;

  public AttackType Melee;
  public AttackType Range;
}