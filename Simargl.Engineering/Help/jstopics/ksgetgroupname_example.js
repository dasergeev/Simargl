hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetGroupName - Пример использования",
hmDescription:"\/\/создаем рабочую группу reference gr = NewGroup (0); LineSeg (20, 20, 40, 20, 1); LineSeg (40, 20, 40, 40, 1); LineSeg (40, 40, 20, 40, 1); LineSeg (20, 40, 20, 20, 1); En",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetGroupName - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1724680\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/создаем рабочую группу<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference gr = NewGroup (0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (20, 20, 40, 20, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (40, 20, 40, 40, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (40, 40, 20, 40, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LineSeg (20, 40, 20, 20, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">EndGroup ();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/рабочую группу сохраняем в именной<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/именная группа хранится в документе<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">SaveGroup (gr, &quot;group1&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char grName [255];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/получим имя группы<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksGetGroupName (gr, \/\/ указатель на группу<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">grName, \/\/ указатель строки для имени группы<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">255); \/\/ размер строки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message (grName);<\/span><\/p>\n\r"
})
