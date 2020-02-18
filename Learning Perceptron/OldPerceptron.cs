using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning_Perceptron
{
    class OldPerceptron
    {
        public const float BIAS = 0.65f;

        public int Fire(List<float> inputs, List<float> weights)
        {
            float sum = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                sum += (inputs[i] * weights[i]);
            }

            return (Activation(sum));
        }

        public int Adder(float input, double weight)
        {
            return Activation(input * weight);
        }

        public int fired = 0;

        public int Activation(double sum)
        {
            if ((sum + BIAS) > 1)
            {
                fired++;
                return 1;
            }

            return 0;
        }
    }
}
