namespace Game
{
    public class ObjectAndTimer<T>
    {
        public T ObjectValue { get; set; }
        public float Timer { get; set; }
        public ObjectAndTimer() { }
        public ObjectAndTimer(T obj, float timer = 0)
        {
            ObjectValue = obj;
            Timer = timer;
        }
    }
}
