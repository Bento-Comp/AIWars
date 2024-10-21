using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework.SimpleGame
{
	[DefaultExecutionOrder(-32000)]
	[ExecuteInEditMode()]
	[AddComponentMenu("GameFramework/SimpleGame/LevelManager")]
	public class LevelManager : GameBehaviour 
	{
		[System.Serializable]
		public class Editor_LaunchParameters
		{
			public bool loopLevelStart = false;

			public bool forceLevelStart = false;

			public int forceLevelStart_Index = 1;

			public void CopyFrom(Editor_LaunchParameters parametersToCopy)
			{
				loopLevelStart = parametersToCopy.loopLevelStart;

				forceLevelStart = parametersToCopy.forceLevelStart;

				forceLevelStart_Index = parametersToCopy.forceLevelStart_Index;
			}
		}

		[System.Serializable]
		public class LevelIndicesMapping
		{
			[Header("Initial Sequence")]
			public int shuffledIndexSeed = 0;

			public List<int> levelsToSkip = new List<int>();

			[Header("End Loops")]
			public bool useEndLoops = false;

			public int endLoopsCount = 10;

			public int lastUnloopedLevel = 20;

			public List<int> notReplayableLevels = new List<int>();

			[Header("Generated")]

			[SerializeField]
			List<int> initialSequence;

			[SerializeField]
			UniUtilities.ShuffledIntMapping endLoops;

			public void CopyFrom(LevelIndicesMapping parametersToCopy)
			{
				shuffledIndexSeed = parametersToCopy.shuffledIndexSeed;

				levelsToSkip = new List<int>(parametersToCopy.levelsToSkip);

				useEndLoops = parametersToCopy.useEndLoops;

				endLoopsCount = parametersToCopy.endLoopsCount;

				lastUnloopedLevel = parametersToCopy.lastUnloopedLevel;

				notReplayableLevels = new List<int>(parametersToCopy.notReplayableLevels);
			}

			public void Initialize()
			{
				// Create initial sequence
				int initialSequenceMaxIndex;
				if(useEndLoops)
				{
					initialSequenceMaxIndex = lastUnloopedLevel;
				}
				else
				{
					int maxSkippedIndex = 0;
					foreach(int skippedIndex in levelsToSkip)
					{
						if(skippedIndex > maxSkippedIndex)
						{
							maxSkippedIndex = skippedIndex;
						}
					}
					initialSequenceMaxIndex = maxSkippedIndex;
				}

				initialSequence = new List<int>();
				for(int i = 1; i <= initialSequenceMaxIndex; ++i)
				{
					if(levelsToSkip.Contains(i))
						continue;

					initialSequence.Add(i);
				}

				// Enumerate the indices we have to loop (replayable levels of the initial sequence)
				if(useEndLoops)
				{
					List<int> replayableLevelIndices = new List<int>();
					foreach(int i in initialSequence)
					{
						if(notReplayableLevels.Contains(i))
							continue;

						replayableLevelIndices.Add(i);
					}

					Random.InitState(shuffledIndexSeed);

					endLoops =
						new UniUtilities.ShuffledIntMapping(endLoopsCount, replayableLevelIndices);

					LevelManager.ClearRandomSeed();
				}
				else
				{
					if(endLoops != null)
						endLoops.Clear();
				}
			}

			public int GetMappedIndex(int index)
			{
				if(index < 1)
					return 0;

				if(index <= initialSequence.Count)
				{
					return initialSequence[index-1];
				}
				else if(useEndLoops)
				{
					return endLoops.GetMappedIndex(index, initialSequence.Count + 1);
				}
				else
				{
					return index + levelsToSkip.Count;
				}
			}
		}

		static public System.Action onUseLevelingChange;

		static public System.Action onLevelChange;

		public bool leveling = false;

		public bool restartLevelIfFailed = true;

		public LevelIndicesMapping levelIndicesMapping;

		[Header("Editor")]

		public Editor_LaunchParameters editor_launchParameters;

		static LevelManager instance;

#if UNITY_EDITOR
		static bool firstLevelStart = true;

		static int firstLevelStartIndexSave;

		bool levelingLastFrame;
#endif

		static public int LevelIndex_LoopedAndSkipped
		{
			get
			{
				if(instance == null)
				{
					return LevelIndex_RawAndContinuous;
				}
				else
				{
					return instance.ComputeLevelIndex_LoopedAndSkipped(LevelIndex_RawAndContinuous);
				}
			}
		}

		static string levelIndex_saveKey = "SimpleGame_Level";

		protected virtual string LevelIndexSaveKey
		{
			get
			{
				return levelIndex_saveKey;
			}
		}

		static public int LevelIndex_RawAndContinuous
		{
			get
			{
				#if UNITY_EDITOR
				if(Application.isPlaying == false)
				{
					if(instance != null)
					{
						if(instance.editor_launchParameters.forceLevelStart)
							return instance.editor_launchParameters.forceLevelStart_Index;
					}
				}
				#endif

				if(instance == null)
					return 0;

				return PlayerPrefs.GetInt(instance.LevelIndexSaveKey, 1);
			}

			set
			{
				if(instance == null)
					return;

				PlayerPrefs.SetInt(instance.LevelIndexSaveKey, value);

				OnLevelChange();
			}
		}

		static public bool UseLeveling
		{
			get
			{
				if(instance == null)
					return false;

				return instance.leveling;
			}
		}

		static public LevelManager Instance
		{
			get
			{
				return instance;
			}
		}

		// Call this method on child class if you change the way
		// the Level Index is evaluated
		protected void NotifyLevelIndexChange()
		{
			OnLevelChange();
		}

		static public void SetRandomSeedToLevelIndex_LoopedAndSkipped(int level1_seed)
		{
			Random.InitState(level1_seed + LevelIndex_LoopedAndSkipped);
		}

		static public void ClearRandomSeed()
		{
			Random.InitState(System.Environment.TickCount);
		}

#if UNITY_EDITOR
		protected override void OnLoadGameEnd(bool reloadSceneAfter)
		{
			base.OnLoadGameEnd(reloadSceneAfter);
		
			Debug.Log("LevelManager : LoadGameEnd");
			if(editor_launchParameters.loopLevelStart)
			{
				LevelIndex_RawAndContinuous = firstLevelStartIndexSave;
				Debug.Log("LevelManager : LoadGameEnd : Level = " + LevelIndex_RawAndContinuous);
			}
		}
#endif

		protected override void OnAwake()
		{
			if(instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogWarning("A singleton can only be instantiated once!");
				Destroy(gameObject);
				return;
			}

#if UNITY_EDITOR
			if(firstLevelStart)
			{
				firstLevelStart = false;

				if(editor_launchParameters.forceLevelStart)
				{
					LevelIndex_RawAndContinuous = editor_launchParameters.forceLevelStart_Index;
				}

				firstLevelStartIndexSave = LevelIndex_RawAndContinuous;
			}
#endif

			Game.onLevelCompletedEnd += OnLevelCompletedEnd;
		}

		protected override void OnStart()
		{
			OnUseLevelingChange();
			OnLevelChange();
		}

		protected override void OnAwakeEnd()
		{
			if(instance == this)
			{
				instance = null;
			}

			Game.onLevelCompletedEnd -= OnLevelCompletedEnd;
		}

#if UNITY_EDITOR
		void LateUpdate()
		{
			if(Application.isPlaying)
				return;

			instance = this;

			if(editor_launchParameters.forceLevelStart)
			{
				LevelIndex_RawAndContinuous = editor_launchParameters.forceLevelStart_Index;
			}

			levelIndicesMapping.Initialize();

			if(levelingLastFrame != leveling)
			{
				levelingLastFrame = leveling;
				OnUseLevelingChange();
			}
		}
#endif

		void OnLevelCompletedEnd(bool success)
		{
			if(success || restartLevelIfFailed == false)
				++LevelIndex_RawAndContinuous;
		}

		static void OnUseLevelingChange()
		{
			onUseLevelingChange?.Invoke();
		}

		static void OnLevelChange()
		{
			onLevelChange?.Invoke();
		}

		int ComputeLevelIndex_LoopedAndSkipped(int levelRaw)
		{
			return levelIndicesMapping.GetMappedIndex(levelRaw);
		}
	}
}
