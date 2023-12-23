using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork1
{
    class Loader
    {
        public static Loader instance;

        public bool[] image = new bool[Program.size * Program.size];
        private Random rand = new Random();

        public FigureType currentFigure = FigureType.Undef;

        public int FigureCount { get; set; } = 10;

        // Создание обучающей выборки
        public SamplesSet samples = new SamplesSet();
        // Создание тестовой выборки
        public SamplesSet samplesTest = new SamplesSet();

        public Controller controller = null;
        public NeuralNetworksStand mainForm;

        public Loader(Controller _controller, NeuralNetworksStand mainForm = null)
        {
            instance = this;
            controller = _controller;
            this.mainForm = mainForm;
        }

        // функция очистки изображений
        private void ClearImage()
        {
            for (int i = 0; i < Program.size; ++i)
                for (int j = 0; j < Program.size; ++j)
                    image[i * Program.size + j] = false;
        }

        // геттер обучающей выборки
        public SamplesSet LoadSampleSet()
        {
            return samples;
        }

        // геттер тестовой выборки
        public SamplesSet LoadSampleSetTest()
        {
            return samplesTest;
        }

        int threshold = 50;

        // Функция создание файла с векторами признаков для всех методов
        public void CreateDataset()
        {
            // Выборка для метода сумм
            SamplesSet MethodSumSamples = new SamplesSet();
            // Выборка для метода чередования
            SamplesSet MethodAltSamples = new SamplesSet();

            SamplesSet MethodPixelSamples = new SamplesSet();
            SamplesSet MethodCombinedSamples = new SamplesSet();
            // путь к dataset
            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset";
            // получение всех директорий
            List<string> directories = Directory.GetDirectories(path).ToList();

            for (int i = 0; i < FigureCount; i++)
            {
                // получение всех файлов из директории
                List<string> files = Directory.GetFiles(directories[i]).ToList();
                for (int j = 0; j < files.Count(); j++)
                {
                    ClearImage();

                    // загрузка изображения
                    Bitmap bmp = new Bitmap(Image.FromFile(files[j]));
                    controller.processor.ProcessImage(bmp, true);
                    Bitmap bmp48 = controller.processor.processed;

                    currentFigure = (FigureType)i;

                    // получение изображения
                    for (int x = 0; x < Program.size; x++)
                    {
                        for (int y = 0; y < Program.size; y++)
                        {
                            Color newColor = bmp48.GetPixel(x, y);
                            if (newColor.R < threshold || newColor.G < threshold || newColor.B < threshold)
                            {
                                image[x * Program.size + y] = true;
                            }
                        }
                    }

                    // добавление в выборку метода суммирования
                    MethodSumSamples.AddSample(MethodSum());

                    // добавление в выборку метода чередования
                    MethodAltSamples.AddSample(MethodAlt());

                    // добавление в выборку метода суммирования
                    MethodPixelSamples.AddSample(MethodPixel());

                    // добавление в выборку метода чередования
                    MethodCombinedSamples.AddSample(MethodCombined());
                }
            }

            SaveSamples("sum.txt", MethodSumSamples);
            SaveSamples("alt.txt", MethodAltSamples);
            SaveSamples("combo.txt", MethodCombinedSamples);
            SaveSamples("pixel.txt", MethodPixelSamples);
        }

        // Функция хэширования выборки в файл
        private void SaveSamples(string nameFile, SamplesSet MethodSamples)
        {
            // путь к dataset
            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset\\";
            string pathFile = path + nameFile;
            //if (!File.Exists(pathFile))
            //{
                using (StreamWriter sw = File.CreateText(pathFile))
                {
                    foreach (Sample sample in MethodSamples)
                    {
                        string text = ((int)sample.actualClass).ToString() + ";";
                        for (int i = 0; i < sample.input.Length; i++)
                        {
                            if (i != sample.input.Length - 1)
                            {
                                text += sample.input[i].ToString() + " ";
                            }
                            else
                            {
                                text += sample.input[i].ToString();
                            }
                        }
                        sw.WriteLine(text);
                    }
                }
            //}
        }

        // Функция загрузки файла с векторами признаков для разных методов
        public void LoadDataset(int method)
        {
            string nameFile = "";
            int size = 0;
            switch (method)
            {
                case 0: // метод сложение пикселей по строке и столбцу
                    nameFile = "sum.txt";
                    size = Program.size * 2;
                    break;
                case 1:
                    nameFile = "pixel.txt";
                    size = Program.size * Program.size;
                    break;
                case 2: // метод чередования пикселей
                    nameFile = "alt.txt";
                    size = Program.size * 2;
                    break;
                case 3:
                    nameFile = "combo.txt";
                    size = Program.size * 2;
                    break;
                default:
                    break;
            }

            SamplesSet MethodSamples = new SamplesSet();
            SamplesSet MethodSamplesTest = new SamplesSet();
            // получение директории,  где хранится файл с векторами признаков
            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset\\";
            string pathFile = path + nameFile;
            int c = 1;
            using (StreamReader sr = File.OpenText(pathFile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    List<string> splitSep = s.Split(';').ToList();
                    // загружаем из файла все классы, пока не достигли выбранного
                    if (Int32.Parse(splitSep[0]) == FigureCount)
                    {
                        break;
                    }
                    List<string> splitSpace = splitSep[1].Split(' ').ToList();

                    double[] input = new double[size];
                    for (int k = 0; k < size; k++)
                        input[k] = 0;

                    for (int i = 0; i < splitSpace.Count(); i++)
                    {
                        input[i] = double.Parse(splitSpace[i]);
                    }
                    currentFigure = (FigureType)Int32.Parse(splitSep[0]);
                    if (MethodSamples.samples.Count() < 15 * c)
                        MethodSamples.AddSample(new Sample(input, FigureCount, currentFigure)); // берем по 15 векторов для обучения
                    else
                    {
                        MethodSamplesTest.AddSample(new Sample(input, FigureCount, currentFigure)); // берем по 5 векторов для обучения
                        if (MethodSamplesTest.samples.Count() > 5 * c) c += 1;
                    }
                }
            }

            samples = MethodSamples;
            samplesTest = MethodSamplesTest;
        }

        Bitmap lastImage;
        // Функция загрузки случайного изображения для тестирования в зависимости от метода(при нажатии на экран)
        public Sample LoadImage(int method, bool isInput = false, bool last = false)
        {
            ClearImage();

            Bitmap bmp48;
            if (!last)
            {
                bmp48 = !isInput ? GetRandomImage() : GetInputImage();
                lastImage = bmp48;
            }
            else
            {
                bmp48 = lastImage;
            }
            // получение изображения
            for (int x = 0; x < Program.size; x++)
            {
                for (int y = 0; y < Program.size; y++)
                {
                    Color newColor = bmp48.GetPixel(x, y);
                    if (newColor.R < threshold || newColor.G < threshold || newColor.B < threshold)
                    {
                        image[x * Program.size + y] = true;
                    }
                }
            }

            switch (method)
            {
                case 0:
                    return MethodSum(); // создание Sample с помощью метода суммирования
                case 1:
                    return MethodPixel();
                case 2:
                    return MethodAlt(); // создание Sample с помощью метода чередования
                case 3:
                    return MethodCombined();
                default:
                    return null;
            }
        }

        public Sample LoadImage(int method, Bitmap bmp48)
        {
            ClearImage();
            controller.processor.ProcessImage(bmp48, true);
            var res = controller.processor.processed;
            res.Save("tg_input.jpg");
            // получение изображения
            for (int x = 0; x < Program.size; x++)
            {
                for (int y = 0; y < Program.size; y++)
                {
                    Color newColor = res.GetPixel(x, y);
                    if (newColor.R < threshold || newColor.G < threshold || newColor.B < threshold)
                    {
                        image[x * Program.size + y] = true;
                    }
                }
            }

            switch (method)
            {
                case 0:
                    return MethodSum(); // создание Sample с помощью метода суммирования
                case 1:
                    return MethodPixel();
                case 2:
                    return MethodAlt(); // создание Sample с помощью метода чередования
                case 3:
                    return MethodCombined();
                default:
                    return null;
            }
        }

        // Функция получения случайного изображения
        private Bitmap GetRandomImage()
        {
            // путь к dataset
            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset";
            // получение всех директорий
            List<string> directories = Directory.GetDirectories(path).ToList();

            // получение случайной директории(класса)
            int randomDirectory = rand.Next(0, FigureCount);
            // получение всех файлов
            List<string> files = Directory.GetFiles(directories[randomDirectory]).ToList();

            // получение случайной файла
            int randomFile = rand.Next(0, files.Count());

            currentFigure = (FigureType)randomDirectory;

            // загрузка изображения
            Bitmap bmp = new Bitmap(Image.FromFile(files[randomFile]));
            controller.processor.ProcessImage(bmp, true);
            return controller.processor.processed;
        }

        // Функция для получения зафиксированного изображения
        private Bitmap GetInputImage()
        {
            // путь к фото
            string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset";
            string pathFile = path + "\\input.jpg";

            currentFigure = FigureType.Undef;

            // загрузка изображения
            Bitmap bmp = new Bitmap(Image.FromFile(pathFile));
            //if (bmp.Width > Program.size)
            {
                controller.processor.ProcessImage(bmp, true);

                return controller.processor.processed;
            }
            //else
            {
                //return bmp;
            }
        }

        // метод сумм
        private Sample MethodSum()
        {
            // input для метода суммирования
            double[] inputSum = new double[Program.size * 2];
            for (int k = 0; k < Program.size * 2; k++)
                inputSum[k] = 0;

            // получение вектора признаков метода суммирования
            for (int x = 0; x < Program.size; x++)
                for (int y = 0; y < Program.size; y++)
                {
                    if (!image[x * Program.size + y])
                    {
                        inputSum[x] += 1;
                        inputSum[Program.size + y] += 1;
                    }
                }

            // добавление в выборку метода суммирования
            return new Sample(inputSum, FigureCount, currentFigure);
        }
        // Попиксельный метод
        private Sample MethodPixel()
        {
            double[] inputPixel = new double[Program.size * Program.size];
            for (int k = 0; k < Program.size * Program.size; k++)
                inputPixel[k] = 0;

            for (int x = 0; x < Program.size; x++)
            {
                for (int y = 0; y < Program.size; y++)
                {
                    if (image[x * Program.size + y])
                    {
                        inputPixel[x * Program.size + y] = 1;
                    }
                }
            }

            return new Sample(inputPixel, FigureCount, currentFigure);
        }

        // метод чередования
        private Sample MethodAlt()
        {
            // input для метода чередования
            double[] inputAlt = new double[Program.size * 2];
            for (int k = 0; k < Program.size * 2; k++)
                inputAlt[k] = 0;


            // получение вектора признаков метода чередования
            for (int x = 0; x < Program.size; x++)
                for (int y = 0; y < Program.size; y++)
                    if (x - 1 > 0 && image[x * Program.size + y] != image[(x - 1) * Program.size + y])
                    {
                        inputAlt[x] += 1;
                    }

            for (int x = 0; x < Program.size; x++)
                for (int y = 0; y < Program.size; y++)
                    if (y - 1 > 0 && image[x * Program.size + y] != image[x * Program.size + y - 1])
                    {
                        inputAlt[Program.size + y] += 1;
                    }

            // добавление в выборку метода чередования
            return new Sample(inputAlt, FigureCount, currentFigure);
        }

        private Sample MethodCombined()
        {
            // input для метода чередования
            double[] inputCombo = new double[Program.size * 2];
            for (int k = 0; k < Program.size * 2; k++)
                inputCombo[k] = 0;

            // получение вектора признаков метода суммирования
            for (int x = 0; x < Program.size; x++)
                for (int y = 0; y < Program.size; y++)
                {
                    if (!image[x * Program.size + y])
                    {
                        inputCombo[x] += 1;
                        inputCombo[Program.size + y] += 1;
                    }
                }

            // получение вектора признаков метода чередования
            for (int x = 0; x < Program.size; x++)
                for (int y = 0; y < Program.size; y++)
                    if (x - 1 > 0 && image[x * Program.size + y] != image[(x - 1) * Program.size + y])
                    {
                        inputCombo[x] += 1;
                    }

            for (int x = 0; x < Program.size; x++)
                for (int y = 0; y < Program.size; y++)
                    if (y - 1 > 0 && image[x * Program.size + y] != image[x * Program.size + y - 1])
                    {
                        inputCombo[Program.size + y] += 1;
                    }

            // добавление в выборку метода чередования
            return new Sample(inputCombo, FigureCount, currentFigure);
        }

        // Функция отрисовки изображения на pictureBox
        public Bitmap GenImage()
        {
            Bitmap drawArea = new Bitmap(Program.size, Program.size);
            for (int i = 0; i < Program.size; ++i)
                for (int j = 0; j < Program.size; ++j)
                    if (!image[i * Program.size + j]) drawArea.SetPixel(i, j, Color.Black);
            return drawArea;
        }
    }
}
