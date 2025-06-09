[System.Flags]
public enum MovementType
{
    None = 0,
    Straight = 1 << 0,
    Rotating = 1 << 1,
    ZigZag = 1 << 2
}