hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetTextAlign, ksSetTextAlign - Пример использования",
hmDescription:"reference iter = ::CreateIterator(TEXT_OBJ, 0); \/\/ итератор по всем текстам документа",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetTextAlign, ksSetTextAlign - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1721654\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference iter = ::CreateIterator(TEXT_OBJ, 0); \/\/ итератор по всем текстам документа<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (iter) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference txt = ::MoveIterator(iter, \'F\'); \/\/ первый текст<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">while (::ExistObj(txt)) { \/\/ пройдем по всем текстам<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::ksGetTextAlign(txt) == txta_Left) \/\/ если точка привязки текста слева<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksSetTextAlign(txt, txta_Right); \/\/ установим ее справа<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">txt = ::MoveIterator(iter, \'N\'); \/\/ следующий текст<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::DeleteIterator(iter); \/\/ удалим итератор<\/span><\/p>\n\r"
})
