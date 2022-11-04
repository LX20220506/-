using System;


namespace MS.Common.IDCode
{
    public static class RandomNumber
    {

        private static System.Random random;

        #region static constructor

        /// <summary>
        /// static constructor
        /// </summary>
        static RandomNumber()
        {
            random = new System.Random();
        }

        #endregion

        #region functions

        /// <summary>
        ///生成一个短于或等于maxLength的随机数
        ///maxLength必须介于1到10之间
        ///当constraintMaxLength为true时，number长度必须等于maxLength
        /// </summary>
        /// <param name="maxLength">number最大长度（必须介于1到10之间）</param>
        /// <param name="constraintMaxLength">约束数长度等于maxLength</param>
        /// <returns>random number</returns>
        public static int GetRandomNumber(int maxLength, bool constraintMaxLength = false)
        {
            if (maxLength <= 0)
            {
                return 0;
            }
            int maxValue = maxLength >= 10 ? int.MaxValue : GetMaxNumber(maxLength);
            int minValue = constraintMaxLength ? GetMinNumber(maxLength) : 0;
            return random.Next(minValue, maxValue);
        }

        /// <summary>
        ///通过numberLength获取最大数量
        ///numberLength必须介于1和9之间
        /// </summary>
        /// <param name="numberLength">number length(must be between 1 and 9)</param>
        /// <returns>max number</returns>
        public static int GetMaxNumber(int numberLength)
        {
            if (numberLength < 1 || numberLength > 9)
            {
                throw new Exception("numberLength must be between 1 to 9");
            }
            string maxValueString = new string('9', numberLength);
            return Convert.ToInt32(maxValueString);
        }

        /// <summary>
        ///通过numberLength获取最小值
        ///numberLength必须介于1到10之间
        /// </summary>
        /// <param name="numberLength">number length(must be between 1 to 10)</param>
        /// <returns>min number</returns>
        public static int GetMinNumber(int numberLength)
        {
            if (numberLength < 1 || numberLength > 10)
            {
                throw new Exception("numberLength must be between 1 to 10");
            }
            string minValueString = new string('0', numberLength - 1);
            return Convert.ToInt32("1" + minValueString);

        }

        #endregion
    }
}
