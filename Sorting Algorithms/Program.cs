using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Программа сортирует данные о сотруднике по одному из выбранных полей и 
    реализует 5 видов сортировки:

    1. Сортировку пузырьком
    2. Сортировку вставкой
    3. Сортировку выбором
    4. Сортировку слиянием
    5. Быструю сортировку

    Программный код основан на паттерне проектирования Strategy и 
    позволяет выбирать алгоритм сортировки во время выполнения
*/

namespace Sorting_Algorithms
{
    class Employee : IComparable<Employee>
    {
        public Employee(int age, string name, string surname)
        {
            Age = age;
            Name = name;
            Surname = surname;
        }

        // свойства для доступа к данным
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int CompareTo(Employee other)
        {
            return Age.CompareTo(other.Age);
        }
    }


    // via pattern Strategy (предусматривает параллельные иерархии классов)

    class Context
    {
        public SortingType SortingType { get; set; }

        public Context(SortingType sortingType)
        {
            SortingType = sortingType;
        }

        /// <summary>
        /// Сортировка массива по заданному полю 
        /// </summary>
        /// <typeparam name="T">тип Employee или производный от него</typeparam>
        /// <param name="array">массив элементов типа Employee</param>
        /// <param name="sortBy">делегат для задания поля сортировки</param>
        public void SortingRequest<T>(ref T[] array, Func<T, T, int> sortBy) where T : Employee
        {
            SortingType.SortAlgorithm(ref array, sortBy);
        }
    }

    abstract class SortingType
    {
        /// <summary>
        /// Абстрактный метод, выполняющий сортировку массива по заданному полю
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>
        public abstract T[] SortAlgorithm<T>(ref T[] array, Func<T, T, int> sortBy) where T : Employee;



