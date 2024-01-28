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
                // 仮数部の絶対値が10より大きい場合は10未満になるまで割り続ける
                while (Math.Abs(tmp_mantissa) > BASE)
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
    }
}
