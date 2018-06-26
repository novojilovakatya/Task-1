using System;
using System.IO;

namespace Task_1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Переменные для работы с текстом
            string text = "";
            string copyText = "";

            //Пробельные символы
            char symEnter = Convert.ToChar(13);
            char symTab = Convert.ToChar(9);
            char[] ch = { symTab, ' ', symEnter };

            int index = 0;  // Индекс

            // Файлы
            StreamReader fileRead = new StreamReader("input.txt");
            StreamWriter fileWrite = new StreamWriter("output.txt");

            // Считывание запроса и перевод в нижний регистр
            string MainStr = fileRead.ReadLine().ToLower();

            // Считывание текста
            string temp = fileRead.ReadLine();
            while (temp != null)
            {
                text += temp + symEnter;
                temp = fileRead.ReadLine();
            }

            // Разбиваем текст на слова по пробельным символам
            string[] arrWords = text.Split(ch, StringSplitOptions.RemoveEmptyEntries);

            // Создаем копию для поиска пробельных символов
            copyText = text;
            // Если перед текстом есть пробельные символы, то запоминаем их
            string sym = "";
            index = copyText.IndexOf(arrWords[0]);
            if (index != 0)
                sym = copyText.Substring(0, index);
            copyText = copyText.Substring(index);
            // В копии текста удаляем слова, сохраняя пробельные символы
            for (int i = 0; i < arrWords.Length; i++)
            {
                index = copyText.IndexOf(arrWords[i]);
                copyText = copyText.Substring(0, index) + '!' + copyText.Remove(0, arrWords[i].Length + index);
            }
            // Разбиваем пробельные символы по позициям
            char[] lim = { '!' };
            string[] arrSym = copyText.Split(lim, StringSplitOptions.RemoveEmptyEntries);
      
            // Соединяем слова в текст с одинарными пробелами
            text = string.Join(" ", arrWords);
            // Копию текста переводим в нижний регистр
            copyText = text.ToLower();
            // Ищем запрос в копии текста и добавляем символ @ в оба текста
            index = copyText.IndexOf(MainStr);
            while (index != -1)
            {
                copyText = copyText.Substring(0, index) + '@' + copyText.Substring(index);
                text = text.Substring(0, index) + '@' + text.Substring(index);
                index = copyText.IndexOf(MainStr, index + 2);
            }

            // Разбиваем полученный основной текст по пробелам
            arrWords = text.Split(ch, StringSplitOptions.RemoveEmptyEntries);
            // Соединяем полученные слова с пробельными символами
            text = sym;
            for (int i = 0; i < arrWords.Length; i++)
                text += arrWords[i] + arrSym[i];

            // Выводим ответ в файл
            index = text.IndexOf(symEnter);
            while (index != -1)
            {
                copyText = text.Substring(0, index);
                fileWrite.WriteLine(copyText);
                text = text.Substring(index + 1);
                index = text.IndexOf(symEnter);
            }

            // Закрытие файлов
            fileWrite.Close();
            fileRead.Close();
        }
    }
}
