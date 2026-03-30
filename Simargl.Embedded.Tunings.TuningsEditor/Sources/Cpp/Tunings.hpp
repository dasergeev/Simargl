#pragma once
//	__GEN_BEGIN
#define __gen_namespace	Simargl::Embedded::Tunings
#define __gen_Knot		Knot
#define __gen_DataSize	87
#define __gen_Data
//	__GEN_END
#include <array>

#pragma pack(push, 1) // установить выравнивание в 1 байт

/// <summary>
/// Представляет пространство имён, которое содержит типы для работы с настраиваемыми параметрами.
/// </summary>
namespace __gen_namespace
{
	/// <summary>
	/// Представляет узел настраиваемых параметров.
	/// </summary>
	class __gen_Knot
	{
	public:
		enum
		{
			/// <summary>
			/// Постоянная, определяющая размер данных.
			/// </summary>
			DataSize = __gen_DataSize,
		};
	public:
		/// <summary>
		/// Поле для хранения буфера для данных.
		/// </summary>
		std::array<std::uint8_t, DataSize> Buffer;
	public:
		/// <summary>
		/// Инициализирует новый экземпляр.
		/// </summary>
		inline __gen_Knot(void) noexcept = default;
	public:
		/// <summary>
		/// Выполняет шаг работы.
		/// </summary>
		/// <returns>
		/// Значение, определяющее требуется ли отправить ответ.
		/// </returns>
		inline bool Invoke(void);
	public:
		/// <summary>
		/// Представляет данные узла настраиваемых параметров.
		/// </summary>
		struct Data;
	public:
		/// <summary>
		/// Возвращает данные узла.
		/// </summary>
		/// <returns>
		/// Сcылка на данные узла.
		/// </returns>
		inline const Data& GetData(void) const noexcept;
	public:
		/// <summary>
		/// Возвращает данные узла.
		/// </summary>
		/// <returns>
		/// Сcылка на данные узла.
		/// </returns>
		inline Data& GetData(void) noexcept;
	public:
		/// <summary>
		/// Представляет датаграмму.
		/// </summary>
		class Datagram
		{
		public:
			enum {
				/// <summary>
				/// Постоянная, определяющая максимальный размер датаграммы.
				/// </summary>
				DatagramMaxSize = 508,
			};
		private:
			/// <summary>
			/// Поле для хранения данных датаграммы.
			/// </summary>
			std::array<std::uint8_t, DatagramMaxSize> _Buffer;
		private:
			/// <summary>
			/// Поле для хранения размера датаграммы.
			/// </summary>
			std::uint16_t _Size;
		public:
			/// <summary>
			/// Инициализирует новый экземпляр.
			/// </summary>
			inline Datagram(void) noexcept = default;
		public:
			/// <summary>
			/// Устанавливает данные датаграммы.
			/// </summary>
			/// <param name="source">
			/// Данные датаграммы.
			/// </param>
			/// <param name="size">
			/// Размер данных датаграммы.
			/// </param>
			/// <returns>
			/// Результат выполнения:
			/// true - данные установлены
			/// false - данные не установлены (превышение размера данных).
			/// </returns>
			inline bool SetData(const std::uint8_t* source, std::uint16_t size);
		public:
			/// <summary>
			/// Выполняет преобразование датаграммы к указателю на данные.
			/// </summary>
			inline operator const std::uint8_t* () noexcept;
		public:
			/// <summary>
			/// Возвращает размер данных датаграммы.
			/// </summary>
			/// <returns>
			/// Размер данных датаграммы.
			/// </returns>
			inline std::uint16_t GetSize(void) const noexcept;
		private:
			Datagram(const Datagram&) = delete;
			Datagram(Datagram&&) = delete;
			Datagram& operator = (const Datagram&) = delete;
			Datagram& operator = (Datagram&&) = delete;
		};
	private:
		/// <summary>
		/// Поле для хранения датаграммы.
		/// </summary>
		Datagram _Datagram;
	public:
		/// <summary>
		/// Возвращает датаграмму.
		/// </summary>
		/// <returns>
		/// Сcылка на датаграмму.
		/// </returns>
		inline const Datagram& GetDatagram(void) const noexcept;
	public:
		/// <summary>
		/// Возвращает датаграмму.
		/// </summary>
		/// <returns>
		/// Сcылка на датаграмму.
		/// </returns>
		inline Datagram& GetDatagram(void) noexcept;
	private:
		__gen_Knot(const __gen_Knot&) = delete;
		__gen_Knot(__gen_Knot&&) = delete;
		__gen_Knot& operator = (const __gen_Knot&) = delete;
		__gen_Knot& operator = (__gen_Knot&&) = delete;
	};

