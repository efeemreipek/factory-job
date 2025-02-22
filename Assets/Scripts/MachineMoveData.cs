using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "MachineMoveData", menuName = "Scriptable Objects/MachineMoveData")]
public class MachineMoveData : ScriptableObject
{
    public float MoveInterval = 1f;
    public float MoveDuration = 0.5f;
    public Ease MoveEase = Ease.InOutQuad;
}
