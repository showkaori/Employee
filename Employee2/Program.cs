using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // タイトル表示
            Console.WriteLine("------------");
            Console.WriteLine("　社員名簿");
            Console.WriteLine("------------");

            var finish = true;

            do
            {
                Console.WriteLine("\n　1 - 全データの取得");
                Console.WriteLine("　2 - データの追加");
                Console.WriteLine("　3 - データの編集");
                Console.WriteLine("　4 - データの削除");
                Console.WriteLine("\n");

                // 行いたい処理を尋ねる.
                Console.WriteLine("行いたい処理の番号を入力しENTERを押してください");
                Console.Write("どの処理を行いますか? →");
                //入力された番号をnumに保管
                var num = Console.ReadLine();
                var num1 = int.Parse(num);

                switch (num1)
                {
                    case 1:
                        //データの取得
                        Sql s1 = new Sql();
                        finish = s1.SelectAll();
                        break;
                    case 2:
                        //データの追加
                        Sql s2 = new Sql();
                        finish = s2.AddData();
                        break;
                    case 3:
                        //データの編集
                        Sql s3 = new Sql();
                        finish = s3.EditData();
                        break;
                    case 4:
                        //データの削除
                        Sql s4 = new Sql();
                        finish = s4.DeleteData();
                        break;
                    default:
                        Console.WriteLine("入力内容に誤りがございます");
                        finish = true;
                        break;
                }


            } while (finish);       //falseが入るとアプリ終了
            Console.WriteLine("終了します");

        }   
    }
}
