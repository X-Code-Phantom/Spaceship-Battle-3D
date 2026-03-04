using UnityEngine;

namespace SpaceshipBattle3D.Player
{
    /// <summary>
    /// Immutable designer-facing configuration for the Player.
    /// Assigned via Inspector. Never modified at runtime.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SpaceshipBattle3D/Player/Config")]
    public sealed class PlayerConfig : ScriptableObject
    { 
         private const float  SpeedMin = 0.1f;
         private const float  SpeedMax = 50f;

         private const float  FireRateMin = 0.1f;
         private const float  FireRateMax = 20f;
         
         private const int    LivesMin = 1;
         private const int    LivesMax = 99;

       

        [Header("Movement")]
        [Tooltip("Top speed in Unity units/second. Used to clamp Rigidbody velocity.")]
        [Range(SpeedMin, SpeedMax)]
        [SerializeField] private float _maxSpeed = 10f;
        

        [Header("Initial State")]
        [Tooltip("Number of lives assigned to the player at session start.")]
        [Range(LivesMin, LivesMax)]
        [SerializeField] private int _startLives = 3;
        

        [Header("Weapon")]
        [Tooltip("Projectiles spawned per second. FireCooldown is derived from this value.")]
        [Range(FireRateMin, FireRateMax)]
        [SerializeField] private float _fireRatePerSecond = 3f;

        
        
        public float  MaxSpeed           =>  _maxSpeed;
        public int    StartLives         =>  _startLives;
        public float  FireRatePerSecond  =>  _fireRatePerSecond;
        

        /// <summary>
        /// Seconds between shots. Derived from FireRatePerSecond.
        /// Pre-calculated so no consumer performs this math at runtime.
        /// </summary>
        public float FireCooldown => 1f / Mathf.Max ( FireRateMin, _fireRatePerSecond );
        

#if UNITY_EDITOR
        private void OnValidate()
        {
            
            if (_maxSpeed   <= 0f) Debug.LogError ($"[PlayerConfig] MaxSpeed is invalid after clamp. Asset: {name}", this);
            if (_startLives <= 0)  Debug.LogError ($"[PlayerConfig] StartLives is invalid after clamp. Asset: {name}", this);
            if (_fireRatePerSecond <= 0f) Debug.LogError ($"[PlayerConfig] FireRatePerSecond is invalid after clamp. Asset: {name}", this);
            

            _maxSpeed          = Mathf.Clamp(_maxSpeed, SpeedMin, SpeedMax);
            _startLives        = Mathf.Clamp(_startLives, LivesMin, LivesMax);
            _fireRatePerSecond = Mathf.Clamp(_fireRatePerSecond, FireRateMin, FireRateMax);
        }
#endif
    }
}