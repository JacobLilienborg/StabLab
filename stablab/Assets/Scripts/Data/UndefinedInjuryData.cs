using System;

[Serializable]
public class UndefinedInjuryData : InjuryData
{
    private const string MARKER_NAME = "Markers/UndefinedMarker";
    private const string ICON_NAME = "Icons/Undefined";

    private const string WEAPON_NAME = "Models/Undefined/undefinedModel";
    private const string WEAPON_PATH = "Models/Undefined";

    public UndefinedInjuryData(Guid id) : base(id)
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

    public UndefinedInjuryData(InjuryData injuryData) : base(injuryData)
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
        return "Undefined";
    }

}
