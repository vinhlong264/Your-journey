using newQuestSystem;
public interface IQuest
{
    void ReceiveQuest(Quest q);
    void ExcuteQuest(EnemyType _type);
    void CompeleteQuest(Quest q);
}
