#pragma once

#include <array>

namespace simargl::embedded::tunings {
	namespace core {
		class tunings_config_definer {

		};

		enum class tunings_section_id : std::uint32_t {
			unknown,
			spec,		//	Раздел спецификации.
			identity,	//	Раздел идентификации.
			firmware,	//	Раздел информации о прошивке.
			ports,		//	Раздел настройки портов.
		};

		enum class tunings_access : std::uint8_t {
			no_access,
			read_only,
			write_only,
			read_write,
		};

		template <tunings_section_id id, tunings_access access, class... types>
		class tunings_section;

		template <class... sections>
		class tunings_map_definer;

		enum class tunings_type_format : std::uint8_t {
			uint8,
			uint16,
			uint32,
			uint64,
			int8,
			int16,
			int32,
			int64,
			float32,
			float64,
			boolean,
			version,
			device_type,
			serial_number,
			date_only,
			time_only,
			date_time,
			time_span,
			ipv4_address,
			mac_address,


			bit_field,

			binary,
			string,


			command,
		};


		class tunings_bit_field {

		};

		class tunings_command {
			std::uint8_t code;
		};
		static_assert(sizeof(tunings_command) == sizeof(std::uint8_t), "sizeof(tunings_cmd) != sizeof(std::uint8_t)");

		class tunings_version {

		};

		enum class tunings_device_type : std::uint16_t {
			unknown,
			adxl_357,
			ad_7730,
		};

		class tunings_serial_number {

		};

		class tunings_date_only {

		};

		class tunings_time_only {

		};

		class tunings_date_time {

		};

		class tunings_time_span {

		};




		class tunings_ipv4_address {

		};
		class tunings_mac_address {

		};

		template <std::uint8_t size>
		class tunings_alignment {

		};

		template <std::uint16_t size>
		class tunings_binary {

		};

		template <std::uint16_t length>
		class tunings_string {

		};

		namespace types {
			using uint8 = std::uint8_t;
			using uint16 = std::uint16_t;
			using uint32 = std::uint32_t;
			using uint64 = std::uint64_t;
			using int8 = std::int8_t;
			using int16 = std::int16_t;
			using int32 = std::int32_t;
			using int64 = std::int64_t;
			using float32 = float;
			using float64 = double;
			using boolean = bool;
			using bit_field = tunings_bit_field;
			using command = tunings_command;
			using version = tunings_version;
			using device_type = tunings_device_type;
			using serial_number = tunings_serial_number;
			using date_only = tunings_date_only;
			using time_only = tunings_time_only;
			using date_time = tunings_date_time;
			using time_span = tunings_time_span;
			using ipv4_address = tunings_ipv4_address;
			using mac_address = tunings_mac_address;

			template <std::uint8_t size>
			using alignment = tunings_alignment<size>;

			template <std::uint16_t size>
			using binary = tunings_binary<size>;

			template <std::uint16_t length>
			using string = tunings_string<length>;

			static_assert(sizeof(float32) == sizeof(std::uint32_t), "sizeof(float32) != sizeof(std::uint32_t)");
			static_assert(sizeof(float64) == sizeof(std::uint64_t), "sizeof(float64) != sizeof(std::uint64_t)");
		}


		template <class config, class map>
		class tunings_knot_definer
		{

		};
	} // core

	namespace common {

	} // common

	namespace spec {
		using namespace common;
		using namespace core::types;

		using config_definer = core::tunings_config_definer;
		using id = core::tunings_section_id;
		using access = core::tunings_access;

		template <id id, access access, class... types>
		using section = core::tunings_section<id, access, types...>;

		template <class... sections>
		using map_definer = core::tunings_map_definer<sections...>;

		template <class config, class map>
		using knot_definer = core::tunings_knot_definer<config, map>;



	} // spec












} // simargl::embedded::tunings

//
///// <summary>
///// Содержит типы для работы с настраиваемыми параметрами.
///// </summary>
//namespace Simargl::Embedded::Tunings
//{
//
//
//
//	/// <summary>
//	/// Представляет узел настраиваемых параметров.
//	/// </summary>
//	class TuningsKnot;
//
//	/// <summary>
//	/// Представляет датаграмму.
//	/// </summary>
//	class TuningsDatagram;
//	
//	/// <summary>
//	/// Представляет узел настраиваемых параметров.
//	/// </summary>
//	class TuningsKnot
//	{
//	private:
//		std::array<uint8_t, TuningsDatagram::DatagramMaxSize> _Datagram;
//		typename TuningsDatagram::SizeType _DatagramSize;
//	public:
//		TuningsDatagram Datagram;
//	public:
//		inline TuningsKnot(void) noexcept = default;
//	private:
//		TuningsKnot(const TuningsKnot&) = delete;
//		TuningsKnot(TuningsKnot&&) = delete;
//		TuningsKnot& operator = (const TuningsKnot&) = delete;
//		TuningsKnot& operator = (TuningsKnot&&) = delete;
//	public:
//		inline bool Invoke();
//	private:
//		class tunings_core;
//	};
//
//	/// <summary>
//	/// Представляет датаграмму.
//	/// </summary>
//	class TuningsDatagram
//	{
//	public:
//		enum : size_t {
//			DatagramMaxSize = 508
//		};
//	public:
//		using SizeType = uint16_t;
//	private:
//		std::array<uint8_t, TuningsDatagram::DatagramMaxSize> _Buffer;
//		SizeType _Size;
//	public:
//		inline TuningsDatagram(void) noexcept = default;
//	private:
//		TuningsDatagram(const TuningsDatagram&) = delete;
//		TuningsDatagram(TuningsDatagram&&) = delete;
//		TuningsDatagram& operator = (const TuningsDatagram&) = delete;
//		TuningsDatagram& operator = (TuningsDatagram&&) = delete;
//	public:
//		inline bool SetData(const uint8_t* source, SizeType size)
//		{
//			if (size > DatagramMaxSize)
//			{
//				return false;
//			}
//			std::copy_n(source, size, _Buffer.begin());
//			_Size = size;
//			return true;
//		}
//	public:
//		inline operator const uint8_t* () noexcept
//		{
//			return _Buffer.data();
//		}
//	public:
//		inline SizeType GetSize(void) const noexcept
//		{
//			return _Size;
//		}
//	};
//
//
//
//
//	//class TuningsKnot::Core
//	//{
//	//	enum class DatagramFormat;
//	//	class DatagramPreamble;
//	//};
//
//	//inline bool TuningsKnot::Invoke()
//	//{
//	//	return true;
//	//}
//
//	//enum class TuningsKnot::Core::DatagramFormat :
//	//	int32_t
//	//{
//
//	//};
//
//	//class TuningsKnot::Core::DatagramPreamble
//	//{
//	//	static_assert(sizeof(TuningsKnot::Core::DatagramPreamble) == 24);
//	//private:
//	//	enum : int32_t
//	//	{
//	//		Signature = 0x0DE83994,
//	//		PreambleFormat = 0x10000000,
//	//		FormatVersion = 1,
//	//	};
//	//private:
//	//	int32_t _Signature;
//	//	int32_t _PreambleFormat;
//	//	int64_t _Size;
//	//	int32_t _FormatVersion;
//	//	DatagramFormat _DatagramFormat;
//
//	//};
//
//
//}
