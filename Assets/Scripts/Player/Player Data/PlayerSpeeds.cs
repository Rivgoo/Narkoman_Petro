using UnityEngine;

namespace Player
{
	[System.Serializable]
    [SerializeField]
    internal struct PlayerSpeeds
    {
    	[Header("Speeds")]
        [SerializeField] internal float Walk; // 3.5
        [SerializeField] internal float Run; // 5.5
        [SerializeField] internal float Jump; // 7.5
        [SerializeField] internal float Squatting; //2
    }
}

