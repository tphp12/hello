using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buoi8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            Console.WriteLine();
            int chon;
            do
            {
                Console.Clear();
                chon = Menu();
                switch (chon)
                {
                    case 1:
                        {
                            // Bài toán đường ra biên
                            Console.WriteLine("    BAI TOAN TIM DUONG RA BIEN");

                            string filePath = "../../TextFile/Table.txt";
                            WeightList g = new WeightList();
                            g.FileToMatrix(filePath);   // Doc ma tran.txt -> ma tran -> DSK
                            // chuyển ma trậ a -> đồ thị danh sách kề
                            Console.WriteLine("  Nhap diem xuat phat : ");
                            Console.Write("  Nhap toa do x : ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("  Nhap toa do y : ");
                            int y = int.Parse(Console.ReadLine());
                            int s = 6 * x + y;  // Xac dinh dinh tuong ung
                            Console.WriteLine();
                            filePath = "../../TextFile/DSKrabien.txt";
                            g.FileToWeightList(filePath);
                            Console.WriteLine();
                            g.Duongrabien(s);
                            break;
                        }
                    case 2:
                        {
                            // Chọn thành phố để tổ chức họp
                            WeightList g = new WeightList();
                            string filePath = "../../TextFile/SelectCty.txt";
                            g.FileToWeightList(filePath);
                            g.Output();
                            g.Select();

                            break;
                        }
                    case 3:
                        {
                            WeightList g = new WeightList();
                            string filePath = "../../TextFile/Circle.txt";
                            g.dOfCircle(filePath);
                            //Console.Write("Nhập vòng tròn 1: ");
                            //int v1 = int.Parse(Console.ReadLine());
                            //Console.Write("Nhập vòng tròn 2: ");
                            //int v2 = int.Parse(Console.ReadLine());
                            

                            break;
                        }

                }
                Console.ReadKey();
            } while (chon != 0);
        }
        static int Menu()
        {
            int chon;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 30) + " Ứng dụng Dijkstra & Floyd " + new string('-', 30));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("      1. Bài toán đường ra biên");
            Console.WriteLine("      2. Chọn thành phố để tổ chức họp");
            Console.WriteLine("      3. Bai 3");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 100));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Chon : "); chon = int.Parse(Console.ReadLine());
            return chon;
        }

    }
}