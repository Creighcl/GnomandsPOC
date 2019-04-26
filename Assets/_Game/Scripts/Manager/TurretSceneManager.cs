using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TurretSceneManager : Singleton<TurretSceneManager>
{
    private const string CASTLE_HP_PERCENTAGE_FORMAT = "0.00";
    private const string TIME_LEFT_FORMAT = "0.000";
    public delegate void FloatReturnDelegate(float a);
    public delegate void DoubleIntegerReturnDelegate(int a, int b);
    public delegate void NoParamDelegate();
    public delegate void LevelDelegate(Level level);
    public DoubleIntegerReturnDelegate OnCastleHealthChange;
    public FloatReturnDelegate OnTimeLeftChange;
    public NoParamDelegate OnCastleDestroyed;
    public NoParamDelegate OnPlayerVictory;
    public NoParamDelegate OnPlayerDefeat;
    public NoParamDelegate OnTimerExpire;
    public LevelDelegate OnLevelStart;
    public NoParamDelegate OnPlayerAttack;

    public Level Level;
    [SerializeField] private int _castleCurrentHitPoints = 100;
    [SerializeField] private float _timeLeft = 60f;
    private bool _levelComplete = false;
    private bool _levelStarted = false;
    private Coroutine _waveCoroutine;

    private void Start()
    {
        SetupTheLevel();
    }

    private void Update()
    {
        if (!_levelStarted) return;
        AdvanceCountdownClock();
        AdvanceGameState();
    }

    private void SetupTheLevel()
    {
        string selectedLevelName = PlayerPrefs.GetString(CustomPlayerPrefKeys.SelectedLevel.ToString());

        if (selectedLevelName == null || selectedLevelName == "")
        {
            selectedLevelName = Levels.WORLD_01_LEVEL_01.ToString();
        }
        
        Level = ResourceLoader.GetLevelByName(selectedLevelName);
        LevelStart();
    }

    private void LevelStart()
    {
        _timeLeft = Level.MapDurationSec;
        _castleCurrentHitPoints = Level.CastleMaxHp;
        _waveCoroutine = StartCoroutine(DoWaveLoop(Level));
        _levelStarted = true;
        _levelComplete = false;
        OnLevelStart?.Invoke(Level);
    }

    IEnumerator DoWaveLoop(Level level)
    {
        
        foreach(Wave wave in level.Waves)
        {
            wave.Start();
            yield return new WaitForSeconds(level.WaveIntervalSeconds);
        }
    }

    private void AdvanceGameState()
    {
        if (_levelComplete) return;

        if (IsPlayerVictorious())
        {
            _levelComplete = true;
            OnPlayerVictory?.Invoke();
            Debug.Log("Victory");
            return;
        }

        if (IsPlayerDefeated())
        {
            _levelComplete = true;
            OnPlayerDefeat?.Invoke();
            Debug.Log("DEFEAT" + (OnPlayerDefeat == null));
            return;
        }
    }

    protected bool IsPlayerVictorious()
    {
        return (_timeLeft == 0f && !EnemyUnitsExist());
    }

    protected bool IsPlayerDefeated()
    {
        return _castleCurrentHitPoints == 0;
    }

    private void HandleCastleDestroyed()
    {
        OnCastleDestroyed?.Invoke();
    }

    private void HandleTimeExpired()
    {
        OnTimerExpire?.Invoke();
    }

    private bool EnemyUnitsExist()
    {
        return GameObject.FindWithTag(CustomGameObjectTags.EnemyUnit.ToString()) != null;
    }

    private void AdvanceCountdownClock()
    {
        if (_timeLeft == 0) return;

        _timeLeft = Mathf.Clamp(_timeLeft - Time.deltaTime, 0f, Level.MapDurationSec);
        OnTimeLeftChange?.Invoke(_timeLeft);

        if (_timeLeft == 0)
        {
            HandleTimeExpired();
        }
    }

    public void DoPlayerAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    public string GetFormattedTimeLeft()
    {
        return _timeLeft.ToString(TIME_LEFT_FORMAT);
    }

    public float GetTimeLeft()
    {
        return _timeLeft;
    }

    public int GetCastleMaxHp()
    {
        if (Level == null) return 0;

        return Level.CastleMaxHp;
    }

    public int GetCastleHp()
    {
        if (Level == null) return 0;

        return _castleCurrentHitPoints;
    }

    public string GetCastleHpPercentString()
    {
        if (Level == null) return "";

        return (_castleCurrentHitPoints * 100f / Level.CastleMaxHp).ToString(CASTLE_HP_PERCENTAGE_FORMAT);
    }

    public void CastleModifyHitPoints(int modification)
    {
        if (_castleCurrentHitPoints == 0) return;

        _castleCurrentHitPoints = Mathf.Clamp(_castleCurrentHitPoints + modification, 0, Level.CastleMaxHp);
        OnCastleHealthChange?.Invoke(_castleCurrentHitPoints, Level.CastleMaxHp);

        if (_castleCurrentHitPoints == 0)
        {
            HandleCastleDestroyed();
        }
    }
}
