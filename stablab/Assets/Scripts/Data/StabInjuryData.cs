using System;

[Serializable]
public class StabInjuryData : InjuryData
{
    private const string MARKER_NAME = "Markers/StabMarker";
    private const string ICON_NAME = "Icons/Stab";

    private const string WEAPON_NAME = "Models/Stab/stabModel";
    private const string WEAPON_PATH = "Models/Stab";

    public StabInjuryData(Guid id) : base(id)
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

    public StabInjuryData(InjuryData injuryData) : base(injuryData)
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
        return "Stab"; 
    }
}
