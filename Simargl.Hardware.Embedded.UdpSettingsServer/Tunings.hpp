#pragma once

//	Подключение стандартных заголовочных файлов.
#include <cstdint>
#include <climits>

/// <summary>
/// Содержит типы для работы с настройками.
/// </summary>
namespace Simargl::Embedded::Tunings
{


	namespace Core
	{
		/// <summary>
		/// Представляет беззнаковое целое число фиксированной ширины 8 бит.
		/// </summary>
		using UInt8 = uint8_t;

		/// <summary>
		/// Представляет беззнаковое целое число фиксированной ширины 16 бит.
		/// </summary>
		using UInt16 = uint16_t;

		/// <summary>
		/// Представляет беззнаковое целое число фиксированной ширины 32 бита.
		/// </summary>
		using UInt32 = uint32_t;

		/// <summary>
		/// Представляет беззнаковое целое число фиксированной ширины 64 бита.
		/// </summary>
		using UInt64 = uint64_t;

		/// <summary>
		/// Представляет целое число фиксированной ширины 8 бит со знаком.
		/// </summary>
		using Int8 = int8_t;

		/// <summary>
		/// Представляет целое число фиксированной ширины 16 бит со знаком.
		/// </summary>
		using Int16 = int16_t;

		/// <summary>
		/// Представляет целое число фиксированной ширины 32 бита со знаком.
		/// </summary>
		using Int32 = int32_t;

		/// <summary>
		/// Представляет целое число фиксированной ширины 64 бита со знаком.
		/// </summary>
		using Int64 = int64_t;

		/// <summary>
		/// Представляет число с плавающей запятой фиксированной ширины 32 бита (одинарная точность, IEEE 754).
		/// </summary>
		using Float32 = float;

		/// <summary>
		/// Представляет число с плавающей запятой фиксированной ширины 64 бита (двойная точность, IEEE 754).
		/// </summary>
		using Float64 = double;

		//	Проверка размеров базовых типов.
		static_assert(sizeof(UInt8)* CHAR_BIT == 8, "Ширина UInt8 должна составлять ровно 8 бит.");
		static_assert(sizeof(UInt16)* CHAR_BIT == 16, "Ширина UInt16 должна составлять ровно 16 бит.");
		static_assert(sizeof(UInt32)* CHAR_BIT == 32, "Ширина UInt32 должна составлять ровно 32 бита.");
		static_assert(sizeof(UInt64)* CHAR_BIT == 64, "Ширина UInt64 должна составлять ровно 64 бита.");
		static_assert(sizeof(Int8)* CHAR_BIT == 8, "Ширина Int8 должна составлять ровно 8 бит.");
		static_assert(sizeof(Int16)* CHAR_BIT == 16, "Ширина Int16 должна составлять ровно 16 бит.");
		static_assert(sizeof(Int32)* CHAR_BIT == 32, "Ширина Int32 должна составлять ровно 32 бита.");
		static_assert(sizeof(Int64)* CHAR_BIT == 64, "Ширина Int64 должна составлять ровно 64 бита.");
		static_assert(sizeof(Float32)* CHAR_BIT == 32, "Ширина Float32 должна составлять ровно 32 бита.");
		static_assert(sizeof(Float64)* CHAR_BIT == 64, "Ширина Float64 должна составлять ровно 64 бита.");
	}

	namespace Core
	{
		enum class TypeFormat :
			UInt8
		{
			UInt8,
			UInt16,
			UInt32,
			UInt64,
			Int8,
			Int16,
			Int32,
			Int64,
			Separator,
		};

		using TypeSize = UInt32;

		template<typename TyValue, TypeFormat ValFormat, TypeSize ValSize>
		class TypeInfo
		{
		public:
			using Value = typename TyValue;
			static constexpr TypeFormat Format = ValFormat;
			static constexpr uint32_t Size = ValSize;
		};

	}

	template<Core::TypeSize ValSize>
	class UInt;

	template<>
	class UInt<1> :
		Core::TypeInfo<Core::UInt8, Core::TypeFormat::UInt8, 1>
	{

	};

	template<>
	class UInt<2> :
		Core::TypeInfo<Core::UInt16, Core::TypeFormat::UInt16, 2>
	{

	};

	template<>
	class UInt<4> :
		Core::TypeInfo<Core::UInt32, Core::TypeFormat::UInt32, 4>
	{

	};

	template<>
	class UInt<8> :
		Core::TypeInfo<Core::UInt64, Core::TypeFormat::UInt64, 8>
	{

	};


	template<Core::TypeSize ValSize>
	class Int;

	template<>
	class Int<1> :
		Core::TypeInfo<Core::Int8, Core::TypeFormat::Int8, 1>
	{

	};

