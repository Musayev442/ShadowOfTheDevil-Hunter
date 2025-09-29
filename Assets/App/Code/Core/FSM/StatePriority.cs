namespace App.Code.Core.FSM
{
    public enum StatePriority
    {
        // Priority levels (higher number = higher priority)
        Default = 0, // Basic transitions
        Low = 10, // Secondary movements
        Normal = 20, // Primary movements
        High = 50, // Combat, interactions
        Critical = 100 // Emergency states
    }
}