using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCheckpoint : DoubleCheckpoint
{
    public delegate void GameFinished();
    public static GameFinished GameFinishedEvent;


    public override bool ValidCheckpoint
    {
        get => base.ValidCheckpoint;
        protected set
        {
            base.ValidCheckpoint = value;

            if (value == true)
                GameFinishedEvent?.Invoke();
        }
    }
}