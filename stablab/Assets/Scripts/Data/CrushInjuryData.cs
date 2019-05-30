using System;

[Serializable]
public class CrushInjuryData : InjuryData
{
    private const string MARKER_NAME = "Markers/CrushMarker";
    private const string ICON_NAME = "Icons/Crush";

    public override string DEFAULT_WEAPON_NAME { get { return "Models/Crush/crushModel"; } }
    private const string WEAPON_PATH = "Models/Crush";


    public CrushInjuryData(Guid id) : base(id)
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

    public CrushInjuryData(InjuryData injuryData) : base(injuryData)
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
        return "Crush";
    }
}
