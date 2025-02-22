using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorTimingManager : Singleton<ConveyorTimingManager>
{
    private float globalMoveTimer = 0f;
    private float moveInterval;
    private List<BaseMachine> registeredMachines = new List<BaseMachine>();

    public void Initialize(float defaultMoveInterval)
    {
        moveInterval = defaultMoveInterval;
    }

    private void Update()
    {
        globalMoveTimer += Time.deltaTime;
        if(globalMoveTimer >= moveInterval)
        {
            globalMoveTimer = 0f;
            TriggerMachineUpdates();
        }
    }

    private void TriggerMachineUpdates()
    {
        foreach(var machine in registeredMachines.ToList())
        {
            if(machine != null)
            {
                machine.OnGlobalTick();

                if((machine is ConveyorBelt || machine.NextMachine is ConveyorBelt) && machine.CurrentItem != null && machine.IsPlaced)
                {
                    AudioManager.Current.PlaySound(AudioClips.ItemMove_SFX, 0.05f);
                }
            }
        }
    }

    public void RegisterMachine(BaseMachine machine)
    {
        if(!registeredMachines.Contains(machine))
        {
            registeredMachines.Add(machine);
        }
    }
    public void UnregisterMachine(BaseMachine machine)
    {
        registeredMachines.Remove(machine);
    }
}
