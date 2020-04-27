using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixRecursive
{
    class Program
    {
        static public double[,] A, B, C, C2;
        static public double[,,] C2temp;

        static int n, m_operations = 0, r_operations = 0;

        static void FillMatrixA()
        {           
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i >= j)
                    {
                        A[i, j] = n - i + j;                      
                    }
                }
            }
        }

        static void FillMatrixB()
        {
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                int k = n - 1;

                if (i <= (n / 2) - 1)
                {
                    k -= i;
                }
                else
                {
                    k = i;
                }

                for (int j = k; j < n; j++)
                {
                    B[i, j] = rand.Next(1, 9);
                }
            }
        }
            
        static void EnterMatrixB()
        {
            for (int i = 0; i < n; i++)
            {
                int k = n - 1;

                if (i <= (n / 2) - 1)
                {
                    k -= i;
                }
                else
                {
                    k = i;
                }

                for (int j = k; j < n; j++)
                {
                    B[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
        }

        static void ShowMatrix(double [,] Matrix)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static public void Multiplication()
        {
            if (A.GetLength(1) != B.GetLength(0)) throw new Exception("Unappropriated");
            double[,] r = new double[A.GetLength(0), B.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    for (int k = 0; k < B.GetLength(0); k++)
                    {
                        r[i, j] += A[i, k] * B[k, j];
                        
                    }
                    Console.Write(r[i, j] + " ");
                }
                Console.WriteLine();
            }         
        }

        static void OneTimeAssignment()
        {
            if (A.GetLength(1) != B.GetLength(0)) throw new Exception("Unappropriated");          

            for (int i = 0; i < A.GetLength(0); i++)
            {
               double [,,] temp = new double[n, n, n + 1];
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    temp[i, j, 0] = 0;

                    for (int k = 0; k < A.GetLength(0); k++)
                    {
                        temp[i, j, k + 1] = temp[i, j, k] + A[i, k] * B[k, j];
                        m_operations+=2;                       
                    }
                    C[i, j] = temp[i, j, n];                 
                }             
            }           
        }

       static void RecursiveMulti(int i, int j, int k)
        {
            int size = A.GetLength(0);
           
            if (i < size && j < size && k < size)
            {
               
                if (A[i, k] != 0 && B[k, j] != 0)
                {
                    C2temp[i, j, size] +=  A[i, k] * B[k, j];
                    r_operations+=2;               
                }        
                
                    RecursiveMulti(i, j, k + 1);

                    if (k == size - 1)
                    {
                        k = 0;
                        RecursiveMulti(i, j + 1, k);

                        if (j == size - 1)
                        {
                            j = 0;
                            RecursiveMulti(i + 1, j, k);
                        }
                    }                                  
            }
        }

        static void GetLastData()
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(0); j++)
                {               
                    C2[i, j] = C2temp[i, j, n];
                }             
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter dimension: ");
            n = Convert.ToInt32(Console.ReadLine());
            A = new double[n, n];
            B = new double[n, n];
            C = new double[n, n];
            C2 = new double[n, n];
            C2temp = new double[n, n, n + 1];

            FillMatrixA();
            Console.WriteLine("A matrix: ");
            ShowMatrix(A);

            Console.WriteLine("Generate Matrix B automatically? y/n");
            ConsoleKey response = Console.ReadKey(false).Key;

            if (response == ConsoleKey.Y)
            {
                FillMatrixB();
            }
            else
            {
                EnterMatrixB();
            }
            Console.WriteLine();

            Console.WriteLine("B matrix: ");
            ShowMatrix(B);


            Console.WriteLine("Simple multiplication ");
            Multiplication();


            Console.WriteLine("\nOne time assignment method: ");
            OneTimeAssignment();
            ShowMatrix(C);
            Console.WriteLine("Operations: " + m_operations);

            

            Console.WriteLine("Recursive method: ");
            RecursiveMulti(0, 0, 0);                                      
            GetLastData();
            ShowMatrix(C2);
            Console.WriteLine("Operations: " + r_operations);

            Console.WriteLine("Press anything to exit");
            Console.ReadKey();
        }

    }
}
