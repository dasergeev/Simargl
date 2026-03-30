hmLoadTopic({
hmKeywords:"",
hmTitle:"ksClearGroup - Пример использования",
hmDescription:"reference gr = NewGroup(1); \/\/ временная группа геометрии",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksClearGroup - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1724806\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr = NewGroup(1); \/\/ временная группа геометрии<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LineSeg(100, 100, -100, -100, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LineSeg(10, 10, -10, -10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Удалить или нет временные объекты<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">bool deleteTmp = ::YesNo(&quot;Удалить временные объекты&quot;) == 1 ? true : false;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksClearGroup(gr, deleteTmp\/*true - удалять временные объекты, false - не удалять*\/);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::StoreTmpGroup(gr); \/\/ записываем результаты в документ<\/span><\/p>\n\r"
})
