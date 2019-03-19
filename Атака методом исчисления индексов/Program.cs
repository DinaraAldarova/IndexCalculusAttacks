using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Атака_методом_исчисления_индексов
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public class Culc
    {
        const int a = 10;//alpha
        const int b = 23;//beta
        const int p = 2633;

        //const int a = 4;//alpha
        //const int b = 22;//beta
        //const int p = 3361;

        const int m = 4;//количество простых чисел
        readonly int[] pj = { 2, 3, 5, 7 };//Г - факторная база с B=7 - границей факторной базы
        Random x = new Random();
        public string report = "";
        public Culc()
        {
            report += "a = " + a.ToString() + "\n"
                + "b = " + b.ToString() + "\n"
                + "p = " + p.ToString() + "\n"
                + "pj = { " + pj[0].ToString() + ", "
                + pj[1].ToString() + ", "
                + pj[2].ToString() + ", "
                + pj[3].ToString() + "}\n\n";

            int[][] e = new int[m][];
            for (int i = 0; i < m; i++)
            {
                int buf3 = 0;
                e[i] = new int[m];
                do
                {
                    int buf = x.Next(2, p - 1);//исходное число
                    BigInteger bigInteger = BigInteger.ModPow(b, buf, p);
                    int buf2 = Convert.ToInt32(bigInteger.ToString());//результат по модулю
                    buf3 = buf2;//здесь будет произведение оставшихся множителей
                    for (int j = 0; j < m; j++)
                    {
                        e[i][j] = 0;
                        while (buf3 % pj[j] == 0)
                        {
                            buf3 /= pj[j];
                            e[i][j]++;
                        }
                    }
                    //report += "Взяли e = " + buf.ToString() + ", вычислили " + buf2.ToString() + ", разложили на ";
                    //bool start = false;
                    //for (int j = 0; j < m; j++)
                    //{
                    //    if (e[i][j] != 0)
                    //    {
                    //        if (start)
                    //            report += " * ";
                    //        else
                    //            start = true;
                    //        report += pj[j].ToString();
                    //        if (e[i][j] > 1)
                    //            report += "^" + e[i][j].ToString();
                    //    }
                    //}
                    //report += " * " + buf3.ToString() +  " (mod " + p.ToString() + ")\n";
                }
                while (buf3 != 1);
                //report += "Взяли число!\n";
            }

            bool start;
            for (int i = 0; i < m; i++)
            {
                report += "e = ";
                start = false;
                for (int j = 0; j < m; j++)
                {
                    if (e[i][j] != 0)
                    {
                        if (start)
                            report += " * ";
                        else
                            start = true;
                        report += pj[j].ToString();
                        if (e[i][j] > 1)
                            report += "^" + e[i][j].ToString();
                    }
                }
                report += " (mod " + (p-1).ToString() + ")\n";
            }

            int[] log_b_pj = culc_log();
            int[] buf4 = new int[m];
            for (int i = 0; i < m; i++)
            {
                buf4[i] = 0;
                start = false;
                for (int j = 0; j < m; j++)
                {
                    buf4[i] += log_b_pj[j] * e[i][j];
                    if (e[i][j] > 0)
                    {
                        if (start)
                            report += " + ";
                        else
                            start = true;
                        if (e[i][j] > 1)
                            report += e[i][j].ToString() + " * ";
                        report += log_b_pj[j].ToString();
                    }
                }
                buf4[i] = buf4[i] % (p - 1);
                report += " = " + buf4[i].ToString() + " (mod " + (p - 1).ToString() + ")\n";
            }
            report += "\n";

            int[] rj = new int[m];
            int buf5 = 0, buf6, r;
            do
            {
                r = x.Next(p - 1);
                BigInteger bigInteger = BigInteger.ModPow(b, r, p);
                buf6 = Convert.ToInt32(bigInteger.ToString());
                buf6 = (a * buf6) % p;
                
                buf5 = buf6;//здесь будет произведение оставшихся множителей
                for (int j = 0; j < m; j++)
                {
                    rj[j] = 0;
                    while (buf5 % pj[j] == 0)
                    {
                        buf5 /= pj[j];
                        rj[j]++;
                    }
                }
                //report += "ab^r = " + buf6 + " = ";
                //start = false;
                //for (int j = 0; j < m; j++)
                //{
                //    if (rj[j] != 0)
                //    {
                //        if (start)
                //            report += " * ";
                //        else
                //            start = true;
                //        report += pj[j].ToString();
                //        if (rj[j] > 1)
                //            report += "^" + rj[j].ToString();
                //    }
                //}
                //report += " * " + buf5.ToString() + " (mod " + p.ToString() + ")\n";
            }
            while (buf5 != 1);
            //report += "Взяли число!\n";

            report += a.ToString() + " * " + b.ToString() + "^" + r.ToString() + " = " + buf6.ToString() + " = ";
            start = false;
            for (int j = 0; j < m; j++)
            {
                if (rj[j] != 0)
                {
                    if (start)
                        report += " * ";
                    else
                        start = true;
                    report += pj[j].ToString();
                    if (rj[j] > 1)
                        report += "^" + rj[j].ToString();
                }
            }
            report += " (mod " + (p).ToString() + ")\n";

            report += "log_" + b.ToString() + " " + a.ToString() + " = -" + r.ToString();
            int log = -r;
            for (int j = 0; j < m; j++)
            {
                log += rj[j] * log_b_pj[j];
                report += " + " + rj[j].ToString() + "*" + log_b_pj[j].ToString();
            }
            while (log < 0)
                log += p-1;
            log = log % (p - 1);
            report += " = " + log.ToString() + " (mod " + p.ToString() + ")";
        }
        public int[] culc_log()
        {
            report += "\n";
            //report += "Вычисляю логарифмы:\n";
            int[] b_pow = new int[p];
            int b_pow_i = 1;
            b_pow[0] = b_pow_i;
            for (int i = 1; i < p; i++)
            {
                b_pow[i] = (b_pow[i - 1] * b) % p;
            }

            int[] res = new int[m] { -1, -1, -1, -1 };
            int j = 0;
            bool end = false;
            for (; j < b_pow.Length && !end; j++)
            {
                //report += b.ToString() + "^" + j.ToString() + "=" + b_pow[j].ToString() + "\n";
                if (b_pow[j] == pj[0] || b_pow[j] == pj[1] || b_pow[j] == pj[2] || b_pow[j] == pj[3])
                {
                    //report += "Значение совпало!\n";
                    if (b_pow[j] == pj[0])
                    {
                        //report += "Значение совпало c " + pj[0] + "!\n";
                        if (res[0] == -1)
                        {
                            res[0] = j;
                            //report += "Добавили значение 0: log_" + b.ToString() + " " + pj[0] + " = " + res[0] + "\n";
                        }
                        else
                        {
                            //report += "Но такое значение уже было найдено!\n";
                        }
                    }
                    else if (b_pow[j] == pj[1])
                    {
                        //report += "Значение совпало c " + pj[1] + "!\n";
                        if (res[1] == -1)
                        {
                            res[1] = j;
                            //report += "Добавили значение 1: log_" + b.ToString() + " " + pj[1] + " = " + res[1] + "\n";
                        }
                        else
                        {
                            //report += "Но такое значение уже было найдено!\n";
                        }
                    }
                    else if (b_pow[j] == pj[2])
                    {
                        //report += "Значение совпало c " + pj[2] + "!\n";
                        if (res[2] == -1)
                        {
                            res[2] = j;
                            //report += "Добавили значение 2: log_" + b.ToString() + " " + pj[2] + " = " + res[2] + "\n";
                        }
                        else
                        {
                            //report += "Но такое значение уже было найдено!\n";
                        }
                    }
                    else if (b_pow[j] == pj[3])
                    {
                        //report += "Значение совпало c " + pj[3] + "!\n";
                        if (res[3] == -1)
                        {
                            res[3] = j;
                            //report += "Добавили значение 3: log_" + b.ToString() + " " + pj[3] + " = " + res[3] + "\n";
                        }
                        else
                        {
                            //report += "Но такое значение уже было найдено!\n";
                        }
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                report += "log_" + b.ToString() + " " + pj[i].ToString() + " = " + res[i].ToString() + " (mod " + (p-1).ToString() + ")\n";
            }
            report += "\n";
            return res;
        }
    }
}
