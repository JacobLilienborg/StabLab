using System;

[Serializable]
public class CutInjuryData : InjuryData
{
    private const string MARKER_NAME = "Markers/CuthMarker";
    private const string ICON_NAME = "Markers/CutIcon";

    private const string WEAPON_NAME = "Models/Cut/cuthModel";
    private const string WEAPON_PATH = "Models/Cut";


    public CutInjuryData(Guid id) : base(id)
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

    public CutInjuryData(InjuryData injuryData) : base(injuryData)
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
        return "Cut";
    }
}
