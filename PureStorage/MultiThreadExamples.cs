/**
A simple classic thread Example:

Lets start with a very simple single thread example.
The following code examples will show a simple thread running strategy. 
The thread will run 50 times with an interval of 500 milliseconds between each running.

As you can see, first we need to define a ‘ThreadStart’ instance with the function that we will need to be executed in thread.
Then, this instance are required as parameter in our ‘Thread’ class initialization. 
After calling the ‘start’ method, the function will be called for execution in parallel to other processes.

Also, remember that, the function will be called only once. to keep it alive is our own responsibility. 
That means, we will need to run it inside a for loop until it satisfy a certain criteria. 
The ‘Sleep’ method waits that particular thread for the given amount of time(in milliseconds) before executing next statement.
*/

//Solution for starting and using a single thread
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
 
namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart testThreadStart = new ThreadStart(new Program().testThread);
            Thread testThread = new Thread(testThreadStart);
 
            testThread.Start();
            Console.ReadLine();
        }
 
        public void testThread()
        {
            //executing in thread
            int count = 0;
            while (count++ < 50)
            {
                Console.WriteLine("Thread Executed "+count+" times");
                Thread.Sleep(500);
            }
        }
    }
}

/**
Lets move one step ahead to see how more than one threads can be run in parallel. 
Following code snippet will demonstrate such scenario. Though, I am using two threads only here, 
you can add as many you want to it as per your need.

//Solution for starting and using two threads in parallel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
 
namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart testThread1Start = new ThreadStart(new Program().testThread1);
            ThreadStart testThread2Start = new ThreadStart(new Program().testThread2);
 
            Thread[] testThread = new Thread[2];
            testThread[0] = new Thread(testThread1Start);
            testThread[1] = new Thread(testThread2Start);
 
            foreach (Thread myThread in testThread)
            {
                myThread.Start();
            }
 
            Console.ReadLine();
        }
 
        public void testThread1()
        {
            //executing in thread
            int count = 0;
            while (count++ < 5)
            {
                Console.WriteLine("Thread 1 Executed "+count+" times");
                Thread.Sleep(1);
            }
        }
 
        public void testThread2()
        {
            //executing in thread
            int count = 0;
            while (count++ < 5)
            {
                Console.WriteLine("Thread 2 Executed " + count + " times");
                Thread.Sleep(1);
            }
        }
    }
}
/**
Output:
Thread 1 Executed 1 times
Thread 2 Executed 1 times
Thread 2 Executed 2 times
Thread 1 Executed 2 times
Thread 2 Executed 3 times
Thread 1 Executed 3 times
Thread 2 Executed 4 times
Thread 1 Executed 4 times
Thread 2 Executed 5 times
Thread 1 Executed 5 times

Note that, the output won’t be exact always, 
it completely depends on how OS provides schedules to CPU for execution of the thread.
*/

/**
Working With ThreadPool:

So, as we have seen so far in above code examples, uses ‘Thread’ in raw mode.
However, we can simplify the thread running process with use of ‘ThreadPool’ class that is provided by .NET framework 
and is very much helpful for quick implementation of multithreaded programming. 
All we have to do is to queue the function we want running in thread. 
And it will automatically start executing them. The following code will demonstrate this simplified usage of ‘ThreadPool’ .
*/

//Solution for using ThreadPool to run multi threads more simply.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
 
namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //queue the methods into the ThreadPool and they will be executed in parallel autematically
            ThreadPool.QueueUserWorkItem(new Program().testThread1);
            ThreadPool.QueueUserWorkItem(new Program().testThread2);
 
            Console.ReadLine();
        }
 
        public void testThread1(Object threadContext)
        {
            //executing in thread
            int count = 0;
            while (count++ < 10)
            {
                Console.WriteLine("Thread 1 Executed "+count+" times");
                Thread.Sleep(100);
            }
        }
 
        public void testThread2(Object threadContext)
        {
            //executing in thread
            int count = 0;
            while (count++ < 10)
            {
                Console.WriteLine("Thread 2 Executed " + count + " times");
                Thread.Sleep(100);
            }
        }
    }
}
