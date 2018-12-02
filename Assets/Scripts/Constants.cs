using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {

    #region ____PLAYER____
    public static float Speed = 0.037f;
    public static float StairSpeedModifier = 0.7f;
    public static float KickingForce = 14f;
    #endregion


    #region ____WORKERS____
    public static float SpawnedWorkerAwesomePercent = 1f;
    public static float IntervalToTryTurnWorkerBad = 10.0f;
    public static float LoseAwesomenessPercent = 0.2f; // Checked every IntervalToCheckAwesomeness 
    public static float IntervalToCheckBadBehaviour = 10.0f;
    public static float ChanceToDoBadStuff = 0.3f; // Show bad worker text every IntervalToCheckBadBehaviour 
    public static float RotationSpeed = 450f;

    #endregion
    #region ____INCOME____

    public static int INCOME_PER_GOOD_WORKSTATION = 1; // TODO IMPLEMENT IN GameManager.cs
    public static int INCOME_PER_WORKSTATION = 0; // TODO IMPLEMENT IN GameManager.cs
    
    #endregion
    #region ____Misc____
    public static float RiseSpeed = 0.05f;
    public static float RangeForStartingTarp = 30f;
    

    #endregion

}

public static class Tags
{
    public static string Player = "Player";
    public static string Stairs = "Stairs";
    public static string WalkUpArea = "WalkUpArea";
    public static string WalkDownArea = "WalkDownArea";
    public static string Arbetsplats = "Arbetsplats";
    public static string PlaceToGetWorkers = "PlaceToGetWorkers";
    public static string LeftWall = "LeftWall";
    public static string RightWall = "RightWall";
    public static string Vaning = "Vaning";
    public static string FallingWorker = "FallingWorker";
}
