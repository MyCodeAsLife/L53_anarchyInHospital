using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace L53_anarchyInHospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            hospital.Run();
        }
    }

    class Hospital
    {
        private List<Patient> _patients = new List<Patient>();
        private List<Patient> _formattedPatientList;

        private String _delimeterString;
        private String _delimiterMenu;

        public Hospital()
        {
            char delimiterSymbolMenu = '=';
            char delimiterSymbolString = '-';
            int delimiterLenght = 75;

            _delimeterString = new string(delimiterSymbolString, delimiterLenght);
            _delimiterMenu = new string(delimiterSymbolMenu, delimiterLenght);

            Fill();
            _formattedPatientList = _patients;
        }

        private enum Menu
        {
            SortByFullName = 1,
            SortByAge = 2,
            SearchByDisease = 3,
            ResetFormatting = 4,
            Exit = 5,
        }

        public void Run()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();
                ShowPatients(_formattedPatientList);
                ShowMenu();

                Console.Write("Выберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch ((Menu)number)
                    {
                        case Menu.SortByFullName:
                            SortByFullName();
                            break;

                        case Menu.SortByAge:
                            SortByAge();
                            break;

                        case Menu.SearchByDisease:
                            SearchByDisease();
                            break;

                        case Menu.ResetFormatting:
                            _formattedPatientList = _patients;
                            break;

                        case Menu.Exit:
                            isOpen = false;
                            continue;

                        default:
                            ShowError();
                            break;
                    }
                }
                else
                {
                    ShowError();
                }

                Console.ReadKey(true);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine(_delimiterMenu + $"\nДоступные действия.\n" + _delimeterString + $"\n{(int)Menu.SortByFullName}" +
                              $" - Отсортировать по ФИО.\n{(int)Menu.SortByAge} - Отсортировать по возрасту.\n{(int)Menu.SearchByDisease}" +
                              $" - Отсеять по болезни.\n{(int)Menu.ResetFormatting} - Сбросить форматирование списка пациентов.\n" +
                              $"{(int)Menu.Exit} - Выйти.\n" + _delimiterMenu);
        }

        private void SortByFullName()
        {
            _formattedPatientList = _formattedPatientList.OrderBy(player => player.FullName).ToList();
        }

        private void SortByAge()
        {
            _formattedPatientList = _formattedPatientList.OrderBy(player => player.Age).ToList();
        }

        private void SearchByDisease()
        {
            Console.Write("Введите заболевание: ");
            string inputDisease = Console.ReadLine();
            List<Patient> finedPatients = _patients.Where(patient => patient.Disease == inputDisease).ToList();

            if (finedPatients.Count > 0)
                _formattedPatientList = finedPatients;
            else
                Console.WriteLine("Пациентов с указанным заболеванием не найдено.");
        }

        private void ShowPatients(List<Patient> patients)
        {
            Console.WriteLine("Список пациентов.\n" + _delimeterString);

            foreach (var patient in patients)
                Console.WriteLine($"ФИО: {patient.FullName}\tВозраст: {patient.Age}\tЗаболевание: {patient.Disease}");
        }

        private void Fill()
        {
            _patients.Add(new Patient("Иванов Иван Иванович", 33, "диарея"));
            _patients.Add(new Patient("Петров Ветр Петрович", 52, "скалиоз"));
            _patients.Add(new Patient("Сидоров Сидр Сидорович", 27, "артрит"));
            _patients.Add(new Patient("Ярых Георгий Поликарпович", 25, "астигматизм"));
            _patients.Add(new Patient("Пшеков Пшек Пшекович", 44, "нейропатия"));
            _patients.Add(new Patient("Чубайс Мойша Изральевич", 19, "воспаление хитрости"));
            _patients.Add(new Patient("Полифонова Анна Петровна", 61, "артрит"));
            _patients.Add(new Patient("Батьковна Елизавета Григорьевна", 29, "диарея"));
            _patients.Add(new Patient("Владимиров Владимир Владимирович", 38, "геморой"));
            _patients.Add(new Patient("Евгеньева Евгения Евгеновна", 55, "сальмонелез"));
        }

        private void ShowError()
        {
            Console.WriteLine("\nВы ввели некорректное значение.");
        }
    }

    class Patient
    {
        public Patient(string fullName, int age, string disease)
        {
            FullName = fullName;
            Age = age;
            Disease = disease;
        }

        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }
    }
}
