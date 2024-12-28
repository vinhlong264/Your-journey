public interface IDameHandlePhysical
{
    void DoDamePhysical(CharacterStats _statSender);
    int CheckArmor(int _finalDame);
    bool AvoidAttack();
    bool CanCrit();
    int CalculateCritDame(int _value);

}
