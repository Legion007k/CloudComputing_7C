
using System.ComponentModel;

namespace CenaDeFilosofos.Clases
{
    internal class MainApplicaiton
    {
        public int numPhilosophers { get; set; }
        public Philosopher[] philosophers { get; set; }
        public Fork[] forks { get; set; }

        public Thread[] threads { get; set; }
        public object turnLock = new object();
        public MainApplicaiton(int numPhilosophers)
        {
            this.numPhilosophers = numPhilosophers;
            philosophers = new Philosopher[numPhilosophers];
            forks = new Fork[numPhilosophers];
            threads = new Thread[numPhilosophers];
        }
        public void Initialize()
        {
            //Initialize forks and philosophers
            for (int i = 0; i < numPhilosophers; i++)
            {
                forks[i] = new Fork(i);
            }
            for (int i = 0; i < numPhilosophers; i++)
            {
                philosophers[i] = new Philosopher(forks[i], forks[(i + 1) % numPhilosophers]);
            }
        }
        public void Run()
        {
            Initialize();
            for (int i = 0; i < numPhilosophers; i++)
            {
                int id = i;
                int counter = 0;
                threads[i] = new Thread(() =>
                {
                    while (true)
                    {
                        bool myTurn;

                        if (philosophers[id].instance < counter + 1)
                        {
                            myTurn = true;
                        }
                        else myTurn = false;
                        
                        if (!myTurn) continue;

                        philosophers[id].TryEating(id);
                        counter++;

                    }
                });
            }
            foreach (Thread t in threads) t.Start();
            foreach (Thread t in threads) t.Join();

        }

    }
}
