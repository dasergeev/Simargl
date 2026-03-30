hmLoadTopic({
hmKeywords:"",
hmTitle:"TransformObj - пример использования",
hmDescription:"reference pObj; RequestInfo info; double x, y; memset(&info, 0, sizeof (info)); info.prompt = \"Укажите объект\"; reference g; int j = Cursor (&info, &x ,&y, 0); if (j) {",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">TransformObj - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1726110\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference pObj;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RequestInfo info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset(&amp;info, 0, sizeof (info));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.prompt = &quot;Укажите объект&quot;;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference g;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int j = Cursor (&amp;info, &amp;x ,&amp;y, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (j)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ExistObj (pObj = FindObj (x, y, 1e6)))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Mtr (-10,-10,0, 2);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">TransformObj (pObj);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">DeleteMtr();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
