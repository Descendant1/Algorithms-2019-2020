using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        
        Console.ReadLine();
    }
}

class UnfriendluNumbers
{
    static int RemoveAll<T>(ref LinkedList<T> list, Predicate<T> match)
    {
        var count = 0;
        var node = list.First;
        while (node != null)
        {
            var next = node.Next;
            if (match(node.Value))
            {
                list.Remove(node);
                count++;
            }
            node = next;
        }
        return count;
    }
    static long GCD(long a, long b)
    {
        if (b == 0)
            return a;
        return GCD(b, a % b);
    }
    static int solve(long[] unfNums, long k)
    {
        LinkedList<long> factors = new LinkedList<long>();
        int root = (int)Math.Sqrt((double)k + 1);
        //Halfing the factors
        for (long i = 2; i < root; i++)
            if (k % i == 0)
            {
                factors.AddLast(i);
                if (k / i != i)
                    factors.AddLast((long)(k / i));
            }

        //unique common divisor`s
        HashSet<long> ucd = new HashSet<long>();
        for (int i = 0; i < unfNums.Length; i++)
            ucd.Add(GCD(unfNums[i], k));


        factors.AddLast(k);

        foreach (var i in ucd)
            RemoveAll(ref factors, it => i % it == 0);

        return factors.Count;
    }
    public static void Run()
    {
        string[] nf = Console.ReadLine().Split(' ');

        //Count UnfriendlyNumbers
        //int n = Convert.ToInt32(nf[0]);
        //our friendly num
        long friendly = long.Parse(nf[1]);
        //unfriendly numbers
        long[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), numsTemp => long.Parse(numsTemp));

        Console.WriteLine(solve(nums, friendly));
    }
}

class Divisors3
{
    static List<ulong> Factor(ulong number)
    {
        List<ulong> factors = new List<ulong>();
        ulong max = (ulong)Math.Sqrt(number);  //round down
        for (ulong factor = 1; factor <= max; ++factor)
            if (number % factor == 0)
            {
                factors.Add(factor);
                if (factor != number / factor)
                    factors.Add(number / factor);
            }
        return factors;
    }
    public static void Run()
    {
        Console.WriteLine(Factor(ulong.Parse(Console.ReadLine())).Count);
    }
}

class Euler25NFibbNums
{
    const double phi = 1.61803398874989484820458683436;//GoldenRatio 
    static long numberOfDigits(int n)
    {
        if (n == 1)
            return 1;
        return (long)Math.Ceiling((Math.Log(10) * (n - 1) + Math.Log(5) / 2) / Math.Log(phi));
    }
    public static void Run()
    {
        int T = int.Parse(Console.ReadLine());

        if (T < 1 || T > 5000)
            return;

        Queue<int> N = new Queue<int>();

        while (T-- > 0)
        {
            int tmpInput = int.Parse(Console.ReadLine());
            if (tmpInput < 2 || tmpInput > 5000)
                return;
            N.Enqueue(tmpInput);
        }

        while (N.Count > 0)
            Console.WriteLine(numberOfDigits(N.Dequeue()));
    }
}

class LeftRotation
{
    public static void Run()
    {
        string[] nd = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nd[0]);

        int d = Convert.ToInt32(nd[1]);

        var q = new Queue<int>(Array.ConvertAll(Console.ReadLine().Split(' '), aTemp => Convert.ToInt32(aTemp)));

        while (d > 0)
        {
            q.Enqueue(q.Dequeue());
            d--;
        }
        Console.WriteLine(String.Join(" ", q));
    }
}

class CycleDetection
{
    public class SinglyLinkedListNode
    {
        public int data;
        public SinglyLinkedListNode next;
    }
    public static bool Run(SinglyLinkedListNode head)
    {
        if (head == null)
        {
            return false;
        }
        SinglyLinkedListNode fastOne = head.next;
        SinglyLinkedListNode slowOne = head;

        while (fastOne != null && fastOne.next != null && slowOne != null)
        {
            if (fastOne == slowOne)
            {
                return true;
            }
            fastOne = fastOne.next.next;
            slowOne = slowOne.next;
        }
        return false;

    }
}

