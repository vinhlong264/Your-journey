using newQuestSystem;
public interface IQuest
{
    void ReceiveQuest(Quest q);
    void ExcuteQuest();
    void CompeleteQuest(Quest q);
}
