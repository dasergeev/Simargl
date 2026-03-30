hmLoadTopic({
hmKeywords:"",
hmTitle:"ChoiceAttr - Пример использования",
hmDescription:"double x, y; reference pObj; int j; RequestInfo info; memset(&info, 0, sizeof(info)); info.prompt = \"Укажите объект\";   do { j = Cursor (&info, &x ,&y, 0); if (j) { i",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ChoiceAttr - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1734999\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference pObj;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">int j;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RequestInfo info;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">memset(&amp;info, 0, sizeof(info));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">info.prompt = &quot;Укажите объект&quot;;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">do<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">j = Cursor (&amp;info, &amp;x ,&amp;y, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (j)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ExistObj(pObj = FindObj (x, y, 1e6)))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj (pObj, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ChoiceAttr (pObj);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj (pObj, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">while (j);<\/span><\/p>\n\r"
})
