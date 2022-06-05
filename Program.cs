using System;
using System.Text;
using System.Collections.Generic;

class MainClass
{
	public static void Main(string[] args)
	{
		//Объявление кортежа декоструировано
		(string name, string surname, int age, bool isPets, int amountPets, string[] namePets, int amountColor, string[] colectionColor) = GetFillIn();
		ShowData(in name, in surname, in age, in isPets, in amountPets, namePets, in amountColor, colectionColor);
	}
	/// <summary>
	/// Метод вывода на консоль, все параметры in, кроме ссылочных
	/// </summary>
	/// <param name="name"></param>
	/// <param name="surname"></param>
	/// <param name="age"></param>
	/// <param name="isPets"></param>
	/// <param name="amountPets"></param>
	/// <param name="namePets"></param>
	/// <param name="amountColor"></param>
	/// <param name="colectionColor"></param>
	static void ShowData(in string name, in string surname, in int age, in bool isPets, in  int amountPets, string[] namePets, in int amountColor, string[] colectionColor)
    {
		Console.WriteLine("<--------------->");
		Console.WriteLine($"Вас зовут {name}");
		Console.WriteLine($"Ваша фамилия {surname}");
		Console.WriteLine($"Ваш возраст {age}");
		if (isPets)
		{
			Console.WriteLine($"У Вас есть питомцы, их колличество {amountPets}");
			Console.WriteLine($"Их клички:");
			for (int i = 0; i < namePets.Length; i++)
			{
				Console.WriteLine($"Кличка №{i + 1}: {namePets[i]}");
			}
		}
		else
		{
			Console.WriteLine($"У Вас нет питомцев");
		}

		Console.WriteLine($"Колличество любимых цветов {amountColor}");
		Console.WriteLine("Их список:");
		for (int i = 0; i < colectionColor.Length; i++)
		{
			Console.WriteLine($"Цвет №{i + 1}: {colectionColor[i]}");
		}
	}

	/// <summary>
	/// Метод ввода пользовательских данных
	/// </summary>
	/// <returns></returns>
	static (string, string, int, bool, int, string[], int, string[]) GetFillIn()
	{
		(string name, string surname, int age, bool isPets, int amountPets, string[] petsName, int amountColor, string[] colectionColor) userAdd = default;
		// Вводим имя, если ничего не ввели то возвращаем пустую строку
		Console.WriteLine("Введите свое имя");
		userAdd.name = Console.ReadLine() ?? String.Empty;

		// Вводим фамилию, если ничего не ввели то возвращаем пустую строку
		Console.WriteLine("Введите свою фамилию");
		userAdd.surname = Console.ReadLine() ?? String.Empty;

		// Вводим возраст, пользователь не сможет вести что-то коме цифр
		do
		{
			Console.WriteLine("Введите свой возраст");
		}
		// пока не введут корректные цифры цикл будет выполняться
		while (CheckData(NumberInput(), out userAdd.age));
		// Перевод строки
		Console.WriteLine();

		//Питомцы
		Console.WriteLine("У Вас есть питомцы, нажмите (д/н)?");
		//Код обработчика да/нет	
		ConsoleKeyInfo keyPets;

		//пока не нажали д или н на клавиатуре будем ждать.
		while ((keyPets = Console.ReadKey(true)).KeyChar != 'д' && keyPets.KeyChar != 'н') ; // пока не нажали y или n

		char c = keyPets.KeyChar; // нажатая клавиша
		if (c == 'д') //Если нажата клавиша д, то обрабатвыем код по питомцам
		{
			userAdd.isPets = true;

			do
			{
				Console.WriteLine("Сколько у вас питомцев?");
			}
			// пока не введут корректные цифры цикл будет выполняться
			while (CheckData(NumberInput(), out userAdd.amountPets));
			// Перевод строки
			Console.WriteLine();
			
			userAdd.petsName = GetAraayUser(userAdd.amountPets, "Кличка питамца");
			//в метод
		}
		else
		{
			userAdd.isPets = false;
		}
		//Разбираемся с цветами
		do
		{
			Console.WriteLine("Сколько у вас любимых цветов?");
		}
		// пока не введут корректные цифры цикл будет выполняться
		while (CheckData(NumberInput(), out userAdd.amountColor));
		// Перевод строки
		Console.WriteLine();
		userAdd.colectionColor = GetAraayUser(userAdd.amountColor, "Введите цвет");


		return userAdd;
	}

	/// <summary>
	/// Считывает в массив данные вводимые пользователем
	/// </summary>
	/// <param name="amountPets"></param>
	/// <param name="literalConsole"></param>
	/// <returns></returns>
	static string[] GetAraayUser(int amountPets, string literalConsole)
    {
		//Используем List для заполнения
		List<string> tempArray = new();

		for (int i = 0; i < amountPets; i++)
		{
			Console.WriteLine($"{literalConsole} №{i + 1}");

			tempArray.Add(Console.ReadLine() ?? String.Empty);
		}
		//Переобразуем List  в массив
		return tempArray.ToArray();
	}
		
	/// <summary>
	/// Метод ввода только цифр
	/// </summary>
	/// <returns></returns>
	static string NumberInput()
    {
		StringBuilder sb = new();
		ConsoleKeyInfo key;
		while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter) // пока не нажали Enter
		{
			char c = key.KeyChar; // нажатая клавиша
			if (Char.IsDigit(c)) // только цифры
			{
				Console.Write(c);
				sb.Append(c); // добавляем к буферу
			}
		}
		return sb.ToString();
    }

	/// <summary>
	/// Метод проверки и преобразования введенных цифр
	/// </summary>
	/// <param name="age"></param>
	/// <param name="ageInt"></param>
	/// <returns></returns>
	static bool CheckData(string age, out int ageInt)
    {
        if (int.TryParse(age, out int result) && !(result == 0)) // Проверка бессмыслена пользователь не введет ничего кроме цифр
		{
			ageInt = result;
			return false;
		}
		ageInt = 0;
		return true;
	}
}
