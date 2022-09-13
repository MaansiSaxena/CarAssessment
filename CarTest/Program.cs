using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace CarTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("server=BHAVNAWKS593;database=car;integrated security=true");

            SqlDataAdapter da = new SqlDataAdapter("select * from Employee_table", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "E_table");
            int x = ds.Tables[0].Rows.Count;

            SqlDataAdapter ws = new SqlDataAdapter("select * from Workinghrs", con);
            ws.Fill(ds, "Work_table");
            int y = ds.Tables[1].Rows.Count;

            SqlDataAdapter dept = new SqlDataAdapter("select * from Department_table", con);
            dept.Fill(ds, "dept_table");
            int z = ds.Tables[2].Rows.Count;

            SqlDataAdapter sto = new SqlDataAdapter("select * from stock_table", con);
            sto.Fill(ds, "sto_table");
            int a = ds.Tables[3].Rows.Count;

            SqlDataAdapter pro = new SqlDataAdapter("select * from product_table", con);
            pro.Fill(ds, "pro_table");
            int b = ds.Tables[4].Rows.Count;


            string isRepeat = "Y";

            while (isRepeat.ToUpper() == "Y")
            {
                Console.WriteLine("-------WELCOME-------\n");
                Console.WriteLine("1 for Employee salary details");
                Console.WriteLine("2 for stock details");
                Console.WriteLine("3 for overall balance sheet");
                Console.WriteLine("4 for Details of stock which running low");

                int n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        Console.WriteLine("Enter the Emp Id for salary slip");
                        int id = int.Parse(Console.ReadLine());

                        int total_hrs = 184;
                        int over_hrs = 0;
                        int sal = 0;
                        string depart = " ";
                        for (int i = 0; i < y; i++)
                        {
                            if (id.ToString() == ds.Tables[1].Rows[i][0].ToString())
                            {
                                for (int j = 0; j < z; j++)
                                {
                                    if (ds.Tables[0].Rows[i][2].ToString() == ds.Tables[2].Rows[j][0].ToString())
                                    {
                                        sal = int.Parse(ds.Tables[2].Rows[j][2].ToString());
                                        depart = ds.Tables[2].Rows[j][1].ToString();
                                    }
                                }
                                int work_hrs = int.Parse(ds.Tables[1].Rows[i][1].ToString());
                                over_hrs = work_hrs - total_hrs;
                                int Overtimesal = 0;

                                if (over_hrs >= 0)
                                {
                                    Overtimesal = ((sal / 184) * over_hrs * 2);
                                }
                                else
                                {
                                    sal = ((sal / 184) * (over_hrs)) + sal;
                                    over_hrs = 0;
                                }
                                Console.WriteLine("Emp id: " + ds.Tables["Work_table"].Rows[i][0].ToString());
                                Console.WriteLine("Emp name: " + ds.Tables["E_table"].Rows[i][1].ToString());
                                Console.WriteLine("Dept name: " + depart);
                                Console.WriteLine("Total No. of Hours: " + total_hrs);
                                Console.WriteLine("Working Hours: " + work_hrs);
                                Console.WriteLine("Overtime Hours: " + over_hrs);
                                Console.WriteLine("Working hrs salary: " + sal);
                                Console.WriteLine("Overtime Salary: " + Overtimesal);
                                Console.WriteLine("Total salary: " + (sal + Overtimesal ));

                                Console.WriteLine("\n Press 1 for to save these details in txt file..");
                                int ch = int.Parse(Console.ReadLine());
                                if (ch == 1)
                                {
                                    string str1 = $" {ds.Tables["Work_table"].Rows[i][0].ToString()} || {ds.Tables["E_table"].Rows[i][1].ToString()} || {depart} || { (sal + Overtimesal)} ";
                                    using (TextWriter writer = File.AppendText(@"C:\\Users\\Mansi.saxena\\source\\repos\\CarTest\\textdata.txt"))
                                    {
                                        writer.WriteLine(str1);
                                    }
                                    Console.WriteLine("Saved Successfully");
                                }
                            }
                        }

                        break;

                    case 2:
                        Console.WriteLine("Enter the product Id ");
                        int pid = int.Parse(Console.ReadLine());                                             

                        int in_stock = 0;
                        int t_stock = 0;
                        for(int k=0; k < b; k++)
                        {
                            if (pid.ToString() == ds.Tables[4].Rows[k][0].ToString())
                            {
                                for (int l = 0; l < a; l++)
                                {
                                    if (ds.Tables[4].Rows[k][0].ToString() == ds.Tables[3].Rows[l][0].ToString())
                                    {
                                        in_stock = int.Parse(ds.Tables[3].Rows[l][1].ToString());
                                    }
                                }
                                t_stock = int.Parse(ds.Tables[4].Rows[k][2].ToString());
                                Console.WriteLine("Product id: " + ds.Tables["pro_table"].Rows[k][0].ToString());
                                Console.WriteLine("Product name: " + ds.Tables["pro_table"].Rows[k][1].ToString());
                                Console.WriteLine("Total Stock: " + ds.Tables["pro_table"].Rows[k][2].ToString());
                                Console.WriteLine("In Stock: " + in_stock);
                                Console.WriteLine("Used Stock: " + (t_stock - in_stock));
                                Console.WriteLine("Price per piece: " + ds.Tables["pro_table"].Rows[k][3].ToString());
                            }
                        }
                        break;

                    case 3:
                        int[] pro_list = new int[10];
                        int pur_cost = 0;
                        int manufacturing_cost = 0;
                        int labour_cost = 0;
                        int var = 0;
                        for (int p = 0; p < z; p++)
                        {
                            var = int.Parse(ds.Tables[2].Rows[p][2].ToString());
                            labour_cost = labour_cost + var;
                        }
                        for (int q = 0; q < b; q++)
                        {
                            pro_list[q] = int.Parse(ds.Tables[4].Rows[q][3].ToString());
                        }
                        int t = 0;
                        int total_product_cost = 0;

                        Console.WriteLine("Total Labour cost " + labour_cost);
                        Console.WriteLine("3 Mirror cost:" + (pro_list[t]*3));
                        total_product_cost = total_product_cost + (pro_list[t] * 3); t = t + 1;
                        Console.WriteLine("4 Gate cost:" + (pro_list[t] * 4));
                        total_product_cost = total_product_cost + (pro_list[t] * 4); t = t + 1;
                        Console.WriteLine("1 Engine cost:" + (pro_list[t] * 1));
                        total_product_cost = total_product_cost + (pro_list[t] * 1); t = t + 1;
                        Console.WriteLine("4 Seat cost:" + (pro_list[t] * 4));
                        total_product_cost = total_product_cost + (pro_list[t] * 4); t = t + 1;
                        Console.WriteLine(" Paint cost:" + (pro_list[t] * 1));
                        total_product_cost = total_product_cost + (pro_list[t] * 1); t = t + 1;
                        Console.WriteLine("4 Wheel cost: " + (pro_list[t] * 4));
                        total_product_cost = total_product_cost + (pro_list[t] * 4); t = t + 1;
                        Console.WriteLine(" \n ");

                        manufacturing_cost = total_product_cost + labour_cost;
                        int profit = (manufacturing_cost / 10);
                        pur_cost = profit + manufacturing_cost;
                        Console.WriteLine("Total Manufacturing cost: " + manufacturing_cost);
                        Console.WriteLine("Total Purchasing cost: " + pur_cost);
                        Console.WriteLine("Total Profit: " + profit);

                        break;

                    case 4:
                        Console.WriteLine("Details of stock which running low");
                        Productcheck pc = new Productcheck();
                        Predicate<int> pcheck = new Predicate<int>(pc.runninglow);

                        for (int m = 0; m < b; m++)
                        {
                            if (pcheck.Invoke(int.Parse (ds.Tables[3].Rows[m][1].ToString()))) //calling here... 1 pe names aa rhe h
                            {
                                Console.WriteLine("Product name: " + ds.Tables[4].Rows[m][1].ToString());
                                Console.WriteLine("Product quantity in stock: " + ds.Tables[3].Rows[m][1].ToString());
                            }
                        }
                        break;
                }
                Console.WriteLine("\n \n Do you want to continue y/n");
                isRepeat = Console.ReadLine();
            }

        }
    }
}

