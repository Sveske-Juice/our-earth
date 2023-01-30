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
        double prefixedNum = 0d;

        if (number < 100000) // 100K
        {
            prefixedNum = number / 1000;
            prefix = "K";
        }
        else if (number < 1000000000) // 100M
        {
            prefixedNum = number / 1000000;
            prefix = "M";
        }
        else if (number < 1000000000000) // 100B
        {
            prefixedNum = number / 1000000000;
            prefix = "B";
        }
        else if (number < 1000000000000000) // 100T
        {
            prefixedNum = number / 1000000000000;
            prefix = "T";
        }

        // Set the text on the balance element
        return Math.Round(prefixedNum, decimalRounding) + prefix;
    }
}
