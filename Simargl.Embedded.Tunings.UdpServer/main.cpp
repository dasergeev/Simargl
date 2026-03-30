#include "Tunings.g.hpp"

// Исключение редко используемых заголовков из windows.h для ускорения компиляции
#define WIN32_LEAN_AND_MEAN

// Удаление макросов min и max, чтобы не мешали стандартным функциям std::min/std::max
#define NOMINMAX

#define _WINSOCK_DEPRECATED_NO_WARNINGS // Подавление предупреждений о том, что inet_ntoa считается устаревшей

// Основные определения и функции Winsock2
#include <winsock2.h>

// Дополнительные функции для TCP/IP (например, inet_pton, getaddrinfo)
#include <ws2tcpip.h>

// C-функции ввода/вывода: printf и др.
#include <cstdio>

// C-функции для работы со строками: strlen, strnicmp
#include <cstring>

// Автоматическая линковка с библиотекой Ws2_32.lib (реализация Winsock)
#pragma comment(lib, "Ws2_32.lib")

// Стандартный ввод/вывод в стиле C++ (в коде напрямую не используется)
#include <iostream>

int main()
{
    auto knot = Simargl::Embedded::Tunings::Knot();
   

    // Структура для информации о реализации Winsock. Инициализируется нулями.
    WSADATA wsa{};

    // Инициализация библиотеки Winsock версии 2.2
    // MAKEWORD(2,2) — запрашиваемая версия API
    // &wsa — указатель на структуру, в которую будет записана информация
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        // Вывод ошибки и её кода
        std::printf("WSAStartup failed: %d\n", WSAGetLastError());

        // Завершение программы с ошибкой
        return 1;
    }

    // Создание сокета
    // AF_INET — семейство адресов IPv4
    // SOCK_DGRAM — тип сокета: датаграммы (UDP)
    // IPPROTO_UDP — протокол: UDP
    SOCKET sock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

    // Проверка на ошибку
    if (sock == INVALID_SOCKET)
    {
        // Вывод кода ошибки
        std::printf("socket failed: %d\n", WSAGetLastError());

        // Освобождение ресурсов Winsock
        WSACleanup();

        // Завершение с ошибкой
        return 1;
    }

    // Переменная для установки опции сокета
    BOOL yes = TRUE;

    // Установка опции сокета SO_BROADCAST
    // sock — дескриптор сокета
    // SOL_SOCKET — уровень параметров сокета
    // SO_BROADCAST — разрешение на широковещательную передачу
    // (const char*)&yes — указатель на значение (TRUE)
    // sizeof(yes) — размер значения
    if (setsockopt(sock, SOL_SOCKET, SO_BROADCAST, (const char*)&yes, sizeof(yes)) == SOCKET_ERROR)
    {
        // Ошибка
        std::printf("setsockopt(SO_BROADCAST) failed: %d\n", WSAGetLastError());

        // Закрытие сокета
        closesocket(sock);

        // Освобождение ресурсов Winsock
        WSACleanup();


        return 1;
    }

    // Структура адреса для привязки сокета
    sockaddr_in addr{};

    // Семейство адресов: IPv4
    addr.sin_family = AF_INET;

    // Номер порта: 49004, преобразование в сетевой порядок байт
    addr.sin_port = htons(49004);

    // IP-адрес: слушать на всех интерфейсах, преобразование в сетевой порядок
    addr.sin_addr.s_addr = htonl(INADDR_ANY);

    // Привязка сокета к адресу
    // sock — дескриптор сокета
    // (const sockaddr*)&addr — указатель на структуру адреса
    // sizeof(addr) — размер структуры
    if (bind(sock, (const sockaddr*)&addr, sizeof(addr)) == SOCKET_ERROR)
    {
        // Ошибка
        std::printf("bind failed: %d\n", WSAGetLastError());

        // Закрытие сокета
        closesocket(sock);

        // Очистка Winsock
        WSACleanup();

        return 1;
    }

    // Уведомление, что сокет слушает порт
    std::printf("Listening UDP on 0.0.0.0:49004\n");

    // Размер буфера для приёма
    constexpr int BUF_SZ = 2048;

    // Буфер для входящих данных
    char buffer[BUF_SZ];

    // Бесконечный цикл обработки сообщений
    for (;;)
    {
        // Структура для хранения адреса клиента
        sockaddr_in peer{};

        // Размер структуры (обязателен для recvfrom)
        int peerLen = sizeof(peer);

        // Приём данных
        // sock — сокет
        // buffer — буфер для данных
        // BUF_SZ — максимальный размер принимаемых данных
        // 0 — флаги (обычно 0)
        // (sockaddr*)&peer — адрес клиента
        // &peerLen — указатель на переменную, содержащую размер структуры
        int recvLen = recvfrom(sock, buffer, BUF_SZ, 0, (sockaddr*)&peer, &peerLen);

        // Проверка на ошибку
        if (recvLen == SOCKET_ERROR)
        {
            // Получение кода ошибки
            int err = WSAGetLastError();

            // Игнорируем ошибку WSAECONNRESET (возникает при ICMP-пакетах "порт недоступен")
            if (err != WSAECONNRESET)
            {
                // Иные ошибки выводим
                std::printf("recvfrom failed: %d\n", err);
            }

            // Переход к следующему циклу ожидания
            continue;
        }

        // Преобразование IP-адреса клиента в строку формата "x.x.x.x"
        const char* ip = inet_ntoa(peer.sin_addr);

        // Преобразование порта из сетевого порядка в хостовый
        unsigned short port = ntohs(peer.sin_port);

        // Сообщение о полученных данных
        std::printf("Got %d bytes from %s:%hu\n", recvLen, ip ? ip : "<ip>", port);

        if (!knot.GetDatagram().SetData(reinterpret_cast<const uint8_t*>(buffer), recvLen))
        {
            //  Ошибка.
            std::printf("recvLen > Knot::DatagramMaxSize: %d\n", recvLen);

            // Переход к следующему циклу ожидания
            continue;
        }

        if (knot.Invoke())
        {
            // Переменная для количества отправленных байт
            int sent = 0;

            sent = sendto(
                sock,
                reinterpret_cast<const char*>(static_cast<const uint8_t*>(knot.GetDatagram())),
                (int)knot.GetDatagram().GetSize(),
                0,
                (const sockaddr*)&peer,
                peerLen);

            // Проверка успешности отправки
            if (sent == SOCKET_ERROR)
            {
                // Сообщение об ошибке
                std::printf("sendto failed: %d\n", WSAGetLastError());
            }
        }
    }

    // Закрытие сокета (до сюда выполнение фактически не дойдёт из-за бесконечного цикла)
    closesocket(sock);

    // Освобождение ресурсов Winsock
    WSACleanup();

    // Завершение программы с кодом успеха
    return 0;
}
