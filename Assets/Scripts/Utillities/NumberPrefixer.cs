using System;
using UnityEngine;

public static class NumberPrefixer
{
    public static string Prefix(double number)
    {
        return Prefix(number, 2); // Default decimal rounding to 2
    }

    public static string Prefix(double number, int decimalRounding)
    {
        string prefix = "";
        double prefixedNum = number;

        int sign = Math.Sign(number);
        number = Math.Abs(number); // Use absolute value

        if (number < 999)
        {
            
        }
        else if (number < 999_999d) // 999K
        {
            prefixedNum = number / 1_000d;
            prefix = "K";
        }
        else if (number < 999_999_999d) // 999M
        {
            prefixedNum = number / 1_000_000d;
            prefix = "M";
        }
        else if (number < 999_999_999_999d) // 999B
        {
            prefixedNum = number / 1_000_000_000d;
            prefix = "B";
        }
        else if (number < 999_999_999_999_999d) // 999T
        {
            prefixedNum = number / 1_000_000_000_000d; // 1T
            prefix = "T";
        }
        else if (number < 999_999_999_999_999_999d) // 999Q
        {
            prefixedNum = number / 1_000_000_000_000_000d; // 1Q
            prefix = "Q";
        }

        // Set the text on the balance element
        return Math.Round(prefixedNum * sign, decimalRounding) + prefix;
    }

    public static double Parse(string prefixedNumber)
    {
        char prefix = prefixedNumber[prefixedNumber.Length - 1];
        string number = prefixedNumber.Substring(0, prefixedNumber.Length - 1);

        Debug.Log($"prefix: {prefix}");
        Debug.Log($"num: {number}");
        // TODO also support decimals

        if (prefix == 'K')
        {}
        return 0d;
    }
}
