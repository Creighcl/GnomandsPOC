using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSceneManager : MonoBehaviour
{
    public const string ENEMY_UNIT_TAG = "EnemyUnit";
    public delegate void FloatReturnDelegate(float a);
    public delegate void DoubleIntegerReturnDelegate(int a, int b);
    public delegate void NoParamDelegate();
    public DoubleIntegerReturnDelegate onCastleHealthChange;
    public FloatReturnDelegate onTimeLeftChange;
    public NoParamDelegate onCastleDestroyed;
    public NoParamDelegate onPlayerVictory;
    public NoParamDelegate onPlayerDefeat;

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
    bool mapComplete = false;

    private void Start()
    {
        timeLeft = mapDurationSec;
    }

    private void Update()
    {
        AdvanceCountdownClock();
        AdvanceGameState();
    }

    private void AdvanceGameState()
    {
        if (mapComplete) return;

        if (IsPlayerVictorious())
        {
            mapComplete = true;
            HandleVictory();
            return;
        }

        if (IsPlayerDefeated())
        {
            mapComplete = true;
            HandleDefeat();
            return;
        }
    }

    protected bool IsPlayerVictorious()
    {
        return (timeLeft == 0f && !EnemyUnitsExist());
    }

    protected void HandleVictory()
    {
        onPlayerVictory?.Invoke();
    }

    protected bool IsPlayerDefeated()
    {
        return castleHp == 0;
    }

    protected void HandleDefeat()
    {
        onPlayerDefeat?.Invoke();
    }

    private void HandleCastleDestroyed()
    {
        onCastleDestroyed?.Invoke();
    }

    private bool EnemyUnitsExist()
    {
        return GameObject.FindWithTag(ENEMY_UNIT_TAG) != null;
    }

    private void AdvanceCountdownClock()
    {
        if (timeLeft == 0) return;

        timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0f, mapDurationSec);
        onTimeLeftChange?.Invoke(timeLeft);
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
            HandleCastleDestroyed();
        }
    }
}
