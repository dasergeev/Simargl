#include <iostream>
#include <array>
#include <cstddef>
#include <utility>

class Configuration
{
public:
    //  Тип индекса
    using TypeOfNumberOfParameters = uint8_t;

};

static_assert(
    std::is_same<Configuration::TypeOfNumberOfParameters, std::uint8_t>::value ||
    std::is_same<Configuration::TypeOfNumberOfParameters, std::uint16_t>::value ||
    std::is_same<Configuration::TypeOfNumberOfParameters, std::uint32_t>::value ||
    std::is_same<Configuration::TypeOfNumberOfParameters, std::uint64_t>::value,
    "IndexParameter must be one of: uint8_t, uint16_t, uint32_t, uint64_t"
    );

enum class AccessMode :
    uint8_t
{
    NoAccess = 0b00,
    ReadOnly = 0b01,
    WriteOnly = 0b10,
    ReadWrite = 0b11,
    Auto,
};

template <typename _TyValue, AccessMode _ValMode = AccessMode::Auto>
class Specification;

enum class ValueFormat :
    uint8_t
{
    UInt8,
    UInt16,
    UInt32,
    UInt64,
    Int8,
    Int16,
    Int32,
    Int64,
    Float32,
    Float64,

    Command = 16,
    Boolean,
    Version,
    DateTime,
    DateOnly,
    TimeOnly,
    IPv4Address,

    Alignment = 48,
    Binary,
    String,

    Separator = 63,
    
    Invalid = 128,
};



class Command
{

};

class Version
{

};

class DateTime
{

};

class DateOnly
{

};

class TimeOnly
{

};

class IPv4Address
{

};

class Separator;

template <size_t _ValSize>
class Alignment;

template <size_t _ValSize>
class Binary;

template <size_t _ValLength>
class String;

template <typename _Ty>
struct ValueFormatExtractorBase
{
private:
    template <typename _TyOther>
    static constexpr bool IsType() noexcept
    {
        return std::is_same<_Ty, _TyOther>::value;
    }
    static constexpr ValueFormat GetValueFormat() noexcept
    {
        if (IsType<uint8_t>()) return ValueFormat::UInt8;
        else if (IsType<uint16_t>()) return ValueFormat::UInt16;
        else if (IsType<uint32_t>()) return ValueFormat::UInt32;
        else if (IsType<uint64_t>()) return ValueFormat::UInt64;
        else if (IsType<int8_t>()) return ValueFormat::Int8;
        else if (IsType<int16_t>()) return ValueFormat::Int16;
        else if (IsType<int32_t>()) return ValueFormat::Int32;
        else if (IsType<int64_t>()) return ValueFormat::Int64;
        else if (IsType<float>()) return ValueFormat::Float32;
        else if (IsType<double>()) return ValueFormat::Float64;
        else if (IsType<bool>()) return ValueFormat::Boolean;
        else if (IsType<Command>()) return ValueFormat::Command;
        else if (IsType<DateTime>()) return ValueFormat::DateTime;
        else if (IsType<DateOnly>()) return ValueFormat::DateOnly;
        else if (IsType<TimeOnly>()) return ValueFormat::TimeOnly;
        else if (IsType<IPv4Address>()) return ValueFormat::IPv4Address;
        else if (IsType<Separator>()) return ValueFormat::Separator;
        else return ValueFormat::Invalid;
    }
public:
    static constexpr ValueFormat Format = GetValueFormat();
   
};

template <size_t _ValSize>
struct ValueFormatExtractorBase<Alignment<_ValSize>>
{
    static constexpr ValueFormat Format = ValueFormat::Alignment;
};

template <size_t _ValSize>
struct ValueFormatExtractorBase<Binary<_ValSize>>
{
    static constexpr ValueFormat Format = ValueFormat::Binary;
};

template <size_t _ValLength>
struct ValueFormatExtractorBase<String<_ValLength>>
{
    static constexpr ValueFormat Format = ValueFormat::String;
};

static_assert(sizeof(float) == sizeof(uint32_t));
static_assert(sizeof(double) == sizeof(uint64_t));


template <typename _Ty>
class ValueFormatExtractor :
    public ValueFormatExtractorBase<_Ty>
{
    static_assert(ValueFormatExtractorBase<_Ty>::Format != ValueFormat::Invalid);
};

template <typename _Ty>
struct SizeExtractor
{
    static constexpr size_t Size = sizeof(_Ty);
};

template <>
struct SizeExtractor<bool>
{
    static constexpr size_t Size = 1;
};

template <size_t _ValSize>
struct SizeExtractor<Alignment<_ValSize>>
{
    static constexpr size_t Size = _ValSize;
};

template <size_t _ValSize>
struct SizeExtractor<Binary<_ValSize>>
{
    static constexpr size_t Size = _ValSize;
};

template <size_t _ValLength>
struct SizeExtractor<String<_ValLength>>
{
    static constexpr size_t Size = _ValLength;
};

template <>
struct SizeExtractor<Separator>
{
    static constexpr size_t Size = 0;
};

