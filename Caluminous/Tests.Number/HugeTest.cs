using Caluminous.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Number
{
    public class HugeTest
    {
        [Theory(DisplayName = "Huge Initialize")]
        [InlineData(0, 0, 4, 'E', "0.0000E+000")]
        [InlineData(0, 1, 4, 'e', "0.0000e+000")]
        [InlineData(1, 2, 3, 'E', "1.000E+002")]
        [InlineData(1.25, 10, 4, 'E', "1.2500E+010")]
        [InlineData(1.355, -2, 2, 'E', "1.35E-002")]
        [InlineData(-5, 4, 3, 'E', "-5.000E+004")]
        [InlineData(-2, -3, 3, 'e', "-2.000e-003")]
        [InlineData(0.5, 2, 3, 'E', "5.000E+001")]
        [InlineData(10, 4, 3, 'E', "1.000E+005")]
        [InlineData(-254.5, 4, 6, 'E', "-2.545000E+006")]
        [InlineData(-0.0512, -10, 4, 'e', "-5.1200e-012")]
        [InlineData(double.MaxValue, 0, 5, 'E', "1.79769E+308")]
        [InlineData(double.MinValue, 0, 6, 'e', "-1.797693e+308")]
        public void HugeInitializeTest(double m, long e, int displayDigit, char eNotation, string hugeString)
        {
            Huge huge = new(m, e);
            Assert.Equal(hugeString, huge.ToString(displayDigit, eNotation));
        }

        [Theory(DisplayName = "Huge Additional")]
        [InlineData(1, 1, 3, 1, "4.000000E+001")]
        [InlineData(0.5, 0, 0.2, 0, "7.000000E-001")]
        [InlineData(0.8, 0, 12.2, 0, "1.300000E+001")]
        [InlineData(0.02, 0, 0.0125, 0, "3.250000E-002")]
        [InlineData(2.45, 120, 6.5, 118, "2.515000E+120")]
        [InlineData(7, -3, 6, -5, "7.060000E-003")]
        [InlineData(-2, 5, 3, 4, "-1.700000E+005")]
        [InlineData(-7, -5, 8, -7, "-6.920000E-005")]
        [InlineData(1, 5, 3, -1, "1.000003E+005")]
        [InlineData(6, 1000, 5, 1000, "1.100000E+1001")]
        [InlineData(5, -1000, 4, -999, "4.500000E-999")]
        [InlineData(1, -10, 2, 2, "2.000000E+002")]
        public void HugeAdditionalTest(double m1, long e1, double m2, long e2, string hugeString)
        {
            Huge left = new(m1, e1);
            Huge right = new(m2, e2);
            Assert.Equal(hugeString, (left + right).ToString());
        }

        [Theory(DisplayName = "Huge Substraction")]
        [InlineData(4, 1, 1, 1, "3.000000E+001")]
        [InlineData(0.5, 0, 0.2, 0, "3.000000E-001")]
        [InlineData(2.8, 0, 11.4, 0, "-8.600000E+000")]
        [InlineData(0.02, 0, 0.0125, 0, "7.500000E-003")]
        [InlineData(3.45, 120, 4.5, 119, "3.000000E+120")]
        [InlineData(5.37, -8, 7.25, -9, "4.645000E-008")]
        [InlineData(-5.5, 4, 6.3, 8, "-6.300550E+008")]
        [InlineData(-9.55, -12, 8.66, -14, "-9.636600E-012")]
        [InlineData(1, 2, 1, -5, "9.999999E+001")]
        [InlineData(6, 3000, 5, 3000, "1.000000E+3000")]
        [InlineData(7, -4000, 6, -4001, "6.400000E-4000")]
        [InlineData(-5, -10, 7, -12, "-5.070000E-010")]
        [InlineData(1, -10, 1, -10, "0.000000E+000")]
        public void HugeSubstractionTest(double m1, long e1, double m2, long e2, string hugeString)
        {
            Huge left = new(m1, e1);
            Huge right = new(m2, e2);
            Assert.Equal(hugeString, (left - right).ToString());
        }

        [Theory(DisplayName = "Huge Multiply")]
        [InlineData(1, 0, 1, 0, "1.000000E+000")]
        [InlineData(3, 1, 2, 0, "6.000000E+001")]
        [InlineData(0.5, 0, 0.5, 0, "2.500000E-001")]
        [InlineData(5, 3, 6, -2, "3.000000E+002")]
        [InlineData(7, -4, 8, -5, "5.600000E-008")]
        [InlineData(-1.9, 5, 8, 6, "-1.520000E+012")]
        [InlineData(-12, 65, -34, 95, "4.080000E+162")]
        [InlineData(int.MaxValue, 0, int.MaxValue, 0, "4.611686E+018")]
        [InlineData(double.MaxValue, 0, double.MaxValue, 0, "3.231701E+616")]
        public void HugeMultiplyTest(double m1, long e1, double m2, long e2, string hugeString)
        {
            Huge left = new(m1, e1);
            Huge right = new(m2, e2);
            Assert.Equal(hugeString, (left * right).ToString());
        }

        [Theory(DisplayName = "Huge Division")]
        [InlineData(1, 0, 1, 0, "1.000000E+000")]
        [InlineData(3, 1, 2, 0, "1.500000E+001")]
        [InlineData(7, 6, 5, 4, "1.400000E+002")]
        [InlineData(10, 8, 8, -6, "1.250000E+014")]
        [InlineData(2.5, -5, 5, 6, "5.000000E-012")]
        [InlineData(8, -10, 5, -12, "1.600000E+002")]
        [InlineData(-3, 8, 6, 12, "-5.000000E-005")]
        [InlineData(7, -9, -5, 9, "-1.400000E-018")]
        [InlineData(-5, -9, -2.5, -12, "2.000000E+003")]
        [InlineData(0, 0, 3, 0, "0.000000E+000")]
        public void HugeDivisionTest(double m1, long e1, double m2, long e2, string hugeString)
        {
            Huge left = new(m1, e1);
            Huge right = new(m2, e2);
            Assert.Equal(hugeString, (left / right).ToString());
        }
    }
}
