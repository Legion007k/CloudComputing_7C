
namespace CenaDeFilosofos.Clases
{
    internal class Philosopher
    {
        public readonly Fork leftFork;
        public readonly Fork rightFork;
        public Philosopher(Fork leftFork, Fork rightFork)
        {
            this.leftFork = leftFork;
            this.rightFork = rightFork;
        }
        public void TryEating(int i)
        {
            if (leftFork.isUsed || rightFork.isUsed) 
            {
                return;
            }
            leftFork.PickUp();
            rightFork.PickUp();
            Thread.Sleep(1000); // Simulate eating time
            Console.WriteLine($"The philosopher {i} is eating.");

            leftFork.PutDown();
            rightFork.PutDown();
        }
    }
}
