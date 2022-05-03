

namespace RunMinigames.Interface.Characters
{
    public interface ICharacterItem
    {
        public bool IsItemSpeedActive { get; set; }
        public bool CanMove { get; set; }
        public float CharSpeed { get; set; }
        public float MaxSpeed { get; set; }
    }
}