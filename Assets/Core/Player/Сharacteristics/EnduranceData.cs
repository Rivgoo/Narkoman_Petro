using UnityEngine;
using System;

namespace Core.Player.Characteristics
{
	[Serializable]
	public struct EnduranceData
	{
		[Header("Speeds Recovery")]
		public SpeedsRecovery SpeedsRecovery;
		[Header("Speeds Drop Rate")]
		public SpeedsDropRate SpeedsDropRate;
		[Header("Min Value")]
		public MinValue MinValue;
	}
	
	[Serializable]
	public struct SpeedsRecovery
	{
		[Header("Speeds")]
		[Range(0.0001f, 1)] public float Stay;
		[Range(0.0001f, 1)] public float Walk;
	}
	
	[Serializable]
	public struct SpeedsDropRate
	{
		[Header("Speeds")]
		[Range(0.0001f, 1)] public float Run;
		[Range(0.0001f, 100)] public float Jump;
	}
	
	[Serializable]
	public struct MinValue
	{
		[Header("Minimun Value")]
		[Range(0, 100)] public float Run;
		[Range(0, 100)] public float Jump;
		[Range(0, 100)] public float TakeObject;
	}

    [Serializable]
    public class ObjectEnduranceData
    {
        public float ValueDropRate;
    }
}