	using UInt8 = std::uint8_t;
	using UInt16 = std::uint16_t;
	using UInt32 = std::uint32_t;
	using UInt64 = std::uint64_t;
	using Int8 = std::int8_t;
	using Int16 = std::int16_t;
	using Int32 = std::int32_t;
	using Int64 = std::int64_t;

	using Float32 = float;
	using Float64 = double;
	static_assert(sizeof(Float32) == 4, "Размер float должен быть равен 4.");
	static_assert(sizeof(Float64) == 8, "Размер double должен быть равен 8.");

	class alignas(1) Boolean
	{
		std::uint8_t _Value;
	};

	class alignas(1) Version
	{
		std::uint32_t _Value;
	};

	class alignas(1) DeviceType
	{
		std::uint16_t _Value;
	};

	class alignas(1) SerialNumber
	{
		std::uint32_t _Value;
	};

	class alignas(1) DateOnly
	{
		std::uint32_t _Value;
	};

	class alignas(1) TimeOnly
	{
		std::uint32_t _Value;
	};

	class alignas(1) DateTime
	{
		std::uint64_t _Value;
	};

	class alignas(1) TimeSpan
	{
		std::uint64_t _Value;
	};

	class alignas(1) IPv4Address
	{
		std::uint32_t _Value;
	};

	class alignas(1) MacAddress
	{
		std::array<std::uint8_t, 6> _Value;
	};

	/// <summary>
	/// Представляет текстовую строку с фиксированной максимальной длиной.
	/// </summary>
	/// <typeparam name="MaxLenght">
	/// Максимальная длина строки.
	/// </typeparam>
	template <std::uint8_t MaxLenght>
	class alignas(1) String
	{
	private:
		/// <summary>
		/// Поле для хранения данных строки.
		/// </summary>
		std::array<std::uint8_t, MaxLenght> _Buffer;
	};

	/// <summary>
	/// Представляет двоичные данные с фиксированным максимальным размером.
	/// </summary>
	/// <typeparam name="MaxSize">
	/// Максимальный размер двоичных данных.
	/// </typeparam>
	template <std::uint8_t MaxSize>
	class alignas(1) Binary
	{
	private:
		/// <summary>
		/// Поле для хранения данных.
		/// </summary>
		std::array<std::uint8_t, MaxSize> _Buffer;
	};

	__gen_Data

	inline const __gen_Knot::Data& __gen_Knot::GetData(void) const noexcept
	{
		//	Преобразование и возврат ссылки на данные узла.
		return *reinterpret_cast<const Data*>(Buffer.data());
	}

	inline __gen_Knot::Data& __gen_Knot::GetData(void) noexcept
	{
		//	Преобразование и возврат ссылки на данные узла.
		return *reinterpret_cast<Data*>(Buffer.data());
	}

	inline const __gen_Knot::Datagram& __gen_Knot::GetDatagram(void) const noexcept
	{
		//	Возврат ссылки на датаграмму.
		return _Datagram;
	}

	inline __gen_Knot::Datagram& __gen_Knot::GetDatagram(void) noexcept
	{
		//	Возврат ссылки на датаграмму.
		return _Datagram;
	}

	inline bool __gen_Knot::Datagram::SetData(const std::uint8_t* source, std::uint16_t size)
	{
		//	Проверка размера данных.
		if (size > DatagramMaxSize)
		{
			//	Размер данных превышает допустимое значение.
			return false;
		}

		//	Копирование данных.
		std::copy_n(source, size, _Buffer.begin());

		//	Установка размера данных.
		_Size = size;

		//	Данные успешно скопированы.
		return true;
	}

	inline __gen_Knot::Datagram::operator const std::uint8_t* () noexcept
	{
		//	Возврат указателя на данные.
		return _Buffer.data();
	}

	inline std::uint16_t __gen_Knot::Datagram::GetSize(void) const noexcept
	{
		//	Возврат размера данных.
		return _Size;
	}

	inline bool __gen_Knot::Invoke(void)
	{
		//	Требуется отправить ответ.
		return true;
	}
}

#pragma pack(pop) // вернуть предыдущее значение выравнивания