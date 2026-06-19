namespace NLB.Player
{
    public interface IPlayerSpawner
    {
        public bool IsSpawned {get;}
        public void Spawn();
    }
}