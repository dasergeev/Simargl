hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetZona пример использования",
hmDescription:"RequestInfo info; K ksGetZona \/\/обнулить структуру info; memset (&info, 0, sizeof (info)); double x, y; info.prompt = \"Укажите точку\"; char zona [128]; while (Cursor (&inf",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetZona пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1722466\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RequestInfo info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">K ksGetZona<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/обнулить структуру info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset (&amp;info, 0, sizeof (info));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.prompt = &quot;Укажите точку&quot;;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char zona [128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">while (Cursor (&amp;info, &amp;x ,&amp;y, 0) == -1) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int rez = ksGetZona(x, y, zona, 128); \/\/размер присланного буфера<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (rez == -1 || !rez) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Error(rez ? &quot;В текущем документе нет разбиения на зоны&quot; : &quot;Ошибка&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">break;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">else<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message (zona);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
