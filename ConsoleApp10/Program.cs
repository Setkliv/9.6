using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Unit9._6
{
    internal class Program
    {
        public class MyException : Exception
        {
            public MyException()
            { }
            public MyException(string message) : base(message)
            {

            }
        }
        public class NumReader
        {
            public delegate void NumEnteredDelegate(int num, List<string> l);
            public event NumEnteredDelegate NumEnteredEvent;
            public void KeyRead(List<string> l)
            {
                Console.WriteLine("\n \nПожалуйста, нажмите клавишу 1 или 2 \n Ключ 1 - сортирует А-Я \n Ключ 2 - сортирует Я-А ");
                int num = Convert.ToInt32(Console.ReadLine());
                if (num != 1 && num != 2) throw new MyException("Был нажат неправильный номер. Пожалуйста, попробуйте снова.");
                NumEntered(num, l);
            }
            protected virtual void NumEntered(int num, List<string> l)
            {
                NumEnteredEvent?.Invoke(num, l);
            }
        }
        static void Main(string[] args)
        {
            MyException ex1 = new MyException("Произошла какая-то ошибка.");
            Exception ex2 = new ArgumentException();
            Exception ex3 = new IndexOutOfRangeException();
            Exception ex4 = new KeyNotFoundException();
            Exception ex5 = new DriveNotFoundException();
            Exception[] exArray = new Exception[5];
            exArray[0] = ex1;
            exArray[1] = ex2;
            exArray[2] = ex3;
            exArray[3] = ex4;
            exArray[4] = ex5;
            Console.WriteLine("--->>> Task 1 <<<---");
            exIteration(exArray, 0);
            Console.WriteLine("--->>> ------ <<<---");
            List<string> sName = new List<string>();
            sName.Add("Иванов");
            sName.Add("Петров");
            sName.Add("Сидоров");
            sName.Add("Соболев");
            sName.Add("Карданов");
            Console.WriteLine(" \n \n--->>> Task 2 <<<---");
            Console.WriteLine("Списком источников является:");
            foreach (string st in sName) Console.WriteLine(st);
            NumReader numReader = new NumReader();
            numReader.NumEnteredEvent += SortType;
            while (true)
            {
                try
                {
                    numReader.KeyRead(sName);
                }
                catch (FormatException)
                {
                    Console.Write("Была нажата неправильная клавиша");
                }
            }
        }
        static void SortType(int num, List<string> l)
        {
            switch (num)
            {
                case 1:
                    {
                        l.Sort();
                        Console.WriteLine("\n А-Я отсортированный список:");
                        foreach (string st in l) Console.WriteLine(st);
                    }
                    break;
                case 2:
                    {
                        l.Sort();
                        l.Reverse();
                        Console.WriteLine("\n Я-А отсортированный список:");
                        foreach (string st in l) Console.WriteLine(st);
                    }
                    break;
            }
        }
        static void exIteration(Exception[] exArr, int i)
        {
            try
            {
                throw exArr[i];

            }
            catch
            {
                Console.WriteLine(exArr[i].Message);
                if (i < exArr.Length - 1)
                {
                    i++;
                    exIteration(exArr, i);
                }
            }
        }
    }
}