/*

     = AccessModeCore::NoAccess,
     = AccessModeCore::ReadOnly,
     = AccessModeCore::WriteOnly,
     = AccessModeCore::ReadWrite,
    ,

                NoAccess    ReadOnly    WriteOnly   ReadWrite   Auto

    UInt8       Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    UInt16      Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    UInt32      Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    UInt64      Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Int8        Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Int16       Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Int32       Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Int64       Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Float32     Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Float64     Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Boolean     Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Version     Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    DateTime    Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    DateOnly    Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    TimeOnly    Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    IPv4Address Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Binary      Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    String      Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite
    Alignment   Invalid     ReadOnly    WriteOnly   ReadWrite   ReadWrite

    Command     Invalid     Invalid     WriteOnly   Invalid     WriteOnly
    Separator   NoAccess    Invalid     Invalid     Invalid     NoAccess
    Invalid     Invalid     Invalid     Invalid     Invalid     Invalid
*/

struct AccessModeExtractor
{
    static constexpr AccessMode Invalid = AccessMode::Auto;

    static constexpr AccessMode GetAccessMode(ValueFormat format, AccessMode mode) noexcept
    {
        switch (format)
        {
        case ValueFormat::UInt8:
        case ValueFormat::UInt16:
        case ValueFormat::UInt32:
        case ValueFormat::UInt64:
        case ValueFormat::Int8:
        case ValueFormat::Int16:
        case ValueFormat::Int32:
        case ValueFormat::Int64:
        case ValueFormat::Float32:
        case ValueFormat::Float64:
        case ValueFormat::Boolean:
        case ValueFormat::Version:
        case ValueFormat::DateTime:
        case ValueFormat::DateOnly:
        case ValueFormat::TimeOnly:
        case ValueFormat::IPv4Address:
        case ValueFormat::Binary:
        case ValueFormat::String:
        case ValueFormat::Alignment:
            switch (mode)
            {
            case AccessMode::ReadOnly:
            case AccessMode::WriteOnly:
            case AccessMode::ReadWrite:
                return mode;
            case AccessMode::Auto:
                return AccessMode::ReadWrite;
            default:
                return Invalid;
            }
            break;
        case ValueFormat::Command:
            switch (mode)
            {
            case AccessMode::WriteOnly:
            case AccessMode::Auto:
                return AccessMode::WriteOnly;
            default:
                return Invalid;
            }
            break;
        case ValueFormat::Separator:
            switch (mode)
            {
            case AccessMode::NoAccess:
            case AccessMode::Auto:
                return AccessMode::NoAccess;
            default:
                return Invalid;
            }
            break;
        default:
            return AccessMode::Auto;
        }
    }
};

struct ParameterInfo
{
    const ValueFormat Format;
    const size_t Size;
    const AccessMode Mode;
};

template <typename _TySpecification>
struct ParameterInfoExtractor;

template <typename _TyValue, AccessMode _ValMode>
struct ParameterInfoExtractor<Specification<_TyValue, _ValMode>>
{
    static constexpr ValueFormat Format = ValueFormatExtractor<_TyValue>::Format;
    static constexpr size_t Size = SizeExtractor<_TyValue>::Size;
    static constexpr AccessMode Mode = AccessModeExtractor::GetAccessMode(Format, _ValMode);
    static_assert(AccessModeExtractor::GetAccessMode(Format, _ValMode) != AccessModeExtractor::Invalid);
    static constexpr ParameterInfo Info = { Format, Size, Mode };
};


template <typename _TySpecification>
constexpr ParameterInfo GetParameter()
{
    return ParameterInfoExtractor<_TySpecification>::Info;
}




template <typename... TyArrSpecification>
class MapDefinition;



template <typename TyMap>
class KnotDefinition;

template <typename... TyArrSpecification>
class KnotDefinition<MapDefinition<TyArrSpecification...>>
{
public:
    static constexpr size_t Count = sizeof...(TyArrSpecification);

    // Возвращает массив параметров
    static constexpr std::array<ParameterInfo, Count> GetParameters()
    {
        return { GetParameter<TyArrSpecification>()... };
    }
};

using Map = MapDefinition<
    Specification<uint8_t>,
    Specification<uint16_t>,
    Specification<uint32_t>,
    Specification<uint64_t>>;




class Knot :
    public KnotDefinition<Map>
{

};



class Block {
public:
    uint8_t* Address;
    size_t Size;
};

template <typename... TyArr>
class TestBase
{


public:
    static constexpr size_t Count = sizeof...(TyArr);



    static constexpr size_t Size = (sizeof(TyArr) + ... + 0);

    std::array<uint8_t, Size> buffer{};

private:
    static constexpr std::array<size_t, Count> make_sizes() {
        return { sizeof(TyArr)... };
    }

    template <size_t... I>
    static constexpr std::array<size_t, Count>
        make_offsets(std::index_sequence<I...>) {
        std::array<size_t, Count> arr{};
        size_t off = 0;
        ((arr[I] = off, off += sizeof(TyArr)), ...);
        return arr;
    }

public:
    static constexpr std::array<size_t, Count> sizes = make_sizes();
    static constexpr std::array<size_t, Count> offsets =
        make_offsets(std::index_sequence_for<TyArr...>{});

