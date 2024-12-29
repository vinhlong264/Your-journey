public interface IHarmfulEffect
{
    void LogicApplyAilement(int _fireDame, int _iceDame, int _lightingDame);
    void ApplyAilement(bool _canIngnite, bool _canChill, bool _canShock);
    void ApplyBurn();
    void ApplyChill(bool _canChill);
    void ApplyShock(bool _canShock);
}


