using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class MrDjo
    {
        public string str1, str2;
        public int dva;
        /// <summary>
        /// Основной метод решения
        /// </summary>
        public void Reshenie()
        {
            int[,] mass, mas1;
            int[] Time;
            int schDetal; //счетчик по деталям
            int min, max;            // min - необходим для  основного алгоритма
            int schmin1 = 0, schmin2 = 0;    //счетчики для перестановки минимумов
            int TimeSch = 0; //счетчик времени
            int prost = 0, maxProst, detal = 0;



            Debug.WriteLine("\n\nЗапуск программы/" + DateTime.Now + "/");
            mass = Vvod(ref detal);
            schDetal = detal;
            // формируем массив "деталей"
            mas1 = new int[2, detal];
            max = mass[0, 0];
            //Поиск максимума
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < detal; j++)
                {
                    if (max < mass[i, j])
                    {
                        max = mass[i, j];
                    }
                }
            }
            //Поиск минимума на шагах
            for (int schG = 0; schG < schDetal; schG++)
            {
                min = max;
                for (int schI = 0; schI < 2; schI++)
                {
                    for (int schJ = 0; schJ < detal; schJ++)
                    {
                        if (min > mass[schI, schJ] && mass[schI, schJ] != 0)
                        {
                            min = mass[schI, schJ];
                            schmin1 = schI;//Поиск номера станка (далее по нему производим перестановку)
                            schmin2 = schJ;
                        }
                    }
                }
                //Перестановка
                if (schmin1 == 0)
                {
                    mas1[0, schG] = mass[0, schmin2];
                    mas1[1, schG] = mass[1, schmin2];
                }
                //Если второй станок, то переставляем деталь в конец массива
                else
                {
                    mas1[0, schDetal - 1] = mass[0, schmin2];
                    mas1[1, schDetal - 1] = mass[1, schmin2];
                    schDetal--;
                    schG--; // т.к. элемент ставили в конец, уменьшаем размер счетчика 
                }
                mass[0, schmin2] = 0;
                mass[1, schmin2] = 0;
            }
            //Для подсчета простоя 2 станка
            Time = new int[detal];
            Time[TimeSch] = mass[0, 0];//Первый простой
            for (int o = 1; o < detal; o++)
            {
                //Сумма простоя первого станка (считается с учтением условий решения задач)
                for (int j = 0; j < o; j++)
                {
                    prost += mas1[0, j];
                }
                //Разность простоя первого и второго станка
                for (int j = 0; j < o - 1; j++)
                {
                    prost -= mas1[1, j];
                }
                TimeSch++;
                Time[TimeSch] = prost;
                Debug.WriteLine("Общее время простоя станков: " + prost.ToString());
                prost = 0;
            }
            maxProst = Time.Max();//Максимальный простой 
            Vivod(maxProst, mas1);
        }

        /// <summary>
        /// Метод ввода данных в программу с файла (файл в репозитории (/файлы/vvod.csv))
        /// </summary>
        public int[,] Vvod(ref int detal)//
                            
        {
            int[] ms1 = { }, ms2 = { };
            //Считываем с файла
            try
            {
                using (StreamReader sr = new StreamReader("Vvod.csv"))
                {
                    while (sr.EndOfStream != true)
                    {
                        str1 = sr.ReadLine(); // считываем в 2 строчки т.к. в условии всего 2 строчки и это самый оптимальный вариант
                        str2 = sr.ReadLine(); // считываем 2 строку
                        ms1 = Array.ConvertAll(str1.Split(';'), int.Parse); // парсим в массив
                        ms2 = Array.ConvertAll(str2.Split(';'), int.Parse); // парсим вторую строку во второй массив
                        foreach (int m in ms1)
                        {
                            if (m <= 0)
                                throw new Exception("1");
                        }
                        foreach (int m in ms2)
                        {
                            if (m <= 0)
                                throw new Exception("1");
                        }
                        Debug.WriteLine("Входные данные:\n1 станок: " + str1 + "\n2 строка: " + str2);
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Не верно заданы входные значения");
                Debug.WriteLine("Завершение программы...\n");
                Environment.Exit(0);
            }
            //Количество деталей (общее число столбцов по документу)
            detal = ms1.Length;
            //Массив деталей 
            int[,] mas = new int[2, detal];
            //Из одномерного в двумерный массив 
            for (int i = 0; i < detal; i++)
            {
                mas[0, i] = ms1[i];
                mas[1, i] = ms2[i];
            }
            return mas;
        }

        /// <summary>
        /// Метод записи в файл
        /// </summary>
        /// <param name="maxProst"> Максимальный простой</param>
        /// <param name="mas"> Перераспределенный массив</param>
        /// <returns></returns>
        public bool Vivod(int maxProst, int[,] mas)
        {
            dva = maxProst;
            try
            {
                using (StreamWriter sw =
                 new StreamWriter("Vivod.csv", false, Encoding.Default, 10))
                {
                    Debug.WriteLine("Перераспределение: ");
                    for (int schI = 0; schI < 2; schI++)
                    {
                        for (int schJ = 0; schJ < mas.Length / 2; schJ++)
                        {
                            sw.Write(mas[schI, schJ] + ";");
                            Debug.Write(mas[schI, schJ] + ";");
                        }
                        sw.Write("\n");
                        Debug.Write("\n");
                    }
                    sw.Write("Общее время простоя:" + dva);
                    Debug.WriteLine("Простой:" + dva + "\nОкончание работы программы/" + DateTime.Now + "\n"); ;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Запись прервана: " + e.Message);
                Debug.WriteLine("Завершение программы...\n");
                return false;
            }
        }
    }
}
