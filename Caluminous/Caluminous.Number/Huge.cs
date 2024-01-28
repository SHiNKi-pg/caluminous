using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caluminous.Number
{
    /// <summary>
    /// 指数表記を用いて数を表す構造体。
    /// </summary>
    public struct Huge
    {
        #region Const
        /// <summary>
        /// 基数
        /// </summary>
        private const long BASE = 10;
        #endregion
        #region Property
        /// <summary>
        /// 仮数部
        /// </summary>
        public double Mantissa { get; }

        /// <summary>
        /// 指数部
        /// </summary>
        public long Exponent { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// 0 を表す<see cref="Huge"/>オブジェクトを作成します。
        /// </summary>
        public Huge()
        {
            Mantissa = 0;
            Exponent = 0;
        }

        /// <summary>
        /// <see cref="Huge"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="mantissa">仮数部(絶対値が10未満)</param>
        /// <param name="exponent">指数部</param>
        public Huge(double mantissa, long exponent)
        {
            double tmp_mantissa = mantissa;
            long tmp_exponent = exponent;
            if (tmp_mantissa != 0)
            {
                // 仮数部の絶対値が10以上の場合は10未満になるまで割り続ける
                while (Math.Abs(tmp_mantissa) >= BASE)
                {
                    tmp_mantissa /= BASE;
                    tmp_exponent++;
                }
                // 仮数部の絶対値が1より小さい場合は1以上に掛け続ける
                while(Math.Abs(tmp_mantissa) < 1)
                {
                    tmp_mantissa *= BASE;
                    tmp_exponent--;
                }
            }
            Mantissa = tmp_mantissa;
            Exponent = tmp_exponent;
        }
        #endregion

        #region Cast
        /// <summary>
        /// <see cref="double"/>型の数値を<see cref="Huge"/>型に変換します。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(double value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(float value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Huge(decimal value)
        {
            return new((double)value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(long value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(ulong value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(int value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(uint value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(short value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(ushort value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(byte value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Huge(sbyte value)
        {
            return new(value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Huge(Half value)
        {
            return new((double)value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Huge(Int128 value)
        {
            return new((double)value, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Huge(UInt128 value)
        {
            return new(((double)value), 0);
        }
        #endregion

        #region ToString
        /// <summary>
        /// この指数表記で表された数を文字列として返します。
        /// </summary>
        /// <param name="significantDigits">小数点以下の表示桁数</param>
        /// <param name="eNotationChar">E表記で使用されるEの部分の文字</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string ToString(int significantDigits, char eNotationChar)
        {
            if (significantDigits < 0)
                throw new ArgumentException("小数点以下の表示桁数は 0以上でなければなりません。", nameof(significantDigits));

            StringBuilder sb = new();
            sb.Append(Mantissa.ToString($"F{significantDigits}"));
            sb.Append(eNotationChar);
            if(Exponent >= 0)
            {
                sb.Append("+");
            }
            sb.Append(Exponent.ToString("000"));
            return sb.ToString();
        }

        /// <summary>
        /// この指数表記で表された数を文字列として返します。
        /// </summary>
        /// <returns>小数点以下6桁で表示された文字列</returns>
        public override string ToString()
        {
            return this.ToString(6, 'E');
        }
        #endregion
    }
}