        /// <summary>
        /// Выделение цветом текстового сообщения в консоле
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="color">Цвет текста</param>
        /// <param name="isLine">дописать к строке (false), записать в строку (true)</param>
        public void Message(string message, ConsoleColor color, bool isLine = true)
        {
            Console.ForegroundColor = color;

            if (isLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }


        /// <summary>
        /// Сортировка элементов типа Employee по полю Age
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="current">Текущий элемент</param>
        /// <param name="other">Элемент, сравниваемый с текущим</param>
        /// <returns></returns>
        public int ByAge<T>(T current, T other) where T : Employee, IComparable<Employee>
        {
            return current.Age.CompareTo(other.Age);
        }


        /// <summary>
        /// Сортировка элементов типа Employee по полю Name
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="current">Текущий элемент</param>
        /// <param name="other">Элемент, сравниваемый с текущим</param>
        /// <returns></returns>
        public int ByName<T>(T current, T other) where T : Employee, IComparable<Employee>
        {
            return current.Name.CompareTo(other.Name);
        }


        /// <summary>
        /// Сортировка элементов типа Employee по полю Surname
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="current">Текущий элемент</param>
        /// <param name="other">Элемент, сравниваемый с текущим</param>
        /// <returns></returns>
        public int BySurname<T>(T current, T other) where T : Employee, IComparable<Employee>
        {
            return current.Surname.CompareTo(other.Surname);
        }


        /// <summary>
        /// Показать содержимое массива, выделив цветом поля, по которым проводилась сортировка
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="color">Цвет текста сообщения</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        public void ShowArray<T>(T[] array, string message, ConsoleColor color, params Func<T, T, int>[] sortBy) where T : Employee
        {
            Message(message, color);

            if (sortBy.Length == 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine("Age = {0} \tName = {1} \tSurname = {2}", array[i].Age, array[i].Name, array[i].Surname);
                }
            }
            else
            {
                if (sortBy[0].Method.Name == "ByAge")
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        Message("Age = " + array[i].Age + " \t", ConsoleColor.Cyan, false);
                        Message("Name = " + array[i].Name + " \tSurname = " + array[i].Surname, ConsoleColor.Gray);
                    }
                }
                else if (sortBy[0].Method.Name == "ByName")
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        Message("Age = " + array[i].Age + " \t", ConsoleColor.Gray, false);
                        Message("Name = " + array[i].Name + " \t", ConsoleColor.Cyan, false);
                        Message("Surname = " + array[i].Surname, ConsoleColor.Gray);
                    }
                }
                else if (sortBy[0].Method.Name == "BySurname")
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        Message("Age = " + array[i].Age + " \tName = " + array[i].Name + " \t", ConsoleColor.Gray, false);
                        Message("Surname = " + array[i].Surname, ConsoleColor.Cyan);
                    }
                }
            }


            Console.WriteLine(new string('-', 80));
        }
    }


    
    /// <summary>
    /// Класс, содержащий интерфейс для реализации сортировки пузырьком 
    /// </summary>
    class BubbleSort : SortingType
    {
        /// <summary>
        /// Переопределение абстрактного метода, выполняющего сортировку массива по заданному полю
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>
        public override T[] SortAlgorithm<T>(ref T[] array, Func<T, T, int> sortBy)
        {
            string message = "\n\tСодержимое массива типа " + array.GetType().Name + " до сортировки:\n";

            ShowArray(array, message, ConsoleColor.Yellow);


            T[] sort = array;

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int k = 1; k < array.Length; k++)
                {
                    if (sortBy(array[k - 1], array[k]) == 1)
                    {
                        array = Swap(ref array, k - 1, k);
                    }
                }

            }

            Message("\n\t\tТип сортировки: " + GetType().Name.ToString() + "\n", ConsoleColor.Cyan);

            ShowArray(array, "\n\tСодержимое массива после сортировки " + sortBy.Method.Name + ":\n", ConsoleColor.Green, sortBy);


            return array;
        }
        

        /// <summary>
        /// Поменять местами значения array[index_left]  и  array[index_right]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Массив</param>
        /// <param name="indexLeft">Индекс элемента, в котрый записывается значение по индексу indexRight</param>
        /// <param name="indexRight">Индекс элемента, в котрый записывается значение по индексу indexLeft</param>
        /// <returns></returns>
        T[] Swap<T>(ref T[] array, int indexLeft, int indexRight)
        {
            T tempLeft = array[indexLeft];

            array[indexLeft] = array[indexRight];
            array[indexRight] = tempLeft;

            return array;
        }

    }


    /// <summary>
    /// Класс, содержащий интерфейс для реализации сортировки вставкой
    /// </summary>
    class InsertionSort : SortingType
    {
        /// <summary>
        /// Переопределение абстрактного метода, выполняющего сортировку массива по заданному полю
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>
        public override T[] SortAlgorithm<T>(ref T[] array, Func<T, T, int> sortBy)
        {
            string message = "\n\tСодержимое массива типа " + array.GetType().Name + " до сортировки:\n";

            ShowArray(array, message, ConsoleColor.Yellow);

            for (int i = 1; i < array.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (sortBy(array[i], array[j]) == -1)
                    {
                        array = Remove(array, i, j);
                        break;
                    }
                }

            }

            Message("\n\t\tТип сортировки: " + GetType().Name.ToString() + "\n", ConsoleColor.Cyan);

            ShowArray(array, "\n\tСодержимое массива после сортировки " + sortBy.Method.Name + ":\n", ConsoleColor.Green, sortBy);

            return array;
        }


        /// <summary>
        /// Переместить в массив элемент, содержащийся по индексу fromIndex в элемент по индексу toIndex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Массив</param>
        /// <param name="fromIndex">Индекс перемещаемого элемента</param>
        /// <param name="toIndex">Индекс, в который записывается перемещаемый элемент</param>
        /// <returns></returns>
        T[] Remove<T>(T[] array, int fromIndex, int toIndex) where T : Employee
        {
            T whatInsert = array[fromIndex];

            T[] deletedArray = Delete(array, fromIndex);

            T[] insertedArray = new T[array.Length];

            insertedArray = Push(deletedArray, whatInsert, toIndex);

            return insertedArray;
        }

        /// <summary>
        /// Вставить в массив элемент по индексу toIndex
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="whatInsert">Записываемое значение</param>
        /// <param name="toIndex">Индекс элемента, в который помещается записываемое значение</param>
        /// <returns></returns>
        T[] Push<T>(T[] array, T whatInsert, int toIndex) where T : Employee
        {
            T[] newArray = new T[array.Length + 1];

            for (int i = 0; i < array.Length + 1; i++)
            {
                if (i < toIndex)
                {
                    newArray[i] = array[i];
                }
                else if (i == toIndex)
                {
                    newArray[i] = whatInsert;
                }
                else if (i > toIndex)
                {
                    newArray[i] = array[i - 1];
                }
            }

            return newArray;
        }

       
        /// <summary>
        /// Удалить из массива элемент по индексу fromIndex
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="fromIndex">Индекс элемента, который удаляется из массива</param>
        /// <returns></returns>
        T[] Delete<T>(T[] array, int fromIndex) where T : Employee
        {
            T[] newArray = new T[array.Length - 1];

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (i >= fromIndex)
                {
                    newArray[i] = array[i + 1];
                }
                else
                {
                    newArray[i] = array[i];
                }
            }

            return newArray;
        }


    }


    /// <summary>
    /// Класс, содержащий интерфейс для реализации сортировки выбором
    /// </summary>
    class SelectionSort : SortingType
    {
        /// <summary>
        /// Переопределение абстрактного метода, выполняющего сортировку массива по заданному полю
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>
        public override T[] SortAlgorithm<T>(ref T[] array, Func<T, T, int> sortBy)
        {
            string message = "\n\tСодержимое массива типа " + array.GetType().Name + " до сортировки:\n";

            ShowArray(array, message, ConsoleColor.Yellow);

            int minIndex = 0;

            if (array.Length > 1)
            {
                for (int startInd = 0; startInd < array.Length; startInd++)
                {
                    minIndex = IndexMin(array, startInd, sortBy);

                    if (sortBy(array[startInd], array[minIndex]) == 1)
                    {
                        array = Swap(ref array, startInd, minIndex);
                    }
                }

            }

            Message("\n\t\tТип сортировки: " + GetType().Name.ToString() + "\n", ConsoleColor.Cyan);

            ShowArray(array, "\n\tСодержимое массива после сортировки " + sortBy.Method.Name + ":\n", ConsoleColor.Green, sortBy);

            return array;
        }


        /// <summary>
        /// Найти индекс элемента с минимальным значением, начиная с индекса startInd
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="startInd">Индекс массива, с которого начинается поиск минимального значения</param>
        /// <param name="sortBy">Индекс массива, которым заканчивается поиск минимального значения</param>
        /// <returns></returns>
        int IndexMin<T>(T[] array, int startInd, Func<T, T, int> sortBy) where T : Employee
        {
            int indexMin = startInd;

            T minElem = array[startInd];

            for (int i = startInd; i < array.Length - 1; i++)
            {
                if (sortBy(minElem, array[i + 1]) == 1)
                {
                    minElem = array[i + 1];
                    indexMin = i + 1;
                }
            }


            return indexMin;
        }


        /// <summary>
        /// Поменять местами значения array[index_left]  и  array[index_right]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Массив</param>
        /// <param name="indexLeft">Индекс элемента, в котрый записывается значение по индексу indexRight</param>
        /// <param name="indexRight">Индекс элемента, в котрый записывается значение по индексу indexLeft</param>
        /// <returns></returns>
        T[] Swap<T>(ref T[] array, int indexLeft, int indexRight)
        {
            T tempLeft = array[indexLeft];

            array[indexLeft] = array[indexRight];
            array[indexRight] = tempLeft;

            return array;
        }

    }


    /// <summary>
    /// Класс, содержащий интерфейс для реализации сортировки слиянием
    /// </summary>
    class MergeSort : SortingType
    {

        /// <summary>
        /// Переопределение абстрактного метода, выполняющего сортировку массива по заданному полю (метод-обёртка для соответствия интерфейсов, при вызове метода сортировки слиянием RecursSort(...))
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>

        public override T[] SortAlgorithm<T>(ref T[] array, Func<T, T, int> sortBy)
        {
            string message = "\n\tСодержимое массива типа " + array.GetType().Name + " до сортировки:\n";

            ShowArray(array, message, ConsoleColor.Yellow);


            array = RecursSort<T>(ref array, sortBy);

            Message("\n\t\tТип сортировки: " + GetType().Name.ToString() + "\n", ConsoleColor.Cyan);

            ShowArray(array, "\n\tСодержимое массива после сортировки " + sortBy.Method.Name + ":\n", ConsoleColor.Green, sortBy);


            return array;
        }


        /// <summary>
        /// Метод, реализующий сортировку слиянием
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns>Массив типа T</returns>
        T[] RecursSort<T>(ref T[] array, Func<T, T, int> sortBy)
        {
            if (array.Length <= 1)
            {
                return array;
            }
            else
            {
                int devide = array.Length / 2;

                T[] leftArray = LeftArray(array, devide);
                T[] rightArray = RightArray(array, devide);

                array = Merge(RecursSort(ref leftArray, sortBy), RecursSort(ref rightArray, sortBy), sortBy);


                return array;
            }
        }


        /// <summary>
        /// Объеденить два массива в один
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftArray">Первый массив ("левый массив")</param>
        /// <param name="rightArray">Второй массив ("правый массив")</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>
        T[] Merge<T>(T[] leftArray, T[] rightArray, Func<T, T, int> sortBy)
        {
            T[] joinArray = new T[leftArray.Length + rightArray.Length];
            T[] mergeTwoArrays = new T[leftArray.Length + rightArray.Length];

            int counterMergeTwoArrays = 0;
            int counterJoinArray = 0;


            for (int i = 0; i < leftArray.Length; i++)
            {
                mergeTwoArrays[counterMergeTwoArrays++] = leftArray[i];
            }

            for (int i = 0; i < rightArray.Length; i++)
            {
                mergeTwoArrays[counterMergeTwoArrays++] = rightArray[i];
            }

            counterMergeTwoArrays = 0;

            while (counterJoinArray < joinArray.Length)
            {
                int minIndMergeTwo = IndexMinElem(mergeTwoArrays, sortBy);

                if (minIndMergeTwo != -1)
                {
                    joinArray[counterJoinArray++] = mergeTwoArrays[minIndMergeTwo];

                    mergeTwoArrays = DeleteElem(mergeTwoArrays, minIndMergeTwo);
                }

            }


            return joinArray;
        }


        /// <summary>
        /// Получить из исходного массива новый массив, начиная от 0-го индекса до индекса end_part
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Исходный массив</param>
        /// <param name="endPart">Индекс, вплоть до которого копируются элементы исходного массива в новый массив</param>
        /// <returns></returns>
        T[] LeftArray<T>(T[] array, int endPart)
        {
            T[] newArray = new T[endPart];

            if (newArray.Length > 0)
            {
                for (int i = 0; i < endPart; i++)
                {
                    newArray[i] = array[i];
                }
            }


            return newArray;
        }


        /// <summary>
        /// Получить из исходного массива новый массив, начиная от индекса startPart до индекса последнего элемента исходного массива
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Исходный массив</param>
        /// <param name="endPart">Индекс, вплоть до которого копируются элементы исходного массива в новый массив</param>
        /// <returns></returns>
        T[] RightArray<T>(T[] array, int startPart)
        {
            T[] newArray = new T[array.Length - startPart];

            int countNewArray = 0;

            if (newArray.Length > 0)
            {
                for (int i = startPart; i < array.Length; i++)
                {
                    newArray[countNewArray++] = array[i];
                }

            }


            return newArray;
        }
        

        /// <summary>
        /// Удалить элемент из масива
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Исходный массив</param>
        /// <param name="positionDelete">Индекс удаляемого элемента массива </param>
        /// <returns></returns>
        public T[] DeleteElem<T>(T[] array, int positionDelete)
        {
            if (array.Length > 0)
            {
                T[] newArray = new T[array.Length - 1];
                int countNewArray = 0;


                for (int i = 0; i < array.Length; i++)
                {
                    if (i != positionDelete)
                    {
                        newArray[countNewArray++] = array[i];
                    }
                }


                return newArray;
            }
            else
            {
                return array;
            }
        }



        /// <summary>
        /// Индекс минимального элемента
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns>Номер индекса минимального элемента</returns>
        int IndexMinElem<T>(T[] array, Func<T, T, int> sortBy)
        {
            int position = -1;

            T minElem;

            if (array.Length > 1)
            {
                position = 0;
                minElem = array[position];

                for (int i = 1; i < array.Length; i++)
                {
                    if (sortBy(minElem, array[i]) == 1)
                    {
                        minElem = array[i];
                        position = i;
                    }
                }
            }
            else if (array.Length == 1)
            {
                position = 0;
            }


            return position;
        }


    }



    /// <summary>
    /// Класс, содержащий интерфейс для реализации быстрой сортировки
    /// </summary>
    class QuickSort : SortingType
    {
        /// <summary>
        /// Переопределение абстрактного метода, выполняющего сортировку массива по заданному полю (метод-обёртка для соответствия интерфейсов, для вызова метода быстрой сортировки  Quick(...))
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns></returns>
        public override T[] SortAlgorithm<T>(ref T[] array, Func<T, T, int> sortBy)
        {
            string message = "\n\tСодержимое массива типа " + array.GetType().Name + " до сортировки:\n";

            ShowArray(array, message, ConsoleColor.Yellow);


            array = Quick(array, sortBy);


            Message("\n\t\tТип сортировки: " + GetType().Name.ToString() + "\n", ConsoleColor.Cyan);

            ShowArray(array, "\n\tСодержимое массива после сортировки " + sortBy.Method.Name + ":\n", ConsoleColor.Green, sortBy);


            return array;
        }


        /// <summary>
        /// Размещает слева от опорного - элементы, меньшие опорного, а справа - большие
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <param name="indOpor">Индекс опорного элемента</param>
        /// <param name="resultIndPartit">Индекс опорного элемента после размещения слева от опорного элементов, меньших опорного, а справа - больших опорного</param>
        /// <returns>Массив</returns>
        T[] PartSort<T>(T[] array, Func<T, T, int> sortBy, int indOpor, out int resultIndPartit) 
        {
            int countLeft = 0;
            int countRight = array.Length - 1;

            T[] newArray = new T[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (i != indOpor)
                {
                    if (sortBy(array[i], array[indOpor]) == -1)
                    {
                        newArray[countLeft++] = array[i];
                    }
                    else
                    {
                        newArray[countRight--] = array[i];
                    }
                }
                                
            }

            resultIndPartit = countLeft;

            newArray[countLeft] = array[indOpor];


            return newArray;
        }


        /// <summary>
        /// Рекурсивный метод, реализующий быструю сортировку
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="sortBy">Делегат для задания поля, по которому ведётся сортировка</param>
        /// <returns>Массив типа T</returns>
        T[] Quick<T>(T[] array, Func<T, T, int> sortBy)
        {
            if (array.Length <= 1)
            {
                return array;
            }
            else
            {
                int partIndex = (array.Length - 1) / 2;
                int resultIndex = 0;

                T[] partSort = PartSort(array, sortBy, partIndex, out resultIndex);
                

                T[] leftArray = PartArray(partSort, resultIndex, true);
                T[] rightArray = PartArray(partSort, resultIndex, false);

                
                return Join_Array(Quick(leftArray, sortBy), Quick(rightArray, sortBy), partSort[resultIndex]);                
            }

        }


        /// <summary>
        /// Скопировать элементы массива array, слева (isLeft = true) или справа от индекса partition_ind (isLeft = false)
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="partitionInd">Индекс разбиения</param>
        /// <param name="isLeft">Копирование элементов массива array, слева (isLeft = true) или справа (isLeft = false) от индекса partition_ind</param>
        /// <returns></returns>
        T[] PartArray<T>(T[] array, int partitionInd, bool isLeft) 
        {
            List<T> partArray = new List<T>();

            if (isLeft)
            {
                for (int i = 0; i < partitionInd; i++)
                {
                    partArray.Add(array[i]);
                }
            }
            else
            {
                for (int i = partitionInd + 1; i < array.Length; i++)
                {
                    partArray.Add(array[i]);
                }
            }


            return partArray.ToArray();
        }


        /// <summary>
        /// Объединить два массива в один массив, добавив между ними опорный элемент
        /// </summary>
        /// <typeparam name="T">T типа Employee</typeparam>
        /// <param name="leftArray">Левый массив</param>
        /// <param name="rightArray">Правый массив</param>
        /// <param name="oporValue">Значение, соотвествующее опорному элементу</param>
        /// <returns></returns>
        T[] Join_Array<T>(T[] leftArray, T[] rightArray, T oporValue)
        {
            T[] newArray = new T[leftArray.Length + rightArray.Length + 1];
            int counter = 0;


            for (int i = 0; i < leftArray.Length; i++)
            {
                newArray[counter++] = leftArray[i];
            }

            newArray[counter++] = oporValue;

            for (int i = 0; i < rightArray.Length; i++)
            {
                newArray[counter++] = rightArray[i];
            }
                        

            return newArray;
        }       

    }




    class Program
    {
        static void Main(string[] args)
        {

            Employee[] elements = new Employee[]
                                                    {
                                                        new Employee (20, "Alex", "Smith"),
                                                        new Employee (18, "Jeffrey", "Taylor"),
                                                        new Employee (23, "Garrett", "Brown"),
                                                        new Employee (21, "Henry", "Evans"),
                                                        new Employee (19, "Lewis", "Wilson"),
                                                        new Employee (31, "John", "Thompson"),
                                                        new Employee (20, "Edward", "Robinson"),
                                                        new Employee (30, "Bradley", "Martin"),
                                                        new Employee (27, "Bill", "Parker"),
                                                        new Employee (22, "Andrew", "Fisher"),

                                                    };



            // раскомментить один из экземпляров класса ниже:

            //Context context = new Context(new BubbleSort()); 
            //Context context = new Context(new InsertionSort());
            //Context context = new Context(new SelectionSort());
            //Context context = new Context(new MergeSort());

            Context context = new Context(new QuickSort());



            // сортировка массива

            context.SortingRequest(ref elements, context.SortingType.ByName); // выбрать поле сортировки: .ByAge, .ByName., BySurname


            

            Console.ReadKey();
        }
    }
}
