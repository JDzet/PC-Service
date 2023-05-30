﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Service.ClassProject
{
    internal class PasswordGenerator
    {
        private static Random random = new Random();

        public static string GeneratePassword()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var password = new char[8];

            // Генерация первого символа в верхнем регистре
            password[0] = GetRandomUpperCaseChar();

            // Генерация следующих 2 символов - цифр
            for (int i = 1; i <= 2; i++)
            {
                password[i] = GetRandomDigitChar();
            }

            // Генерация остальных символов
            for (int i = 3; i < 8; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            // Перемешивание символов пароля
            ShufflePassword(password);

            return new string(password);
        }

        private static char GetRandomUpperCaseChar()
        {
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return upperCaseChars[random.Next(upperCaseChars.Length)];
        }

        private static char GetRandomDigitChar()
        {
            const string digitChars = "0123456789";
            return digitChars[random.Next(digitChars.Length)];
        }

        private static void ShufflePassword(char[] password)
        {
            for (int i = password.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = password[i];
                password[i] = password[j];
                password[j] = temp;
            }
        }
    }
}