
namespace CenaDeFilosofos.Clases
{
    internal class MainApplicaiton
    {
        public int numPhilosophers { get; set; }
        public Philosopher[] philosophers { get; set; }
        public Fork[] forks { get; set; }

        public Thread[] threads { get; set; }
        public object turnLock = new object();
        public int turn = -1;// Initialize turn to -1 to indicate no philosopher's turn yet
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
                threads[i] = new Thread(() =>
                {
                    while (true)
                    {
                        bool myTurn;
                        lock (turnLock)
                        {
                            if (turn == -1) turn = id;
                            if (turn == id) myTurn = true;
                            else myTurn = false;
                        }
                        if (!myTurn) continue;

                        philosophers[id].TryEating(id);
                        lock (turnLock)
                        {
                            turn = (turn + 1) % numPhilosophers; // Move to the next philosopher's turn
                        }
                        return;
                    }
                });
            }
            foreach (Thread t in threads) t.Start();
            foreach (Thread t in threads) t.Join();

        }

    }
}
