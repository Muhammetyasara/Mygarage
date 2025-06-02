using System;
using PathCreation;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathCreators : MonoBehaviour
{
    [Header("LiftArea")]
    public PathCreator[] liftPaths;
    public PathCreator[] liftExitPaths;

    [Header("WasherArea")]
    public PathCreator[] washerPaths;
    public PathCreator[] washerExitPaths; 
    
    [Header("GasStationArea")]
    public PathCreator[] gasStationPaths;
    public PathCreator[] gasStationExitPaths;
}