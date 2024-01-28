using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Caluminous.Number
{
    /// <summary>
    /// 指数表記を用いて数を表す構造体。
    /// </summary>
    public struct Huge : INumber<Huge>
    {
        #region Const
        /// <summary>
        /// 基数
        /// </summary>
        private const int BASE = 10;
        #endregion

        #region Field
        private bool isInfinity = false;

        private bool isNaN = false;
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

        /// <summary>
        /// 1 を表します。
        /// </summary>
        public static Huge One { get; }

        /// <summary>
        /// 基数を取得します。この値は常に 10です。
        /// </summary>
        public static int Radix { get; }

        /// <summary>
        /// 0を表します。
        /// </summary>
        public static Huge Zero { get; }

        /// <summary>
        /// 元の数に足しても値が変わらない数、つまり 0を表します。
        /// </summary>
        public static Huge AdditiveIdentity { get; }

        /// <summary>
        /// 元の数に掛けても値が変わらない数、つまり 1を表します。
        /// </summary>
        public static Huge MultiplicativeIdentity { get; }

        /// <summary>
        /// <see cref="Huge"/>型で表すことができる最大の数を取得します。
        /// </summary>
        public static Huge MaxValue { get; }

        /// <summary>
        /// <see cref="Huge"/>型で表すことができる最小の数を取得します。
        /// </summary>
        public static Huge MinValue { get; }
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
        public Huge(double mantissa, long exponent) : this(mantissa, exponent, true)
        {

        }

        /// <summary>
        /// <see cref="Huge"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="mantissa">仮数部(絶対値が10未満)</param>
        /// <param name="exponent">指数部</param>
        /// <param name="normalize">数値を正規化するかどうか</param>
        private Huge(double mantissa, long exponent, bool normalize)
        {
            if (double.IsInfinity(mantissa))
            {
                isInfinity = true;
                Mantissa = mantissa;
                Exponent = exponent;
                return;
            }
            if (double.IsNaN(mantissa))
            {
                Mantissa = mantissa;
                Exponent = exponent;
                isNaN = true;
                return;
            }
            if(mantissa == 0)
            {
                Mantissa = 0;
                Exponent = 0;
                return;
            }
            if (normalize)
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
                    while (Math.Abs(tmp_mantissa) < 1)
                    {
                        tmp_mantissa *= BASE;
                        tmp_exponent--;
                    }
                }
                Mantissa = tmp_mantissa;
                Exponent = tmp_exponent;
            }
            else
            {
                Mantissa = mantissa;
                Exponent = exponent;
            }
        }

        static Huge()
        {
            One = new(1, 0);
            Zero = new();
            Radix = BASE;
            AdditiveIdentity = Zero;
            MultiplicativeIdentity = One;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;
            return CompareTo((Huge)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Huge other)
        {
            if(this.Mantissa == 0 && other.Mantissa == 0)
            {
                // どちらも0
                return 0;
            }
            else if(this.Mantissa == 0 ^ other.Mantissa == 0)
            {
                // どちらかの仮数部が0なら 仮数部を比較する
                return this.Mantissa.CompareTo(other.Mantissa);
            }

            if(this.Exponent == other.Exponent)
            {
                // 指数部が等しければ仮数部を比較する
                return this.Mantissa.CompareTo(other.Mantissa);
            }
            else
            {
                // 指数部が異なれば指数部を比較する
                return this.Exponent.CompareTo(other.Exponent);
            }
        }

        /// <summary>
        /// 絶対値を返します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Huge Abs(Huge value)
        {
            return new(Math.Abs(value.Mantissa), value.Exponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsCanonical(Huge value)
        {
            return value.Mantissa >= 1 && value.Mantissa < 10;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsComplexNumber(Huge value)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEvenInteger(Huge value)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFinite(Huge value)
        {
            return value.isInfinity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsImaginaryNumber(Huge value)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInfinity(Huge value)
        {
            return value.isInfinity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(Huge value)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNaN(Huge value)
        {
            return value.isNaN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNegative(Huge value)
        {
            return value.Mantissa < 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNegativeInfinity(Huge value)
        {
            return double.IsNegativeInfinity(value.Mantissa);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNormal(Huge value)
        {
            return value.Mantissa >= 1 && value.Mantissa < 10;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOddInteger(Huge value)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositive(Huge value)
        {
            return value.Mantissa > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositiveInfinity(Huge value)
        {
            return double.IsPositiveInfinity(value.Mantissa);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsRealNumber(Huge value)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSubnormal(Huge value)
        {
            return !IsNormal(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsZero(Huge value)
        {
            return value.Mantissa == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Huge MaxMagnitude(Huge x, Huge y)
        {
            return x > y ? x : y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Huge MaxMagnitudeNumber(Huge x, Huge y)
        {
            return MaxMagnitude(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Huge MinMagnitude(Huge x, Huge y)
        {
            return x < y ? x : y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Huge MinMagnitudeNumber(Huge x, Huge y)
        {
            return MinMagnitude(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Huge Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            // TODO: Parse機能の実装
            throw new NotSupportedException(nameof(Parse) + "はサポートされていません。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static Huge Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Huge result)
        {
            // TODO: Parse機能の実装
            throw new NotSupportedException(nameof(Parse) + "はサポートされていません。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Huge result)
        {
            return TryParse(s.AsSpan(), style, provider, out result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Huge other)
        {
            return this == other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="charsWritten"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            // TODO: TryFormat機能の実装
            throw new NotSupportedException(nameof(TryFormat) + "はサポートされていません。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">小数点以下の桁数とE表記の文字 (例： 6E, 4e など)</param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if(format is null)
            {
                return ToString();
            }
            try
            {
                int digit = int.Parse(format[0].ToString());
                char eNotation = format[1];
                return ToString(digit, eNotation);
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(format));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Huge Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            // TODO: Parse機能の実装
            throw new NotSupportedException(nameof(Parse) + "はサポートされていません。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Huge result)
        {
            // TODO: Parse機能の実装
            throw new NotSupportedException(nameof(TryParse) + "はサポートされていません。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Huge Parse(string s, IFormatProvider? provider)
        {
            // TODO: Parse機能の実装
            throw new NotSupportedException(nameof(Parse) + "はサポートされていません。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Huge result)
        {
            if (s is null)
            {
                result = default;
                return false;
            }
            else
            {
                return TryParse(s.AsSpan(), provider, out result);
            }
        }

        static bool INumberBase<Huge>.TryConvertFromChecked<TOther>(TOther value, out Huge result)
        {
            // TODO: TryConvertFromChecked実装
            throw new NotImplementedException();
        }

        static bool INumberBase<Huge>.TryConvertFromSaturating<TOther>(TOther value, out Huge result)
        {
            // TODO: TryConvertFromSaturating実装
            throw new NotImplementedException();
        }

        static bool INumberBase<Huge>.TryConvertFromTruncating<TOther>(TOther value, out Huge result)
        {
            // TODO: TryConvertFromTruncating実装
            throw new NotImplementedException();
        }

        static bool INumberBase<Huge>.TryConvertToChecked<TOther>(Huge value, out TOther result)
        {
            // TODO: TryConvertToChecked実装
            throw new NotImplementedException();
        }

        static bool INumberBase<Huge>.TryConvertToSaturating<TOther>(Huge value, out TOther result)
        {
            // TODO: TryConvertToSaturating実装
            throw new NotImplementedException();
        }

        static bool INumberBase<Huge>.TryConvertToTruncating<TOther>(Huge value, out TOther result)
        {
            // TODO: TryConvertToTruncating実装
            throw new NotImplementedException();
        }
        #endregion

        #region INumber
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Huge left, Huge right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Huge left, Huge right)
        {
            return left == right || left > right;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Huge left, Huge right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool operator <=(Huge left, Huge right)
        {
            return left < right || left == right;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Huge operator %(Huge left, Huge right)
        {
            if (left.Mantissa == 0)
                return new();
            if (right.Mantissa == 0)
                return new(double.NaN, 0);
            return left - (right * (left / right));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Huge operator +(Huge left, Huge right)
        {
            // 桁を揃える
            Huge t_right;
            if (left.Exponent != right.Exponent)
                t_right = AlignDigit(right, left.Exponent);
            else
                t_right = right;
            return new(left.Mantissa + t_right.Mantissa, left.Exponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Huge operator --(Huge value)
        {
            return value - One;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Huge operator /(Huge left, Huge right)
        {
            if (left.Mantissa == 0)
                return new();
            if (right.Mantissa == 0)
                return new(double.NaN, 0);
            return new(left.Mantissa / right.Mantissa, left.Exponent - right.Exponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Huge left, Huge right)
        {
            return left.CompareTo(right) == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Huge left, Huge right)
        {
            return left.CompareTo(right) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Huge operator ++(Huge value)
        {
            return value + One;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Huge operator *(Huge left, Huge right)
        {
            return new(left.Mantissa * right.Mantissa, left.Exponent + right.Exponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Huge operator -(Huge left, Huge right)
        {
            return left + -right;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Huge operator -(Huge value)
        {
            return new(-value.Mantissa, value.Exponent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Huge operator +(Huge value)
        {
            return new(value.Mantissa, value.Exponent);
        }
        #endregion

        #region Override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return (obj is Huge huge) && this == huge;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Mantissa.GetHashCode() ^ Exponent.GetHashCode();
        }
        #endregion

        #region Method
        /// <summary>
        /// 指数表記の数の指数を指定した数に合わせます。
        /// </summary>
        /// <param name="value"><see cref="Huge"/>型の数値</param>
        /// <param name="digit">指数</param>
        /// <returns></returns>
        public static Huge AlignDigit(Huge value, long digit)
        {
            double mantissa = value.Mantissa;
            long exponent = value.Exponent;
            long digitDiff = value.Exponent - digit;
            long digitDiffAbs = Math.Abs(digitDiff);
            if(digitDiff > 0)
            {
                for(long i = 1; i <= digitDiffAbs; i++)
                {
                    mantissa *= 10;
                    exponent--;
                }
            }
            else if(digitDiff < 0)
            {
                for (long i = 1; i <= digitDiffAbs; i++)
                {
                    mantissa /= 10;
                    exponent++;
                }
            }
            return new(mantissa, exponent, false);
        }
        #endregion
    }
}
