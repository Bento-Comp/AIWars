using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RessourceTypeEnum
{
    Gold,
    Gear
}

public class Quest_CollectRessources : QuestGoal
{

    [SerializeField]
    private RessourceTypeEnum m_ressourceTypeToTrack = RessourceTypeEnum.Gear;


    protected override void OnEnable()
    {
        base.OnEnable();
        Manager_Gold.OnGainGold += OnGainGold;
        PlayerGearBag.OnGainGear += OnGainGear;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Manager_Gold.OnGainGold -= OnGainGold;
        PlayerGearBag.OnGainGear -= OnGainGear;
    }


    private void OnGainGold(float gainedGold)
    {
        if (m_ressourceTypeToTrack != RessourceTypeEnum.Gold)
            return;

        IncreaseProgression((int)gainedGold);
    }


    private void OnGainGear(float gainedGear)
    {
        if (m_ressourceTypeToTrack != RessourceTypeEnum.Gear)
            return;


        IncreaseProgression((int)gainedGear);
    }



}
