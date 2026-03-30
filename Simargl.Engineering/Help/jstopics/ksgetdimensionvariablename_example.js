hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetDimensionVariableName - пример использования",
hmDescription:"reference iter = ::CreateIterator(LDIMENSION_OBJ, 0); \/\/ создаем итератор по линейным размерам",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetDimensionVariableName - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1725660\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference iter = ::CreateIterator(LDIMENSION_OBJ, 0); \/\/ создаем итератор по линейным размерам<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (iter) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference obj = ::MoveIterator(iter, \'F\'); \/\/ первый объект<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128]; \/\/ буфер для имени переменной<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">while (::ExistObj(obj)) { \/\/ размер существует<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::ksGetDimensionVariableName(obj, buf, 128)) \/\/ получим имя параметрической переменной размера<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(buf); \/\/ выведем имя переменнной<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">else \/\/ у размера нет переменной<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(&quot;Размер не имеет переменной&quot;); \/\/ сообщение<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">obj = ::MoveIterator(iter, \'N\'); \/\/ следующий объект<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::DeleteIterator(iter); \/\/ удалим итератор<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r"
})
