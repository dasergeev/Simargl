#pragma once

#include <array>

#pragma pack(push, 1) // установить выравнивание в 1 байт

/// <summary>
/// Представляет пространство имён, которое содержит типы для работы с настраиваемыми параметрами.
/// </summary>
namespace Simargl::Embedded::Tunings
{
	/// <summary>
	/// Представляет узел настраиваемых параметров.
	/// </summary>
	class Knot
	{
	public:
		enum
		{
			/// <summary>
			/// Постоянная, определяющая размер данных.
			/// </summary>
			DataSize = 85,
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
		inline Knot(void) noexcept = default;
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
		Knot(const Knot&) = delete;
		Knot(Knot&&) = delete;
		Knot& operator = (const Knot&) = delete;
		Knot& operator = (Knot&&) = delete;
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

	struct alignas(1) Knot::Data
	{
	public:
		/// <summary>
		/// Представляет раздел "Идентификация". Содержит информацию для идентификации устройства
		/// </summary>
		struct alignas(1) IdentitySection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "Тип". Тип устройства.
			/// </summary>
			DeviceType DeviceType;
		public:
			/// <summary>
			/// Поле для хранения параметра "Серийный номер". Серийный номер устройства.
			/// </summary>
			SerialNumber SerialNumber;
		};
	private:
		static_assert(sizeof(IdentitySection) == 6, "Размер структуры IdentitySection должен быть равен 6.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Идентификация".
		/// </summary>
		IdentitySection Identity;
	public:
		/// <summary>
		/// Представляет раздел "Прошивка". Содержит информацию о текущей прошивке устройства
		/// </summary>
		struct alignas(1) FirmwareSection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "Версия". Версия прошивки.
			/// </summary>
			Version Version;
		public:
			/// <summary>
			/// Поле для хранения параметра "Дата". Дата изготовления прошивки.
			/// </summary>
			DateOnly Date;
		};
	private:
		static_assert(sizeof(FirmwareSection) == 8, "Размер структуры FirmwareSection должен быть равен 8.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Прошивка".
		/// </summary>
		FirmwareSection Firmware;
	public:
		/// <summary>
		/// Представляет раздел "Аппаратные". Содержит аппаратные настройки
		/// </summary>
		struct alignas(1) HardwareSection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "MAC-адрес". MAC-адрес сетевого устройства.
			/// </summary>
			MacAddress Identifier;
		};
	private:
		static_assert(sizeof(HardwareSection) == 6, "Размер структуры HardwareSection должен быть равен 6.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Аппаратные".
		/// </summary>
		HardwareSection Hardware;
	public:
		/// <summary>
		/// Представляет раздел "Порты". Содержит информацию о номерах портов для взаимодействия.
		/// </summary>
		struct alignas(1) PortsSection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "UDP". Номер порта для взаимодействия по протоколу UDP.
			/// </summary>
			UInt16 UdpPort;
		public:
			/// <summary>
			/// Поле для хранения параметра "TCP". Номер порта для взаимодействия по протоколу TCP.
			/// </summary>
			UInt16 TcpPort;
		};
	private:
		static_assert(sizeof(PortsSection) == 4, "Размер структуры PortsSection должен быть равен 4.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Порты".
		/// </summary>
		PortsSection Ports;
	public:
		/// <summary>
		/// Представляет раздел "Сеть". Содержит основные настройки сети.
		/// </summary>
		struct alignas(1) NetworkSection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "DHCP". Использование DHCP.
			/// </summary>
			Boolean UseDhcp;
		public:
			/// <summary>
			/// Поле для хранения параметра "IP-адрес". Основной сетевой идентификатор устройства.
			/// </summary>
			IPv4Address IPAddress;
		public:
			/// <summary>
			/// Поле для хранения параметра "Маска подсети". Диапазон адресов в подсети.
			/// </summary>
			IPv4Address SubnetMask;
		public:
			/// <summary>
			/// Поле для хранения параметра "Основной шлюз". Адрес маршрутизатора для выхода в другие сети.
			/// </summary>
			IPv4Address DefaultGateway;
		};
	private:
		static_assert(sizeof(NetworkSection) == 13, "Размер структуры NetworkSection должен быть равен 13.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Сеть".
		/// </summary>
		NetworkSection Network;
	public:
		/// <summary>
		/// Представляет раздел "Запись". Содержит параметры регистрации данных.
		/// </summary>
		struct alignas(1) RecordingSection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "Сервер". IP-адрес сервера.
			/// </summary>
			IPv4Address Server;
		public:
			/// <summary>
			/// Поле для хранения параметра "Частота". Желаемая частота дискретизации, Гц
			/// </summary>
			Float32 Sampling;
		public:
			/// <summary>
			/// Представляет параметр "Фильтр". Режим фильтрации.
			/// </summary>
			enum class alignas(1) FilterParam :
				UInt8
			{
				/// <summary>
				/// Представляет значение "Быстрый". Быстро меняющиеся процессы.
				/// </summary>
				Fast = 0,

				/// <summary>
				/// Представляет значение "Медленный". Медленно меняющиеся процессы.
				/// </summary>
				Slow = 1,
			};
		public:
			/// <summary>
			/// Поле для хранения параметра "Фильтр". Режим фильтрации.
			/// </summary>
			FilterParam Filter;
		public:
			/// <summary>
			/// Поле для хранения параметра "Нули". Значение, определяющее следует ли записывать нули при старте.
			/// </summary>
			Boolean AutoZero;
		};
	private:
		static_assert(sizeof(RecordingSection) == 10, "Размер структуры RecordingSection должен быть равен 10.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Запись".
		/// </summary>
		RecordingSection Recording;
	public:
		/// <summary>
		/// Представляет раздел "Состояние". Содержит информацию об общем состоянии устройства.
		/// </summary>
		struct alignas(1) StateSection
		{
		public:
			/// <summary>
			/// Поле для хранения параметра "Максимальная температура". Максимальное значение температуры.
			/// </summary>
			Float32 MaxTemp;
		public:
			/// <summary>
			/// Поле для хранения параметра "Минимальная температура". Минимальное значение температуры.
			/// </summary>
			Float32 MinTemp;
		public:
			/// <summary>
			/// Поле для хранения параметра "Максимальное напряжение". Максимальное значение напряжения питания.
			/// </summary>
			Float32 MaxVoltage;
		public:
			/// <summary>
			/// Поле для хранения параметра "Минимальное напряжение". Минимальное значение напряжения питания.
			/// </summary>
			Float32 MinVoltage;
		public:
			/// <summary>
			/// Поле для хранения параметра "Смещение времени". Смещение времени датчика относительно NTP сервера.
			/// </summary>
			TimeSpan TimeOffset;
		public:
			/// <summary>
			/// Поле для хранения параметра "Частота". Реальная частота дискретизации, Гц
			/// </summary>
			Float32 Sampling;
		public:
			/// <summary>
			/// Поле для хранения параметра "Полоса". Реальная полоса пропускания, Гц
			/// </summary>
			Float32 Bandwidth;
		public:
			/// <summary>
			/// Поле для хранения параметра "Каналы". Число каналов AD7730
			/// </summary>
			UInt8 Channels;
		public:
			struct alignas(1) Chip0Param { UInt8  _Value; };
		public:
			/// <summary>
			/// Поле для хранения параметра "Чип 0". Коды ошибок чип AD7730#0.
			/// </summary>
			Chip0Param Chip0;
		public:
			struct alignas(1) Chip1Param { UInt8  _Value; };
		public:
			/// <summary>
			/// Поле для хранения параметра "Чип 1". Коды ошибок чип AD7730#1.
			/// </summary>
			Chip1Param Chip1;
		public:
			struct alignas(1) Chip2Param { UInt8  _Value; };
		public:
			/// <summary>
			/// Поле для хранения параметра "Чип 2". Коды ошибок чип AD7730#2.
			/// </summary>
			Chip2Param Chip2;
		public:
			struct alignas(1) Chip3Param { UInt8  _Value; };
		public:
			/// <summary>
			/// Поле для хранения параметра "Чип 3". Коды ошибок чип AD7730#3.
			/// </summary>
			Chip3Param Chip3;
		public:
			/// <summary>
			/// Представляет параметр "Датчик". Состояние датчика.
			/// </summary>
			enum class alignas(1) SensorParam :
				UInt8
			{
				/// <summary>
				/// Представляет значение "Простой". Простаивает (нет соединения с сервером).
				/// </summary>
				Idle = 0,

				/// <summary>
				/// Представляет значение "Регистрация". Идет регистрация и передача на сервер.
				/// </summary>
				Recording = 1,
			};
		public:
			/// <summary>
			/// Поле для хранения параметра "Датчик". Состояние датчика.
			/// </summary>
			SensorParam Sensor;
		};
	private:
		static_assert(sizeof(StateSection) == 38, "Размер структуры StateSection должен быть равен 38.");
	public:
		/// <summary>
		/// Поле для хранения раздела "Состояние".
		/// </summary>
		StateSection State;
	};

