using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace Kursovay_Help_Project
{
    public partial class Parser
    {

        public double Root;
        public int N;
        public int Max;
        public int Min = 0;
        public string[] Exp = new string[30];
        public Parser(char[] Expression) 
        { 
            for(int i  = 0; i < Expression.Length; i++) 
            {
                this.Exp[i] = Convert.ToString(Expression[i]);
                if (Expression[i] == '\0') { break; }
            }
             
        }
        public void ParseSemicolon(double x) 
        {

            for (int i = Min; i < Exp.Length; i++) 
            {
                if (Exp[1] == "\0")
                { this.Root = Convert.ToDouble(Exp[0]); break; }

                if (Exp[i] == "(")
                {
                    this.N = 1;
                    for(int j = 0; j < Exp.Length; j++) 
                    {
                        if (Exp[j] == ")") 
                        {
                            this.Max = j;
                            break; 
                        }
                    }
                }
                else 
                {
                    this.N = 0;
                    this.Max = Exp.Length; 
                }
                if(N == 1) { this.Min++; }
                Exp = ParseExpression(x);
                if (N == 1) 
                {Exp =  Revers_Semicolon(Exp, i); this.Min = 0; }
                if (Exp[1] == "\0") 
                { this.Root = Convert.ToDouble(Exp[0]); }
            }
            
        }
        public string[] ParseExpression(double x)
        {
            double Left, Right;
            //To process values greater than 10/100/Эта часть для обработки значений больше 10/100
            //Heavily loads the system/Сильно нагружает систему
            //for (int i = 0; i < Exp.Length; i++)
            //{
            //    for (int j = 0; j < 50; j++)
            //    {
            //        for (int m = 0; m < 50; m++)
            //        {
            //            if (Exp[i] == $"{j}" && Exp[i + 1] == $"{m}")
            //            {
            //                Exp[i] = string.Concat(Exp[i], Exp[i + 1]);
            //                Exp = Revers_Numbers(Exp, i);
            //            }
            //            else if (Exp[i] == $"{m}")
            //            {
            //                break;
            //            }
            //        }
            //        if (Exp[i] == $"{j}")
            //        {
            //            break;
            //        }
            //    }
            //}
            //for (int i = 0; i < Exp.Length; i++)
            //{
            //    if (Exp[i] == ".")
            //    { Exp[i] = ","; }
            //    if (Exp[i] == ",")
            //    {
            //        Exp[i - 1] = string.Concat(Exp[i - 1], ",");
            //        Exp[i - 1] = string.Concat(Exp[i - 1], Exp[i + 1]);
            //        Exp = Revers_For_Func(Exp, i);
            //    }
            //    if (N == 1 && Exp[i + 1] == ")") { break; }
            //}


            for (int i = 0; i < Max - 1; i++)
            {
                if (Exp[i] == "x")
                {
                    Exp[i] = $"{x}";
                }
            }

            for (int i = Min; i < Max - 2; i++) 
            {
                if (Exp[i] == "c" && Exp[i+1] == "o" && Exp[i+2] == "s")
                {
                    if (Exp[i + 3] == "(")
                    {
                        this.Min = Convert.ToInt32(i + 5);
                        ParseSemicolon(x);
                        Exp = Revers_Once(Exp, i+3);
                        this.Min = 0;
                        this.N = 0;
                    }
                    Exp[i] = Convert.ToString(Math.Cos(Convert.ToDouble(Exp[i+3])));
                    Exp = Revers_For_Trig(Exp,i);

                    if (Exp[i] == "\0") { break; };
                }
                else if (Exp[i] == "s" && Exp[i + 1] == "i" && Exp[i + 2] == "n")
                {
                    if (Exp[i + 3] == "(")
                    {
                        this.Min = Convert.ToInt32(i + 5);
                        ParseSemicolon(x);
                        Exp = Revers_Once(Exp, i + 3);
                        this.Min = 0;
                        this.N = 0;
                    }
                    Exp[i] = Convert.ToString(Math.Sin(Convert.ToDouble(Exp[i+3])));
                    Exp = Revers_For_Trig(Exp, i);
                    if (Exp[i] == "\0") { break; };
                }
                else if (Exp[i] == "l" && Exp[i+1] == "o" && Exp[i+2] == "g") 
                {
                    if (Exp[i + 3] == "(")
                    {
                        this.Min = Convert.ToInt32(i + 5);
                        ParseSemicolon(x);
                        Exp = Revers_Once(Exp, i + 3);
                        this.Min = 0;
                        this.N = 0;
                    }
                    Exp[i] = Convert.ToString(Math.Log(Convert.ToDouble(Exp[i + 3])));
                    Exp = Revers_For_Trig(Exp,i);
                    if (Exp[i+1] == "\0") { break; };
                }
            }
            for (int i = Min; i < Max - 1; i++)
            {
                if (Exp[i] == "e")
                {
                    if (Exp[i + 1] == "^")
                    {
                        Exp[i] = Convert.ToString(Math.Exp(Convert.ToDouble(Exp[i + 2])));
                        Exp = Revers_For_Func(Exp, i+1); i--;
                    }
                    else
                    {
                        Exp[i] = Convert.ToString(Math.Exp(1));
                    }
                    if (Exp[i+1] == "\0") { break; };
                }
            }
            for (int i = Min; i < Max-1; i++)
            {
                if (Exp[i + 1] == "(" && N != 1)
                {
                    this.Min = Convert.ToInt32(i + 1);
                    ParseSemicolon(x);
                    this.Min = 0;
                    this.N = 0;
                }
                if (Exp[i] == "^")
                {
                    Left = Convert.ToDouble(Exp[i - 1]);
                    Right = Convert.ToDouble(Exp[i + 1]);
                    Exp[i - 1] = Convert.ToString(Math.Pow(Left, Right));
                    Exp = Revers_For_Func(Exp, i); i--;
                }
                if (Exp[i] == "\0") { break; };
                if (N == 1 && Exp[i + 1] == ")") { break; }
            }


            for (int i = Min; i < Max-1; i++)
            {
                if (Exp[i + 1] == "(" && N != 1)
                {
                    this.Min = Convert.ToInt32(i + 1);
                    ParseSemicolon(x);
                    this.Min = 0;
                    this.N = 0;
                }
                if (Exp[i] == "*")
                {
                    Left = Convert.ToDouble(Exp[i - 1]);
                    Right = Convert.ToDouble(Exp[i + 1]);
                    Exp[i - 1] = Convert.ToString(Left * Right);
                    Exp = Revers_For_Func(Exp, i); i--;
                }
                if (Exp[i] == "\0") { break; };
                if (N == 1 && Exp[i + 1] == ")") { break; }
            }


            for (int i = Min; i < Max-1; i++)
            {
                if (Exp[i + 1] == "(" && N != 1)
                {
                    this.Min = Convert.ToInt32(i + 1);
                    ParseSemicolon(x);
                    this.Min = 0;
                    this.N = 0;
                }
                if (Exp[i] == "/")
                {
                    Left = Convert.ToDouble(Exp[i - 1]);
                    Right = Convert.ToDouble(Exp[i + 1]);
                    Exp[i - 1] = Convert.ToString(Left / Right);
                    Exp = Revers_For_Func(Exp, i); i--;
                }
                if (Exp[i] == "\0") { break; };
                if (N == 1 && Exp[i+1] == ")") { break; }
            }


            for (int i = Min; i < Max-1; i++)
            {
                if (Exp[i + 1] == "(" && N != 1)
                {
                    this.Min = Convert.ToInt32(i + 1);
                    ParseSemicolon(x);
                    this.Min = 0;
                    this.N = 0;
                }
                if (Exp[i] == "+")
                {
                    Left = Convert.ToDouble(Exp[i - 1]);
                    Right = Convert.ToDouble(Exp[i + 1]);
                    Exp[i - 1] = Convert.ToString(Left + Right);
                    Exp = Revers_For_Func(Exp, i); i--;
                }
                if (Exp[i] == "\0") { break; };
                if (N == 1 && Exp[i + 1] == ")") { break; }
            }


            for (int i = Min; i < Max-1; i++)
            {
                if (Exp[i + 1] == "(" && N != 1)
                {
                    this.Min = Convert.ToInt32(i + 1);
                    ParseSemicolon(x);
                    this.Min = 0;
                    this.N = 0;
                }
                if (Exp[i] == "-")
                {
                    Left = Convert.ToDouble(Exp[i - 1]);
                    Right = Convert.ToDouble(Exp[i + 1]);
                    Exp[i - 1] = Convert.ToString(Left - Right);
                    Exp = Revers_For_Func(Exp, i); i--;
                }
                if (Exp[i] == "\0") { break; };
                if (N == 1 && Exp[i + 1] == ")") { break; }
            }


            if (N == 0) 
            { 
                if (Exp[1] == "\0")
                {
                    return Exp;
                }
            }
            else if (N == 1) 
            {
                if (Exp[2] == ")")
                {
                    return Exp;
                }
            }
            return Exp;
        }
        string[] Revers_For_Trig(string[] Exp, int i)
        {
            i++;
            for (; i < Exp.Length; i++)
            {
                Exp[i] = Exp[i + 3];
                if (Exp[i + 3] == "\0") { break; }
            }
            return Exp;
        }
        string[] Revers_Once(string[] Exp, int i)
        {
            for (; i < Exp.Length; i++)
            {
                if (Exp[i] == "(")
                {
                    Exp[i] = Exp[i + 1];
                }
                else if (Exp[i] == ")")
                {
                    Exp[i] = Exp[i + 1];break;
                }
                else Exp[i] = Exp[i + 2];
                if (Exp[i] == "\0") { break; }
            }
            return Exp;
        }
        string[] Revers_For_Func(string[] Exp,int i) 
        {
            for (;i<Exp.Length;i++)
            {
                Exp[i] = Exp[i + 2];
                if (Exp[i+2] == "\0") { break; }
            }
            return Exp;
        }
        //string[] Revers_Numbers(string[] Exp, int i)
        //{
        //    i++;
        //    for (; i < Exp.Length; i++)
        //    {
        //        Exp[i] = Exp[i + 1];
        //        if (Exp[i + 1] == "\0") { break; }
        //    }
        //    return Exp;
        //}
        string[] Revers_Semicolon(string[] Exp, int i)
        {
            Exp[i] = Exp[i+1];
            for (; i < Exp.Length; i++)
            {
                Exp[i+1] = Exp[i + 3];
                if (Exp[i + 3] == "\0") { break; }
            }
            return Exp;
        }
    }
}