class FindMergePointofTwoLists
{
    public class SinglyLinkedListNode
    {
        public int data;
        public SinglyLinkedListNode next;
    }
    public static int Run(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
    {
        int head1Length = 0
            , head2Length = 0;

        SinglyLinkedListNode head1tmp = head1
                             , head2tmp = head2;
        while (head1tmp != null)
        {
            head1tmp = head1tmp.next;
            head1Length++;
        }
        while (head2tmp != null)
        {
            head2tmp = head2tmp.next;
            head2Length++;
        }
        while (head1Length > head2Length)
        {
            head1 = head1.next;
            head1Length--;
        }
        while (head2Length > head1Length)
        {
            head2 = head2.next;
            head2Length--;
        }
        while (head1 != null)
        {
            if (head1 == head2)
                return head2.data;

            head1 = head1.next;
            head2 = head2.next;
        }
        return -1;

    }
}

class ReversePolishNumber
{
    static bool isNumber(string str)
    {
        return int.TryParse(str, out int res);
    }
    static Queue<string> convertInfixToRPN(string[] infixNotation)
    {
        Dictionary<string, int> prededence = new Dictionary<string, int>
        {
            { "/", 5 },
            {"*", 5 },
            {"+", 4},
            {"-", 4},
            {"(", 0}
        };
        Queue<string> Q = new Queue<string>();
        Stack<string> S = new Stack<string>();

        foreach (var token in infixNotation)
        {
            if ("(".Equals(token))
            {
                S.Push(token);
                continue;
            }
            if (")".Equals(token))
            {
                while (!"(".Equals(S.Peek()))
                {
                    Q.Enqueue(S.Pop());
                }
                S.Pop();
                continue;
            }
            // an operator
            if (prededence.ContainsKey(token))
            {

                while (S.Count > 0 && prededence[token] <= prededence[S.Peek()])
                {
                    Q.Enqueue(S.Pop());
                }
                S.Push(token);
                continue;
            }

            if (isNumber(token))
            {
                Q.Enqueue(token);
                continue;
            }

            throw new Exception("Invalid input");
        }
        // at the end, pop all the elements in S to Q
        while (S.Count > 0)
        {
            Q.Enqueue(S.Pop());
        }

        return Q;
    }
    public static void Run()
    {
        string input = Console.ReadLine();

        if (input.Length > 99)
        {
            return;
        }
        foreach (var item in input.Split(' '))
        {
            if (int.TryParse(item.ToString(), out int i))
            {
                if (int.Parse(item.ToString()) > 1000)
                {
                    return;
                }
            }
        }
        var result = convertInfixToRPN(input.Split(' '));

        while (result.Count > 0)
        {
            if (result.Count == 1)
            {
                Console.Write(result.Dequeue());
            }
            else
            {
                Console.Write(result.Dequeue() + " ");
            }
        }

        Console.ReadLine();
    }
}

class MiniMaxSum
{
    static void miniMaxSum(int[] arr)
    {
        Array.Sort(arr);
        long min = arr.Take(4).Sum(num => (long)num);
        long max = arr.Skip(1).Sum(num => (long)num);
        Console.WriteLine(min + " " + max);
    }

    public static void Run()
    {
        int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
        miniMaxSum(arr);
    }
}

class MissingNumbers
{
    public static void Run()
    {
        Console.WriteLine("C++ impl");
        //int n, m, a, mx = -99999, mn = INT_MAX;
        //int res[1000011] = { 0 };

        //cin >> n;
        //for (int i = 0; i < n; i++)
        //{
        //    cin >> a;
        //    res[a]--;
        //}
        //cin >> m;
        //for (int i = 0; i < m; i++)
        //{
        //    cin >> a;
        //    res[a]++;
        //    if (a < mn)
        //        mn = a;
        //    if (a > mx)
        //        mx = a;
        //}
        //for (int i = mn; i <= mx; i++)
        //    if (res[i] > 0) cout << i << " ";
        //return 0;
    }
}

class LegoBlocks
{
    static readonly int Modulo = 1000000007;
    public static long Power(long num, int p)
    {

        if (p == 0)
            return 1;
        if (p == 1)
            return num;

        long number = num;
        for (int i = 2; i <= p; i++)
        {
            num *= number;
            num %= Modulo;
        }
        return num;
    }

    public static void Run()
    {
        var result = new Queue<long>();

        long[] T = new long[1001];
        long[] S = new long[1001];
        long[] P = new long[1001];

        T[0] = T[1] = 1;
        T[2] = 2;
        T[3] = 4;
        T[4] = 8;

        P[0] = P[1] = 1;



        for (int i = 5; i <= 1000; i++)
            T[i] = (T[i - 1] + T[i - 2] + T[i - 3] + T[i - 4]) % Modulo;

        S[0] = 1;
        S[1] = 1;

        long sum;

        int tests = int.Parse(Console.ReadLine());

        while (tests-- > 0)
        {
            string[] nm = Console.ReadLine().Split(' ');

            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);

            for (int i = 0; i <= m; i++)
            {
                P[i] = Power(T[i], n);
            }

            for (int i = 2; i <= m; i++)
            {
                sum = 0;
                for (int j = 1; j < i; j++)
                {
                    sum += (S[j] * P[i - j]) % Modulo;
                    sum %= Modulo;
                }
                S[i] = (P[i] - sum);
                S[i] = S[i] % Modulo;
            }
            while (S[m] < 0)
                S[m] += Modulo;

            result.Enqueue(S[m]);
        }
        while (result.Count > 0)
            Console.WriteLine(result.Dequeue());
    }
}