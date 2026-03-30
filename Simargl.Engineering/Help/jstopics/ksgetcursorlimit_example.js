hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetCursorLimit - Пример использования",
hmDescription:"double x, y; \/\/ координаты точки RequestInfo info; \/\/ параметры запроса memset(&info, 0, sizeof(info)); \/\/ очищаем параметры запроса info.prompt = \"Укажите объект\"; \/\/ строка",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetCursorLimit - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1730418\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y; \/\/ координаты точки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RequestInfo info; \/\/ параметры запроса<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset(&amp;info, 0, sizeof(info)); \/\/ очищаем параметры запроса<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.prompt = &quot;Укажите объект&quot;; \/\/ строка подсказки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double lim = ::ksGetCursorLimit(); \/\/ радиус ловушки курсора<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">while (::Cursor(&amp;info, &amp;x ,&amp;y, 0)) { \/\/ запуск процесса указания точки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference obj = ::FindObj(x, y, lim); \/\/ ближайщий объект<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::ExistObj(obj)) \/\/ если объект найден и существует<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LightObj(obj, 1); \/\/ подсвечиваем объект<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
