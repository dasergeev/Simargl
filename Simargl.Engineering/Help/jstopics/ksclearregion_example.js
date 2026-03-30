hmLoadTopic({
hmKeywords:"",
hmTitle:"ksClearRegion - пример использования",
hmDescription:"reference gr = NewGroup(0); \/\/ группа геометрии, представляющая область очистки",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksClearRegion - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1726651\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr = NewGroup(0); \/\/ группа геометрии, представляющая область очистки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Circle(0, 0, 40, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr1 = NewGroup(1); \/\/ группа геометрии, которую нужно очистить<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LineSeg(100, 100, -100, -100, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::LineSeg(10, 10, -10, -10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ очистка заданной области<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksClearRegion(gr1\/*группа которую надо очистить*\/, gr\/*область очистки*\/, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::StoreTmpGroup(gr1); \/\/ записываем результат в документ<\/span><\/p>\n\r"
})
