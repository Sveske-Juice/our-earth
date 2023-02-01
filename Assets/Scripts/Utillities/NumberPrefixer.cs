using System;
using UnityEngine;

/// <summary>
/// Utility class to prefix and parse prefix numbers.
/// For example to represent 2 trillion one could use
/// "2T" etc.
/// </summary>
/// <remarks>
/// This class is not pretty. Also there is no checks 
/// for overflow, so be aware that it can happen.
/// </remarks>
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
        // Get prefix
        char prefix = prefixedNumber[prefixedNumber.Length - 1];

        // Get double of prefixed version of number
        double number = Double.Parse(prefixedNumber.Substring(0, prefixedNumber.Length - 1));

        // Expand number without prefix
        switch (prefix)
        {
            case 'K':
                number *= 1_000d; // 1K
                break;
            
            case 'M':
                number *= 1_000_000d; // 1M
                break;
            
            case 'B':
                number *= 1_000_000_000d; // 1B
                break;

            case 'T':
                number *= 1_000_000_000_000d; // 1T
                break;

            case 'Q':
                number *= 1_000_000_000_000d; // 1Q
                break;
        }
        return number;
    }
}
