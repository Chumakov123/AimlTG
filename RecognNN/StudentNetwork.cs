﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork1
{
    public class StudentNetwork : BaseNetwork
    {
        public override void Save(string filePath)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                //Запись информации о слоях
                for (int i = 0; i < layers.Length; ++i)
                {
                    writer.Write(layers[i].Length);
                    if (i < layers.Length - 1) writer.Write(";");
                }
                writer.Write(Environment.NewLine);
                //Запись информации о нейронах каждого слоя
                for (int i = 1; i < layers.Length; ++i)
                {
                    for (int j = 0; j < layers[i].Length; ++j)
                    {
                        writer.Write($"{layers[i][j].biasWeight};{string.Join(" ", layers[i][j].weights)}");
                        if (j < layers[i].Length - 1) writer.Write("N");
                    }
                    writer.Write(Environment.NewLine);
                }
                //Метод, которым обучена нейросеть
                writer.Write(MethodIndex);
            }
            Debug.WriteLine("Нейросеть сохранена!");
        }
        public override void Load(string filename)
        {
            var lines = File.ReadAllLines(filename);

            // Загрузка информации о слоях
            var layerSizes = lines[0].Split(';').Select(int.Parse).ToArray();
            layers = layerSizes.Select(size => new Neuron[size]).ToArray();
            sensors = layers[0];
            outputs = layers[layers.Length - 1];

            // Загрузка информации о нейронах каждого слоя
            for (int i = 0; i < layers[0].Length; ++i)
            {
                layers[0][i] = new Neuron();
            }
            for (int i = 1; i < lines.Length-1; ++i)
            {
                var neuronInfo = lines[i].Split('N');

                for (int j = 0; j < neuronInfo.Length; ++j)
                {
                    var neuronData = neuronInfo[j].Split(';');
                    double biasWeight = double.Parse(neuronData[0]);
                    double[] weights = neuronData[1].Split(' ').Select(x=>double.Parse(x)).ToArray();

                    layers[i][j] = new Neuron();
                    layers[i][j].weights = weights;
                    layers[i][j].biasWeight = biasWeight;
                    layers[i][j].inputs = layers[i - 1];
                }
            }
            MethodIndex = int.Parse(lines[lines.Length - 1]);
            Debug.WriteLine("Нейросеть загружена!");
        }
        private static double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
        private static double SigmoidDerivative(double x) => x * (1 - x);

        private static double Tanh(double x) => Math.Tanh(x);
        private static double TanhDerivative(double x) => 1 - x * x;

        public static double learningRate = 0.001;
        private static Random r = new Random();
        private class Neuron
        {
            public Neuron[] inputs;
            public double[] weights;
            public double error;
            public double biasWeight;
            public double output;
            public void Activate()
            {
                double weightedSum = 0;

                for (int i = 0; i < inputs.Length; ++i)
                {
                    weightedSum += inputs[i].output * weights[i];
                }
                weightedSum += biasWeight;

                output = Sigmoid(weightedSum);
            }
            public Neuron(Neuron[] prevLayerNeurons)
            {
                if (prevLayerNeurons == null || prevLayerNeurons.Length == 0)
                    return;
                inputs = prevLayerNeurons;
                weights = new double[inputs.Length];
                randomizeWeights();
            }
            public Neuron()
            {

            }
            private void randomizeWeights()
            {
                for (int i = 0; i < weights.Length; ++i)
                {
                    weights[i] = 0.2 * r.NextDouble() - 0.1;
                }
                biasWeight = 0.2 * r.NextDouble() - 0.1;
            }
            public void AdjustWeights()
            {
                for (int i = 0; i < weights.Length; ++i)
                {
                    weights[i] -= learningRate * error * inputs[i].output;
                }
                biasWeight -= learningRate * error;
            }
        }

        private Neuron[] sensors;
        private Neuron[] outputs;
        private Neuron[][] layers; //Здесь создаются нейроны, остальные массивы - просто ссылки

        private readonly Stopwatch watch = new Stopwatch();

        public StudentNetwork(int[] structure)
        {
            InitializeNetwork(structure);
        }
        public StudentNetwork(string path)
        {
            //TODO LoadFromFile(path);
        }
        private void InitializeNetwork(int[] structure)
        {
            MethodIndex = Program.MethodIndex;
            if (structure.Length < 2)
                throw new Exception("Invalid initialize structure");

            layers = new Neuron[structure.Length][];

            //Сенсоры
            layers[0] = new Neuron[structure[0]];
            for (int neuron = 0; neuron < structure[0]; ++neuron)
                layers[0][neuron] = new Neuron(null);

            //Остальные слои, указывая каждому нейрону предыдущий слой
            for (int layer = 1; layer < structure.Length; ++layer)
            {
                layers[layer] = new Neuron[structure[layer]];
                for (int neuron = 0; neuron < structure[layer]; ++neuron)
                    layers[layer][neuron] = new Neuron(layers[layer - 1]);
            }
            //Ссылки на входной и выходной слои
            sensors = layers[0];
            outputs = layers[layers.Length - 1];
        }
        //Однократный запуск сети
        private double[] Run(Sample image)
        {
            double[] result = Compute(image.input);
            image.ProcessPrediction(result);
            return result;
        }
        //Обучение на одном элементе
        public override int Train(Sample sample, double acceptableError, bool parallel)
        {
            int iterations = 0;

            Run(sample);
            double error = sample.EstimatedError();

            while (error > acceptableError)
            {
                //Debug.WriteLine($"e {error} a {acceptableError}");
                Run(sample);
                error = sample.EstimatedError();

                ++iterations;
                BackProp(sample);
            }
            return iterations;
        }
        //Обучение на наборе
        public override double TrainOnDataSet(SamplesSet samplesSet, int epochsCount, double acceptableError, bool parallel)
        {
            //Debug.WriteLine("Обучение");
            watch.Restart();
            double error = double.PositiveInfinity;

            for (int curEpoch = 0; curEpoch < epochsCount; ++curEpoch)
            {
                //Debug.WriteLine($"Эпоха {curEpoch}");
                double errorSum = 0;
                for (int i = 0; i < samplesSet.Count; ++i)
                {
                    if (Train(samplesSet.samples[i], acceptableError, false) == 0)
                        errorSum += samplesSet.samples[i].EstimatedError();
                }
                error = errorSum;
                OnTrainProgress(((curEpoch + 1) * 1.0) / epochsCount, error, watch.Elapsed);
            }
            watch.Stop();
            return error;
        }

        //Ответ нейросети на входные данные
        public override double[] Compute(double[] input)
        {
            //Передаем значениея сенсорам
            for (int i = 0; i < input.Length; ++i)
                sensors[i].output = input[i];

            //Обрабатываем все остальные слои
            for (int i = 1; i < layers.Length; ++i)
                for (int j = 0; j < layers[i].Length; ++j)
                    layers[i][j].Activate();

            return outputs.Select(x => x.output).ToArray();
        }
        //Обратное распространение ошибки
        private void BackProp(Sample image)
        {
            //Ошибка выходного слоя
            for (int i = 0; i < outputs.Length; ++i)
            {
                outputs[i].error = image.error[i];
            }

            // Рассчитываем ошибки для скрытых слоев
            for (int layer = layers.Length - 2; layer > 0; --layer)
            {
                for (int j = 0; j < layers[layer].Length; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < layers[layer + 1].Length; ++k)
                    {
                        sum += layers[layer + 1][k].error * layers[layer + 1][k].weights[j];
                    }
                    layers[layer][j].error = sum * SigmoidDerivative(layers[layer][j].output);
                }
            }

            // Обновляем веса для выходного слоя
            for (int i = 0; i < outputs.Length; ++i)
            {
                outputs[i].AdjustWeights();
            }

            // Обновляем веса для скрытых слоев
            for (int layer = layers.Length - 2; layer > 0; --layer)
            {
                for (int j = 0; j < layers[layer].Length; ++j)
                {
                    layers[layer][j].AdjustWeights();
                }
            }
        }

        public override void Print()
        {
            Console.WriteLine("none");
        }
    }
}