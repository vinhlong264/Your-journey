﻿public interface IDameHandlePhysical
{
    void DameHandlerPhysical(CharacterStats _statSender); // xử lý dame
    int CheckArmor(int _finalDame);
    bool AvoidAttack();
    bool CanCrit(CharacterStats _statSender);
    int CalculateCritDame(int _finalDame , CharacterStats _statSender);

}