	template<>
	class Int<2> :
		Core::TypeInfo<Core::Int16, Core::TypeFormat::Int16, 2>
	{

	};

	template<>
	class Int<4> :
		Core::TypeInfo<Core::Int32, Core::TypeFormat::Int32, 4>
	{

	};

	template<>
	class Int<8> :
		Core::TypeInfo<Core::Int64, Core::TypeFormat::Int64, 8>
	{

	};


	/// <summary>
	/// Представляет версию.
	/// </summary>
	/// <typeparam name="Number">
	/// Номер версии.
	/// </typeparam>
	template <uint32_t Number>
	class Version;

	class Separator :
		Core::TypeInfo<void, Core::TypeFormat::Separator, 0>
	{

	};

	/// <summary>
	/// Представляет выравнивание данных.
	/// </summary>
	/// <typeparam name="Size">
	/// Размер области для выравнивания данных.
	/// </typeparam>
	template <uint32_t Size>
	class Alignment;

	/// <summary>
	/// Представляет значение, определяющее режим доступа к настраиваемому параметру.
	/// </summary>
	enum class AccessMode :
		std::uint8_t
	{
		/// <summary>
		/// Доступ к параметру определяется автоматически.
		/// </summary>
		Auto,

		/// <summary>
		/// Полный доступ к параметру: чтение и запись.
		/// </summary>
		ReadWrite,

		/// <summary>
		/// Параметр доступен только для чтения.
		/// </summary>
		ReadOnly,

		/// <summary>
		/// Параметр доступен только для записи.
		/// </summary>
		WriteOnly,

		/// <summary>
		/// Параметр недоступен.
		/// </summary>
		NoAccess,
	};

	/// <summary>
	/// Представляет спецификацию настраиваемого параметра.
	/// </summary>
	/// <typeparam name="Type">
	/// Тип значения параметра.
	/// </typeparam>
	/// <typeparam name="Mode">
	/// Значение, определяющее режим доступа к параметру.
	/// </typeparam>
	template <typename Type, AccessMode Mode = AccessMode::Auto>
	class Specification;

	/// <summary>
	/// Представляет определение карты настраиваемых параметров.
	/// </summary>
	/// <typeparam name="SpecificationCollection">
	/// Коллекция спецификаций настраиваемых параметров.
	/// </typeparam>
	template <typename... SpecificationCollection>
	class MapDefinition;

	/// <summary>
	/// Представляет промежуточную специализацию определения карты настраиваемых параметров.
	/// </summary>
	/// <typeparam name="SpecificationCollection">
	/// Коллекция предыдущих спецификаций настраиваемых параметров.
	/// </typeparam>
	/// <typeparam name="Type">
	/// Тип значения параметра.
	/// </typeparam>
	/// <typeparam name="Mode">
	/// Значение, определяющее режим доступа к параметру.
	/// </typeparam>
	template <typename TyType, AccessMode ValMode, typename... TyArrSpecificationCollection>
	class MapDefinition<Specification<TyType, ValMode>, TyArrSpecificationCollection...> :
		public MapDefinition<TyArrSpecificationCollection...>
	{

	};

	/// <summary>
	/// Представляет терминальную специализацию определения карты настраиваемых параметров.
	/// </summary>
	/// <typeparam name="Type">
	/// Тип значения параметра.
	/// </typeparam>
	/// <typeparam name="Mode">
	/// Значение, определяющее режим доступа к параметру.
	/// </typeparam>
	template <typename Type, AccessMode Mode>
	class MapDefinition<Specification<Type, Mode>>
	{

	};


	template <typename TyMap>
	class Temp;

	template <typename... TyArrSpecificationCollection>
	class TempBase
	{

	};


	template <typename... TyArrSpecificationCollection>
	class Temp<MapDefinition<TyArrSpecificationCollection...>> :
		public TempBase< TyArrSpecificationCollection...>
	{
		static constexpr uint8_t Size = 0;
	};




	/// <summary>
	/// Представляет определение узла настраиваемых параметров.
	/// </summary>
	/// <typeparam name="Map">
	/// Карта настраиваемых параметров.
	/// </typeparam>
	template <typename TyMap>
	class KnotDefinition
	{

	};





	//	Определение карты настраиваемых параметров.
	using Map = MapDefinition
		<
			Specification<Version<1>>,
			Specification<UInt<8>>,
			Specification<Alignment<4>>,
			Specification<Separator>,
			Specification<UInt<1>, AccessMode::ReadOnly>,
			Specification<Int<2>, AccessMode::ReadOnly>
		>;
	
	/// <summary>
	/// Предоставляет общие 
	/// </summary>
	class Configuration
	{

	};

	//	Определение узла настраиваемых параметров.
	using Knot = KnotDefinition<Map>;


}
