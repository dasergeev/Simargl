hmLoadTopic({
hmKeywords:"",
hmTitle:"ksTrimCurve - пример использования",
hmDescription:"reference iter = ::CreateIterator(ALL_OBJ, 0); \/\/ итератор по всем объектам",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksTrimCurve - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1726709\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference iter = ::CreateIterator(ALL_OBJ, 0); \/\/ итератор по всем объектам<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (iter) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference obj = ::MoveIterator(iter, \'F\'); \/\/ первый объект<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ если объект геометрический - усечем его<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::IsGeomObject(obj)) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference newCurve = ::ksTrimCurve(obj, 0, 20, 0, -20, 1, false);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (newCurve) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LightObj(newCurve, 1); \/\/ подсветим объект<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message (&quot;Усекли кривую&quot;); \/\/ выдадим сообщение<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LightObj(newCurve, 0); \/\/ погасим подсветку объекта<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::DeleteIterator(iter); \/\/ удалить итератор<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
