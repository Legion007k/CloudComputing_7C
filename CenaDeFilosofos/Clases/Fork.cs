
namespace CenaDeFilosofos.Clases
{
    internal class Fork
    {
        private readonly object _lock = new object();
        public int id { get; }
        public bool isUsed { get; set; }
        public Fork(int id)
        {
            this.id = id;
        }
        public bool PickUp()
        {
            lock (_lock)
            {
                if (isUsed) return false;
                isUsed = true;  
                return true;
            }
        }
        public void PutDown()
        {
            {
                isUsed = false;
            }
        }
    }
}
