namespace App.Code.Core.Systems.Movement.Structs
{
    public struct MovementStateData
    {
        public float Speed { get; set; }
        public float BlendValue { get; set; }

        public MovementStateData(float speed, float blendValue)
        {
            Speed = speed;
            BlendValue = blendValue;
        }
    }
}