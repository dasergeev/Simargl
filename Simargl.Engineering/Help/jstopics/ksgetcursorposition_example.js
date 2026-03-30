hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetCursorPosition - Пример использования",
hmDescription:"\/\/получить координаты курсора typeCursorPos = YesNo(\"С учетом привязок?\") == 1 ? 1 : 0; int j ; double x, y; do { ksGetCursorPosition(&x, &y, typeCursorPos); \/\/ координаты к",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetCursorPosition - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1730362\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/получить координаты курсора<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">typeCursorPos = YesNo(&quot;С учетом привязок?&quot;) == 1 ? 1 : 0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int j ;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">do {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksGetCursorPosition(&amp;x, &amp;y, typeCursorPos); \/\/ координаты курсора в миллиметрах,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ 0 - без учета привязок 1 - с учетом привязок,<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[255];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf(buf, &quot; x = %0.2f y = %0.2f; Продолжать? &quot;, x, y);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">j = YesNo(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">} while(j == 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r"
})
