using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestRewardType
{
    Gold,
    XP
}

[System.Serializable]
public class QuestReward
{
    public static System.Action<QuestReward, Quest> OnGiveQuestReward;
    public static System.Action<QuestReward, QuestGoal> OnGiveQuestGoalReward;
    public static System.Action<QuestReward> OnGiveReward;

    public QuestRewardType m_questRewardType;

    public int m_amount;

    public bool m_isRewardGranted = false;

    public void GiveReward(Quest quest)
    {
        OnGiveQuestReward?.Invoke(this, quest);
        OnGiveReward?.Invoke(this);

        m_isRewardGranted = true;
    }

    public void GiveReward(QuestGoal questGoal)
    {
        OnGiveQuestGoalReward?.Invoke(this, questGoal);
        OnGiveReward?.Invoke(this);

        m_isRewardGranted = true;
    }
}
