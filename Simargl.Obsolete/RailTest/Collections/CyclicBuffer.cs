using System;
using System.Collections.Generic;

namespace RailTest.Collections;

    /// <summary>
    /// Представляет циклический буфер.
    /// </summary>
    /// <typeparam name="T">
    /// Тип элемента буфера.
    /// </typeparam>
    public class CyclicBuffer<T> where T : struct
{
	/// <summary>
	/// Поле для хранения длины буфера.
	/// </summary>
	private readonly long _Length;

	/// <summary>
	/// Поле для хранения массива элементов буфера.
	/// </summary>
	private readonly T[] _Items;

	/// <summary>
	/// Поле для хранения текущего положения.
	/// </summary>
	private long _Position;

	/// <summary>
	/// Инициалзирует новый экземпляр класса.
	/// </summary>
	/// <param name="length">
	/// Длина буфера.
	/// </param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// В параметре <paramref name="length"/> передано отрицательное или равное нулю значение.
	/// </exception>
	public CyclicBuffer(int length)
	{
		if (length <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(length), "Передано отрицательное или равное нулю значение.");
		}
		_Length = length;
		_Items = new T[length];
		_Position = 0;
	}

	/// <summary>
	/// Возвращает длину буфера.
	/// </summary>
	public int Length => (int)_Length;

	/// <summary>
	/// Возвращает текущее положение.
	/// </summary>
	public int Position => (int)_Position;

	/// <summary>
	/// Возвращает значение с указанным индексом.
	/// </summary>
	/// <param name="index">
	/// Индекс значения.
	/// </param>
	/// <returns>
	/// Значение.
	/// </returns>
	public T this[int index]
	{
		get
		{
			return _Items[((index % _Length) + _Length) % _Length];
		}
	}

	/// <summary>
	/// Записывает значение в текущее положение.
	/// </summary>
	/// <param name="item">
	/// Значение, которое требуется записать в буфер.
	/// </param>
	public void Write(T item)
	{
		_Items[_Position] = item;
		_Position = (_Position + 1) % _Length;
	}

	/// <summary>
	/// Записывает массив значений в текущее положение.
	/// </summary>
	/// <param name="items">
	/// Массив значений, которые требуется записать в буфер.
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// В параметре <paramref name="items"/> передана пустая ссылка.
	/// </exception>
	/// <exception cref="OverflowException">
	/// В параметре <paramref name="items"/> передан массив, который содержит больше элементов, чем задано в <see cref="int.MaxValue"/>.
	/// </exception>
	public void Write(T[] items)
	{
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Передана пустая ссылка.");
		}
		int length = items.Length;
		for (int i = 0; i != length; ++i)
		{
			_Items[_Position] = items[i];
			_Position = (_Position + 1) % _Length;
		}
	}

	/// <summary>
	/// Записывает индексированную коллекцию значений в текущее положение.
	/// </summary>
	/// <param name="items">
	/// Индексированная коллекция значений, которые требуется записать в буфер.
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// В параметре <paramref name="items"/> передана пустая ссылка.
	/// </exception>
	public void Write(IList<T> items)
	{
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Передана пустая ссылка.");
		}
		int length = items.Count;
		for (int i = 0; i != length; ++i)
		{
			_Items[_Position] = items[i];
			_Position = (_Position + 1) % _Length;
		}
	}

	/// <summary>
	/// Записывает коллекцию значений в текущее положение.
	/// </summary>
	/// <param name="items">
	/// Коллекция значений, которые требуется записать в буфер.
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// В параметре <paramref name="items"/> передана пустая ссылка.
	/// </exception>
	public void Write(IEnumerable<T> items)
	{
		if (items is null)
		{
			throw new ArgumentNullException(nameof(items), "Передана пустая ссылка.");
		}
		foreach (T item in items)
		{
			_Items[_Position] = item;
			_Position = (_Position + 1) % _Length;
		}
	}

	/// <summary>
	/// Возвращает значение с указанным индексом.
	/// </summary>
	/// <param name="index">
	/// Индекс значения.
	/// </param>
	/// <returns>
	/// Значение.
	/// </returns>
	public T Read(int index)
	{
		return _Items[((index % _Length) + _Length) % _Length];
	}

	/// <summary>
	/// Возвращает заданное количество значений начиная с указанного индекса.
	/// </summary>
	/// <param name="index">
	/// Начальный индекс.
	/// </param>
	/// <param name="count">
	/// Количество значений.
	/// </param>
	/// <returns>
	/// Массив значений.
	/// </returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// В параметре <paramref name="count"/> передано отрицательное значение.
	/// </exception>
	public T[] Read(int index, int count)
	{
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(count), "Передано отрицательное значение.");
		}
		else if (count == 0)
		{
			return Array.Empty<T>();
		}
		else
		{
			T[] items = new T[count];
			long position = ((index % _Length) + _Length) % _Length;
			for (int i = 0; i != count; ++i)
			{
				items[i] = _Items[position];
				position = (position + 1) % _Length;
			}
			return items;
		}
	}
}
