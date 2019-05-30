using System;

[Serializable]
public class UndefinedInjuryData : InjuryData
{
    private const string MARKER_NAME = "Markers/UndefinedMarker";
    private const string ICON_NAME = "Icons/Undefined";

    public override string DEFAULT_WEAPON_NAME { get { return "Models/Undefined/undefinedModel"; } }
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
            prefabName = injuryData.weaponData.prefabName,
            transformData = injuryData.weaponData.transformData
        };
    }

    public override string ToString()
    {
        return "Undefined";
    }

}
