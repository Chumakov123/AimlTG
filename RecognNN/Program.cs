using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork1
{
    /// <summary>
    /// Тип цифры
    /// </summary>
    public enum FigureType : byte { Play = 0, Pause, Stop, Rec, SpeedUp, SpeedDown, NextFrame, PrevFrame, SkipForward, SkipBackward, Undef };
    static class Program
    {
        public static int MethodIndex = 0;
        public static Dictionary<FigureType, string> titles = new Dictionary<FigureType, string>
        {
            { FigureType.Play, "Старт"},
            { FigureType.Pause, "Пауза"},
            { FigureType.Stop, "Стоп"},
            { FigureType.Rec, "Запись"},
            { FigureType.SpeedUp, "Перемотка вперед"},
            { FigureType.SpeedDown, "Перемотка назад"},
            { FigureType.NextFrame, "Следующий кадр"},
            { FigureType.PrevFrame, "Предыдущий кадр"},
            { FigureType.SkipForward, "Следующие видео"},
            { FigureType.SkipBackward, "Предыдущее видео"},
            { FigureType.Undef, "Undef"},
        };
        public static Dictionary<FigureType, string> folders;
        public static int size = 48;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NeuralNetworksStand(new Dictionary<string, Func<int[], BaseNetwork>>
            {
                // Тут можно добавить свои нейросети
                {"Первый персептрон", structure => new StudentNetwork(structure)},
                {"Второй персептрон", structure => new StudentNetwork(structure)},
            }));
        }

    }
}