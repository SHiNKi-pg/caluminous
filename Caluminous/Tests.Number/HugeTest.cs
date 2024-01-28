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
        [InlineData(0, 1, 4, 'e', "0.0000e+001")]
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
    }
}
