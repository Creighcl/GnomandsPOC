using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public Levels Id;
    public Backgrounds Background;
    public List<Wave> Waves;
    public float WaveIntervalSeconds;
    public int CastleMaxHp;
    public float MapDurationSec;
}
