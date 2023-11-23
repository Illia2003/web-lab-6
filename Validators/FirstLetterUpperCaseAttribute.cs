using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class FirstLetterUpperCaseAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return true;
        }

        string stringValue = value.ToString();
        char firstChar = stringValue[0];

        return char.IsUpper(firstChar);
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} повинен починатися з великої літери.";
    }
}