using System;
using UnityEngine;
using Creatures;

public class BaseLifeHandler : MonoBehaviour
{
    public CharacterStats stats;
    public Action OnCreatureDeath;

    /// How much seconds to wait before start regenerating
    public float regenerationDelay = 5f;

    private float _regenDelayLeft;
    private bool _regenActive;

    /// how much seconds to wait between each regeneration cycle
    private float _regenCycleDelay = 1f;

    private float _nextRegenCycle;

    // Start is called before the first frame update
    void Start()
    {
        _regenDelayLeft = regenerationDelay;
        _regenActive = true;

        _nextRegenCycle = _regenCycleDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_regenActive)
        {
            _regenDelayLeft -= Time.deltaTime;
        }
        else
        {
            // regen cycle
            _nextRegenCycle -= Time.deltaTime;
            if (_nextRegenCycle <= 0)
            {
                _regenDelayLeft = regenerationDelay;
                UpdateHp(stats.regenerationAmount);
            }
        }

        if (_regenDelayLeft <= 0)
        {
            _regenActive = true;
            _regenDelayLeft = regenerationDelay;
        }
    }

    public void ReceiveDamage(float amount)
    {
        UpdateHp(amount);
        _regenActive = false;
        _nextRegenCycle = _regenCycleDelay;
    }

    /// <summary>
    /// Adds [amount] to the creature's health.
    /// </summary>
    /// <param name="amount">The amount of HP to add. May be positive or negative.</param>
    private void UpdateHp(float amount)
    {
        stats.currentHP = Math.Clamp(stats.currentHP + amount, 0, stats.maxHP);
        if (stats.currentHP == 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
        OnCreatureDeath.Invoke();
    }
}