using System;

public static class NumberPrefixer
{
    public static string PrefixNumber(double number)
    {
        return PrefixNumber(number, 2); // Default decimal rounding to 2
    }

    public static string PrefixNumber(double number, int decimalRounding)
    {
        string prefix = "";
        double prefixedNum = number;

        int sign = Math.Sign(number);
        number = Math.Abs(number); // Use absolute value

        if (number < 999_999d) // 999K
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
}
