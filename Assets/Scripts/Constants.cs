using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {

    #region ____PLAYER____
    public static const float Speed = 0.037f;
    public static const float StairSpeedModifier = 0.5f;
    public static const float KickingForce = 14f;
    #endregion


    #region ____WORKERS____
    public static const float IntervalToCheckAwesomeness = 10.0f;
    public static const float YouMustBeThisAwesome = 0.8f; // 0 - 1 inclusive
    public static const float IntervalToCheckBadBehaviour = 10.0f;
    public static const float YouMustBeThisBad = 0.8f; // 0 - 1 inclusive
    public static const float RotationSpeed = 3f;

    #endregion
    #region ____INCOME____

    public static const int INCOME_PER_GOOD_WORKSTATION = 1; // TODO IMPLEMENT IN GameManager.cs
    public static const int INCOME_PER_WORKSTATION = 0; // TODO IMPLEMENT IN GameManager.cs
    
    #endregion
    #region ____Misc____
    public static const float RiseSpeed = 0.2f;
    public static const float RangeForStartingTarp = 30f;
    

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
