using System;

namespace Creatures
{
    [Serializable]
    public class CharacterStats
    {
        public float movementSpeed = 8;
        public float jumpForce = 8;
        public float rotationSpeed = 6;
        public float gravity = -16f;

        public float maxHP;
        public float currentHP;

        public bool canRegenerate;

        /// in health points. every regen duration will be added this much health points
        public float regenerationAmount;


        public float attackDamage;

        /// boost in percent
        public float attackDamageBoost;

        public CharacterStats(float movementSpeed = 8, float jumpForce = 9, float rotationSpeed = 6,
            float gravity = -16f, float maxHp = 100f,
            float currentHp = 100f, bool canRegenerate = false, float regenerationAmount = 1f, float attackDamage = 10f,
            float attackDamageBoost = 0f)
        {
            this.movementSpeed = movementSpeed;
            this.jumpForce = jumpForce;
            this.rotationSpeed = rotationSpeed;
            this.gravity = gravity;
            maxHP = maxHp;
            currentHP = currentHp;
            this.canRegenerate = canRegenerate;
            this.regenerationAmount = regenerationAmount;
            this.attackDamage = attackDamage;
            this.attackDamageBoost = attackDamageBoost;
        }
    }

    [Serializable]
    public class PlayerStats : CharacterStats
    {
        public int doubleJumpAmount;
        private int _doubleJumpsLeft;

        public float maxMana;
        public float currentMana;

        public PlayerStats(int doubleJumpAmount = 0, float maxMana = 100, float currentMana = 100,
            float movementSpeed = 8, float jumpForce = 9, float rotationSpeed = 6, float gravity = -16,
            float maxHp = 100, float currentHp = 100, bool canRegenerate = false, float regenerationAmount = 1,
            float attackDamage = 10, float attackDamageBoost = 0) : base(movementSpeed, jumpForce, rotationSpeed,
            gravity, maxHp, currentHp, canRegenerate, regenerationAmount, attackDamage, attackDamageBoost)
        {
            this.doubleJumpAmount = doubleJumpAmount;
            _doubleJumpsLeft = doubleJumpAmount;
            this.maxMana = maxMana;
            this.currentMana = currentMana;
        }
    }
}