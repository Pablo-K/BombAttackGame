namespace BombAttackGame.Interfaces
{
    internal interface IGun : IHoldableObject
    {
        public int Damage { get; set; }
    }
}
