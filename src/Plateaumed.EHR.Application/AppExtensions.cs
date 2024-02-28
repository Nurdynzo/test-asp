using System;

namespace Plateaumed.EHR;

public static class AppExtensions
{
    public static int CalculateAge(DateTime dateOfBirth)  
    {  
        int age = 0;  
        age = DateTime.Now.Year - dateOfBirth.Year;  
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)  
            age = age - 1;  
  
        return age;  
    }  
    
}