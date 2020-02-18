using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning_Perceptron
{
    public class Perceptron
    {
        public const float BIAS = 0.65f;

        public double Fire(List<float> inputs, List<float> weights)
        {
            float sum = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                sum += (inputs[i] * weights[i]);
            }

            return (Activation(sum));
        }

        public double Adder(float input, double weight)
        {
            return Activation(input * weight);
        }

        public int fired = 0;

        public double Activation(double sum)
        {
            sum += BIAS;
            return ((double)1 / (double)(1 + Math.Pow(Math.E, -sum)));
        }
    }
}
