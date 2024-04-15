using BurnoutBuster.Character;

namespace BurnoutBuster.Utility
{
    public interface ICreatureObserver : IObserver
    {
        bool isHeld { get; set; }
        MonogameCreature creatureSubject { get; set; }
        void UpdateObserver(MonogameCreature creature);
    }
}
