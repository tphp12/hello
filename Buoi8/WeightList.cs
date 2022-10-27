using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buoi8
{
    internal class WeightList
    {
        LinkedList<Tuple<int, int>>[] v;
        int n;
        // Các array là biến toàn cục chỉ phục vụ cho giải thuật
        int[] pre;
        int[] dist;
        bool[] processed;
        //Propeties
        public LinkedList<Tuple<int, int>>[] V
        { get => v; set => v = value; }
        // Contructor
        public WeightList() { }
        public WeightList(int k)
        {
            v = new LinkedList<Tuple<int, int>>[k];
            n = k;
        }
        // Doc file ra ds ke co trong so
        public void FileToWeightList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            v = new LinkedList<Tuple<int, int>>[n];

            for (int i = 0; i < n; i++)
            {
                v[i] = new LinkedList<Tuple<int, int>>();
                string str = sr.ReadLine();
                if (str != "")
                {
                    string[] s = str.Split();
                    int j = 0;
                    while (j < s.Length)
                    {
                        int t1 = int.Parse(s[j]);
                        int t2 = int.Parse(s[j + 1]);
                        Tuple<int, int> t = new Tuple<int, int>(t1, t2);
                        v[i].AddLast(t);
                        j = j + 2;
                    }
                }
            }
            sr.Close();
        }
        //Xuat do thi ds ke
        public void Output()
        {
            Console.WriteLine("   Đồ thị danh sách kề có trọng số - số đỉnh : " + n);
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write("     Dinh " + i + " -> ");
                foreach (Tuple<int, int> x in v[i])
                    Console.Write("({0, 2}, {1, 2})  ", x.Item1, x.Item2);
                Console.WriteLine();
            }
        }

        // Dijkstra tim duong di ngan nhat tu s den cac dinh con lai
        // Ket qua luu trong array dist[], luu vet duong di trong pre[]
        // processed[] : danh dau duyet cac dinh
        public void Dijkstra(int s)
        {
            // pre : Lưu đỉnh nằm trước trên đường từ s đi qua
            pre = new int[n];
            // dist : Lưu độ dài ngán nhất từ s đến các đỉnh còn lại
            dist = new int[n];
            // processed : Đánh dấu đỉnh đã đi qua
            processed = new bool[n];
            // Khoi gan cac gia tri ban dau
            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                pre[i] = s;
                processed[i] = false;
            }

            // Danh dau duyet s
            processed[s] = true;
            // gan gia tri ban dau cho cac canh (s,i) : dist[i], neu co
            foreach (Tuple<int, int> x in v[s])
                dist[x.Item1] = x.Item2;
            dist[s] = 0;
            // Lần lượt tìm các đỉnh gần nhất để cập nhật dist, pre
            for (int i = 0; i < n; i++)
            {
                int a;  // chon dinh a gan nhat
                a = dmin();
                processed[a] = true;    // Danh dau duyet a
                // Cap nhat lai cac gia tri trong dist[] cho cac dinh con lai,
                // Tìm đỉnh trung gian b mà qua đó sẽ ngắn hơn 
                for (int b = 0; b < n; b++)
                {
                    int ab = weight(a, b);
                    if (processed[b] == false && ab < int.MaxValue && (dist[b] > dist[a] + weight(a, b)))
                    {
                        dist[b] = dist[a] + weight(a, b);
                        pre[b] = a;
                    }
                }
            }
            //// Xuat cac ket qua cua dist[] va pre[]
            //Console.WriteLine("Đường đi ngắn nhất từ " + s + " đến các đỉnh còn lại :");
            //Console.WriteLine("  Đỉnh :   0  1  2  3  4  5  6");
            //Console.WriteLine("------------------------------");
            //Console.Write("   pre : ");
            //for (int i = 0; i < n; i++)
            //{
            //    Console.Write("{0, 3}", pre[i]);
            //}
            //Console.WriteLine();
            //Console.Write("  dist : ");
            //for (int i = 0; i < n; i++)
            //{
            //    Console.Write("{0, 3}", dist[i]);
            //}
            //Console.WriteLine();
        }
        // Ham chon dinh gan nhat
        public int dmin()
        {
            int min = int.MaxValue;    // xuat phat min la gia tri lon nhat
            int vmin = 0;               // dinh tra ve
            for (int i = 0; i < n; i++)
            {
                if (processed[i] == false && dist[i] < min)
                {
                    min = dist[i]; vmin = i;
                }
            }
            return vmin;
        }
        // Ham lay trong so canh (a,b)
        public int weight(int a, int b)
        {
            int wt = int.MaxValue;
            foreach (Tuple<int, int> x in v[a])
            {
                if (x.Item1 == b)
                {
                    wt = x.Item2; break;
                }
            }
            return wt;
        }


        //bai 2
        public void Select()
        {
            int[] idist = new int[n];
            for(int i = 0; i < n; i++)
            {
                Dijkstra(i);
                int max = dist[0];
                for (int j = 1; j < n; j++)
                    if (max < dist[j])
                    {
                        max = dist[j];
                    }
                idist[i] = max;
            }
            int min = idist[0];
            int vtmin = 0;
            for (int i = 1; i < n; i++)
                if (min > idist[i])
                { min = idist[i]; 
                    vtmin = i;
                }
            Console.WriteLine("Thành phố được chọn: " + vtmin);
            Console.WriteLine("Khoảng cách: " + min);
        }

        //bai 3
        public void dOfCircle(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            int [,]k = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] s = sr.ReadLine().Split();
                for (int j = 0; j < n; j++)
                    k[i, j] = int.Parse(s[j]);
            }
            sr.Close();
            for (int i = 0; i < n; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < n; j++)
                    Console.Write("  {0, 3}", k[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
            double[,] a = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) a[i, j] = 0;
                    else
                    {
                        double x0 = k[i, 0];
                        double x1 = k[j, 0];
                        double y0 = k[i, 1];
                        double y1 = k[j, 1];
                        double r0 = k[i, 2];

                        double d = Math.Round(Math.Sqrt((Math.Pow((x1 - x0), 2) + Math.Pow((y1 - y0), 2))), 2);
                        a[i, j] = d - r0;

                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < n; j++)
                    Console.Write("  {0, 3}", a[i, j]);
                Console.WriteLine();
            }


            v = new LinkedList<Tuple<int, int>>[n];


            for (int i = 0; i < n; i++)
            {
                v[i] = new LinkedList<Tuple<int, int>>();
                string str = sr.ReadLine();
                if (str != "")
                {
                    string[] s = str.Split();
                    int j = 0;
                    while (j < s.Length)
                    {
                        int t1 = int.Parse(s[j]);
                        int t2 = int.Parse(s[j + 1]);
                        Tuple<int, int> t = new Tuple<int, int>(t1, t2);
                        v[i].AddLast(t);
                        j = j + 2;
                    }
                }
            }
        }
        


        //bai4
        public void FileToMatrix(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string[] s = sr.ReadLine().Split();
            int row = int.Parse(s[0]);
            int col = int.Parse(s[1]);
            int [,] a = new int[row, col];

            for (int i = 0; i < row; i++)
            {
                s = sr.ReadLine().Split();
                for (int j = 0; j < col; j++)
                {
                    a[i, j] = int.Parse(s[j]);
                }
            }

            v = new LinkedList<Tuple<int,int>>[row * col];
            for (int i = 0; i < row * col; i++)
            {
                v[i] = new LinkedList<Tuple<int,int>>();
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int k = j + (i * col);

                    if (j > 0)
                    {
                        int t1 = k - 1;
                        int t2 = a[i, j - 1];
                        Tuple<int, int> t = new Tuple<int, int>(t1, t2);
                        if (!v[k].Contains(t))
                        {
                            v[k].AddLast(t);
                        }
                    }

                    if (j < col - 1)
                    {
                        int t1 = k + 1; 
                        int t2 = a[i, j+1];
                        Tuple<int, int> t = new Tuple<int, int>(t1, t2);
                        if (!v[k].Contains(t)) v[k].AddLast(t);
                    }

                    if (i > 0)
                    {
                        int t1 = k - col;
                        int t2 = a[i-1, j];
                        Tuple<int, int> t = new Tuple<int, int>(t1, t2);
                        if (!v[k].Contains(t)) v[k].AddLast(t);
                    }

                    if (i < row - 1)
                    {
                        int t1 = k + col;
                        int t2 = a[i + 1, j];
                        Tuple<int, int> t = new Tuple<int, int>(t1, t2);
                        if (!v[k].Contains(t)) v[k].AddLast(t);
                    }
                } 
            }
            //StreamWriter sw = new StreamWriter("../../TextFile/DSKrabien.txt");
            //sw.WriteLine(row*col);
            //for (int i = 0; i < v.Length; i++)
            //{
            //    foreach (Tuple<int, int> t in v[i])
            //{
            //        sw.Write($"{t.Item1} {t.Item2} ");
            //    }
            //    sw.WriteLine();
            //}

            //sw.Close(); // Đóng file
        }
        public bool iSBien(int i)
        {
            int count = 0;
            foreach (Tuple<int, int> t in v[i])
            {
                count++;
            }
            if (count < 4) return true;
            return false;
        }
        public void Duongrabien(int s)
        {
            for(int i = 0; i <v.Length; i++)
            {
                if (iSBien(i) == true)
                {
                    Dijkstra(s);
                    Stack<int> stk = new Stack<int>();
                    int k = i;
                    stk.Push(k);
                    bool route = false;
                    while (pre[k] != k)
                    {
                        k = pre[k];
                        stk.Push(k);
                        if (k == s) route = true;
                    }
                    if (route)
                    {
                            Console.WriteLine($"Đường đi ngắn nhất từ {s} đến {i} : ");
                            while (stk.Count > 0)
                            {
                                Console.Write(stk.Pop() + " -> ");
                            }
                            Console.WriteLine(" Độ dài: " + dist[i]);
                        
                    }

                }
            }




        }
    }
}
