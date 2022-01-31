using System;
using System.Collections.Generic;
using System.Linq;

namespace Прийом_у_директора
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            List<PairTime> schedule = new List<PairTime>();

            for (int i = 0; i < n; i++)
            {
                var time = Console.ReadLine().Split().ToList();// порезали на начало и конец
                List<int> buf = new List<int>(); // массив из 4х
                for (int j = 0; j < 2; j++)
                {
                    var hoursAndMinutes = time[j].Split(":").Select(int.Parse).ToList(); // режем на часы и минуты
                    buf.AddRange(hoursAndMinutes);
                }
                schedule.Add(new PairTime(buf[0], buf[1], buf[2], buf[3]));
            }

            Sort.BubbleSort(schedule);
            int result = 1;
           
            for (int i=0; i < schedule.Count-1; i++)
            {
                if (schedule[i].isCrossAfterSort(schedule[i + 1]))
                {
                    schedule.RemoveAt(i + 1);
                    i--;
                }
                else
                    result++;
            }
            Console.WriteLine(result);
        }
        static void SwapObj(PairTime e1, PairTime e2)
        {
            PairTime temp = new PairTime(0,0,0,0);
            temp.CloneFrom(e1);
            e1.CloneFrom(e2);
            e2.CloneFrom(temp);
        }
        public class Sort
        {
            public static void BubbleSort(List<PairTime> list)
            {
                for (int i=0; i < list.Count; i++)
                {
                    for (int j = i+1; j < list.Count; j++)
                        if (list[i].IsBigger(list[j]))
                            SwapObj(list[i], list[j]);
                }
            }
        }
        public class PairTime
        {
            int _hour_begin;
            int _minute_begin;
            int _hour_end;
            int _minute_end;
            public PairTime(int hour_begin, int minute_begin, int hour_end, int minute_end)
            {
                _hour_begin = hour_begin;
                _minute_begin = minute_begin;
                _hour_end = hour_end;
                _minute_end = minute_end;
            }
            public bool IsBigger(PairTime nextPair)
            {
                if (this._hour_end > nextPair._hour_end ||
                    (this._hour_end == nextPair._hour_end && this._minute_end > nextPair._minute_end))
                    return true;

                else return false;
            }
            public void CloneFrom(PairTime source)
            {
                this._hour_begin = source._hour_begin;
                this._hour_end = source._hour_end;
                this._minute_begin = source._minute_begin;
                this._minute_end = source._minute_end;
            }
            public bool isCrossAfterSort(PairTime nextPair)
            {
                //если некст раньше
                if (this._hour_end > nextPair._hour_begin ||
                    (this._hour_end == nextPair._hour_begin && this._minute_end > nextPair._minute_begin))
                    return true;

                return false;
            }
        }

    }
}
