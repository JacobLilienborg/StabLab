using System;


[Serializable]
public class ShotInjuryData : InjuryData
{
    private const string MARKER_NAME =  "Markers/ShotMarker";
    private const string ICON_NAME = "Icons/Shot";

    private const string WEAPON_NAME = "Models/Shot/shotModel";
    private const string WEAPON_PATH = "Models/Shot";

    public ShotInjuryData(Guid id) : base(id)
    {
        markerData = new MarkerData
        {
            prefabName = MARKER_NAME,
            iconName = ICON_NAME
        };

        weaponData = new WeaponData
        {
            resourcePath = WEAPON_PATH,
            prefabName = WEAPON_NAME
        };
    }

    public ShotInjuryData(InjuryData injuryData) : base(injuryData)
    {
        markerData = new MarkerData
        {
            prefabName = MARKER_NAME,
            iconName = ICON_NAME,
            transformData = injuryData.markerData.transformData
        };

        weaponData = new WeaponData
        {
            resourcePath = WEAPON_PATH,
            prefabName = WEAPON_NAME,
            transformData = injuryData.weaponData.transformData
        };
    }
   
    public override string ToString()
    {
        return "Shot";
    }
}
