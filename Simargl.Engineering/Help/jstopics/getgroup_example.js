hmLoadTopic({
hmKeywords:"",
hmTitle:"GetGroup - Пример использования",
hmDescription:"void GetGroup_Example (void) { reference gr, p ;   gr = NewGroup(1); \/\/ задание группы объектов   LineSeg (-15, 0, 15, 0, 3); \/\/ объекты записываются LineSeg ( 0, -15, 0,",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">GetGroup - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1724913\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">void GetGroup_Example (void)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr, p ;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">gr = NewGroup(1); \/\/ задание группы объектов<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (-15, 0, 15, 0, 3); \/\/ объекты записываются<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg ( 0, -15, 0, 15, 3); \/\/ во временный список<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Circle (0, 0, 10, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup(); \/\/ закончить формирование группы<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">SaveGroup (gr, Отверстие);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">p = GetGroup (Отверстие);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj (p, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">};<\/span><\/p>\n\r"
})
