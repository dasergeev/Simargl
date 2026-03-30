hmLoadTopic({
hmKeywords:"",
hmTitle:"GetMacroParamSize - пример использования",
hmDescription:"reference p; RequestInfo info; \/\/обнулить структуру info; memset (&info, 0, sizeof (info)); double x, y; info.prompt = \"Укажите макроэлемент\"; int j = Cursor (&info, &x ,&y",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">GetMacroParamSize - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcq1943596\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference p;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RequestInfo info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/обнулить структуру info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset (&amp;info, 0, sizeof (info));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.prompt = &quot;Укажите макроэлемент&quot;;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int j = Cursor (&amp;info, &amp;x ,&amp;y, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (j) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ExistObj (p = FindObj (x, y, 1e6)) &amp;&amp; GetObjParam (p, 0, 0, 0) == MACRO_OBJ) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int size = GetMacroParamSize (p);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf [128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">sprintf (buf, &quot;Размер параметров макроэлемента = %d&quot;, size);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message (buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
