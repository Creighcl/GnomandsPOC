using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSceneManager : MonoBehaviour
{
    public delegate void FloatReturnDelegate(float a);
    public delegate void DoubleIntegerReturnDelegate(int a, int b);
    public delegate void NoParamDelegate();
    public DoubleIntegerReturnDelegate onCastleHealthChange;
    public FloatReturnDelegate onTimeLeftChange;
    public NoParamDelegate onCastleDestroyed;
    public NoParamDelegate onTimeHasRunOut;

    public static TurretSceneManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] int castleMaxHp = 100;
    [SerializeField] int castleHp = 100;
    [SerializeField] float mapDurationSec = 60f;
    [SerializeField] float timeLeft = 60f;

    private void Start()
    {
        timeLeft = mapDurationSec;
    }

    private void Update()
    {
        AdvanceCountdownClock();
    }

    private void AdvanceCountdownClock()
    {
        if (timeLeft == 0) return;

        timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0f, mapDurationSec);
        onTimeLeftChange?.Invoke(timeLeft);

        if (timeLeft == 0)
        {
            onTimeHasRunOut?.Invoke();
        }
    }

    public string GetFormattedTimeLeft()
    {
        return timeLeft.ToString("0.000");
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public int GetCastleMaxHp()
    {
        return castleMaxHp;
    }

    public int GetCastleHp()
    {
        return castleHp;
    }

    public string GetCastleHpPercentString()
    {
        return (castleHp * 100f / castleMaxHp).ToString("0.00");
    }
    
    public void CastleModifyHitPoints(int modification)
    {
        if (castleHp == 0) return;

        castleHp = Mathf.Clamp(castleHp + modification, 0, castleMaxHp);
        onCastleHealthChange?.Invoke(castleHp, castleMaxHp);

        if (castleHp == 0)
        {
            onCastleDestroyed?.Invoke();
        }
    }
}
