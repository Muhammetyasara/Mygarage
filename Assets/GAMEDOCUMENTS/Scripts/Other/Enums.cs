using System;

[Serializable]
public class Enums
{
    public Name name;
    public Type type;
    
    public enum Name
    {
        Car,
        Washer1,
        Washer2,
        Lift1,
        Lift2,
        Lift3,
        Lift4,
        GasPump1,
        GasPump2,
        GasPump3,
        GasPump4,
        LiftAreaEntrance,
        WasherAreaEntrance,
        GasStationAreaEntrance
    }

    public enum Type
    {
        Washer,
        Lift,
        Gas
    }
}