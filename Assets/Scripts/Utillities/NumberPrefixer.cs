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

        int sign = Math.Sign(number);
        number = Math.Abs(number); // Use absolute value

        if (number < 100_000d) // 100K
        {
            prefixedNum = number / 1_000d;
            prefix = "K";
        }
        else if (number < 100_000_000d) // 100M
        {
            prefixedNum = number / 1_000_000d;
            prefix = "M";
        }
        else if (number < 100_000_000_000d) // 100B
        {
            prefixedNum = number / 1_000_000_000d;
            prefix = "B";
        }
        else if (number < 100_000_000_000_000d) // 100T
        {
            prefixedNum = number / 1_000_000_000_000d;
            prefix = "T";
        }

        // Set the text on the balance element
        return Math.Round(prefixedNum * sign, decimalRounding) + prefix;
    }
}