	inline const Knot::Data& Knot::GetData(void) const noexcept
	{
		//	Преобразование и возврат ссылки на данные узла.
		return *reinterpret_cast<const Data*>(Buffer.data());
	}

	inline Knot::Data& Knot::GetData(void) noexcept
	{
		//	Преобразование и возврат ссылки на данные узла.
		return *reinterpret_cast<Data*>(Buffer.data());
	}

	inline const Knot::Datagram& Knot::GetDatagram(void) const noexcept
	{
		//	Возврат ссылки на датаграмму.
		return _Datagram;
	}

	inline Knot::Datagram& Knot::GetDatagram(void) noexcept
	{
		//	Возврат ссылки на датаграмму.
		return _Datagram;
	}

	inline bool Knot::Datagram::SetData(const std::uint8_t* source, std::uint16_t size)
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

	inline Knot::Datagram::operator const std::uint8_t* () noexcept
	{
		//	Возврат указателя на данные.
		return _Buffer.data();
	}

	inline std::uint16_t Knot::Datagram::GetSize(void) const noexcept
	{
		//	Возврат размера данных.
		return _Size;
	}

	inline bool Knot::Invoke(void)
	{
		//	Требуется отправить ответ.
		return true;
	}
}

#pragma pack(pop) // вернуть предыдущее значение выравнивания
