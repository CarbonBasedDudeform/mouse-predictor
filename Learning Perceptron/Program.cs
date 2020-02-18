using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Learning_Perceptron
{
    class Program
    {
        static void Main(string[] args)
        {
            List<float> mousemovements = new List<float>();
            List<float> weights = new List<float>();

            StreamReader reader = new StreamReader("mouse.txt");
            Random rand = new Random(DateTime.Now.Millisecond);

            while (!reader.EndOfStream)
            {
                mousemovements.Add(float.Parse(reader.ReadLine()));
            }

            //add five weights
            for (int i = 0; i < 5; i++)
            weights.Add((float)rand.NextDouble());
            
            Perceptron perceptron = new Perceptron();

            float learning_rate = 0.1f;
            
            List<float> temp = new List<float>();
            double error_function = 0;
            double lowest_error = 1000000;
            do
            {
                error_function = 0;
                temp.Clear();
                temp.Add(mousemovements[0]);
                temp.Add(mousemovements[1]);
                temp.Add(mousemovements[2]);
                temp.Add(mousemovements[3]);
                temp.Add(mousemovements[4]);

                for (int i = 5; i < mousemovements.Count; i++)
                {
                    double output = perceptron.Fire(temp, weights);
                    double error = mousemovements[i] - output;
                    if (Math.Abs(error) > double.Epsilon)
                    {
                        for (int j = 0; j < temp.Count; j++)
                        {
                            weights[j] += (float)(learning_rate * error * temp[j]);
                        }

                        error_function += error;
                    }
                    temp.RemoveAt(0);
                    temp.Add(mousemovements[i]);
                }

                if (0.5 * (error_function * error_function) < lowest_error) lowest_error = 0.5 * (error_function * error_function);
            
                Console.WriteLine("Lowest: " + lowest_error + " Current: " + 0.5*(error_function * error_function));
            } while (0.5*(error_function*error_function) > 0.01);
            
            Console.WriteLine("Found weights: ");
            foreach (float w in weights)
            {
                Console.Write(", " + w);
            }
            Console.WriteLine();   
            StreamWriter writer = new StreamWriter("output.txt");

            temp.Clear();
            temp.Add(mousemovements[0]);
            temp.Add(mousemovements[1]);
            temp.Add(mousemovements[2]);
            temp.Add(mousemovements[3]);
            temp.Add(mousemovements[4]);

            for (int i = 5; i < mousemovements.Count; i++)
            {
                writer.WriteLine(perceptron.Fire(temp, weights));
                temp.RemoveAt(0);
                temp.Add(mousemovements[i]);
            }

            writer.Flush();
            writer.Close();

            Console.WriteLine("Finished Training");
            List<float> predictionIns = new List<float>();
            Console.WriteLine("Enter sameple number to predict: ");
            int sample_num = int.Parse(Console.ReadLine());
            Console.WriteLine("predicting " + sample_num);
            for (int i = sample_num - 6; i < sample_num - 1; i++)
            {
                predictionIns.Add(mousemovements[i]);
            }

            Console.WriteLine("using inputs: {0} through {1} to predict input {2}", sample_num - 6, sample_num - 1, sample_num);
            Console.WriteLine("predicted: " + perceptron.Fire(predictionIns, weights) + " expected: " + mousemovements[sample_num]);
            Console.ReadKey();
        }
    }
}