    Block GetBlock(size_t index) {
        return { buffer.data() + offsets[index], sizes[index] };
    }
};

#define WIN32_LEAN_AND_MEAN
#define NOMINMAX
#define _WINSOCK_DEPRECATED_NO_WARNINGS  // чтобы не ругался на inet_ntoa


#include <winsock2.h>
#include <ws2tcpip.h>
#include <cstdio>
#include <cstring>

#pragma comment(lib, "Ws2_32.lib")


int main() {
    using Test = TestBase<uint8_t, uint16_t, uint32_t, uint64_t>;
    Test t;

    using extr = ParameterInfoExtractor<Specification<uint32_t>>;

    std::cout << "Format = " << static_cast<int>(extr::Info.Format) << "\n";
    std::cout << "Size = " << static_cast<int>(extr::Info.Size) << "\n";
    std::cout << "Mode = " << static_cast<int>(extr::Info.Mode) << "\n";


    std::cout << "Format = " << static_cast<int>(ValueFormatExtractor<Binary<10>>::Format) << "\n";

    std::cout << "Count = " << Test::Count << "\n";
    std::cout << "Size  = " << Test::Size << "\n";

    for (size_t i = 0; i < Test::Count; i++) {
        Block b = t.GetBlock(i);
        std::cout << "Block " << i
            << ": offset=" << Test::offsets[i]
            << ", size=" << Test::sizes[i] << "\n";
    }

    std::cout << "\n";


    auto params = Knot::GetParameters();
    for (size_t i = 0; i < Knot::Count; i++)
    {
        auto param = params[i];

        std::cout << "param " << i << " Format = " << static_cast<int>(param.Format)
            << " Size = " << static_cast<int>(param.Size)
            << " Mode = " << static_cast<int>(param.Mode) << "\n";

    }




    // 1) Инициализация Winsock
    WSADATA wsa{};
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) {
        std::printf("WSAStartup failed: %d\n", WSAGetLastError());
        return 1;
    }

    // 2) Создаём UDP-сокет
    SOCKET sock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (sock == INVALID_SOCKET) {
        std::printf("socket failed: %d\n", WSAGetLastError());
        WSACleanup();
        return 1;
    }

    // 3) Разрешаем широковещание (для получения не обязательно, но не мешает; для ответа пригодится)
    BOOL yes = TRUE;
    if (setsockopt(sock, SOL_SOCKET, SO_BROADCAST, (const char*)&yes, sizeof(yes)) == SOCKET_ERROR) {
        std::printf("setsockopt(SO_BROADCAST) failed: %d\n", WSAGetLastError());
        closesocket(sock);
        WSACleanup();
        return 1;
    }

    // 4) Привязываем к 0.0.0.0:49004
    sockaddr_in addr{};
    addr.sin_family = AF_INET;
    addr.sin_port = htons(49004);
    addr.sin_addr.s_addr = htonl(INADDR_ANY);

    if (bind(sock, (const sockaddr*)&addr, sizeof(addr)) == SOCKET_ERROR) {
        std::printf("bind failed: %d\n", WSAGetLastError());
        closesocket(sock);
        WSACleanup();
        return 1;
    }

    std::printf("Listening UDP on 0.0.0.0:49004\n");

    // 5) Основной цикл приёма/ответа
    constexpr int BUF_SZ = 2048;
    char buffer[BUF_SZ];

    for (;;) {
        sockaddr_in peer{};
        int peerLen = sizeof(peer);

        int recvLen = recvfrom(sock, buffer, BUF_SZ, 0, (sockaddr*)&peer, &peerLen);
        if (recvLen == SOCKET_ERROR) {
            int err = WSAGetLastError();
            // На UDP изредка бывает WSAECONNRESET из-за ICMP — просто пропустим
            if (err != WSAECONNRESET) {
                std::printf("recvfrom failed: %d\n", err);
            }
            continue;
        }

        const char* ip = inet_ntoa(peer.sin_addr);
        unsigned short port = ntohs(peer.sin_port);
        std::printf("Got %d bytes from %s:%hu\n", recvLen, ip ? ip : "<ip>", port);

        // Небольшой "анализ": если начинается с "ping" (без учета регистра) — отвечаем "pong",
        // иначе отправляем ACK с количеством байт.
        int sent = 0;
        if (recvLen >= 4 && _strnicmp(buffer, "ping", 4) == 0) {
            const char* pong = "pong";
            sent = sendto(sock, pong, (int)std::strlen(pong), 0, (const sockaddr*)&peer, peerLen);
        }
        else {
            char reply[128];
            _snprintf_s(reply, sizeof(reply), _TRUNCATE, "ACK: %d bytes", recvLen);
            sent = sendto(sock, reply, (int)std::strlen(reply), 0, (const sockaddr*)&peer, peerLen);
        }

        if (sent == SOCKET_ERROR) {
            std::printf("sendto failed: %d\n", WSAGetLastError());
        }
    }

    // недостижимо
    closesocket(sock);
    WSACleanup();
    return 0;
}
