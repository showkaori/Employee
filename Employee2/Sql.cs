using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;// MySQLを使用
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2
{
    internal class Sql
    {
        // MySQLへの接続情報
        private static readonly string server = "localhost";
        private static readonly string database = "address_db";//使用するデータベース
        private static readonly string MysqlTable = "employee";//使用するテーブル
        private static readonly string user = "****";//ユーザー名（自分で設定したもの））
        private static readonly string pass = "****";//インストール時に設定したパスワード（自分で設定したもの）
        private static readonly string charset = "utf8";
        // MySQLへの接続
        private static readonly string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);
        //処理を続けるか
        private static bool finish = true;


        //【１．全データ取得】
        public bool SelectAll()
        {
            try
            {
                // コネクションオブジェクトとコマンドオブジェクトの生成
                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;

                    //データ抽出用SQL
                    string SelectSql = $"SELECT * FROM {MysqlTable}";

                    command.CommandText = SelectSql;

                    Console.WriteLine($"テーブル「{MysqlTable}」のデータを出力します");
                    //カラム名を出力
                    MySqlDataReader reader = command.ExecuteReader();
                    string[] column = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                        column[i] = reader.GetName(i);
                    Console.WriteLine(string.Join("\t", column));

                    //テーブルのデータを出力
                    while (reader.Read())
                    {
                        string[] row = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[i] = reader.GetString(i);
                        Console.WriteLine(string.Join("\t", row));
                    }
                    reader.Close();

                    Console.WriteLine("\n作業を続けますか？");
                    Console.WriteLine("続ける → y, 終わる → n");
                    var intention = Console.ReadLine();
                    switch (intention)
                    {
                        case "y":
                            finish = true;
                            break;
                        case "n":
                            finish = false;
                            break;
                        default:
                            Console.WriteLine("入力内容が確認できませんでしたので終了します。");
                            finish = false;
                            break;

                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                finish = true;
            }
            return finish;
        }




        //【２．データの追加】
        public bool AddData()
        {
            try
            {
                // コネクションオブジェクトとコマンドオブジェクトの生成
                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;

                    //追加したいデータの入力
                    Console.WriteLine("名前を入力しENTERを押してください");
                    var name = Console.ReadLine();
                    Console.WriteLine("メールアドレスを入力しENTERを押してください");
                    var email = Console.ReadLine();
                    Console.WriteLine("電話番号を入力しENTERを押してください");
                    var tel = Console.ReadLine();
                    Console.WriteLine("入社日をYYYY-MM-DD形式で入力しENTERを押してください");
                    var dateOfEmp = Console.ReadLine();

                    string InsertData = $"('{name}', '{email}', '{tel}', '{dateOfEmp}')";　//追加内容
                    //INSERT用SQL
                    string InsertTableSql = $"INSERT INTO {MysqlTable} (name, email, tel, dateOfEmp) VALUES {InsertData}";
                    command.CommandText = InsertTableSql;

                    Console.WriteLine($"テーブル「{MysqlTable}」にデータ「{InsertData}」を挿入しました");
                    command.ExecuteNonQuery(); //実行

                    Console.WriteLine("\n作業を続けますか？");
                    Console.WriteLine("続ける → y, 終わる → n");
                    var intention = Console.ReadLine();
                    switch (intention)
                    {
                        case "y":
                            finish = true;
                            break;
                        case "n":
                            finish = false;
                            break;
                        default:
                            Console.WriteLine("入力内容が確認できませんでしたので終了します。");
                            finish = false;
                            break;
                    }
                }
  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                finish = false;
            }
            return finish;
        }



        //【３．データの編集】
        public bool EditData()
        {
            try
            {
                // コネクションオブジェクトとコマンドオブジェクトの生成
                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;

                    //編集したいデータの決定
                    Console.WriteLine("変更するデータのIDを入力してください");
                    int id = int.Parse(Console.ReadLine());
                    string SelectSql = $"SELECT * FROM {MysqlTable} where id = {id}";
                    command.CommandText = SelectSql;
                    MySqlDataReader reader = command.ExecuteReader(); //実行と結果の格納
                    while (reader.Read())
                    {
                        Console.WriteLine("\n　ID:{0}\n　名前:{1}\n　メールアドレス：{2}\n　電話番号：{3}\n　入社日：{4}\n\nこちらのデータを変更します", 
                            reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                    reader.Close();

                    //編集するデータの決定
                    Console.WriteLine("\n　1 - 名前");
                    Console.WriteLine("　2 - メールアドレス");
                    Console.WriteLine("　3 - 電話番号");
                    Console.WriteLine("　4 - 入社日");
                    Console.WriteLine("\n編集したい箇所の番号を入力しENTERを押してください");
                    Console.Write("どこを編集しますか? →");
                    //編集したい列名の選択
                    var num = Console.ReadLine();
                    var num1 = int.Parse(num);
                    var column = "";
                    switch (num1)
                    {
                        case 1:
                            column = "name";
                            break;
                        case 2:
                            column = "email";
                            break;
                        case 3:
                            column = "tel";
                            break;
                        case 4:
                            column = "dateOfEmp";
                            break;
                        default:
                            Console.WriteLine("入力内容に誤りがございます。もう一度最初からやり直してください");
                            finish = true;
                            return finish;                            
                    }
                    Console.WriteLine($"変更後の{column}を入力してください");
                    var str = Console.ReadLine();
                    string editSql = $"UPDATE {MysqlTable} SET {column} = '{str}' WHERE id = {id}";　//sql分作成
                    command.CommandText = editSql;       //SQL文セット
                    command.ExecuteNonQuery();           //実行
                    Console.WriteLine($"ID：{id}の内容を変更しました");

                    Console.WriteLine("\n作業を続けますか？");
                    Console.WriteLine("続ける → y, 終わる → n");
                    var intention = Console.ReadLine();
                    switch (intention)
                    {
                        case "y":
                            finish = true;
                            break;
                        case "n":
                            finish = false;
                            break;
                        default:
                            Console.WriteLine("入力内容が確認できませんでしたので終了します。");
                            finish = false;
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                finish = true;
            }
            return finish;
        }



        //【４．データの消去】
        public bool DeleteData()
        {
            try
            {
                // コネクションオブジェクトとコマンドオブジェクトの生成
                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;

                    //消去したいデータの決定
                    Console.WriteLine("消去したいデータのIDを入力してください");
                    int id = int.Parse(Console.ReadLine());
                    string SelectSql = $"SELECT * FROM {MysqlTable} where id = {id}";
                    command.CommandText = SelectSql;
                    MySqlDataReader reader = command.ExecuteReader(); //実行と結果の格納
                    while (reader.Read())
                    {
                        Console.WriteLine("\n　ID:{0}\n　名前:{1}\n　メールアドレス：{2}\n　電話番号：{3}\n　入社日：{4}\n\nこちらのデータを消去します",
                            reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                    reader.Close();//クローズ

                    Console.WriteLine("本当に消去してもよろしいですか？");
                    Console.Write("消去する → y　　止める → n\t");
                    var str = Console.ReadLine();
                    if(str == "y")
                    {
                        //消去を実行する
                        string DeleteSql = $"DELETE FROM {MysqlTable} WHERE id = {id}";
                        command.CommandText = DeleteSql;       //SQL文セット
                        command.ExecuteNonQuery();           //実行
                        Console.WriteLine($"【ID:{id}】のデータを消去しました。");
                    }
                    else if(str == "n")
                    {
                        Console.WriteLine("消去を取りやめました。最初の画面に戻ります。");
                        finish = true;
                        return finish;
                    }else
                    {
                        Console.WriteLine("入力内容に誤りがございます");
                    }

                    Console.WriteLine("\n作業を続けますか？");
                    Console.WriteLine("続ける → y, 終わる → n\t");
                    var intention = Console.ReadLine();
                    switch (intention)
                    {
                        case "y":
                            finish = true;
                            break;
                        case "n":
                            finish = false;
                            break;
                        default:
                            Console.WriteLine("入力内容が確認できませんでしたので終了します。");
                            finish = false;
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                finish = false;
            }
            return finish;
        }

    }
}
