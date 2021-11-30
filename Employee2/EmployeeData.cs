using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2
{
    class EmployeeData
    {
        public int Id { get; set; }//管理番号
        public string Name { get; set; }//名前
        public string Email { get; set; }//メールアドレス
        public string Tel { get; set; }//電話番号
        public DateTime DateOfEmp { get; set; }//入社日

        //コンストラクタ
        public EmployeeData(int id, string name, string email, string tel, DateTime dateOfEmp)
        {
            Id = id;
            Name = name;
            Email = email;
            Tel = tel;
            DateOfEmp = dateOfEmp;
        }

        public void print()
        {
            //データをstring型の一文にしたもの
            Console.WriteLine($"\tID：{Id}\n\t名前：{Name}\n\tメールアドレス：{Email}\n\t電話番号：{Tel}\n\t入社日：{DateOfEmp}");
        }
    }
}
